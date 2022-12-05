using System;
using AlbumReviews.Models;

namespace AlbumReviews.Services
{
    public interface IRecordStoreService
    {
        /// <summary>
        /// Artist services, POST PUT and DELETE accept Artist objects arguments
        /// </summary>
        /// <returns></returns>
        #region Artists
        public Task<List<Artist>> GetArtistsAsync(); // GET Artists

        public Task<Artist> GetArtistByIdAsync(int id, bool includeAlbums = false); // GET single artist, optional include albums

        public Task<Artist> AddArtistAsync(Artist artist); // POST artist (and return it)

        public Task<Artist> UpdateArtistAsync(Artist artist); // PUT artist

        public Task<(bool, string)> DeleteArtistAsync(Artist artist); // DELETE artist
        #endregion  


        #region Albums
        public Task<List<Album>> GetAlbumsAsync();

        public Task<Album> GetAlbumById(int id, bool includeArtist = false);

        public Task<Album> AddAlbumAsync(Album album);

        public Task<Album> UpdateAlbumAsync(Album album);

        public Task<(bool, string)> DeleteAlbumAsync(Album album);
        #endregion

    }
}

