using arcos_system.Models;
using arcos_system.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace arcos_system.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(Context context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public  IActionResult EnviarComentario(string comentario , int idItemMenu)
        {
            var idUsuario = HttpContext.Session.GetString("IdUsuarioLogado");
            Usuario usuario =  _context.Usuarios.Find(Convert.ToInt32(idUsuario));
            ItensDeMenu itemDeMenu = _context.ItensDeMenu.Find(idItemMenu);
            Comentario objComentario = new Comentario();

            objComentario.Usuario = usuario;
            objComentario.Comentarios = comentario;
            objComentario.ItensDeMenu = itemDeMenu;

            _context.Comentarios.Add(objComentario);
            _context.SaveChanges();

            ViewBag.lista = GetItemsDeMenu();
            return View("Index");
        }

        public IActionResult Index()
        {
            var idUsuario = HttpContext.Session.GetString("IdUsuarioLogado");
            if (idUsuario != null)
            {
                ViewBag.lista = GetItemsDeMenu();
                ViewBag.msg = "Comentário enviado com sucesso!";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private List<ItensDeMenu> GetItemsDeMenu() {

            List<ItensDeMenu> lista =
               _context.ItensDeMenu.
                   OrderBy(x => x.Nome).
                   ThenBy(x => x.Video).
                   ToList();

            return lista;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
