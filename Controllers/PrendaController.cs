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
    public IActionResult agregarPrenda(int Tipo, string Modelo, string descripcion, double Precio, List<int> estilos, List<int> color, List<int> talles, int Temporada, string variantStock, IFormFile foto){
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
        PrendaBD.agregarPrenda(tienda.IdTienda, Tipo, Modelo, descripcion, Precio, estilos, color, talles, Temporada, nombreArchivo, variantStock);

}
        return RedirectToAction("vistaTienda","Tienda");
    }

}