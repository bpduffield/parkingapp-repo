using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    //ApplicationUser inherits identity user class
    public class ApplicationUser : IdentityUser
    {
        //key is ID that is built in
        //it is a string

        public string FirstName { get; set; }
        public string LastName { get; set; }

        //read only
        public string FullName
        {
            get { return (FirstName + " " + LastName); }
        }

        public ApplicationUser()
        {

        }

        public ApplicationUser(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.UserName = email;
            this.PhoneNumber = phoneNumber;

            //Passwords require hash(not saved as clear text)
            //option to encrypt(v hashing)
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            this.PasswordHash = passwordHasher.HashPassword(this, password);

            //Guid = Globally unique ID
            this.SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
