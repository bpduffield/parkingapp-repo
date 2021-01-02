using DiscussionMVCAppDuffield.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class PermitRepo : IPermitRepo
    {
        private ApplicationDbContext database;

        public PermitRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public Task AddPermit(Permit permit)
        {
            database.Permits.AddAsync(permit);
            return database.SaveChangesAsync();
        }

        public bool DoesWVUEmployeeHavePermit(string wvuEmployeeID)
        {
            bool hasPermit;
            Permit permit = database.Permits.Where(p => p.WVUEmployeeID == wvuEmployeeID).FirstOrDefault();

            if(permit == null)
            {
                hasPermit = false;
            }
            else
            {
                hasPermit = true;
            }
            return hasPermit;
        }

        public List<Permit> ListAllPermits()
        {
            List<Permit> permitList = database.Permits.ToList<Permit>();
            return permitList;
        }
    }
}
