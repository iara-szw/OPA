using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class CompradorController : Controller
{
    private readonly ILogger<CompradorController> _logger;

    public CompradorController(ILogger<CompradorController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
