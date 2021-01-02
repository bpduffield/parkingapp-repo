using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public interface IApplicationUserRepo
    {
        List<ApplicationUser> ListAllAppUsers();

        List<WVUEmployee> ListAllWVUEmployees();
        string FindUserID();
        WVUEmployee FindWvuEmployee(string wvuEmployeeID);
    }
}
