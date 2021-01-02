using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public interface ILotStatusRepo
    {
        double FindPermitAmount(int lotID, int lotTypeID);
    }
}
