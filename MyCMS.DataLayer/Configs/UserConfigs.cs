using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Configs
{
    public class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.UserName).IsRequired().IsUnicode(false);
            builder.HasIndex(e => e.UserName).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.Email).IsRequired().IsUnicode(false);
            builder.Property(e => e.Password).IsRequired().IsUnicode(false);
            builder.Property(e => e.RoleId).IsRequired();
            
        }
    }
}
