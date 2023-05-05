﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._Albums_Info
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions options)
            :base(options)
        {

        }
        public MusicContext()
        {

        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<SongPerformer> SongPerformers { get; set; }
        public virtual DbSet<Writer> Writers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=02. Albums Info;TrustServerCertificate=True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongPerformer>().HasKey(x => new { x.SongId, x.PerformerId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
