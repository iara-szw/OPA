
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

    public void GuardarCarrito(int idPrenda)
    {
        List<int> prendas=Objeto.StringToList<int>(HttpContext.Session.GetString("carrito"));
        if(prendas.Count() == 0){
            List<int> ropa=new List<int>();
            ropa.Add(idPrenda);
            HttpContext.Session.SetString("carrito", Objeto.ListToString(ropa));
            return;
        }
        prendas.Add(idPrenda);
        HttpContext.Session.SetString("carrito", Objeto.ListToString(prendas));
        return;
    }
    public IActionResult verCarrito(){
        List<int> prendas=Objeto.StringToList<int>(HttpContext.Session.GetString("carrito"));
        List<Prenda> Ropa=new List<Prenda>();
        foreach(int id in prendas){
            Ropa.Add(BD.levantarPrenda(id));
        }
        ViewBag.ropa=Ropa;
        return View();
    }
    public void eliminarPrenda(int IdPrenda){
        List<int> prendas=Objeto.StringToList<int>(HttpContext.Session.GetString("carrito"));
        prendas.Remove(IdPrenda);
       HttpContext.Session.SetString("carrito", Objeto.ListToString(prendas));
       return;
    }
}
