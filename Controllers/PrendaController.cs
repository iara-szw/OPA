using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class PrendaController : Controller
{
private readonly IWebHostEnvironment _env;

public PrendaController (IWebHostEnvironment env)
{
_env = env;
}
[HttpPost]
<<<<<<< HEAD
    public IActionResult agregarPrenda(string Tipo, string Modelo, int IdTalle,string descripcion,double Precio, List<int> estilos,List<int> color, int Temporada, IFormFile foto){
=======
    public IActionResult agregarPrenda(string Tipo, string Modelo, int IdTalle,string descripcion,double Precio, List<int> estilos,int color, int Temporada, IFormFile foto){
>>>>>>> e74b5af953a42157a6a9be6fbbc2a0395bae5d2f
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));

        if (foto != null && foto.Length>0){
                    string nombreArchivo=foto.FileName;

string rutaCarpeta=Path.Combine(_env.WebRootPath,"img");
if(!Directory.Exists(rutaCarpeta)){
Directory.CreateDirectory(rutaCarpeta);
}
string rutaCompleta=Path.Combine(rutaCarpeta, nombreArchivo);

using (var stream = new FileStream (rutaCompleta, FileMode.Create)){
foto.CopyTo(stream);

}
        PrendaBD.agregarPrenda(tienda.IdTienda, Tipo, Modelo, IdTalle,descripcion, Precio,estilos,color,Temporada, nombreArchivo);

}
        return RedirectToAction("vistaTienda","Tienda");
    }

    public IActionResult EliminarPrenda(int IdPrenda){
        PrendaBD.eliminarPrenda(IdPrenda);
        return RedirectToAction("index","Home");
    }
}