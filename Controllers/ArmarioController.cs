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

    public IActionResult armario(){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            ViewBag.estado="Tenes que iniciar sesión para ver tu armario";
            return RedirectToAction("iniciarSesion","Comprador");
        }
        
        // Registrar visita al armario
        LogBD.RegistrarAccion(usu.Usuario, "VisitaArmario", null);
        
        ViewBag.poseidos=ArmarioBD.levantarPoseidos(usu.Usuario);
        ViewBag.recomendados=BD.levantarRecomendados(usu.Usuario);
        ViewBag.deseados=ArmarioBD.levantarDeseados(usu.Usuario);
        ViewBag.estilos=CompradorBD.levantarEstilos(usu.Usuario);
        ViewBag.colores=CompradorBD.levantarColores(usu.Usuario);
        ViewBag.prendas=CompradorBD.levantarPrendas(usu.Usuario);
                ViewBag.TodoEstilos=BD.levantarEstilos();
                                ViewBag.TodoColores=BD.levantarColor();


        return View();
    }

    [HttpPost]
    public IActionResult eliminarPoseido(int idPrenda){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            return RedirectToAction("iniciarSesion","Comprador");
        }
        ArmarioBD.eliminarPoseido(usu.Usuario,idPrenda);
        return RedirectToAction("vistaPrenda","Home",new{IdPrenda=idPrenda, from="armario"});
    }

 
    public IActionResult guardarEstilos(List<int> estilos){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            return RedirectToAction("iniciarSesion","Comprador");
        }

        ArmarioBD.agregarEstilos(usu.Usuario,estilos);
        return RedirectToAction("Armario");
    }

    public IActionResult guardarColores(List<int> colores){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            return RedirectToAction("iniciarSesion","Comprador");
        }

        ArmarioBD.agregarColores(usu.Usuario,colores);
        return RedirectToAction("Armario");
    }
}
