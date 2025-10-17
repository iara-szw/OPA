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

  public IActionResult verTiendasAdministrador(){
        Comprador usu=HttpContext.Session.GetString("usuario");
        if (usu != null)
        {
            ViewBag.tiendas=AdministradorBD.levantarAdministrador(usu.Usuario);
        
      
        return View();

        }
        return RedirectToAction("iniciarSesion","Comprador");
    }


    public IActionResult seleccionarTienda(int IdTienda){

              Tienda tiendaActual=TiendaBD.levantarTienda(IdTienda);
            HttpContext.Session.SetString("tienda", Objeto.ObjectToString(tiendaActual));
            return RedirectToAction("vistaTienda");
    }
    public IActionResult agregarTienda(string Nombre,string Ubicacion,string Mail,string Telefono,string Descripcion,string FotoDePerfil,string Contacto){
        TiendaBD.crearTienda(Nombre,Ubicacion,Mail,Telefono,Descripcion,FotoDePerfil,Contacto);
        //return si funciono
        return View();
    }

    public IActionResult vistaTienda(){
        Comprador usu=HttpContext.Session.GetString("usuario");
        Tienda tienda=HttpContext.Session.GetString("tienda");
        ViewBag.permisos=TiendaBD.verPermisos(tienda.IdTienda,usu.Usuario);
        ViewBag.tienda=tienda;
        ViewBag.productos=TiendaBD.levantarProductos(tienda.IdTienda);
        return View();

    }

}
