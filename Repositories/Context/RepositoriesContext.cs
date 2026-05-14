using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Context
{
    public class RepositoriesContext : DbContext
    {
        public RepositoriesContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } //Users Tablosu
        public DbSet<Lobby> Lobby { get; set; } //Mesaj lobilerinin tutulduğu tablo
        public DbSet<Message> Messages { get; set; } //Mesajların tutulduğu tablo
        public DbSet<LobbyMember> LobbyMember { get; set; } //Lobi kullanıcılarının tutulduğu tablo


        //İlgili veritabanı tablolarının konfig dosyaları
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurasyonları ayrı dosyalarda tuttuk.
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new LobbyConfig());
            modelBuilder.ApplyConfiguration(new MessageConfig());
            modelBuilder.ApplyConfiguration(new LobbyMemberConfig());
        }
    }
}
