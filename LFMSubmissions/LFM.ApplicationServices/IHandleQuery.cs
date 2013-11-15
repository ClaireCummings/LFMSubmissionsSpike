using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFM.ApplicationServices
{
    public interface IHandleQuery<out TQueryResult>
    {
        TQueryResult Query();
    }

    public interface IHandleQuery<in TQuery, out TQueryResult>
    {
        TQueryResult Query(TQuery query);
    }
}
