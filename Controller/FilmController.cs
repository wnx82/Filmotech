using Filmotech.Entities; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Filmotech.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly Database dbSession;

        public FilmsController(Database db)
        {
            this.dbSession = db;
        }

        // Get all films
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                var films = this.dbSession.Films.ToList();
                return StatusCode(StatusCodes.Status200OK, films);
            }
            catch (SqlNullValueException ex)
            {
                // Create an object containing error information
                var errorResponse = new
                {
                    message = "An error occurred while retrieving data.",
                    exception = ex.ToString() // This includes the message and stack trace
                };

                // Return the object as a JSON response with a 500 status code
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        // Add a film
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Add([FromBody] Film film)
        {
            if (film != null)
            {
                this.dbSession.Films.Add(film);
                this.dbSession.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, null);
            }
            else
            {
                // The film is null, you can return an appropriate error code
                return StatusCode(StatusCodes.Status400BadRequest, "Film is null.");
            }
        }

        // Update a film
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Update(Film updatedFilm)
        {
            var film = dbSession.Films.Find(updatedFilm.Id);

            if (film == null)
            {
                return NotFound("Film not found.");
            }

            // Update film properties
            film.Titre = updatedFilm.Titre;
            film.Qualité = updatedFilm.Qualité;
            film.CreatedAt = updatedFilm.CreatedAt;
            film.UpdatedAt = updatedFilm.UpdatedAt;
            film.DeletedAt = updatedFilm.DeletedAt;

            this.dbSession.Films.Update(film);
            this.dbSession.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, null);
        }

        // Delete a film
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Delete(Film film)
        {
            try
            {
                var filmToDelete = this.dbSession.Films.Find(film.Id);

                if (filmToDelete == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Film not found.");
                }

                this.dbSession.Films.Remove(filmToDelete);
                this.dbSession.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to delete this film; it is associated with one or more orders.");
            }
        }
    }
}
