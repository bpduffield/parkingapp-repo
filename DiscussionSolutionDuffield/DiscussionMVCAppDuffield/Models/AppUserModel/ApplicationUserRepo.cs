using DiscussionMVCAppDuffield.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class ApplicationUserRepo : IApplicationUserRepo
    {
        private ApplicationDbContext database;
        private IHttpContextAccessor httpContextAccessor;

        public ApplicationUserRepo(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            this.database = dbContext;
            this.httpContextAccessor = contextAccessor;
        }

        public string FindUserID()
        {
            string userID = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return userID;
        }

        public WVUEmployee FindWvuEmployee(string wvuEmployeeID)
        {
            WVUEmployee employee = database.WVUEmployees.Include(w => w.Department).Where(w => w.Id == wvuEmployeeID).FirstOrDefault();
            return employee;
        }

        public List<ApplicationUser> ListAllAppUsers()
        {
            List<ApplicationUser> applicationUsers = database.ApplicationUsers.ToList();

            return applicationUsers;
        }


        public List<WVUEmployee> ListAllWVUEmployees()
        {
            List<WVUEmployee> employeeList = database.WVUEmployees.Include(w => w.Department).ToList<WVUEmployee>();

            return employeeList;
        }
    }
}
