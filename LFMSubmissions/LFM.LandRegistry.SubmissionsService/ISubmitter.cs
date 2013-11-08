using System.Dynamic;

namespace LFM.LandRegistry.SubmissionsService
{
    public interface ISubmitter
    {
        SubmitLrap1Result Send(SubmitLrapp1Command command);
        void Send(SubmitLrapp1AttachmentCommand command);
    }
}