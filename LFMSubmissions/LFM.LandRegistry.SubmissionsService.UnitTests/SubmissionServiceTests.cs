using System;
using System.Collections.Generic;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax;
using LFM.LandRegistry.Commands;
using Xunit;
using Xunit.Extensions;

namespace LFM.LandRegistry.SubmissionsService.UnitTests
{
    public class When_the_submissions_service_is_asked_to_send_an_LRAPP_package
    {
        private readonly Lrap1Package _package;
        private readonly ISendMessages _fakeMessageSender;
        private readonly SubmitLrap1Command _submitLrap1command;

        public When_the_submissions_service_is_asked_to_send_an_LRAPP_package()
        {
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

            _submitLrap1command = new SubmitLrap1Command()
            {
                Username = "LRUser001",
                Password = "BGPassword01",
                ApplicationId = "1234567890",
                Payload = _package.Payload
            };

            _fakeMessageSender = A.Fake<ISendMessages>();

            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1Command>.That.Matches(
                c => c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _submitLrap1command.Payload)))
                .Returns(new SubmitLrap1Result()
                    {
                        Command = _submitLrap1command
                    });

            var sut = new Lrap1SubmissionService(_fakeMessageSender);
            sut.Submit(_submitLrap1command.Username,_submitLrap1command.Password, _package);
        }

        [Fact]
        public void the_main_application_is_sent_to_the_messaging_service()
        {
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1Command>.That.Matches(
                c => c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _submitLrap1command.Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_first_attachment_is_sent_to_the_messaging_service()
        {
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == _submitLrap1command.ApplicationId &&
                     c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _package.Attachments[0].Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_second_attachment_is_sent_to_the_messaging_service()
        {
            A.CallTo(() => _fakeMessageSender.Send(A<SubmitLrap1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == _submitLrap1command.ApplicationId &&
                     c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _package.Attachments[1].Payload)))
                .MustHaveHappened();
        }
    }

    public class When_the_messaging_service_processes_an_LRAP1_submission
    {
        public ICommsService _fakeCommsService;
        public ISendMessages _fakeMessageSender;
        public Lrap1Processor _sut;
        public SubmitLrap1Command _command;


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
}
