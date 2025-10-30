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
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if (usu != null)
        {
            ViewBag.tiendas=AdministradorBD.levantarAdministrador(usu.Usuario);
           if(ViewBag.tiendas==null){
                return RedirectToAction("nuevaTienda");
            }
      
        return View();

        } 
        return RedirectToAction("iniciarSesion","Comprador");
    }


    public IActionResult seleccionarTienda(int IdTienda){

              Tienda tiendaActual=TiendaBD.levantarTienda(IdTienda);
            HttpContext.Session.SetString("tienda", Objeto.ObjectToString(tiendaActual));
            return RedirectToAction("vistaTienda");
    }

    public IActionResult subidaProducto(){
              Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        if(tienda==null){
            return RedirectToAction("verTiendasAdministrador");
        }
            return View();
    }
    public IActionResult nuevaTienda(){
        return View();
    }
    public IActionResult agregarTienda(string Nombre,string Ubicacion,string Mail,string Telefono,string Descripcion,string FotoDePerfil,string Contacto){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            return RedirectToAction("iniciarSesion","Comprador");
        }
        int idNuevo=TiendaBD.crearTienda(Nombre,Ubicacion,Mail,Telefono,Descripcion,FotoDePerfil,Contacto,usu.Usuario);
        if (idNuevo == -1){
            return RedirectToAction("nuevaTienda");
        }
        Tienda tiendaActual=TiendaBD.levantarTienda(idNuevo);
        HttpContext.Session.SetString("tienda", Objeto.ObjectToString(tiendaActual));
        return View();
    }

    public IActionResult vistaTienda(){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        if(tienda == null){
            return RedirectToAction("verTiendasAdministrador");
        }
        ViewBag.permisos=AdministradorBD.verPermisos(tienda.IdTienda,usu.Usuario);
        ViewBag.tienda=tienda;
        ViewBag.productos=TiendaBD.levantarProductos(tienda.IdTienda);
        return View();

    }

}
