using LFM.LandRegistry;
using LFM.LandRegistry.Commands;

namespace LFM.ApplicationServices.LandRegistry
{
    public class SubmissionDataService : 
        IHandleQuery<Lrap1StatusQuery, Lrap1StatusQueryResult>, 
        IHandleCommand<CreateLrap1SubmissionCommand, CreateLrap1SubmissionQueryResult>, 
        IHandleCommand<CreateLrap1AttachmentCommand, CreateLrap1AttachmentQueryResult>
    {
        private readonly ILrap1SubmissionRepository _lrap1SubmissionRepository;
        private readonly ILrap1AttachmentRepository _lrap1AttachmentRepository;
        public SubmissionDataService(ILrap1SubmissionRepository lrap1SubmissionRepository, ILrap1AttachmentRepository lrap1AttachmentRepository)
        {
            _lrap1SubmissionRepository = lrap1SubmissionRepository;
            _lrap1AttachmentRepository = lrap1AttachmentRepository;
        }

        public Lrap1StatusQueryResult Query(Lrap1StatusQuery query)
        {
            return new Lrap1StatusQueryResult()
            {
                ResponseType = _lrap1SubmissionRepository.GetById(query.ApplicationId).ResponseType
            };
        }

        public CreateLrap1SubmissionQueryResult Execute(CreateLrap1SubmissionCommand command)
        {
            _lrap1SubmissionRepository.Save(new Lrap1Submission()
            {
                ApplicationId = command.ApplicationId,
                Username = command.Username,
                Payload = command.Payload
            });

            return new CreateLrap1SubmissionQueryResult()
            {
                Command = command
            };
        }

        public CreateLrap1AttachmentQueryResult Execute(CreateLrap1AttachmentCommand command)
        {
            _lrap1AttachmentRepository.Save(new Lrap1Attachment()
            {
                AttachmentId = command.AttachmentId,
                ApplicationId = command.ApplicationId,
                Username = command.Username,
                Payload = command.Payload
            });

            return new CreateLrap1AttachmentQueryResult()
            {
                Command = command
            };        
        }
    }
}