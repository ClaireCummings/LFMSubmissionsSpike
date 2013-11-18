namespace LFM.ApplicationServices
{
    public interface IHandleCommand<in TCommand, out TResponse>
    {
        TResponse Execute(TCommand command);
    }
}