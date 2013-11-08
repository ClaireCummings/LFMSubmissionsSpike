using System;
using System.Collections.Generic;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax;
using Xunit;
using Xunit.Extensions;

namespace LFM.LandRegistry.SubmissionsService.UnitTests
{
    public class When_submissions_service_is_asked_to_send_an_LRAPP_package
    {
        private readonly Lrap1Package _package;
        private readonly ISubmitter _fakeSubmitter;
        private readonly SubmitLrap1Command _submitLrap1command;

        public When_submissions_service_is_asked_to_send_an_LRAPP_package()
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

            _fakeSubmitter = A.Fake<ISubmitter>();

            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrap1Command>.That.Matches(
                c => c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _submitLrap1command.Payload)))
                .Returns(new SubmitLrap1Result()
                    {
                        Command = _submitLrap1command
                    });

            var sut = new Lrap1SubmissionService { Submitter = _fakeSubmitter };
            sut.Submit(_submitLrap1command.Username,_submitLrap1command.Password, _package);
        }

        [Fact]
        public void the_main_application_is_sent_to_the_LRAP1_service()
        {
            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrap1Command>.That.Matches(
                c => c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _submitLrap1command.Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_first_attachment_is_sent_to_the_LRAP1_Attachment_service()
        {
            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrap1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == _submitLrap1command.ApplicationId &&
                     c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _package.Attachments[0].Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_second_attachment_is_sent_to_the_LRPP1_Attachment_service()
        {
            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrap1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == _submitLrap1command.ApplicationId &&
                     c.Username == _submitLrap1command.Username &&
                     c.Password == _submitLrap1command.Password &&
                     c.Payload == _package.Attachments[1].Payload)))
                .MustHaveHappened();
        }
    }
}
