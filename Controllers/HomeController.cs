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
        // Si está en modo vendedor, redirigir a la tienda seleccionada o a la selección de tienda
        var modo = HttpContext.Session.GetString("ModoUsuario");
        if (string.Equals(modo, "Vendedor", StringComparison.OrdinalIgnoreCase))
        {
            int? tiendaSeleccionada = HttpContext.Session.GetInt32("TiendaSeleccionada");
            if (tiendaSeleccionada.HasValue)
            {
                return RedirectToAction("vistaTienda", "Tienda");
            }
            else
            {
                return RedirectToAction("verTiendasAdministrador", "Tienda");
            }
        }

        string id = "";
        Comprador usu = Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if (usu == null){
            id = "default";
            return RedirectToAction ("LandingPage");
        }else{
            id = usu.Usuario;
            // Registrar visita a home
            LogBD.RegistrarAccion(usu.Usuario, "VisitaHome", null);
        }
        
        // Obtener recomendaciones personalizadas si hay usuario logueado
        List<Prenda> recomendaciones = new List<Prenda>();
        if (usu != null)
        {
            recomendaciones = RecomendadorBD.ObtenerRecomendaciones(usu.Usuario);
        }
        
        ViewBag.Ropa = BD.levantarRecomendados(id);
        ViewBag.tiendas = BD.levantarRecomendadosTienda();
        ViewBag.Recomendadas = recomendaciones;
        return View();
    }
    public IActionResult LandingPage()
    {
        return View();
    }
        public IActionResult vistaPrenda(int IdPrenda){
        Prenda prenda1=PrendaBD.LevantarPrenda(IdPrenda);
        ViewBag.prenda = prenda1;

        ViewBag.vendedor = TiendaBD.levantarTienda(prenda1.IdTienda).Nombre;
        ViewBag.estilitos=PrendaBD.LevantarPrendaxEstilo(IdPrenda);
        ViewBag.Similares = PrendaBD.LevantarSimilar(IdPrenda);
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        bool esPoseida=false;
        if(Usu!=null){ 
        ViewBag.Poseido=CompradorBD.verSiPoseido(IdPrenda, Usu.Usuario);
        ViewBag.Deseado=CompradorBD.verSiDeseado(IdPrenda, Usu.Usuario);
        ViewBag.recomendados=BD.levantarRecomendados(Usu.Usuario);
        
        // Registrar visita a prenda
        LogBD.RegistrarAccion(Usu.Usuario, "VisitaPrenda", new { IdPrenda = IdPrenda });
        
        List<int> prendas=Objeto.StringToList<int>(HttpContext.Session.GetString("carrito"));
            if (prendas != null)
            {
                ViewBag.estaEnCarrito = prendas.Contains(IdPrenda);
            }
            else
            {
                    ViewBag.estaEnCarrito = false;

}
        ViewBag.Usuario=true;

}else{
    ViewBag.Usuario=false;
    ViewBag.recomendados=BD.levantarRecomendados("default");
}
        return View();
    }
    public IActionResult Explorar(double? minPrecio, double? maxPrecio, int? colorId, int? estiloId, string q)
    {
        ViewBag.colores = BD.levantarColor();
        ViewBag.estilos = BD.levantarEstilos();
        var prendas = PrendaBD.FiltrarPrendas(minPrecio, maxPrecio, colorId, estiloId, q);
        ViewBag.prendas = prendas;
        ViewBag.filtro = new { minPrecio, maxPrecio, colorId, estiloId, q };
        return View();
    }
    public IActionResult verTiendas( string? q)
    {
        ViewBag.tiendas = TiendaBD.FiltrarTiendas(q);
       
        ViewBag.filtro = new { q };
        return View();
    }
}
