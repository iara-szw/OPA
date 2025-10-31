using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult editarPerfil(string Nombre,string Apellido,string Telefono,string FotoDePerfil,string Mail,int Genero){
        Comprador Usu=Objeto.StringToobject<Comprador>(HttpContext.Session.GetString("usuario"));
        CompradorBD.editarComprador(Usu.Usuario,Nombre,Apellido,Telefono,FotoDePerfil,Mail,Genero);
        return RedirectToAction("vistaUsuario");
    }
}
