using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LFM.LandRegistry.Commands;
using LFM.LandRegistry.CommsService;
using NServiceBus;
using NServiceBus.Logging.Loggers;
using NServiceBus.Testing;
using Xunit;
using Xunit.Sdk;

namespace LFM.MessageService.UnitTests
{
    public class When_a_SubmitLrap1Command_is_processed
    {
        [Fact]
        public void the_Agentcomms_sends_the_submission()
        {
            //Arrange
            var command = new SubmitLrap1Command()
            {
                ApplicationId = "1234567890",
                Username = "username",
                Password = "password",
                Payload = "payload"
            };

            var fakeCommunicator = A.Fake<IEdrsCommunicator>();
            var commsService = new CommsService(fakeCommunicator);
            var bus = A.Fake<IBus>();
            var sut = A.Fake<Lrap1Processor>();
            sut.CommsService = commsService;
            sut.Bus = bus;
            
            //Act
            sut.Handle(command);

            //Assert
            A.CallTo(()=>fakeCommunicator.Submit(A<Lrap1Request>.That.Matches(r => r.ApplicationId == command.ApplicationId &&
                r.Payload == command.Payload)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

//        public void the_message_is_not_resent_if_the_payload_is_invalid()
//        {
//            //Arrange
//            var command = new SubmitLrap1Command()
//            {
//                ApplicationId = "1234567890",
//                Username = "username",
//                Password = "password",
//                Payload = "payload"
//            };
//
//            var fakeCommunicator = A.Fake<IEdrsCommunicator>();
//            var commsService = new CommsService(fakeCommunicator);
//            var sut = A.Fake<Lrap1Processor>();
//            sut.CommsService = commsService;
//
//            A.CallTo(() => fakeCommunicator.Submit(A<Lrap1Request>.Ignored)).Throws(new InvalidPayloadException());
//            
//            //Act
//            Test.Initialize();
//            Test.Handler<Lrap1Processor>()
//                .ExpectDoNotContinueDispatchingCurrentMessageToHandlers()
//                .OnMessage(command);
//
//
//        }

        [Fact]
        public void the_message_is_resent_when_no_response_is_received()
        {
            //Arrange
            var command = new SubmitLrap1Command()
            {
                ApplicationId = "1234567890",
                Username = "username",
                Password = "password",
                Payload = "payload"
            };

            var fakeCommunicator = A.Fake<IEdrsCommunicator>();
            var commsService = new CommsService(fakeCommunicator);

            A.CallTo(() => fakeCommunicator.Submit(A<Lrap1Request>.Ignored))
                .Returns(new Lrap1Response() {ResponseType = ResponseType.None});

            //Act
            MessageConventionExtensions.IsCommandTypeAction =
                t => t.Namespace != null &&
                    t.Namespace.EndsWith(".Commands") && 
                    !t.Namespace.StartsWith("NServiceBus");

            Test.Initialize();
            
            //Assert
            Test.Handler<Lrap1Processor>()
                .WithExternalDependencies(d=>d.CommsService = commsService)
                .ExpectSendLocal<SubmitLrap1Command>(m => m.ApplicationId == command.ApplicationId &&
                    m.Username == command.Username && 
                    m.Password == command.Password && 
                    m.Payload == command.Payload)
                .OnMessage(command);

        }

        [Fact]
        public void the_Agentcomms_receives_a_response_from_the_eDRSService()
        {
            
        }

        [Fact]
        public void the_Agentcomms_processes_an_acknowledgment_response_from_the_eDRSService()
        {
            
        }



    }
}
