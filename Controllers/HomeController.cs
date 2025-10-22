using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<int> carrito=new Carrito();
        HttpContext.Session.SetString("carrito", Objeto.ListToString(carrito));
        HttpContext.Session.SetString("carrito", Objeto.ObjectToString(carrito));
        ViewBag.Ropa=BD.levantarRopa(); //Esto seria para la linea de recomendados
        ViewBag.tiendas=BD.levantarTiendas();
        return View();
    }
    
    //- web con el catálogo de productos y carrito.
}
