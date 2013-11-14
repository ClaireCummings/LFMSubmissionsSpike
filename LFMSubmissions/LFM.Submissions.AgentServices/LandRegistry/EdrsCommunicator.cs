using System;
using LFM.LandRegistry;
using LFM.LandRegistry.CommsService;
using System.Security.Cryptography.X509Certificates;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsCommunicator : IEdrsCommunicator
    {
        public Lrap1Response Submit(Lrap1Request request)
        {
            EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type webRequest;

            webRequest =
                ObjectSerializer
                    .XmlDeserializeFromString<EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type>(
                        request.Payload);

            webRequest.MessageId = request.ApplicationId;

            // create an instance of the client
            var client = new EdrsSubmissionService.EDocumentRegistrationV1_0ServiceClient();

            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySerialNumber, "47 ce 29 6f");

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(request.Username, request.Password));

            // submit the request
            var serviceResponse = client.eDocumentRegistration(webRequest);
            
            //TODO: return correct response from serviceResponse!!!!

            return new Lrap1Response(){ResponseType = ResponseType.Acknowledgment};
        }

        public Lrap1Response Submit(Lrap1AttachmentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}