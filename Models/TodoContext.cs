using Microsoft.EntityFrameworkCore;
namespace helloapi.Models {
  public class TodoContext : DbContext {
    public TodoContext(DbContextOptions<TodoContext> options) : base(options){

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Actor>()
        .HasOne<Movie>(a => a.Movie)
        .WithMany(m => m.actors)
        .HasForeignKey(a => a.MovieId);
    }
    public DbSet<Todo> TodoItems { get; set; }
    public DbSet<Movie> movies { get; set; }
    public DbSet<Actor> actors { get; set; }
  }
}
