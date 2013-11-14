using FakeItEasy;
using LFM.LandRegistry.Commands;
using Xunit;

namespace LFM.MessageService.UnitTests
{
    public class When_a_SubmitLrap1Command_is_handled
    {
        [Fact]
        public void the_submission_service_processes_the_message()
        {
            //Arrange
            var command = new SubmitLrap1Command()
            {
                ApplicationId = "1234567890",
                Username = "username",
                Password = "password",
                Payload = "payload"
            };

            var fakeMessageProcessor = A.Fake<IMessageProcessor>();
            var sut = A.Fake<Lrap1Processor>();
            sut.MessageProcessor = fakeMessageProcessor;

            //Act
            sut.Handle(command);

            //Assert
            A.CallTo(() => fakeMessageProcessor.Process(command))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }

    public class When_a_SubmitLrap1AttachmentCommand_is_handled
    {
        [Fact]
        public void the_submission_service_processes_the_message()
        {
            //Arrange
            var command = new SubmitLrap1AttachmentCommand()
            {
                AttachmentId = "9876543210",
                ApplicationId = "1234567890",
                Username = "username",
                Password = "password",
                Payload = "payload"
            };

            var fakeMessageProcessor = A.Fake<IMessageProcessor>();
            var sut = A.Fake<Lrap1Processor>();
            sut.MessageProcessor = fakeMessageProcessor;

            //Act
            sut.Handle(command);

            //Assert
            A.CallTo(() => fakeMessageProcessor.Process(command))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
