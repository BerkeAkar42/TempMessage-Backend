using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Features.LobbyMembers
{
    public class LobbyMemberConfig : IEntityTypeConfiguration<LobbyMember>
    {
        public void Configure(EntityTypeBuilder<LobbyMember> builder)
        {
            builder.HasKey(lm => lm.LobbyMemberId); //PK
            builder.Property(lm => lm.JoinedDate)
                .IsRequired(); //Boş olamaz.
            builder.Property(lm => lm.IsAdmin)
                .IsRequired(); //Boş olamaz.

            //FK Tanımlamaları
            builder.HasOne(lm => lm.User)
                .WithMany()
                .HasForeignKey(lm => lm.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse bu lobi üyeliği de silinsin

            builder.HasOne(lm => lm.Lobby)
                .WithMany()
                .HasForeignKey(lm => lm.LobbyId)
                .OnDelete(DeleteBehavior.Cascade); // Lobi silinirse tüm üyelik kayıtları uçsun
        }
    }
}
