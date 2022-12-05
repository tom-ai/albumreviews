using System;
using AlbumReviews.Data;
using AlbumReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumReviews.Services
{
    public class MockRecordStore : IRecordStoreService
    {
        private readonly AppDbContext _db;

        public MockRecordStore(AppDbContext db)
        {
            _db = db;
        }

        #region Artists
        public async Task<List<Artist>> GetArtistsAsync()
        {
            try
            {
                return await _db.Artists.OrderBy(a => a.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
                // console.WriteLine(ex)
            }
        }

        public async Task<Artist> GetArtistByIdAsync(int id, bool includeAlbums)
        {
            try
            {
                // if include albums is true...
                if (includeAlbums) 
                {
                    // return artist and album using EntityFramework to match IDs
                    return await _db.Artists.Include(a => a.Albums).FirstOrDefaultAsync(a => a.Id == id);
                }
                else
                {
                    // or only return the artist
                    return await _db.Artists.FindAsync(id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Artist> AddArtistAsync(Artist artist)
        {
            try
            {
                await _db.Artists.AddAsync(artist);
                await _db.SaveChangesAsync();

                return await _db.Artists.FindAsync(artist.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Artist> UpdateArtistAsync(Artist artist)
        {
            try
            {
                /* 
                 * My first thoughts...
                 * Find the given artist in the db
                 * replace it with the given artist
                 * 
                 * var currentArtist = await _db.Artists.FirstOrDefaultAsync(a => a.Id == artist.Id);
                 * 
                 * currentArtist = artist;
                */


                // however this line of code is a better approach?!
                _db.Entry(artist).State = EntityState.Modified;

                // now it's in a modified state, update properties
                //artist.Name = "Tom";

                await _db.SaveChangesAsync();

                return artist;
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteArtistAsync(Artist artist)
        {
            try
            {
                var dbArtist = await _db.Artists.FindAsync(artist.Id);

                // check if artist exists
                if (artist == null)
                {
                    // if not, send error message
                    return (false, "Artist not found");
                }
                else
                {
                    // otherwise artist is found so delete
                    _db.Artists.Remove(dbArtist);
                    await _db.SaveChangesAsync();
                    return (true, "Artist has been deleted");
                }
            }
            catch (Exception ex)
            {
                // some other error
                return (false, $"Error occured. Error message: {ex.Message}");
            }
        }
        #endregion Artists

        #region Albums
        public async Task<List<Album>> GetAlbumsAsync()
        {
            try
            {
                return await _db.Albums.OrderBy(a => a.Name).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Album> GetAlbumById(int id, bool includeArtist)
        {
            try
            {
                if (includeArtist)
                {
                    return await _db.Albums.Include(a => a.Artist).FirstOrDefaultAsync(a => a.Id == id);
                }
                else
                {
                    return await _db.Albums.FindAsync(id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Album> AddAlbumAsync(Album album)
        {
            try
            {
                await _db.Albums.AddAsync(album);
                // save changes!
                await _db.SaveChangesAsync();
                return await _db.Albums.FindAsync(album.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Album> UpdateAlbumAsync(Album album)
        {
            try
            {
                _db.Entry(album).State = EntityState.Modified;

                // do some stuff...

                // save changes
                await _db.SaveChangesAsync();

                return album;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAlbumAsync(Album album)
        {
            try
            {
                var dbAlbum = await _db.Albums.FindAsync(album.Id);

                if (dbAlbum == null)
                {
                    return (false, "Album could not be found");
                }

                _db.Albums.Remove(dbAlbum);
                await _db.SaveChangesAsync();

                return (true, "Album has been deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Error occured. Error message: {ex.Message}");
            }
        }
        #endregion Albums        
    }
}

