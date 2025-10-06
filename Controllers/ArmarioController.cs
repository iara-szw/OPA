using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class ArmarioController : Controller
{
    private readonly ILogger<ArmarioController> _logger;

    public ArmarioController(ILogger<ArmarioController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
