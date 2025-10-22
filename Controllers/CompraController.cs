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
        List<int> prendas=Objetos.StringToList<int>(HttpContext.Session.GetString("carrito"));
        prendas.add(idPrenda);
        HttpContext.Session.SetString("carrito", Objeto.ListToString(prendas));
    }
    public IActionResult verCarrito(){
        List<int> prendas=Objetos.StringToList<int>(HttpContext.Session.GetString("carrito"));
        List<Prenda> Ropa=new List<Prenda>();
        foreach(int id in prendas){
            Ropa.add(BD.levantarPrenda(id));
        }
        ViewBag.ropa=Ropa;
        return view();
        //Ver si hay mas de uno?
    }
    public void eliminarPrenda(int IdPrenda){
        List<int> prendas=Objetos.StringToList<int>(HttpContext.Session.GetString("carrito"));
        prendas.Remove(IdPrenda);
       HttpContext.Session.SetString("carrito", Objeto.ListToString(prendas));
    }
    public void AgregarOtraPrenda(int IdPrenda){
        //Si hay mas de uno hay que cambiar esto pq no tiene sentido
        List<int> prendas=Objetos.StringToList<int>(HttpContext.Session.GetString("carrito"));
        prendas.add(IdPrenda);
       HttpContext.Session.SetString("carrito", Objeto.ListToString(prendas));
    }

}
