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
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult Libro(string isbn)
        {
            Libro libroNuevo = db.Libros.Find(isbn);      

            ViewBag.Titulo = libroNuevo.Titulo;
            ViewBag.Portada = libroNuevo.Portada;
            ViewBag.Descripcion = libroNuevo.DescripcionLibro;

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

        public JsonResult AgregarUsuarioALaSession(string mail, string clave)
        {

            Carrito carritoCliente = db.Carritos.Where(c => c.ClienteID == mail).First();
    
            Cliente nuevoCliente = new Cliente{
                Mail = mail,
                Clave = clave,
                Carrito = carritoCliente
            };

            HttpContext.Session.Set<Cliente>("ClienteLogueado", nuevoCliente);
            return Json(nuevoCliente);
        }

        public JsonResult ConsultarUsuarioEnSession()
        {
            Cliente cliente = HttpContext.Session.Get<Cliente>("ClienteLogueado");
            return Json(cliente);
        }

       // [HttpPost]
        public IActionResult Login(string email, string clave)
        {
           return View();
        }

        public IActionResult ConfirmarCompra()
        {
            Cliente cliente = HttpContext.Session.Get<Cliente>("ClienteLogueado");

            try
            {
                string mailBookshop = "clientebookshop@gmail.com";
                string pass = "nayaholic4256!";

                
                Carrito nuevoCarrito = db.Carritos.Include(c => c.Libros).Where(l => l.ClienteID == cliente.Mail).FirstOrDefault();
                var lista = nuevoCarrito.Libros.ToList();

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailBookshop);
                mailMessage.To.Add(cliente.Mail);
                mailMessage.Subject = "¡Gracias por tu compra!";
                string cadena1 ="Hola, confirmamos tu compra! El contenido de tu carrito es: ";
                
                foreach (var item in lista)
                {
                    cadena1.Concat(item.Titulo + "\n");
                }
                
                mailMessage.Body = cadena1;
                mailMessage.IsBodyHtml = false;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailBookshop,pass);

                SmtpServer.Send(mailMessage);

                db.Carritos.Find(cliente.Mail).Libros.Clear();
                db.SaveChanges();

            }catch(Exception e){
                Console.Out.Write(e);
            }
            
            ViewBag.Mensaje = $"CONFIRMAMOS TU COMPRA";
            //String productos ="";
            /*var lista2 = db.Carritos.Include(c => c.Libros).Where(l => l.ClienteID == cliente.Mail).FirstOrDefault().Libros.ToList();

            foreach (Libro item in lista2)
            {
                productos.Concat(item.Titulo);
            }

             ViewBag.Mensaje2 = $"Tu compra: {productos}";
*/
            return View();
        }        

        [HttpPost]
         public IActionResult EnviarMail(string nombreUsuario, string mailUsuario, string mensajeUsuario)
         {
            try
            {
                string mailBookshop = "clientebookshop@gmail.com";
                string pass = "nayaholic4256!";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailBookshop);
                mailMessage.To.Add(mailUsuario);
                mailMessage.Subject = "Recibimos tu consulta";
                mailMessage.Body = $"Hola {nombreUsuario}, recibimos tu consulta. Nos comunicaremos a la brevedad\nTu mensaje fue: {mensajeUsuario}";
                mailMessage.IsBodyHtml = false;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailBookshop,pass);

                SmtpServer.Send(mailMessage);

                

            }catch(Exception e){
                Console.Out.Write(e);
            }
            ViewBag.Mensaje = $"Gracias por dejar tu comentario {nombreUsuario}, te mandamos un mail a {mailUsuario}";
            return View();
         }

         [HttpPost]
         public IActionResult Suscripcion(string mailUsuario)
         {
            try
            {
                string mailBookshop = "clientebookshop@gmail.com";
                string pass = "nayaholic4256!";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailBookshop);
                mailMessage.To.Add(mailUsuario);
                mailMessage.Subject = "Te suscribiste a nuestro newsletter";
                mailMessage.Body = $"Hola, gracias por suscribirte a nuestro newsletter";
                mailMessage.IsBodyHtml = false;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailBookshop,pass);

                SmtpServer.Send(mailMessage);

                

            }catch(Exception e){
                Console.Out.Write(e);
            }
            ViewBag.Mensaje = $"Gracias por suscribirte a nuestro newsletter, te mandamos un mail a {mailUsuario}.";
            return View();
         }
        public IActionResult Shop()
        {
            Cliente clienteNuevo = HttpContext.Session.Get<Cliente>("ClienteLogueado");
            if(clienteNuevo != null)
            {
                Carrito nuevoCarrito = db.Carritos.Include(c => c.Libros).Where(l => l.ClienteID == clienteNuevo.Mail).FirstOrDefault();
                         
                Tienda objetoShop = new Tienda{
                    Lista_libros = db.Libros.ToList(),
                    Lista_generos = db.Generos.ToList(),
                    Lista_carrito = nuevoCarrito.Libros.ToList()
                };
                String id = nuevoCarrito.ClienteID;
                ViewBag.carritoID = id;
                return View(objetoShop);

            }


            return View("Login");

            
        }
        [HttpPost]
        public IActionResult Filtro(string titulo)
        {

            List<Libro> lista = new List<Libro>();

            List<Libro> librosdb = db.Libros.ToList();

            foreach (var item in librosdb)
            {
                if(titulo.Equals(item.Titulo))
                {
                    lista.Add(item);
                }
            }
            Tienda filtroTienda = new Tienda
            {
                Lista_libros = lista.ToList(),
                Lista_generos = db.Generos.ToList(),
                Lista_carrito = db.Libros.ToList()
            };
            
            return View("Shop",filtroTienda);
        }
       
        public IActionResult AgregarCarrito(string ISBN)
        {
            Libro nuevoLibro = db.Libros.Find(ISBN);
            //Carrito carritocliente = db.Carritos.Find(myEmail);
           
           
              Carrito carritoCliente = db.Carritos.Include(c => c.Libros).FirstOrDefault();
                 carritoCliente.Libros.Add(nuevoLibro);
                 var preciototal = 0.0;
                 preciototal += nuevoLibro.Precio;
                  db.SaveChanges();
                    Tienda nuevaTienda = new Tienda()
                    {
                        Lista_generos = db.Generos.ToList(),
                        Lista_libros = db.Libros.ToList(),
                        Lista_carrito = carritoCliente.Libros.ToList()
                    
                    };

                    

                    ViewBag.total = preciototal;


            return View("Shop", nuevaTienda);
        }
    
         [HttpPost]
        public IActionResult Search_result(string titulo)
        {
            List<Libro> librosdb = db.Libros.ToList();

            List<Libro> lista = db.Libros.Where(l => l.Titulo == titulo).ToList();

            if(lista.Count == 0)
            {
                ViewBag.Mensaje = "No se encuentra disponible";
            }
            else
            {
                ViewBag.Mensaje = "El libro esta disponible";
            }

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
       
         public IActionResult Register(string mail, string clave)
         {
             return View();
         }

         [HttpPost]
         public IActionResult CrearCliente(string mail, string clave)
         {
            
             List<Libro> listaCarrito = new List<Libro>();

            Carrito nuevoCarrito = new Carrito()
            {
                ClienteID = mail,
                Libros = listaCarrito
            };

            Cliente nuevoCliente = new Cliente()
             {
                 Mail = mail,
                 Clave = clave,
                 Carrito = nuevoCarrito
             };

             db.Clientes.Add(nuevoCliente);
             db.SaveChanges();

             return View("CrearCliente");
         }

        [HttpPost]
         public IActionResult IniciarSesion(string mail, string clave)
         {
             Cliente clienteCheck = db.Clientes.FirstOrDefault(c => c.Mail == mail);
             if(clienteCheck != null)
             {
                 if(clienteCheck.Clave == clave)
                 {
                     AgregarUsuarioALaSession(mail, clave);

                     Carrito carritoCliente = db.Carritos.Include(c => c.Libros).FirstOrDefault();
                
                    Tienda nuevaTienda = new Tienda()
                    {
                        Lista_generos = db.Generos.ToList(),
                        Lista_libros = db.Libros.ToList(),
                        Lista_carrito = carritoCliente.Libros.ToList()
                    
                    };
                     return View("Shop", nuevaTienda);
                 }
                 else
                 {
                     ViewBag.badLogin = true;
                     return View("Login");
                 }
                
             }
             else
             {
                 ViewBag.badLogin = false;
                 return View("Login");
             }
            }
        
        public IActionResult SacarUsuarioEnSesion()
        {
            HttpContext.Session.Remove("ClienteLogueado");
            return View("Login");
        }
             


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
