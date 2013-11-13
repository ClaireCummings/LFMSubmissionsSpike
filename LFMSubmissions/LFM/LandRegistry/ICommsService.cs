using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry
{
    public interface ICommsService
    {
        ResponseType Send(SubmitLrap1Command submission);
        ResponseType Send(SubmitLrap1AttachmentCommand submission);
    }
}