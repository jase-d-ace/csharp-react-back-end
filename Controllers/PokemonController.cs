using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace helloapi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class PokemonController : ControllerBase {
    string baseUrl = "https://pokeapi.co/api/v2/";
    private readonly RestClient _client;

    public PokemonController(RestClient client) {
      _client = new RestClient(baseUrl);
    }

    [HttpGet("{id}", Name="PokemonById")]
    public string GetPokemon(int id) {
      RestRequest req = new RestRequest("pokemon/{id}");
      req.AddParameter("id", id, ParameterType.UrlSegment);
      IRestResponse res = _client.Execute(req);
      return res.Content;
    }
  }
}
