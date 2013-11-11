using System;
using FakeItEasy;
using LFM.LandRegistry.Commands;
using Xunit;

namespace LFM.LandRegistry.CommsService.UnitTests
{
    public class When_a_Lrap1_is_sent
    {
        private readonly IEdrsCommunicator _fakeEdrsCommunicator;
        private readonly CommsService _sut;
        private readonly SubmitLrap1Command _lrap1Submission;

        public When_a_Lrap1_is_sent()
        {
            _fakeEdrsCommunicator = A.Fake<IEdrsCommunicator>();
            _sut = new CommsService(_fakeEdrsCommunicator);
            _lrap1Submission = new SubmitLrap1Command()
            {
                ApplicationId = "1234567890",
                Username = "LRUsername01",
                Password = "LRPassword01",
                Payload = "Lrap1 Payload Data"
            };
        }

        [Fact]
        public void without_a_ApplicationId_throws()
        {
            _lrap1Submission.ApplicationId = null;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1Submission));
        }

        [Fact]
        public void with_a_blank_ApplicationId_throws()
        {
            _lrap1Submission.ApplicationId = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1Submission));
        }

        [Fact]
        public void without_a_username_throws()
        {
            _lrap1Submission.Username = null;
            Assert.Throws<ArgumentException>(()=> _sut.Send(_lrap1Submission));
        }

        [Fact]
        public void with_a_blank_username_throws()
        {
            _lrap1Submission.Username = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1Submission));
        }

        [Fact]
        public void without_a_password_throws()
        {
            _lrap1Submission.Password = null;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1Submission));
        }

        [Fact]
        public void with_a_blank_password_throws()
        {
            _lrap1Submission.Password = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1Submission));
        }

        [Fact]
        public void the_payload_is_submitted_to_the_eDRS_service()
        {
            _sut.Send(_lrap1Submission);
            A.CallTo(()=>_fakeEdrsCommunicator.Submit(A<Lrap1Request>
                .That.Matches(r=>r.Payload == _lrap1Submission.Payload)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void an_acknowledgement_is_returned()
        {
            var edrsAcknowledgment = new Lrap1Response(){ResponseType=ResponseType.Acknowledgment};
            A.CallTo(() => _fakeEdrsCommunicator.Submit(A<Lrap1Request>.Ignored)).Returns(edrsAcknowledgment);
            var response = _sut.Send(_lrap1Submission);
            Assert.Equal(ResponseType.Acknowledgment,response);
        }

        [Fact]
        public void a_rejection_is_returned()
        {
            var edrsRejection = new Lrap1Response() { ResponseType = ResponseType.Rejection };
            A.CallTo(() => _fakeEdrsCommunicator.Submit(A<Lrap1Request>.Ignored)).Returns(edrsRejection);
            var response = _sut.Send(_lrap1Submission);
            Assert.Equal(ResponseType.Rejection, response);            
        }
    }

    public class When_a_Lrap1Attachment_is_sent
    {
        private IEdrsCommunicator _fakeEdrsCommunicator;
        private CommsService _sut;
        private SubmitLrap1AttachmentCommand _lrap1AttachmentSubmission;

        public When_a_Lrap1Attachment_is_sent()
        {
            _fakeEdrsCommunicator = A.Fake<IEdrsCommunicator>();
            _sut = new CommsService(_fakeEdrsCommunicator);
            _lrap1AttachmentSubmission = new SubmitLrap1AttachmentCommand()
            {
                AttachmentId = "9876543210",
                ApplicationId = "1234567890",
                Username = "LRUsername01",
                Password = "LRPassword01",
                Payload = "Attachment Payload Data"
            };
        }
        
        [Fact]
        public void it_is_submitted_to_the_Edrs_attachment_service()
        {
            _sut.Send(_lrap1AttachmentSubmission);
            A.CallTo(() => _fakeEdrsCommunicator.Submit(A<Lrap1AttachmentRequest>
                .That.Matches(r => r.Payload == _lrap1AttachmentSubmission.Payload && 
                    r.ApplicationId == _lrap1AttachmentSubmission.ApplicationId)))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void without_a_AttachmentId_throws()
        {
            _lrap1AttachmentSubmission.AttachmentId = null;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }
        
        [Fact]
        public void with_a_blank_AttachmentId_throws()
        {
            _lrap1AttachmentSubmission.AttachmentId = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }

        [Fact]
        public void without_a_ApplicationId_throws()
        {
            _lrap1AttachmentSubmission.ApplicationId = null;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }

        [Fact]
        public void with_a_blank_ApplicationId_throws()
        {
            _lrap1AttachmentSubmission.ApplicationId = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }

        [Fact]
        public void without_a_username_throws()
        {
            _lrap1AttachmentSubmission.Username = null;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }

        [Fact]
        public void with_a_blank_username_throws()
        {
            _lrap1AttachmentSubmission.Username = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }

        [Fact]
        public void without_a_password_throws()
        {
            _lrap1AttachmentSubmission.Password = null;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }

        [Fact]
        public void with_a_blank_password_throws()
        {
            _lrap1AttachmentSubmission.Password = string.Empty;
            Assert.Throws<ArgumentException>(() => _sut.Send(_lrap1AttachmentSubmission));
        }
    }
}
