namespace LFM.LandRegistry.CommsService
{
    public interface IEdrsCommunicator
    {
        Lrap1Response Submit(Lrap1Request request);
    }
}