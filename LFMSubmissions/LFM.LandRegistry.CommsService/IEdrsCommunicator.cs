namespace LFM.LandRegistry.CommsService
{
    public interface IEdrsCommunicator
    {
        Lrap1Response Submit(Lrap1Request request);
        Lrap1Response Submit(Lrap1AttachmentRequest request);
    }
}