using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModellingAmbient.Core.Interfaces;
using ModellingAmbient.Infra.Interfaces;

namespace ModellingAmbient.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class TestesController : ControllerBase
{
    private readonly IB3Repository _b3Repository;
    private readonly IPythonEngineService _pythonEngineService;

    public TestesController(
        IB3Repository b3Repository,
        IPythonEngineService pythonEngineService)
    {
        _b3Repository = b3Repository;
        _pythonEngineService = pythonEngineService;
    }

    [HttpGet("{ticker_name}")]
    public IActionResult TesteRepositorio(string ticker_name)
    {
        _b3Repository.CreatePlot(ticker_name);
        return Ok();
    }

    [HttpGet("TesteDllPath")]
    public IActionResult TesteDllPath()
    {
        var path = _pythonEngineService.GetDllPath();
        return Ok();
    }
    
}