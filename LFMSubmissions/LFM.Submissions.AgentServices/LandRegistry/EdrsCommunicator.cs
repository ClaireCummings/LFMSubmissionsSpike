using LFM.LandRegistry.CommsService;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsCommunicator : IEdrsCommunicator
    {
        private readonly IObjectSerializer _objectSerializer;
        public IRequestSender RequestSender { get; set; }

        public EdrsCommunicator(IObjectSerializer objectSerializer)
        {
            _objectSerializer = objectSerializer;
            RequestSender = new RequestSender();
        }

        public Lrap1Response Submit(Lrap1Request request)
        {
            EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type webRequest;

            webRequest =
                _objectSerializer
                    .XmlDeserializeFromString<EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type>(
                        request.Payload);

            webRequest.MessageId = request.ApplicationId;
           
            return RequestSender.Send(webRequest,request.Username,request.Password);
        }

        public Lrap1Response Submit(Lrap1AttachmentRequest request)
        {
            EdrsAttachmentService.newAttachmentRequest webRequest;
 
            webRequest =
                    _objectSerializer.XmlDeserializeFromString<EdrsAttachmentService.newAttachmentRequest>(request.Payload);

            webRequest.arg0.ApplicationMessageId = request.ApplicationId;
            
            return RequestSender.Send(webRequest, request.Username, request.Password);
        }
    }
}