using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Xunit;

namespace LFM.LandRegistry.CommsService.UnitTests
{
    public class When_a_Lrap1_is_sent
    {
        private readonly IEdrsCommunicator _fakeEdrsCommunicator;
        private readonly CommsService _sut;

        public When_a_Lrap1_is_sent()
        {
            _fakeEdrsCommunicator = A.Fake<IEdrsCommunicator>();
            _sut = new CommsService(_fakeEdrsCommunicator);
        }

        [Fact]
        public void the_payload_is_submitted_to_the_eDRS_service()
        {
            //todo: need to check that the payload data is on the request
            _sut.Send("payload data");
            A.CallTo(()=>_fakeEdrsCommunicator.Submit(A<Lrap1Request>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void an_acknowledgement_is_returned()
        {
            var edrsAcknowledgment = new Lrap1Response(){ResponseType=ResponseType.Acknowledgment};
            A.CallTo(() => _fakeEdrsCommunicator.Submit(A<Lrap1Request>.Ignored)).Returns(edrsAcknowledgment);
            var response = _sut.Send("payload data");
            Assert.Equal(ResponseType.Acknowledgment,response);
        }

        [Fact]
        public void a_rejection_is_returned()
        {
            var edrsRejection = new Lrap1Response() { ResponseType = ResponseType.Rejection };
            A.CallTo(() => _fakeEdrsCommunicator.Submit(A<Lrap1Request>.Ignored)).Returns(edrsRejection);
            var response = _sut.Send("payload data");
            Assert.Equal(ResponseType.Rejection, response);            
        }
    }
}
