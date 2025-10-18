using Microsoft.EntityFrameworkCore;
using MyCMS.DataLayer.AddAuditFieldInterceptors;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Contexts
{
    public class MyCmsDbContext : DbContext
    {
        private readonly AddAuditFieldInterceptor _auditInterceptor;
        public MyCmsDbContext(DbContextOptions options, AddAuditFieldInterceptor auditInterceptor) : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }

        public DbSet<User>  Users { get; set; }
        public DbSet<Role>  Roles { get; set; }
        public DbSet<Categury> Categuries { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>(e =>
            {e.HasOne(c => c.User)
               .WithOne() 
               .HasForeignKey<Comment>(c => c.CommentBy);
            }
           );

            //SetConfigEntities
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
           
            //ShdowPropertiesAndQueryFilter
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {

                //AddShadowPropertiesAllTables
                modelBuilder.Entity(item.ClrType).Property<int>("CreateUser").IsRequired();
                modelBuilder.Entity(item.ClrType).Property<DateTime>("CreateDate").IsRequired();
                modelBuilder.Entity(item.ClrType).Property<int>("UpdateUser");
                modelBuilder.Entity(item.ClrType).Property<DateTime>("UpdateDate");


                // FilterIsDeletedAllTables
                var isDeletedProperty = item.ClrType.GetProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.PropertyType == typeof(bool))
                {
                    var parameter = Expression.Parameter(item.ClrType, "e");
                    var body = Expression.Equal(
                        Expression.Property(parameter, isDeletedProperty),
                        Expression.Constant(false));
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(item.ClrType).HasQueryFilter(lambda);
                }

            }
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().HaveMaxLength(255).AreUnicode(true);
        }
    }
}
