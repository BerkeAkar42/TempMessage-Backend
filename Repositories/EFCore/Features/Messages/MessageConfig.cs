using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repositories.EFCore.Features.Messages
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageId); //PK
            builder.Property(m => m.Content)
                .IsRequired() //Boş olamaz.
                .HasMaxLength(1000); //mesaj max 1000 karakter olacak
            builder.Property(m => m.SendDate)
                .IsRequired(); //Boş olamaz.
            builder.Property(m => m.Type)
                .IsRequired(); //Boş olamaz.

            builder.HasOne<Message>() // Bir mesajın bir parent'ı olabilir, bir parent'ın birçok cevabı olabilir.
                .WithMany()
                .HasForeignKey(m => m.ParentMessageId)
                .OnDelete(DeleteBehavior.Restrict); //Burası "Cascade" olmamalı

            builder.HasQueryFilter(m => !m.IsDeleted); //Silinen mesajları getirmez.


            //FK Tanımlamaları
            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId) //FK olarak tanımla
                .OnDelete(DeleteBehavior.Cascade); // User silinirse mesajlarını sil

            builder.HasOne(m => m.Lobby)
                .WithMany()
                .HasForeignKey(m => m.LobbyId) //FK olarak tanımla
                .OnDelete(DeleteBehavior.Cascade); // Lobi silinirse mesajları sil
        }
    }
}
