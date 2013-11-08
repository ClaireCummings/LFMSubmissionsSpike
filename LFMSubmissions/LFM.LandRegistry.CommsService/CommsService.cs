using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFM.LandRegistry.CommsService
{
    public class CommsService
    {
        private readonly IEdrsCommunicator _edrsCommunicator;

        public CommsService(IEdrsCommunicator edrsCommunicator)
        {
            _edrsCommunicator = edrsCommunicator;
        }

        public ResponseType Send(string payload)
        {
            return _edrsCommunicator.Submit(new Lrap1Request()
            {
                Payload = payload
            }).ResponseType;
        }
    }
}
