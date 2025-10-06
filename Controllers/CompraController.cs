using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class CompraController : Controller
{
    private readonly ILogger<CompraController> _logger;

    public CompraController(ILogger<CompraController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
