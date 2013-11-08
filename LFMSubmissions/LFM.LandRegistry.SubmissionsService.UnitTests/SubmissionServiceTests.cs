using System;
using System.Collections.Generic;
using FakeItEasy;
using Xunit;
using Xunit.Extensions;

namespace LFM.LandRegistry.SubmissionsService.UnitTests
{
    public class When_submissions_service_is_asked_to_send_an_LRAPP_package
    {
        private readonly Lrapp1Package _package;
        private readonly string _username;
        private readonly string _password;
        private readonly ISubmitter _fakeSubmitter;

        public When_submissions_service_is_asked_to_send_an_LRAPP_package()
        {
            var attachments = new List<Lrap1Attachment>
            {
                new Lrap1Attachment() {Payload = "Attachment 1 payload data"},
                new Lrap1Attachment() {Payload = "Attachment 2 payload data"}
            };

            _package = new Lrapp1Package()
            {
                Payload = "Lrapp1 Payload Data",
                Attachments = attachments
            };

            _username = "LRUser001";
            _password = "BGPassword01";

            _fakeSubmitter = A.Fake<ISubmitter>();

            var sut = new Lrapp1SubmissionService { Submitter = _fakeSubmitter };

            sut.Submit(_username, _password, _package);
        }

        [Fact]
        public void the_main_application_is_sent_to_the_LRAPP1_service()
        {
            var command = new SubmitLrapp1Command()
            {
                ApplicationId = "1234567890",
                Username = _username,
                Password = _password,
                Payload = _package.Payload
            };

            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrapp1Command>.That.Matches(
                c => c.Username == command.Username &&
                c.Password == command.Password &&
                c.Payload == _package.Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_first_attachment_is_sent_to_the_LRAPP1_Attachment_service()
        {
            var command = new SubmitLrapp1AttachmentCommand()
            {
                AttachmentId = "9876543210",
                ApplicationId = "1234567890",
                Username = _username,
                Password = _password,
                Payload = _package.Payload
            };

            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrapp1Command>.That.Matches(
                c => c.Username == command.Username &&
                     c.Password == command.Password &&
                     c.Payload == _package.Payload)))
                .Returns(new SubmitLrap1Result() {Command = new SubmitLrapp1Command(){ApplicationId = command.ApplicationId}});

            // todo:  Ensure Application ID matches that on the main Application
            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrapp1AttachmentCommand>.That.Matches(
                c => c.ApplicationId == command.ApplicationId &&
                    c.Username == command.Username &&
                c.Password == command.Password &&
                c.Payload == _package.Attachments[0].Payload)))
                .MustHaveHappened();
        }

        [Fact]
        public void the_second_attachment_is_sent_to_the_LRAPP1_Attachment_service()
        {
            var command = new SubmitLrapp1AttachmentCommand()
            {
                AttachmentId = "9876543210",
                ApplicationId = "1234567890",
                Username = _username,
                Password = _password,
                Payload = _package.Payload
            };

            A.CallTo(() => _fakeSubmitter.Send(A<SubmitLrapp1AttachmentCommand>.That.Matches(
                c => c.Username == command.Username &&
                c.Password == command.Password &&
                c.Payload == _package.Attachments[1].Payload)))
                .MustHaveHappened();
        }
    }
}
