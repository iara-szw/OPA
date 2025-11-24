using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using OPA.Models;

namespace OPA.Controllers;

public class CompradorController : Controller
{
    private readonly ILogger<CompradorController> _logger;

    public CompradorController(ILogger<CompradorController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult iniciarSesion(string estado){
            ViewBag.estado=estado;

        if (HttpContext.Session.GetString("usuario") != null)
        {
            return RedirectToAction("vistaUsuario");
            }

        return View();
    }

    public IActionResult comprobarDatos(string nombreUsuario, string password){
       Comprador usu = CompradorBD.levantarComprador(nombreUsuario,encriptar.HashearPassword(password));
            if(usu != null){
                HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usu));
                return RedirectToAction("vistaUsuario");
            }else{
                return RedirectToAction("iniciarSesion",new{estado="error"});
            }
    }
        public IActionResult cerrarSesion(){
        HttpContext.Session.Remove("usuario");
        return RedirectToAction("iniciarSesion");
    }
    public IActionResult registrarse(string estado){
        ViewBag.estado=estado;
        return View();
    }

public IActionResult medidas(){
            if(HttpContext.Session.GetString("usuario") == null){
                return RedirectToAction("iniciarSesion");
            }
        return View();
    }
      public IActionResult registrarNuevo(string nombreUsuario,string password, string nombre,string apellido,string telefono,string Mail, int Genero, bool esVendedor){
        if(string.IsNullOrWhiteSpace(telefono)){
            return RedirectToAction("registrarse", new { estado = "telefonoRequerido" });
        }
        if(CompradorBD.yaExiste(nombreUsuario)){
            return RedirectToAction("registrarse",new{estado="errorUsuario"});
        }else{
            Comprador usu =new Comprador();
            string passwordHasheada = encriptar.HashearPassword(password);
            usu.crearComprador(nombreUsuario, passwordHasheada, nombre, apellido, telefono, Mail, Genero,esVendedor);
            CompradorBD.agregarComprador(usu);
            return RedirectToAction("registrarse",new{estado="funciono"});
        }
    }

[HttpGet]
public JsonResult validarUsuario(string username)
{
    bool existe = CompradorBD.yaExiste(username);
    return Json(new { existe = existe });
}

public IActionResult editarUsuario(){
            ViewBag.Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
            if(ViewBag.Usu==null){
                return RedirectToAction("iniciarSesion()");
            }
    return View();
}
    public IActionResult vistaUsuario(){
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
         if (Usu == null)
        {
            return RedirectToAction("iniciarSesion");
        }

        ViewBag.usu=Usu;
        return View();
    }

  public IActionResult cargarMedidas(double MedidaTorso, double MedidaCintura, double MedidaPierna, double MedidaHombros, double MedidaBrazos, double MedidaCadera){
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        CompradorBD.cargarMedidas(Usu.Usuario,MedidaTorso,MedidaCintura,MedidaPierna,MedidaHombros,MedidaBrazos,MedidaCadera);
        return View();
        //Arreglar en BD todas las medidas posibles
    }
    [HttpPost]

    public IActionResult editarPerfil([FromBody] PayloadModel payload){
        try
    {
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
                string passwordHasheada=Usu.Contraseña;



if (Usu==null)
{

    return RedirectToAction("iniciarSesion");
    
}
    if(payload.contrasenia!=Usu.Contraseña){
         passwordHasheada = encriptar.HashearPassword(payload.contrasenia);
    }


        CompradorBD.editarComprador(Usu.Usuario,payload.nombre,payload.apellido,payload.mail,payload.telefono,passwordHasheada);
                        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(CompradorBD.levantarComprador(Usu.Usuario,passwordHasheada)));
                return Json(new { ok = true });
 }
    catch (Exception ex)
    {
        // Si ocurre un error, devolver un JSON con el mensaje de error
        return Json(new { ok = false, error = ex.Message });
    }   
    }

    public IActionResult agregarDeseado(int idPrenda){
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        CompradorBD.agregarDeseado(idPrenda, Usu.Usuario);

        return RedirectToAction("vistaPrenda","Home",new{idPrenda=idPrenda});
    }
 public IActionResult quitarDeseado(int idPrenda){
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        CompradorBD.agregarDeseado(idPrenda, Usu.Usuario);

        return RedirectToAction("vistaPrenda","Home",new{idPrenda=idPrenda});
    }
    public IActionResult comprarPrenda(){
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        List<int> prendas=Objeto.StringToList<int>(HttpContext.Session.GetString("carrito"));
        foreach(int Prenda in prendas){
            CompradorBD.comprarPrenda(Prenda, Usu.Usuario);
        }
        return RedirectToAction("limpiarCarrito","Compra");

    }
}
