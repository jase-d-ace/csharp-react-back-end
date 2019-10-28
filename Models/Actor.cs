namespace helloapi.Models {
  public class Actor {
    public int id { get; set; }
    public string name { get; set; }
    public int movie_id { get; set; }
    public Movie Movie { get; set; }
  }
}
