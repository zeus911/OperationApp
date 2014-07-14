using Nbugs.OperationApp.Models.System;
using System.Data.Entity;

namespace Nbugs.OperationApp.Models
{
    public class Db144Context : DbContext
    {
        public Db144Context()
            : base("name=Db144")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModuleAction>().Map(m => m.ToTable("Action"));
            modelBuilder.Entity<User>().Map(m => m.ToTable("Users"));
            modelBuilder.Entity<Role>().Map(m => m.ToTable("Role"));
            modelBuilder.Entity<Module>().Map(m => m.ToTable("Module"));
            modelBuilder.Entity<UserRole>().Map(m => m.ToTable("UserRole"));
            modelBuilder.Entity<RoleModule>().Map(m => m.ToTable("RoleModule"));
            modelBuilder.Entity<RoleModuleAction>().Map(m => m.ToTable("RoleModuleAction"));
            //modelBuilder.Entity<Article>().Map(m => m.ToTable("Article"));
            //modelBuilder.Entity<Category>().Map(m => m.ToTable("Category"));
            //modelBuilder.Entity<Photo>().Map(m => m.ToTable("Photo"));
            //modelBuilder.Entity<Appendix>().Map(m => m.ToTable("Appendix"));
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<ModuleAction> ModuleActions { get; set; }

        public DbSet<RoleModule> RoleModules { get; set; }

        //public DbSet<RoleModuleAction> RoleModuleActions { get; set; }
        //public DbSet<Article> Articles { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Photo> Photoes { get; set; }
        //public DbSet<Appendix> Appendixes { get; set; }
    }
}