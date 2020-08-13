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
    }
}
