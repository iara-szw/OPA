using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class TiendaController : Controller
{
    private readonly ILogger<TiendaController> _logger;
private readonly IWebHostEnvironment _env;

    public TiendaController(ILogger<TiendaController> logger,IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
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
        Tienda tiendaActual = TiendaBD.levantarTienda(IdTienda);
        if (tiendaActual == null)
        {
            return RedirectToAction("verTiendasAdministrador");
        }

        HttpContext.Session.SetString("tienda", Objeto.ObjectToString(tiendaActual));
        // Modo vendedor persistente y tienda seleccionada
        HttpContext.Session.SetString("ModoUsuario", "Vendedor");
        HttpContext.Session.SetInt32("TiendaSeleccionada", IdTienda);

        return RedirectToAction("vistaTienda");
    }

    public IActionResult subidaProducto(){
              Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));

        if(tienda==null){
            return RedirectToAction("verTiendasAdministrador");
        }
        ViewBag.estilos=BD.levantarEstilos();
        ViewBag.colores=BD.levantarColor();
        ViewBag.tipos=BD.levantarTipos();
        ViewBag.talles=BD.levantarTalles();
        ViewBag.Temporada=BD.levantarTemporada();
        
            return View();
    }
    public IActionResult editarProducto(int idPrenda){
              Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));

        if(tienda==null){
            return RedirectToAction("verTiendasAdministrador");
        }
        ViewBag.estilos=PrendaBD.levantarColor(idPrenda);
        ViewBag.colores=PrendaBD.levantarTalle(idPrenda);
        ViewBag.tipos=PrendaBD.LevantarPrendaxEstilo(idPrenda);
        ViewBag.talles=PrendaBD.LevantarPrendaxEstilo(idPrenda);
        ViewBag.Temporada=BD.levantarTemporada();
        
            return View();
    }
    public IActionResult nuevaTienda(){   
             Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            return RedirectToAction("iniciarSesion","Comprador");
        }
        return View();
    }
    //  public IActionResult aniadirAdministrador(int idTienda, string idUsuario){   
       // Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
       // if(usu==null){
        //    return RedirectToAction("iniciarSesion","Comprador");
       // }
       // TiendaBD.aniadirAdministrador(idTienda, idUsuario,usu.Usuario);

      //  return RedirectToAction("vistaTienda");
   // }
[HttpPost]


    public IActionResult agregarTienda(string Nombre,string Ubicacion,string Mail,string Telefono,string Descripcion,IFormFile  FotoDePerfil,string Contacto){
Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
string nombreArchivo=FotoDePerfil.FileName;

if (FotoDePerfil != null && FotoDePerfil.Length>0){
string rutaCarpeta=Path.Combine(_env.WebRootPath,"img");
if(!Directory.Exists(rutaCarpeta)){
Directory.CreateDirectory(rutaCarpeta);
}
string rutaCompleta=Path.Combine(rutaCarpeta, nombreArchivo);

using (var stream = new FileStream (rutaCompleta, FileMode.Create)){
FotoDePerfil.CopyTo(stream);

}
}
         int idNuevo=TiendaBD.crearTienda(Nombre,Ubicacion,Mail,Telefono,Descripcion,nombreArchivo,Contacto,usu.Usuario);
        if (idNuevo == -1){
            return RedirectToAction("nuevaTienda");
        }
        Tienda tiendaActual = TiendaBD.levantarTienda(idNuevo);
        HttpContext.Session.SetString("tienda", Objeto.ObjectToString(tiendaActual));
        // Nuevo modo vendedor y tienda seleccionada
        HttpContext.Session.SetString("ModoUsuario", "Vendedor");
        HttpContext.Session.SetInt32("TiendaSeleccionada", idNuevo);

        return RedirectToAction("vistaTienda");
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
    public IActionResult misProductos(){
        Comprador usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        if(usu==null){
            return RedirectToAction("iniciarSesion","Comprador");
        }
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        if(tienda == null){
            return RedirectToAction("verTiendasAdministrador");
        }
        ViewBag.tienda=tienda;
        ViewBag.productos=TiendaBD.levantarProductos(tienda.IdTienda) ?? new List<Prenda>();
        return View();
    }
 
        public IActionResult EliminarPrenda(int IdPrenda){
        PrendaBD.eliminarPrenda(IdPrenda);
        return RedirectToAction("misProductos");
    }

}
