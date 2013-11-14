using FakeItEasy;
using LFM.LandRegistry;
using LFM.LandRegistry.CommsService;
using LFM.Submissions.AgentServices.EdrsAttachmentService;
using LFM.Submissions.AgentServices.EdrsSubmissionService;
using LFM.Submissions.AgentServices.LandRegistry;
using Xunit;
using Xunit.Extensions;

namespace LFM.Submissions.AgentServices.UnitTests
{
    public class When_an_Lrap1_is_submitted
    {
        private readonly IObjectSerializer _fakeObjectSerializer;
        private readonly IRequestSender _fakeRequestSender;
        private readonly Lrap1Request _lrap1Request;
        private readonly EdrsCommunicator _sut;

        public When_an_Lrap1_is_submitted()
        {
            //Arrange
            _fakeObjectSerializer = A.Fake<IObjectSerializer>();
            _fakeRequestSender = A.Fake<IRequestSender>();

            _lrap1Request = new Lrap1Request()
            {
                Payload = "payload data"
            };

            _sut = new EdrsCommunicator(_fakeObjectSerializer);
            _sut.RequestSender = _fakeRequestSender;
        }

        [Fact]
        public void the_payload_is_deserialized()
        {
            //Arrange
            A.CallTo(() => _fakeRequestSender.Send(A<RequestApplicationToChangeRegisterV1_0Type>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(A<Lrap1Response>.Ignored);

            //Act
            _sut.Submit(_lrap1Request);

            //Assert
            A.CallTo(()=>_fakeObjectSerializer.XmlDeserializeFromString<RequestApplicationToChangeRegisterV1_0Type>(_lrap1Request.Payload))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void the_deserialized_payload_is_sent()
        {
            //Arrange
            A.CallTo(() => _fakeObjectSerializer.XmlDeserializeFromString<RequestApplicationToChangeRegisterV1_0Type>(_lrap1Request.Payload))
                .Returns(new RequestApplicationToChangeRegisterV1_0Type());

            //Act
            _sut.Submit(_lrap1Request);

            //Assert
            A.CallTo(() => _fakeRequestSender.Send(A<RequestApplicationToChangeRegisterV1_0Type>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Theory]
        [InlineData(ResponseType.None)]
        [InlineData(ResponseType.Acknowledgment)]
        [InlineData(ResponseType.Rejection)]
        public void the_response_is_returned(ResponseType responseType)
        {
            //Arrange
            A.CallTo(() => _fakeObjectSerializer.XmlDeserializeFromString<RequestApplicationToChangeRegisterV1_0Type>(_lrap1Request.Payload))
                .Returns(new RequestApplicationToChangeRegisterV1_0Type());

            var lrap1Response = new Lrap1Response() {ResponseType = responseType};

            A.CallTo(
                () =>
                    _fakeRequestSender.Send(A<RequestApplicationToChangeRegisterV1_0Type>.Ignored, A<string>.Ignored,
                        A<string>.Ignored))
                        .Returns(lrap1Response);

            //Act
            _sut.Submit(_lrap1Request);

            //Assert
            A.CallTo(() => _fakeRequestSender.Send(A<RequestApplicationToChangeRegisterV1_0Type>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(lrap1Response);
        }
    }

    public class When_an_Lrap1_attachment_is_submitted
    {
        private readonly IObjectSerializer _fakeObjectSerializer;
        private readonly IRequestSender _fakeRequestSender;
        private readonly Lrap1AttachmentRequest _lrap1AttachmentRequest;
        private readonly EdrsCommunicator _sut;

        public When_an_Lrap1_attachment_is_submitted()
        {
            //Arrange
            _fakeObjectSerializer = A.Fake<IObjectSerializer>();
            _fakeRequestSender = A.Fake<IRequestSender>();

            _lrap1AttachmentRequest = new Lrap1AttachmentRequest()
            {
                Payload = "payload data"
            };

            _sut = new EdrsCommunicator(_fakeObjectSerializer);
            _sut.RequestSender = _fakeRequestSender;
        }

        [Fact]
        public void the_payload_is_deserialized()
        {
            //Arrange
            A.CallTo(() => _fakeRequestSender.Send(A<newAttachmentRequest>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(A<Lrap1Response>.Ignored);

            A.CallTo(
                () =>
                    _fakeObjectSerializer.XmlDeserializeFromString<newAttachmentRequest>(_lrap1AttachmentRequest.Payload))
                .Returns(new newAttachmentRequest(new AttachmentV1_0Type()));

            //Act
            _sut.Submit(_lrap1AttachmentRequest);

            //Assert
            A.CallTo(() => _fakeObjectSerializer.XmlDeserializeFromString<newAttachmentRequest>(_lrap1AttachmentRequest.Payload))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void the_deserialized_payload_is_sent()
        {
            //Arrange
            A.CallTo(() => _fakeObjectSerializer.XmlDeserializeFromString<newAttachmentRequest>(_lrap1AttachmentRequest.Payload))
                .Returns(new newAttachmentRequest(new AttachmentV1_0Type()));

            //Act
            _sut.Submit(_lrap1AttachmentRequest);

            //Assert
            A.CallTo(() => _fakeRequestSender.Send(A<newAttachmentRequest>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
