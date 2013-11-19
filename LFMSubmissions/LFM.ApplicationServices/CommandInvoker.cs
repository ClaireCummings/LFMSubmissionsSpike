using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.ApplicationServices.LandRegistry;

namespace LFM.ApplicationServices
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly SubmissionDataService _submissionDataService;

        public CommandInvoker(SubmissionDataService submissionDataService)
        {
            _submissionDataService = submissionDataService;
        }

        public void Execute<TCommand>(TCommand command)
        {
            throw new NotImplementedException();
        }

        public TResponse Execute<TCommand, TResponse>(TCommand command)
        {
            return ((IHandleCommand<TCommand, TResponse>) _submissionDataService).Execute(command);
        }
    }
}
