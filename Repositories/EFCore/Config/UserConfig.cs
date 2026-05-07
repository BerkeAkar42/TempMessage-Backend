using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId); //PK
            builder.Property(u => u.NickName)
                .IsRequired() //Boş olamaz.
                .HasMaxLength(50);
            builder.Property(u => u.AccessKey)
                .IsRequired(); //Boş olamaz.
            builder.Property(u => u.CreateDate)
                .IsRequired(); //Boş olamaz.
            builder.Property(u => u.LastActiveDate)
                .IsRequired(); //Boş olamaz.
        }
    }
}
