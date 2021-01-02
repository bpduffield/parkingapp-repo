using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class Visitor : ApplicationUser
    {
        public string VisitorOrganization { get; set; }

        public Visitor()
        {

        }

        public Visitor(string firstname, string lastname, string email, string phonenumber, string password, string visitorOrganization) : base(firstname, lastname, email, phonenumber, password)
        {
            this.VisitorOrganization = visitorOrganization;
        }
    }
}
