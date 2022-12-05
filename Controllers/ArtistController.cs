using System;
using AlbumReviews.Models;
using AlbumReviews.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlbumReviews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IRecordStoreService _recordStoreService;

        public ArtistController(IRecordStoreService recordStoreService)
        {
            _recordStoreService = recordStoreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await _recordStoreService.GetArtistsAsync();

            if (artists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }
            return StatusCode(StatusCodes.Status200OK, artists);
        }

        // get the id parameter
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id, bool includeBooks = false)
        {
            // call our repo method passing in the id and assign it to a variable
            Artist artist = await _recordStoreService.GetArtistByIdAsync(id, includeBooks);

            if (artist == null)
            {
                // if null return 204
                return StatusCode(StatusCodes.Status204NoContent, $"No artist found for id: {id}");
            }

            // otherwise return 200 status and the artist variable
            return StatusCode(StatusCodes.Status200OK, artist);
        }

        [HttpPost]
        public async Task<IActionResult> AddArtist(Artist artist)
        {           
            // supply the artist object to repo method
            // set return value to a variable
            var dbArtist = await _recordStoreService.AddArtistAsync(artist);

            // if null return 204

            if (dbArtist == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error on server");
            }
            var actionName = nameof(GetArtistById); // creates a string name from the method
            var routeParams = new { id = artist.Id };

            return CreatedAtAction(actionName,routeParams, artist);
        }

        //[HttpPut] // WHICH AUTHOR TO UPDATE DUMMY! TAKES AN ID!
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, Artist artist)
        {
            // if the ID and supplied artist Id do not match
            if (id != artist.Id)
            {
                // return a bad request
                return StatusCode(StatusCodes.Status400BadRequest, "IDs do not match");
            }

            Artist dbArtist = await _recordStoreService.UpdateArtistAsync(artist);

            if (dbArtist == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{artist.Name} could not be updated");
            }

            //return StatusCode(StatusCodes.Status200OK, dbArtist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
             var dbArtist = await _recordStoreService.GetArtistByIdAsync(id, false);

            (bool status, string message) = await _recordStoreService.DeleteArtistAsync(dbArtist);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, dbArtist);
        }



    }
}

