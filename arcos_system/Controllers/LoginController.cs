using arcos_system.Models;
using arcos_system.Dados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arcos_system.Controllers
{
    public class LoginController : Controller
    {
        private Context _context;

        public LoginController(Context context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logar(Usuario usuarioModel)
        {
            var LoginValidate = ModelState["NomeLogin"];
            var SenhaValidate = ModelState["Senha"];
            if ((LoginValidate != null && !LoginValidate.Errors.Any()) ||
                (SenhaValidate != null && !SenhaValidate.Errors.Any()))
            { //Validações estão OK
                string usuario = usuarioModel.NomeLogin;
                string senha = usuarioModel.Senha;
                string tipo = usuarioModel.Tipo;
                //Busca objeto no banco de dados

                var usuarioObj = _context.Usuarios.Where(linha =>
                        linha.NomeLogin.Equals(usuario) &&
                        linha.Senha.Equals(senha)).FirstOrDefault();

                if (usuarioObj != null)
                {//Usuario existente no banco de dados
                    TempData["usuarioLogado"] = usuarioModel.Nome;
                    HttpContext.Session.
                        SetString("IdUsuarioLogado", usuarioObj.Id.ToString());
                    HttpContext.Session.
                        SetString("NomeUsuarioLogado", usuarioObj.Nome.ToString());
                    HttpContext.Session.
                        SetString("TipoUsuarioLogado", usuarioObj.Tipo.ToString());
                         
                    if (usuarioObj.Tipo == "adm")
                    {
                        return RedirectToAction("Index", "ItensDeMenu");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   
                }
                else
                {
                    TempData["ErrorLogin"] = "Credenciais inválidas";
                    return View("Index");
                }
            }
            else { return View("Index"); }
        }
    }
}