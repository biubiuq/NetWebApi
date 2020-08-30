using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DapperManager;

namespace NetCoreApi.Data
{
    public class NetCoreApiContext : DbContext
    {
        public NetCoreApiContext (DbContextOptions<NetCoreApiContext> options)
            : base(options)
        {
        }

        public DbSet<DapperManager.UserInfo> UserInfo { get; set; }
        public DbSet<DapperManager.Role> Role { get; set; }
        public DbSet<DapperManager.Role_User> Role_User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DapperManager.Role_User>().HasKey(a => a.Role_Id);
            modelBuilder.Entity<DapperManager.Role>().HasKey(a => a.Role_Id);
        }
    }
}
