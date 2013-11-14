using LFM.LandRegistry.CommsService;
using LFM.Submissions.AgentServices.EdrsAttachmentService;
using LFM.Submissions.AgentServices.EdrsSubmissionService;

namespace LFM.Submissions.AgentServices
{
    public interface IRequestSender
    {
        Lrap1Response Send(RequestApplicationToChangeRegisterV1_0Type webRequest, string username, string password);
        Lrap1Response Send(newAttachmentRequest webRequest, string username, string password);
    }
}