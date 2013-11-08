using System.Dynamic;

namespace LFM.LandRegistry.SubmissionsService
{
    public interface ISubmitter
    {
        SubmitLrap1Result Send(SubmitLrap1Command command);
        void Send(SubmitLrap1AttachmentCommand command);
    }
}