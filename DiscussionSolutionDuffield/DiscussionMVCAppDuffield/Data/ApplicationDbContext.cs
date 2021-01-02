using System;
using System.Collections.Generic;
using System.Text;
using DiscussionMVCAppDuffield.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMVCAppDuffield.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //properties to help translate classes to tables
        public DbSet<Lot> Lots {get;set;} //translator
        public DbSet<LotType> LotTypes { get; set; }
        public DbSet<LotStatus> LotStatuses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<WVUEmployee> WVUEmployees { get; set; }
        public DbSet<Permit> Permits { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }//end of constructor

    }//end of class
}//end of namespace
