using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using helloapi.Models;
using System.Threading.Tasks;
using System;

//https://debuxing.com/entity-framewok-join-tables-example-c
//https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx

namespace helloapi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ActorController : ControllerBase {
    private readonly TodoContext _context;

    public class ActorDto {
      public int id { get; set; }
      public string name { get; set; }
      public string title { get; set; }
    }

    public ActorController(TodoContext context) {
      _context = context;
    }

    [HttpGet("{id}", Name="GetActorJoin")]
    public List<ActorDto> GetActors(int id) {
      var actor = _context.actors.Find(id);
      if (actor == null) {
        Response.StatusCode = (int) System.Net.HttpStatusCode.NotFound;
        return new List<ActorDto> { new ActorDto { id = 0, name = null, title = null } };
      }
      var actorList = (from a in _context.actors
          join m in _context.movies
          on a.movie_id equals m.id
          where a.id == id
          select new ActorDto {id = a.id, name = a.name, title = m.title })
        .ToList();
      return actorList;
    }
  }
}
