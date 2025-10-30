using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class PrendaController : Controller
{


    public IActionResult agregarPrenda(string Tipo,int IdPrenda, string Modelo, int IdTalle,string descripcion,double Precio, List<Estilo> estilos, int Temporada){
        Tienda tienda=Objeto.StringToobject<Tienda>(HttpContext.Session.GetString("tienda"));
        if(tienda == null){
            return RedirectToAction("verTiendasAdministrador");
        }
        PrendaBD.agregarPrenda(tienda.IdTienda, Tipo,IdPrenda, Modelo, IdTalle,descripcion, Precio,estilos,Temporada);
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