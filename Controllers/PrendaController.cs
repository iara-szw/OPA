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
    public IActionResult agregarPrenda(string Tipo, string Modelo, int IdTalle,string descripcion,double Precio, List<int> estilos,int color, int Temporada, IFormFile foto){
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        string nombreArchivo=foto.FileName;

        if (foto != null && foto.Length>0){
string rutaCarpeta=Path.Combine(_env.WebRootPath,"img");
if(!Directory.Exists(rutaCarpeta)){
Directory.CreateDirectory(rutaCarpeta);
}
string rutaCompleta=Path.Combine(rutaCarpeta, nombreArchivo);

using (var stream = new FileStream (rutaCompleta, FileMode.Create)){
foto.CopyTo(stream);

}
}
        PrendaBD.agregarPrenda(tienda.IdTienda, Tipo, Modelo, IdTalle,descripcion, Precio,estilos,color,Temporada, nombreArchivo);
        return RedirectToAction("vistaTienda","Tienda");
    }

    public IActionResult LevantarPrenda(int IdPrenda){
        PrendaBD.LevantarPrenda(IdPrenda);
        return RedirectToAction("vistaPrenda","Home");
    }

    public IActionResult EliminarPrenda(int IdPrenda){
        PrendaBD.eliminarPrenda(IdPrenda);
        return RedirectToAction("vistaPrenda","Home");
    }
}