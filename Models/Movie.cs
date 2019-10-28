using System.Collections.Generic;

namespace helloapi.Models {
  public class Movie {
    public int id { get; set; }
    public string title { get; set; }
    public bool oscar { get; set; }
    public ICollection<Actor> actors { get; set; }
  }
}
