using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class WVUEmployee : ApplicationUser
    {
        //Dont create seperate primary key for child class

        public string EmployeeNameAndDepartment
        {
            get { return (FullName + " in " + Department.DepartmentName); }
        }

        [Required]
        public int DepartmentID { get; set; }

        public int? PermitID { get; set; }      

        [ForeignKey("PermitID")]
        public Permit Permit { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }

        public WVUEmployee()
        {

        }

        public WVUEmployee(string firstname, string lastname, string email, string phonenumber,string password, int departmentID) : base(firstname, lastname, email, phonenumber, password)
        {
            this.DepartmentID = departmentID;
            
        }
    }
}
