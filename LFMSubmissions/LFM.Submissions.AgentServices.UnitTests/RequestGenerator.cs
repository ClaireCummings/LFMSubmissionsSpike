using System.IO;
using LFM.LandRegistry;
using LFM.Submissions.AgentServices.EdrsAttachmentService;
using LFM.Submissions.AgentServices.EdrsSubmissionService;

namespace LFM.Submissions.AgentServices.UnitTests
{
    internal class RequestGenerator
    {
        private readonly ObjectSerializer _objectSerializer;
        public RequestGenerator()
        {
            _objectSerializer = new ObjectSerializer();
        }

        public RequestApplicationToChangeRegisterV1_0Type Lrap1Request(ResponseType requiredResponse)
        {
            var testXmlFileName = "./TestXml/";
            switch (requiredResponse)
            {
                case ResponseType.Acknowledgment:
                    testXmlFileName += "eDRS Test 4 XmlRequest.xml";
                    break;
                case ResponseType.Rejection:
                    testXmlFileName += "eDRS Test 1 XmlRequest.xml";
                    break;
            }

            var xml = File.ReadAllText(testXmlFileName);

            return _objectSerializer.XmlDeserializeFromString<RequestApplicationToChangeRegisterV1_0Type>(xml);
        }
        
        public newAttachmentRequest Lrap1AttachmentRequest(ResponseType requiredResponse)
        {
            var testXmlFileName = "./TestXml/";
            switch (requiredResponse)
            {
                case ResponseType.Acknowledgment:
                    testXmlFileName += "Attachment Service Test 1 XmlRequest.xml";
                    break;
                case ResponseType.Rejection:
                    testXmlFileName += "Attachment Service Test 6 XmlRequest.xml";
                    break;
            }

            var xml = File.ReadAllText(testXmlFileName);

            return _objectSerializer.XmlDeserializeFromString<newAttachmentRequest>(xml);
        }

    }
}
