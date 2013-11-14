using System.Security.Cryptography.X509Certificates;
using LFM.LandRegistry;
using LFM.LandRegistry.CommsService;
using LFM.Submissions.AgentServices.EdrsAttachmentService;
using LFM.Submissions.AgentServices.EdrsSubmissionService;
using LFM.Submissions.AgentServices.LandRegistry;
using ProductResponseCodeContentType = LFM.Submissions.AgentServices.EdrsSubmissionService.ProductResponseCodeContentType;

namespace LFM.Submissions.AgentServices
{
    public class RequestSender : IRequestSender
    {
        public Lrap1Response Send(RequestApplicationToChangeRegisterV1_0Type webRequest, string username, string password)
        {
            // create an instance of the client
            var client = new EdrsSubmissionService.EDocumentRegistrationV1_0ServiceClient();

            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My,
                X509FindType.FindBySerialNumber, "47 ce 29 6f");

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(username, password));

            // submit the request
            var serviceResponse = client.eDocumentRegistration(webRequest);

            var lrap1Response = new Lrap1Response();

            switch (serviceResponse.GatewayResponse.TypeCode)
            {
                case ProductResponseCodeContentType.Item10:
                    lrap1Response.ResponseType = ResponseType.Acknowledgment;
                    break;
                case ProductResponseCodeContentType.Item20:
                    lrap1Response.ResponseType = ResponseType.Rejection;
                    break;
            }
            return lrap1Response;

        }

        public Lrap1Response Send(newAttachmentRequest webRequest, string username, string password)
        {
            Lrap1AttachmentRequest request;
            // create an instance of the client
            var client = new EdrsAttachmentService.AttachmentV1_0ServiceClient();
            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My,
                X509FindType.FindBySerialNumber, "47 ce 29 6f");
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(username, password));

            // submit the request
            var serviceResponse = client.newAttachment(webRequest.arg0);

            var lrap1Response = new Lrap1Response();

            switch (serviceResponse.GatewayResponse.TypeCode)
            {
                case EdrsAttachmentService.ProductResponseCodeContentType.Item10:
                    lrap1Response.ResponseType = ResponseType.Acknowledgment;
                    break;
                case EdrsAttachmentService.ProductResponseCodeContentType.Item20:
                    lrap1Response.ResponseType = ResponseType.Rejection;
                    break;
            }
            return lrap1Response;
        }
    }
}
