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

    [HttpGet]
    public IActionResult TesteRepositorio()
    {
        _b3Repository.GetB3Data();
        return Ok();
    }
    
}