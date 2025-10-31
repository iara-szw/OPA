using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class PrendaController : Controller
{


    public IActionResult agregarPrenda(string Tipo, string Modelo, int IdTalle,string descripcion,double Precio, List<int> estilos,int color, int Temporada, string foto){
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        PrendaBD.agregarPrenda(tienda.IdTienda, Tipo, Modelo, IdTalle,descripcion, Precio,estilos,color,Temporada, foto);
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