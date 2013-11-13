namespace LFM
{
    public interface IProcessMessages<in TMessage>
    {
        void Process(TMessage message);
    }
}