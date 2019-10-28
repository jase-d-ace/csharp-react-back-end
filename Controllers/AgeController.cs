using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using RestSharp;

namespace helloapi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class AgeController : ControllerBase {

    string baseUrl = "https://api.agify.io/";
    private readonly RestClient _client;


    public AgeController(RestClient client) {
      _client = new RestClient(baseUrl);
    }

    //GET /api/age
    [HttpGet]
    public string GetAll() {
      return "Found route";
    }
    
    //GET /api/age/{name}
    [HttpGet("{name}", Name="GetName")]
    public string Get(string name) {
      //when downloading external libraries from nuget without using visual studio, remember to configure them in Startup.cs
      //whatever classes/services you need from them, instantiate in Startup.cs using
      //services.AddScoped<YourInstanceHere>
      //otherwise, you end up with an error: "Unable to resolve service for type YourInstanceHere when activating YourController"
      RestRequest req = new RestRequest(Method.GET);
      req.AddQueryParameter("name", name);
      IRestResponse res = _client.Execute(req);
      return res.Content;
    }

  }
}
