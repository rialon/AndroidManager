using AndroidManager.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidManager.DAL.EF {

    public class ApplicationContext : IdentityDbContext<ApplicationOperator> {

        public ApplicationContext(string connectionString) : base(connectionString) {
        }

        public DbSet<Android> Androids { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
