namespace LFM.LandRegistry.SubmissionsService
{
    public interface ISubmitter
    {
        void Send(SubmitLrapp1Command command);
        void Send(SubmitLrapp1AttachmentCommand command);
    }
}