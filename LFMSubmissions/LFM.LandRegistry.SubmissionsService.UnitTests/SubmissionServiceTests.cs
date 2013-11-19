using System.Collections.Generic;
using FakeItEasy;
using LFM.ApplicationServices;
using LFM.ApplicationServices.LandRegistry;
using LFM.LandRegistry.Commands;
using Xunit;
using Xunit.Extensions;

namespace LFM.LandRegistry.SubmissionsService.UnitTests
{
    public class When_the_submissions_service_is_asked_to_send_an_LRAP1_package
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _applicationId;
        private readonly Lrap1Package _package;
        private readonly ISendMessages _fakeMessageSender;
        private readonly SubmitLrap1Result _response;
        private readonly ICommandInvoker _fakeCommandInvoker;

        public When_the_submissions_service_is_asked_to_send_an_LRAP1_package()
        {
            _applicationId = "01234567890";
            _username = "LRUser001";
            _password = "BGPassword01";

            _fakeCommandInvoker = A.Fake<ICommandInvoker>();

            var attachments = new List<Lrap1Attachment>
            {
                new Lrap1Attachment() {Payload = "Attachment 1 payload data"},
                new Lrap1Attachment() {Payload = "Attachment 2 payload data"}
            };

            _package = new Lrap1Package()
            {
                Payload = "Lrap1 Payload Data",
                Attachments = attachments
            };

            A.CallTo(
                () =>
                    _fakeCommandInvoker.Execute<CreateLrap1SubmissionCommand, CreateLrap1SubmissionQueryResult>(
                        A<CreateLrap1SubmissionCommand>.Ignored)).Returns(new CreateLrap1SubmissionQueryResult()
                        {
                            Command = new CreateLrap1SubmissionCommand()
                                {
                                    ApplicationId = _applicationId,
                                    Username = _username,
                                    Payload = _package.Payload
                                }
                        });

            A.CallTo(
                () =>
                    _fakeCommandInvoker.Execute<CreateLrap1AttachmentCommand, CreateLrap1AttachmentQueryResult>(
                        A<CreateLrap1AttachmentCommand>.Ignored)).Returns(new CreateLrap1AttachmentQueryResult()
                        {
                            Command = new CreateLrap1AttachmentCommand()
                            {
                                AttachmentId = "98765",
                                ApplicationId = _applicationId,
                                Username = _username,
                                Payload = _package.Payload
                            }
                        });

            _fakeMessageSender = A.Fake<ISendMessages>();

            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1Command>.That.Matches(
                c => c.Username == _username &&
                     c.Password == _password &&
                     c.Payload == _package.Payload)))
                .Returns(new SubmitLrap1Result()
                    {
                        Command = new SubmitLrap1Command()
                        {
                            ApplicationId = _applicationId, 
                            Username = _username, 
                            Password = _password, 
                            Payload = _package.Payload
                        }
                    });

            var sut = new Lrap1SubmissionService(_fakeMessageSender, _fakeCommandInvoker);
            _response = sut.Submit(_username, _password, _package);
        }
        
        [Fact]
        public void the_submission_is_saved_to_the_database()
        {
            A.CallTo(() => _fakeCommandInvoker
                    .Execute<CreateLrap1SubmissionCommand, CreateLrap1SubmissionQueryResult>(A<CreateLrap1SubmissionCommand>
                        .That.Matches(c => 
                            c.Username == _username &&
                            c.Payload == _package.Payload)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void the_first_attachment_is_saved_to_the_database()
        {
            A.CallTo(() => _fakeCommandInvoker
                    .Execute<CreateLrap1AttachmentCommand, CreateLrap1AttachmentQueryResult>(A<CreateLrap1AttachmentCommand>
                        .That.Matches(c => 
                            c.ApplicationId == _applicationId &&
                            c.Username == _username &&
                            c.Payload == _package.Attachments[0].Payload)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
        
        [Fact]
        public void the_main_application_is_sent_to_the_messaging_service()
        {
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1Command>.That.Matches(
                c => c.Username == _username &&
                     c.Password == _password &&
                     c.Payload == _package.Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_first_attachment_is_sent_to_the_messaging_service()
        {
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == _applicationId && 
                     c.Username == _username &&
                     c.Password == _password &&
                     c.Payload == _package.Attachments[0].Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_second_attachment_is_sent_to_the_messaging_service()
        {
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == _applicationId &&
                     c.Username == _username &&
                     c.Password == _password &&
                     c.Payload == _package.Attachments[1].Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_applicationId_is_returned()
        {
            Assert.Equal(_applicationId, _response.Command.ApplicationId);
        }

        
    }

    public class When_the_messaging_service_processes_an_LRAP1_submission
    {
        private readonly ICommsService _fakeCommsService;
        private readonly ISendMessages _fakeMessageSender;
        private readonly Lrap1Processor _sut;
        private readonly SubmitLrap1Command _command;

        public When_the_messaging_service_processes_an_LRAP1_submission()
        {
            //Arrange
            _fakeCommsService = A.Fake<ICommsService>();
            _fakeMessageSender = A.Fake<ISendMessages>();

            _sut = new Lrap1Processor(_fakeMessageSender, _fakeCommsService);

            _command = new SubmitLrap1Command()
            {
                ApplicationId = "123456789",
                Username = "LRUserName",
                Password = "LRPassword",
                Payload = "Payload"
            };
        }

        [Theory]
        [InlineData (ResponseType.Acknowledgment)]
        [InlineData(ResponseType.Rejection)]
        [InlineData(ResponseType.None)]
        public void the_LRAP1_is_submitted_to_the_AgentGateway(ResponseType responseType)
        {
            //Arrange
            A.CallTo(() => _fakeCommsService.Send(_command)).Returns(responseType);

            //Act
            _sut.Process(_command);

            //Assert
            A.CallTo(()=> _fakeCommsService.Send(A<SubmitLrap1Command>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void the_message_is_resent_if_no_response_is_returned()
        {
            //Arrange
            A.CallTo(() => _fakeCommsService.Send(_command)).Returns(ResponseType.None);

            //Act
            _sut.Process(_command);

            //Assert
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1Command>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }

    public class When_the_messaging_service_processes_an_LRAP1Attachment_submission
    {
        private readonly ICommsService _fakeCommsService;
        private readonly ISendMessages _fakeMessageSender;
        private readonly Lrap1Processor _sut;
        private readonly SubmitLrap1AttachmentCommand _command;


        public When_the_messaging_service_processes_an_LRAP1Attachment_submission()
        {
            //Arrange
            _fakeCommsService = A.Fake<ICommsService>();
            _fakeMessageSender = A.Fake<ISendMessages>();

            _sut = new Lrap1Processor(_fakeMessageSender, _fakeCommsService);

            _command = new SubmitLrap1AttachmentCommand()
            {
                AttachmentId = "9876543210",
                ApplicationId = "123456789",
                Username = "LRUserName",
                Password = "LRPassword",
                Payload = "Payload"
            };
        }

        [Theory]
        [InlineData(ResponseType.Acknowledgment)]
        [InlineData(ResponseType.Rejection)]
        [InlineData(ResponseType.None)]
        public void the_LRAP1Attachment_is_submitted_to_the_AgentGateway(ResponseType responseType)
        {
            //Arrange
            A.CallTo(() => _fakeCommsService.Send(_command)).Returns(responseType);

            //Act
            _sut.Process(_command);

            //Assert
            A.CallTo(() => _fakeCommsService.Send(A<SubmitLrap1AttachmentCommand>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void the_message_is_resent_if_no_response_is_returned()
        {
            //Arrange
            A.CallTo(() => _fakeCommsService.Send(_command)).Returns(ResponseType.None);

            //Act
            _sut.Process(_command);

            //Assert
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1AttachmentCommand>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }

    public class When_the_submissions_service_requests_a_status_update_for_an_LRAP1_package
    {
        [Fact]
        public void the_current_status_of_the_submission_is_returned()
        {
            //Arrange
            var fakeLrap1Repository = A.Fake<ILrap1SubmissionRepository>();
            var sut = new SubmissionDataService(fakeLrap1Repository, null);
            var queryInvoker = new QueryInvoker(sut);

            A.CallTo(() => fakeLrap1Repository.GetById(A<string>.Ignored))
                .Returns(new Lrap1Submission() {ResponseType = ResponseType.Acknowledgment});
            
            //Act
            var query = new Lrap1StatusQuery()
            {
                ApplicationId = "123456890"
            };

            var result = queryInvoker.Query<Lrap1StatusQuery, Lrap1StatusQueryResult>(query);

            //Assert
            Assert.Equal(ResponseType.Acknowledgment, result.ResponseType);
        }
    }
}
