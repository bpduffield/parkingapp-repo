using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class LotType
    {
       
            [Key]
            public int LotTypeID { get; set; }
            
            public string LotTypeName { get; set; }
            
            [NotMapped]
            public List<LotStatus> LotStatuses { get; set; }

            public LotType(string lotTypeName)
            {
                
                this.LotTypeName = lotTypeName;
                this.LotStatuses = new List<LotStatus>();
            }

            public LotType() { }
        
    }
}
