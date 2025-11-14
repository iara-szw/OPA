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
        string id="";
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu == null){
            id="default";
        }else{
        id=usu.Usuario;

        }
        ViewBag.Ropa=BD.levantarRecomendados(id); //Esto seria para la linea de recomendados
        ViewBag.tiendas=BD.levantarRecomendadosTienda();
        return View();
    }
        public IActionResult vistaPrenda(int IdPrenda){
        ViewBag.prenda=PrendaBD.LevantarPrenda(IdPrenda);
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(Usu!=null){ 
    ViewBag.Poseido=CompradorBD.verSiPoseido(IdPrenda, Usu.Usuario);
    ViewBag.Deseado=CompradorBD.verSiDeseado(IdPrenda, Usu.Usuario);
    List<int> prendas=Objeto.StringToList<int>(HttpContext.Session.GetString("carrito"));
    if(prendas!=null){
            ViewBag.estaEnCarrito=prendas.Contains(IdPrenda);

    }
        ViewBag.Usuario=true;

}else{
    ViewBag.Usuario=false;
}
        return View();
    }
    //- web con el catálogo de productos y carrito.
}
