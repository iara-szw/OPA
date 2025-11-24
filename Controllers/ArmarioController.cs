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
        ViewBag.poseidos=ArmarioBD.levantarPoseidos(usu.Usuario);
        ViewBag.recomendados=BD.levantarRecomendados(usu.Usuario);
        ViewBag.deseados=ArmarioBD.levantarDeseados(usu.Usuario);
        ViewBag.estilos=CompradorBD.levantarEstilos(usu.Usuario);
        ViewBag.colores=CompradorBD.levantarColores(usu.Usuario);
        ViewBag.prendas=CompradorBD.levantarPrendas(usu.Usuario);
        return View();
    }


}
