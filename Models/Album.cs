using System;
using System.ComponentModel.DataAnnotations;

namespace AlbumReviews.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }

        // one to many relationship with artist
        public int ArtistID {get; set;}
        public Artist? Artist { get; set; }

    }
}

