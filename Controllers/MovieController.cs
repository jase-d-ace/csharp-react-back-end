using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using helloapi.Models;
using System.Threading.Tasks;
using System;

//https://www.brentozar.com/archive/2016/09/select-specific-columns-entity-framework-query/

namespace helloapi.Controllers {
  [Route("api/[controller]")]
  [ApiController]

  public class MovieController : ControllerBase {

    private readonly TodoContext _context;
    
    public class MovieDto {
      public string title { get; set; }
      public bool oscar { get; set; }
    }

    public class ActorDto {
      public int id { get; set; }
      public string name { get; set; }
      public string title { get; set; }
    }

    public MovieController(TodoContext context) {
      _context = context;
    }

    [HttpGet]
    public List<MovieDto> GetAll() {
      return _context.movies
        .Select(m => new MovieDto {title = m.title, oscar = m.oscar})
        .ToList();
    }

    [HttpGet("{id}", Name="GetMovieById")]
    public IQueryable GetById(int id) {
      var movie = _context.movies
        .Where(m => m.id == id)
        .Select(m => new { m.title, m.oscar });
      if (movie != null) {
        return movie;
      }
      throw new InvalidOperationException("something went wrong");
    }

    [HttpGet("{id}/actors", Name="GetActorsByMovie")]
    public List<ActorDto> GetActorsByMovie(int id) {
      var actorList = (from a in _context.actors
          join m in _context.movies
          on a.movie_id equals m.id
          where a.movie_id == id
          select new ActorDto { id = a.id, name = a.name, title = m.title })
          .ToList();
      if (actorList != null) {
        return actorList;
      }
      Response.StatusCode = (int) System.Net.HttpStatusCode.NotFound;
      return new List<ActorDto> { new ActorDto { id = 0, name = null, title = null } };
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> AddMovie(Movie movie) {
      _context.movies.Add(movie);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetById), new {id = movie.id}, movie);
    }

    [HttpPut("{id}", Name="EditMovie")]
    public async Task<ActionResult<Movie>> EditMovie(int id, [FromBody]Movie movie) {
      var item = _context.movies.Find(id);
      item.title = movie.title;
      item.oscar = movie.oscar;
      await _context.SaveChangesAsync();
      return NoContent();
    }

    [HttpDelete("{id}", Name="DeleteMovie")]
    public IActionResult DeleteMovie(int id) {
      var item = _context.movies.Find(id);
      if (item != null) {
        _context.movies.Remove(item);
        _context.SaveChanges();
        return Ok(item);
      }
      return NotFound();
    }
  }
}
