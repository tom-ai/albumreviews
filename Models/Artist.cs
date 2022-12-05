using System;
using System.ComponentModel.DataAnnotations;

namespace AlbumReviews.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        // one to many relationship with Albums
        public ICollection<Album>? Albums { get; set; }
    }
}

