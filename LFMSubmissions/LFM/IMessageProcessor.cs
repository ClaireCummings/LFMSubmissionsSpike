namespace LFM
{
    public interface IMessageProcessor
    {
        void Process<TMessage>(TMessage message);
    }
}
