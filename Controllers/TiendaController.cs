using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class TiendaController : Controller
{
    private readonly ILogger<TiendaController> _logger;

    public TiendaController(ILogger<TiendaController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
