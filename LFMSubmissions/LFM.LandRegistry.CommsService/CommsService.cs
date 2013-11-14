using System;
using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry.CommsService
{
    public class CommsService : ICommsService
    {
        private readonly IEdrsCommunicator _edrsCommunicator;

        public CommsService(IEdrsCommunicator edrsCommunicator)
        {
            _edrsCommunicator = edrsCommunicator;
        }

        public ResponseType Send(SubmitLrap1Command submission)
        {
            if (string.IsNullOrEmpty(submission.ApplicationId))
            {
                throw new ArgumentException("Missing ApplicationId");
            }

            if (string.IsNullOrEmpty(submission.Username))
            {
                throw new ArgumentException("Missing Username");
            }

            if (string.IsNullOrEmpty(submission.Password))
            {
                throw new ArgumentException("Missing Password");
            }

            return _edrsCommunicator.Submit(new Lrap1Request()
            {
                ApplicationId = submission.ApplicationId,
                Username = submission.Username,
                Password = submission.Password,
                Payload = submission.Payload
            }).ResponseType;
        }

        public ResponseType Send(SubmitLrap1AttachmentCommand submission)
        {
            if (string.IsNullOrEmpty(submission.AttachmentId))
            {
                throw new ArgumentException("Missing AttachmentId");
            }

            if (string.IsNullOrEmpty(submission.ApplicationId))
            {
                throw new ArgumentException("Missing ApplicationId");
            }

            if (string.IsNullOrEmpty(submission.Username))
            {
                throw new ArgumentException("Missing Username");
            }

            if (string.IsNullOrEmpty(submission.Password))
            {
                throw new ArgumentException("Missing Password");
            }

            return _edrsCommunicator.Submit(new Lrap1AttachmentRequest()
            {
                AttachmentId = submission.AttachmentId,
                Username = submission.Username,
                Password = submission.Password,
                ApplicationId = submission.ApplicationId,
                Payload = submission.Payload
            }).ResponseType;
        }
    }
}
