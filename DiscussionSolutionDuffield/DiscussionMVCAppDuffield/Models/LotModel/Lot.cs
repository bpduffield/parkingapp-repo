using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class Lot
    {
        //attributes (instance variables):private
        //MVC Properties: public
        [Key]
        public int LotID { get; set; }

        [Required]
        public string LotNumber { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public string LotAddress { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public int CurrentOccupancy { get; set; }

        //[NotMapped]
        public List<LotStatus> LotStatuses { get; set; }
        

        public Lot(string lotNumber, string locationName, string lotAddress, int maxCapacity)
        {
            this.LotNumber = lotNumber;
            this.LocationName = locationName;
            this.LotAddress = lotAddress;
            this.MaxCapacity = maxCapacity;
            this.CurrentOccupancy = 0;
            this.LotStatuses = new List<LotStatus>();
            
        }



        //Signature of a method: accessType returnDataType MethodName(paramType paramName,...)
        //Constructor
        ////1. No Return Data type
        ////2. Same name as class
     
            //Object - Relational - Mapper (ORM)
            //Entity Framework .NET core
            //For EF provide empty constructor 
        public Lot() { }

       



    }
}
