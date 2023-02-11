using Microsoft.AspNetCore.Mvc;
using mssqlconnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using mssqlconnect.Repository;

namespace mssqlconnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                return Ok(await _movieRepository.GetMovie());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> SaveMovies([FromBody] Movie body)
        {
            try
            {
                if (body == null) return BadRequest();
                var result = await _movieRepository.AddMovie(body);
                return CreatedAtAction(nameof(GetMovieById), new { id = result.Id }, result);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Movie>> UpdataMovies(int id, [FromBody] Movie body)
        {
            try {
                
                if (body == null) return BadRequest();

                if (id != body.Id)
                    return BadRequest("Movia ID mismatch");

                var movie = await _movieRepository.GetMovieById(id);
                if (movie == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }

                return await _movieRepository.UpdateMovie(body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            try
            {
                return Ok(await _movieRepository.GetMovieById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                _movieRepository.DeleteMovie(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
