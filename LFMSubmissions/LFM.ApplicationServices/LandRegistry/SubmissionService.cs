namespace LFM.ApplicationServices.LandRegistry
{
    public class SubmissionService : IHandleQuery<Lrap1StatusQuery, Lrap1StatusQueryResult>
    {
        private readonly ILrap1SubmissionRepository _lrap1SubmissionRepository;

        public SubmissionService(ILrap1SubmissionRepository lrap1SubmissionRepository)
        {
            _lrap1SubmissionRepository = lrap1SubmissionRepository;
        }

        public Lrap1StatusQueryResult Query(Lrap1StatusQuery query)
        {
            return new Lrap1StatusQueryResult()
            {
                ResponseType = _lrap1SubmissionRepository.GetById(query.ApplicationId).ResponseType
            };
        }
    }
}