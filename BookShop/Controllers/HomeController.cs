using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookShop.Models;
using System.Net.Mail;
using System.Net;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        public string myEmail = "clientebookshop@gmail.com";
        public string myPassword = "nayaholic4256!";
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

        [HttpPost]
         public ActionResult EnviarMail(string nombreUsuario, string mailUsuario, string mensajeUsuario)
         {
            try
            {
                string mailBookshop = "contactobookshop@gmail.com";
                string pass = "nayaholic4256!";

                MailMessage mailMessage = new MailMessage(mailBookshop, mailUsuario, "hiciste una consulta","cuerpo del mensaje");
                
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailBookshop,pass);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mailMessage);

                

            }catch(Exception e){
                Console.Out.Write(e);
            }
            ViewBag.Mensaje = $"Gracias por dejar tu comentario {nombreUsuario}, te mandamos un mail a {mailUsuario}";
            return View();
         }

        public IActionResult Shop()
        {
           
            Tienda objetoShop = new Tienda{
                Lista_libros = db.Libros.ToList(),
                Lista_generos = db.Generos.ToList()
                };

               
            return View(objetoShop);

            
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
