using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public interface IPermitRepo
    {
        bool DoesWVUEmployeeHavePermit(string wvuEmployeeID);
        Task AddPermit(Permit permit);

        List<Permit> ListAllPermits();
    }
}
