using System;
using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry.SubmissionsService
{
    public class Lrap1SubmissionService
    {
        private readonly ISendMessages _messageSender;

        public Lrap1SubmissionService(ISendMessages messageSender)
        {
            _messageSender = messageSender;
        }

        public SubmitLrap1Result Submit(string username, string password, Lrap1Package lrap1Package)
        {
            var applicationId = Guid.NewGuid().ToString();

            var result = _messageSender.Send(new SubmitLrap1Command()
            {
                ApplicationId = applicationId,
                Username = username,
                Password = password,
                Payload = lrap1Package.Payload
            });

            foreach (var attachment in lrap1Package.Attachments)
            {
                _messageSender.Send(new SubmitLrap1AttachmentCommand()
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    ApplicationId = result.Command.ApplicationId,
                    Username = username,
                    Password = password,
                    Payload = attachment.Payload
                });
            }
            return result;
        }
    }
}
