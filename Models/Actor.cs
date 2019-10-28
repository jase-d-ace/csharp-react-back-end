namespace helloapi.Models {
  public class Actor {
    public int id { get; set; }
    public string name { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
  }
}
