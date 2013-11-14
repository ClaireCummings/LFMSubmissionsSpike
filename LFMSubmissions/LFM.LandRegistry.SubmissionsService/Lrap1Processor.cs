using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry.SubmissionsService
{
    public class Lrap1Processor : IProcessMessages<SubmitLrap1Command>, IProcessMessages<SubmitLrap1AttachmentCommand>
    {
        public ISendMessages _messageSender;
        public ICommsService _commsService;

        public Lrap1Processor(ISendMessages messageSender, ICommsService commsService)
        {
            _messageSender = messageSender;
            _commsService = commsService;
        }

        public void Process(SubmitLrap1Command message)
        {
            var response = _commsService.Send(message);

            if (response == ResponseType.None)
            {
                _messageSender.Send(message);
            }
        }

        public void Process(SubmitLrap1AttachmentCommand message)
        {
            var response = _commsService.Send(message);

            if (response == ResponseType.None)
            {
                _messageSender.Send(message);
            }
        }
    }
}