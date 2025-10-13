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
        Comprador usu=HttpContext.Session.GetString("usuario");
        if(usu==null){
            ViewBag.estado="Tenes que iniciar sesión para ver tu armario";
            return View();
        }
        ViewBag.poseidos=ArmarioBD.levantarPoseidos(usu.IdComprador);
        ViewBag.recomendados=BD.levantarRecomendados(usu.IdComprador);
        ViewBag.deseados=ArmarioBD.levantarDeseados(usu.IdComprador);
        ViewBag.estilos=UsuarioBD.levantarEstilos(usu.IdComprador);
        //Hacer lo mismo con colores y tipos de prendas
        return View();
    }
}
