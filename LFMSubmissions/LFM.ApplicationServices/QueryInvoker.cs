using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.ApplicationServices.LandRegistry;

namespace LFM.ApplicationServices
{
    public class QueryInvoker
    {
        private readonly IHandleQuery<Lrap1StatusQuery, Lrap1StatusQueryResult> _submissionService;

        public QueryInvoker(IHandleQuery<Lrap1StatusQuery, Lrap1StatusQueryResult> submissionService)
        {
            _submissionService = submissionService;
        }
        
        public TQueryResult Query<TQueryResult>()
        {
            throw new NotImplementedException();
        }

        public TQueryResult Query<TQuery, TQueryResult>(TQuery query)
        {
            return ((IHandleQuery<TQuery, TQueryResult>)_submissionService).Query(query);
        }
    }
}
