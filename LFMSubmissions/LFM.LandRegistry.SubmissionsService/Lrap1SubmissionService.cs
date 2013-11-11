using System;
using System.Net.Mime;
using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry.SubmissionsService
{
    public class Lrap1SubmissionService
    {
        public ISubmitter Submitter { get; set; }

        public void Submit(string username, string password, Lrap1Package lrap1Package)
        {
            var applicationId = Guid.NewGuid().ToString();

            var result = Submitter.Send(new SubmitLrap1Command()
            {
                ApplicationId = applicationId,
                Username = username,
                Password = password,
                Payload = lrap1Package.Payload
            });

            foreach (var attachment in lrap1Package.Attachments)
            {
                Submitter.Send(new SubmitLrap1AttachmentCommand()
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    ApplicationId = result.Command.ApplicationId,
                    Username = username,
                    Password = password,
                    Payload = attachment.Payload
                });
            }
        }
    }
}
