using System;
using AlbumReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumReviews.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Populates hardcoded data to each entity
        /// </summary>
        public void Seed()
        {
            _builder.Entity<Artist>(a =>
            {
                a.HasData(new Artist
                {
                    Id = 1,
                    Name = "Wayne Shorter"
                });
                a.HasData(new Artist { Id = 2, Name = "Red Hot Chili Peppers" });
            });

            _builder.Entity<Album>(a =>
            {
                a.HasData(new Album { Id = 1, Name = "Freaky Style", ReleaseDate = new DateTime(1985, 01, 29), Genre = Genre.Rock, ArtistID = 2 });
                a.HasData(new Album { Id = 2, Name = "By The Way", ReleaseDate = new DateTime(2002, 10, 01), Genre = Genre.Rock, ArtistID = 2 });
                a.HasData(new Album { Id = 3, Name = "JuJu", ReleaseDate = new DateTime(1965, 07, 07), Genre = Genre.Jazz, ArtistID = 1 });
            });
        }
    }
}

