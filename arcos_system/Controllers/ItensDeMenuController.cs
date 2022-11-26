using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using arcos_system.Dados;
using arcos_system.Models;
using Microsoft.AspNetCore.Http;

namespace arcos_system.Controllers
{
    public class ItensDeMenuController : Controller
    {
        private readonly Context _context;

        public ItensDeMenuController(Context context)
        {
            _context = context;
            
        }
       

        // GET: ItemsDeMenu
        public async Task<IActionResult> Index()
        {



            //var user = _context.Usuarios.FirstOrDefault(u => u.NomeLogin.Equals(Nome) && u.Senha.Equals(Senha));
            //if (user != null)
            //{
            //    HttpContext.Session.SetString("idUsuarioLogado", user.Id);
            //}
            var idUsuario = Convert.ToString(HttpContext.Session.GetString("TipoUsuarioLogado"));
            if (idUsuario == "adm" )
            {
                return View(await _context.ItensDeMenu.ToListAsync());
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult Home()
        {
            List<ItensDeMenu> lista =
            _context.ItensDeMenu.
                OrderBy(x => x.Nome).
                ThenBy(x => x.Video).
                ToList();

            ViewBag.lista = lista;
            return View();
        }

        // GET: ItemsDeMenu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsDeMenu = await _context.ItensDeMenu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemsDeMenu == null)
            {
                return NotFound();
            }

            return View(itemsDeMenu);
        }

        // GET: ItemsDeMenu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemsDeMenu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Video")] ItensDeMenu itemsDeMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemsDeMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemsDeMenu);
        }

        // GET: ItemsDeMenu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsDeMenu = await _context.ItensDeMenu.FindAsync(id);
            if (itemsDeMenu == null)
            {
                return NotFound();
            }
            return View(itemsDeMenu);
        }

        // POST: ItemsDeMenu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Video")] ItensDeMenu itensDeMenu)
        {
            if (id != itensDeMenu.Id)
            {
                return NotFound();
            }

            var lista = _context.ItensDeMenu.ToList();
            ViewBag.lista = lista;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itensDeMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsDeMenuExists(itensDeMenu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itensDeMenu);
        }

        // GET: ItemsDeMenu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsDeMenu = await _context.ItensDeMenu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemsDeMenu == null)
            {
                return NotFound();
            }

            return View(itemsDeMenu);
        }

        // POST: ItemsDeMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemsDeMenu = await _context.ItensDeMenu.FindAsync(id);
            _context.ItensDeMenu.Remove(itemsDeMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsDeMenuExists(int id)
        {
            return _context.ItensDeMenu.Any(e => e.Id == id);
        }
    }
}
