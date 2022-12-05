using System;
using AlbumReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumReviews.Data
{
    public class AppDbContext : DbContext
    {
        // pass options UP to the DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // add our data models as entitys in the database
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }

        // customize database in the code
        // using OnModelCreating for many-many-relationships
        // to link tabels together


        // (i think) this is run automativally by MVC
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Album>()
                .HasOne(a => a.Artist)
                .WithMany(a => a.Albums);

            // when our custom OnModelCreating is called
            // an instance of DbInitializer is created, passing in the builder
            // which calls the Seed method

           
            new DbInitializer(builder).Seed();
        }
    }
}

