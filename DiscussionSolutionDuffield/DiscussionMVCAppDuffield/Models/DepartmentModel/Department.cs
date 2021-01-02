using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentAddress { get; set; }

        [NotMapped]
        public List<WVUEmployee> WVUEmployees { get; set; }

        public Department() { }

        public Department(string departmentName, string departmentAddress) 
        {
            this.DepartmentName = departmentName;
            this.DepartmentAddress = departmentAddress;
        }
    }
}
