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
    public class MessageLobbyConfig : IEntityTypeConfiguration<MessageLobby>
    {
        public void Configure(EntityTypeBuilder<MessageLobby> builder)
        {
            builder.HasKey(ml => ml.MessageLobbyId); //PK
            builder.Property(ml => ml.Name)
                .HasMaxLength(100)
                .IsRequired(); // Lobi adı max 100 karakter olabilir.
            builder.Property(u => u.CreateDate)
                .IsRequired(); //Boş olamaz.
            builder.Property(u => u.ValidityPeriod)
                .IsRequired(); //Boş olamaz.
            builder.Property(ml => ml.IsActive)
                .HasDefaultValue(true); //Lobi açıldığında otomatik olarak true yazsın (yani lobi aktif)
        }
    }
}
