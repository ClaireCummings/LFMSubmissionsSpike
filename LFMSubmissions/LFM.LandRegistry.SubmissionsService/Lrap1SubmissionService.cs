using System;
using LFM.ApplicationServices;
using LFM.ApplicationServices.LandRegistry;
using LFM.LandRegistry.Commands;

namespace LFM.LandRegistry.SubmissionsService
{
    public class Lrap1SubmissionService 
    {
        private readonly ISendMessages _messageSender;
        private readonly ICommandInvoker _commandInvoker;

        public Lrap1SubmissionService(ISendMessages messageSender, ICommandInvoker commandInvoker )
        {
            _messageSender = messageSender;
            _commandInvoker = commandInvoker;
        }

        public SubmitLrap1Result Submit(string username, string password, Lrap1Package lrap1Package)
        {
            var applicationId = Guid.NewGuid().ToString();

            var saveResult = _commandInvoker.Execute<CreateLrap1SubmissionCommand, CreateLrap1SubmissionQueryResult>(new CreateLrap1SubmissionCommand()
            {
                ApplicationId = applicationId,
                Username = username,
                Payload = lrap1Package.Payload
            });

            var saveAttachmentResult = _commandInvoker.Execute<CreateLrap1AttachmentCommand, CreateLrap1AttachmentQueryResult>(new CreateLrap1AttachmentCommand()
            {
                AttachmentId = Guid.NewGuid().ToString(),
                ApplicationId = saveResult.Command.ApplicationId,
                Username = username,
                Payload = lrap1Package.Attachments[0].Payload
            });

            var sendResult = _messageSender.Send(new SubmitLrap1Command()
            {
                ApplicationId = saveResult.Command.ApplicationId,
                Username = username,
                Password = password,
                Payload = lrap1Package.Payload
            });

            foreach (var attachment in lrap1Package.Attachments)
            {
                _messageSender.Send(new SubmitLrap1AttachmentCommand()
                {
                    AttachmentId = saveAttachmentResult.Command.AttachmentId,
                    ApplicationId = saveResult.Command.ApplicationId,
                    Username = username,
                    Password = password,
                    Payload = attachment.Payload
                });
            }

            return sendResult;
        }
    }
}
