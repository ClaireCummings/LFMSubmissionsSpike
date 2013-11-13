using LFM.LandRegistry;
using LFM.LandRegistry.Commands;

namespace LFM
{
    public interface ISendMessages
    {
        SubmitLrap1Result Send(SubmitLrap1Command command);
        void Send(SubmitLrap1AttachmentCommand command);
    }
}