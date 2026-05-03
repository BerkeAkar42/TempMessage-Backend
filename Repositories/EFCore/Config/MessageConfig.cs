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
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageId); //PK
            
            //FK Tanımlamaları
            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId) //FK olarak tanımla
                .OnDelete(DeleteBehavior.Cascade); // User silinirse mesajlarını sil

            builder.HasOne(m => m.MessageLobby)
                .WithMany()
                .HasForeignKey(m => m.MessageLobbyId) //FK olarak tanımla
                .OnDelete(DeleteBehavior.Cascade); // Lobi silinirse mesajları sil
        }
    }
}
