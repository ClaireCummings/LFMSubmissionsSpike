using System.Security.Cryptography.X509Certificates;
using LFM.LandRegistry;

namespace LFM
{
    public interface ILrap1SubmissionRepository
    {
        Lrap1Submission GetById(string applicationId);
        void Save(Lrap1Submission lrap1Submission);
    }
}