using System;
using System.Net.Mime;

namespace LFM.LandRegistry.SubmissionsService
{
    public class Lrapp1SubmissionService
    {
        public ISubmitter Submitter { get; set; }

        public void Submit(string username, string password, Lrapp1Package lrapp1Package)
        {
            var applicationId = Guid.NewGuid().ToString();

            Submitter.Send(new SubmitLrapp1Command()
            {
                ApplicationId = applicationId,
                Username = username,
                Password = password,
                Payload = lrapp1Package.Payload
            });

            foreach (var attachment in lrapp1Package.Attachments)
            {
                Submitter.Send(new SubmitLrapp1AttachmentCommand()
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    ApplicationId = applicationId,
                    Username = username,
                    Password = password,
                    Payload = attachment.Payload
                });
            }
        }
    }
}
