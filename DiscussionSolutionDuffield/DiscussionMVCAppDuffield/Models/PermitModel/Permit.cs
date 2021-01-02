using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class Permit
    {
        [Key]
        public int PermitID { get; set; }

        [Required]
        public double PermitAmount { get; set; }

        public DateTime PStartDate { get; set; }

        public DateTime PEndDate { get; set; }

        [Required]
        public string WVUEmployeeID { get; set; }

        [ForeignKey("WVUEmployeeID")]
        public WVUEmployee WVUEmployee { get; set; }

        public string ParkingEmployeeWhoAssignedPermitID { get; set; }

        [NotMapped]
        public WVUEmployee ParkingEmployeeWhoAssignedPermit { get; set; }

        public DateTime DateTimeWhenParkingPermitWasAssigned { get; set; }

        public Permit() { }

        public Permit(double permitAmount, DateTime pStartDate, DateTime pEndDate, string wvuEmployeeID, string parkingEmployeeID)
        {
            this.PermitAmount = permitAmount;
            this.PStartDate = pStartDate;
            this.PEndDate = pEndDate;
            this.WVUEmployeeID = wvuEmployeeID;
            this.DateTimeWhenParkingPermitWasAssigned = DateTime.Now;
            this.ParkingEmployeeWhoAssignedPermitID = parkingEmployeeID;
        }
    }
}
