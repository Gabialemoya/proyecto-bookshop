using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly LibrosContext db;

        public HomeController(ILogger<HomeController> logger, LibrosContext contexto)
        {
            this.logger = logger;
            this.db = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View(db.Libros.ToList());
        }
    

        public IActionResult Search_result()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public JsonResult ConsultarAutores()
        {
            return Json(db.Autores.ToList());
        }

        public JsonResult CrearAutor(string nombreCompleto)
        {
            Autor nuevoAutor = new Autor{
                nombreCompleto = nombreCompleto
            };

            db.Autores.Add(nuevoAutor);
            db.SaveChanges();
            return Json(nuevoAutor);
        }

         public JsonResult CrearGenero(string descripcionGenero)
        {
            Genero nuevoGenero = new Genero{
                DescripcionGenero = descripcionGenero
            };

            db.Generos.Add(nuevoGenero);
            db.SaveChanges();
            return Json(nuevoGenero);
        }

         public JsonResult CrearLibro(string isbn, string titulo, string descripcionLibro, string portada, int creadorID, int clasificacionID, float precio, int stock)
         {
            Autor nuevoAutor = db.Autores.Find(creadorID);
            Genero nuevoGenero = db.Generos.Find(clasificacionID);
            Libro nuevoLibro = new Libro{
                ISBN = isbn,
                Titulo = titulo,
                DescripcionLibro = descripcionLibro,
                Portada = portada,
                Creador = nuevoAutor ,
                Clasificacion = nuevoGenero,
                Precio = precio,
                Stock = stock
            };

            db.Libros.Add(nuevoLibro);
            db.SaveChanges();
            return Json(nuevoLibro);
         }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
