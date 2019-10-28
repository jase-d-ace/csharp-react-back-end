using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using helloapi.Models;
using System.Threading.Tasks;
using System;

//https://medium.com/@agavatar/webapi-with-net-core-and-postgres-in-visual-studio-code-8b3587d12823
//https://dotnetthoughts.net/using-postgresql-with-aspnet-core/
//https://code-maze.com/efcore-modifying-data/
//adding -v to any dotnet ef command allows you to see more output. allows for better debugging
//didn't realize i had accidentally copy/pasted incorrectly. didn't see it until i ran
//dotnet ef migrations add name_migration_here -v

namespace helloapi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TodoController : ControllerBase {

    //load database context into the class
    private readonly TodoContext _context;

    //expose controller and write methods
    public TodoController(TodoContext context) {
      
      //expose database context to controller
      _context = context;
    }

    //GET /api/todo
    [HttpGet]
    public ActionResult<List<Todo>> GetAll() {
      return _context.todos.ToList();
    }

    //GET /api/todo/{id}
    [HttpGet("{id}", Name="GetTodo")]
    public ActionResult<Todo> GetById(long id) {
      var item = _context.todos.Find(id);
      if (item == null) {
        return NotFound();
      }
      return item;
    }

    //POST /api/todo
    [HttpPost]
    public async Task<ActionResult<Todo>> Post(Todo todo) {
       _context.todos.Add(todo);
       await _context.SaveChangesAsync();
       return CreatedAtAction(nameof(GetById), new { id = todo.id }, todo);
    }

    //PUT /api/todo/{id}
    [HttpPut("{id}", Name="EditTodo")]
    public async Task<ActionResult<Todo>> Put(long id, [FromBody]Todo todo) {
      var item = _context.todos.Find(id);
      item.text = todo.text;
      item.complete = todo.complete;
      await _context.SaveChangesAsync();
      return NoContent();
    }

    //DELETE /api/todo/{id}
    [HttpDelete("{id}", Name="DeleteTodo")]
    public IActionResult Delete(long id) {
      //IActionResult allows for flexibility in return value
      var item = _context.todos.Find(id);
      if (item != null) {
        _context.todos.Remove(item);
        _context.SaveChanges();
        return Ok(item);
      }
      return NotFound();
    }
  }
}
