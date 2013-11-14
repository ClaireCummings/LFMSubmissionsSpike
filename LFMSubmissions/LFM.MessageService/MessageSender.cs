using LFM.LandRegistry;
using LFM.LandRegistry.Commands;
using NServiceBus;

namespace LFM.MessageService
{
    public class MessageSender : ISendMessages
    {
        public IBus _bus;

        public MessageSender(IBus bus)
        {
            _bus = bus;
        }

        public SubmitLrap1Result Send(SubmitLrap1Command command)
        {
            _bus.Send(command);
            return  new SubmitLrap1Result()
            {
                Command = command
            };
        }

        public void Send(SubmitLrap1AttachmentCommand command)
        {
            _bus.Send(command);


        }
    }
}
