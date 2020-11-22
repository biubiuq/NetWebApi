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
        public DbSet<DapperManager.Permisson> Permisson { get; set; }
        public DbSet<DapperManager.Role_Permisson> Role_Permisson { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DapperManager.Role_User>().HasKey(a => a.User_Id);
            modelBuilder.Entity<DapperManager.Role>().HasKey(a => a.Role_Id);
            modelBuilder.Entity<DapperManager.Permisson>().HasKey(a => a.KeyId);
            modelBuilder.Entity<Permisson>(p =>
              p.HasMany(p => p.Permissons)////通过零件表的Permisson可以找到多个关联行
              .WithOne()////设置Permisson实体通过属性Permissons可以找到一个Permisson实体，表示关联行表是一对多关系中的从表
              .HasForeignKey(p => p.Parent_Id)////指定连接属性
              .HasPrincipalKey(p => p.Permisson_Id)


            );

            modelBuilder.Entity<DapperManager.Role_Permisson>().HasKey(a => a.Guid);

        }
     

    }
}
