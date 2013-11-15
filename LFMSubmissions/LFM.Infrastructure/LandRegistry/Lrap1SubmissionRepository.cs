using LFM.LandRegistry;

namespace LFM.Infrastructure.LandRegistry
{
    public class Lrap1SubmissionRepository : ILrap1SubmissionRepository
    {
        public string ApplicationId { get; set; }
        public ResponseType ResponseType { get; set; }
        public Lrap1Submission GetById(string applicationId)
        {
            throw new System.NotImplementedException();
        }
    }
}
