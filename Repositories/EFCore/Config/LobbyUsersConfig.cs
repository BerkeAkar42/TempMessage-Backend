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
    public class LobbyUsersConfig : IEntityTypeConfiguration<LobbyUsers>
    {
        public void Configure(EntityTypeBuilder<LobbyUsers> builder)
        {
            builder.HasKey(lu => lu.LobbyUsersId); //PK

            //FK Tanımlamaları
            builder.HasOne(lu => lu.User)
                .WithMany()
                .HasForeignKey(lu => lu.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse bu lobi üyeliği de silinsin

            builder.HasOne(lu => lu.MessageLobby)
                .WithMany()
                .HasForeignKey(lu => lu.MessageLobiId)
                .OnDelete(DeleteBehavior.Cascade); // Lobi silinirse tüm üyelik kayıtları uçsun
        }
    }
}
