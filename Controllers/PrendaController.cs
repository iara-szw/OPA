using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OPA.Models;

namespace OPA.Controllers;

public class PrendaController : Controller
{


    public IActionResult agregarPrenda(int IdTienda, string Tipo,int IdPrenda, string Modelo, int IdTalle,string descripcion,double Precio, List<Estilo> estilos, int Temporada){
        PrendaBD.agregarPrenda(IdTienda, Tipo,IdPrenda, Modelo, IdTalle,descripcion, Precio,estilos,Temporada);
        return RedirectToAction("vistaTienda","Tienda");
    }

    public IActionResult LevantarPrenda(int IdPrenda){
        PrendaBD.agregarPrenda(IdPrenda);
        return RedirectToAction("vistaPrenda","Home");
    }
}