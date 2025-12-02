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
    public IActionResult UpdateStock(int idPrenda, int stock)
    {
        try
        {
            PrendaBD.UpdateStock(idPrenda, stock);
            return Json(new { ok = true });
        }
        catch (Exception ex)
        {
            return Json(new { ok = false, error = ex.Message });
        }
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
    
    [HttpPost]
    public IActionResult editarPrenda(int IdPrenda, int Tipo, string Modelo, string descripcion, double Precio, List<int> estilos, List<int> color, List<int> talles, int Temporada, string variantStock, IFormFile foto){
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        
        if(tienda == null){
            return RedirectToAction("verTiendasAdministrador","Tienda");
        }

        // Obtener la prenda original para mantener la foto si no se sube una nueva
        var prendaOriginal = PrendaBD.LevantarPrenda(IdPrenda);
        string nombreArchivo = prendaOriginal?.foto ?? "";

        if (foto != null && foto.Length > 0){
            nombreArchivo = foto.FileName;
            string rutaCarpeta = Path.Combine(_env.WebRootPath, "img");
            if(!Directory.Exists(rutaCarpeta)){
                Directory.CreateDirectory(rutaCarpeta);
            }
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
            using (var stream = new FileStream(rutaCompleta, FileMode.Create)){
                foto.CopyTo(stream);
            }
        }
        
        PrendaBD.editarPrenda(IdPrenda, Tipo, Modelo, descripcion, Precio, estilos, color, talles, Temporada, nombreArchivo, variantStock);
        
        return RedirectToAction("misProductos","Tienda");
    }
    
    [HttpGet]
    public IActionResult obtenerPrenda(int IdPrenda){
        try{
            var prenda = PrendaBD.LevantarPrenda(IdPrenda);
            if(prenda == null || prenda.IdPrenda == 0){
                return Json(new { ok = false, error = "Prenda no encontrada" });
            }
            
            var estilos = PrendaBD.LevantarPrendaxEstilo(IdPrenda);
            var temporadas = PrendaBD.LevantarTemporadas(IdPrenda);
            var variantes = PrendaBD.LevantarVariantes(IdPrenda);
            
            // Obtener todos los colores y talles del modelo
            Tienda tienda = Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
            if(tienda == null){
                return Json(new { ok = false, error = "Tienda no encontrada" });
            }
            
            var todasVariantes = PrendaBD.LevantarPrendasPorModelo(tienda.IdTienda, prenda.Modelo);
            
            var coloresUnicos = todasVariantes.Select(p => p.Color).Distinct().ToList();
            var tallesUnicos = todasVariantes.Select(p => p.IdTalle).Distinct().ToList();
            
            // Obtener nombres de talles
            var tallesConNombres = tallesUnicos.Select(tId => {
                var talle = PrendaBD.levantarTalle(tId);
                return new { id = tId, nombre = talle?.Nombre ?? tId.ToString() };
            }).ToList();
            
            // Construir mapa de stock
            var stockMap = new Dictionary<int, Dictionary<int, int>>();
            foreach(var variante in todasVariantes){
                if(!stockMap.ContainsKey(variante.Color)){
                    stockMap[variante.Color] = new Dictionary<int, int>();
                }
                stockMap[variante.Color][variante.IdTalle] = variante.stock;
            }
            
            return Json(new { 
                ok = true, 
                prenda = prenda,
                estilos = estilos.Select(e => e.IdEstilo).ToList(),
                temporada = temporadas.FirstOrDefault()?.idTemporada ?? 0,
                colores = coloresUnicos,
                talles = tallesConNombres,
                stockMap = stockMap
            });
        }
        catch (Exception ex){
            return Json(new { ok = false, error = ex.Message });
        }
    }

}