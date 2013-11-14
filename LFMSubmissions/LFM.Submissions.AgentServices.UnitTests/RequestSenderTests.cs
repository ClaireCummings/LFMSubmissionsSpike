using LFM.LandRegistry;
using Xunit;
using Xunit.Extensions;

namespace LFM.Submissions.AgentServices.UnitTests
{
    public class When_a_LRAP1_request_is_sent
    {
        [Theory]
        [InlineData(ResponseType.Acknowledgment)]
        [InlineData(ResponseType.Rejection)]
        public void the_response_is_returned(ResponseType expectedResponseType)
        {
            // Arrange
            var sut = new RequestSender();
            var requestGenerator = new RequestGenerator();
            var request = requestGenerator.Lrap1Request(expectedResponseType);
            
            // Act
            var lrapResponse = sut.Send(request, "LRUsername001", "BGPassword001");

            // Assert
            Assert.Equal(expectedResponseType, lrapResponse.ResponseType);
        }
    }

    public class When_a_LRAP1_Attachment_request_is_sent
    {
        [Theory]
        [InlineData(ResponseType.Acknowledgment)]
        [InlineData(ResponseType.Rejection)]
        public void the_response_is_returned(ResponseType expectedResponseType)
        {
            // Arrange
            var sut = new RequestSender();
            var requestGenerator = new RequestGenerator();
            var request = requestGenerator.Lrap1AttachmentRequest(expectedResponseType);

            // Act
            var lrapResponse = sut.Send(request, "LRUsername001", "BGPassword001");

            // Assert
            Assert.Equal(expectedResponseType, lrapResponse.ResponseType);
        }
    }
}
