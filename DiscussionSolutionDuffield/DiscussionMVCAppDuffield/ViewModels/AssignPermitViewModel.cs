using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.ViewModels
{
    public class AssignPermitViewModel
    {
        public int LotID { get; set; }
        public string WVUEmployeeID { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
