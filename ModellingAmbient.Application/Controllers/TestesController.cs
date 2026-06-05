using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModellingAmbient.Core.Interfaces;

namespace ModellingAmbient.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class TestesController : ControllerBase
{
    private readonly IB3Repository _b3Repository;

    public TestesController(IB3Repository b3Repository)
    {
        _b3Repository = b3Repository;
    }

    [HttpGet("{ticker_name}")]
    public IActionResult TesteRepositorio(string ticker_name)
    {
        _b3Repository.CreatePlot(ticker_name);
        return Ok();
    }
    
}