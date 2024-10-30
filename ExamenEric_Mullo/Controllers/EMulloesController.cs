using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenEric_Mullo.Data;
using ExamenEric_Mullo.Models;

namespace ExamenEric_Mullo.Controllers
{
    public class EMulloesController : Controller
    {
        private readonly ExamenEric_MulloContext _context;

        public EMulloesController(ExamenEric_MulloContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var examenEric_MulloContext = _context.EMullo.Include(e => e.Celular);
            return View(await examenEric_MulloContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eMullo = await _context.EMullo
                .Include(e => e.Celular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eMullo == null)
            {
                return NotFound();
            }

            return View(eMullo);
        }

        public IActionResult Create()
        {
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] EMullo eMullo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(eMullo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError("", "Error en la base de datos: " + dbEx.Message);
                    if (dbEx.InnerException != null)
                    {
                        ModelState.AddModelError("", "Detalle: " + dbEx.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error inesperado: " + ex.Message);
                }
            }
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Id", eMullo.IdCelular);
            return View(eMullo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eMullo = await _context.EMullo.FindAsync(id);
            if (eMullo == null)
            {
                return NotFound();
            }
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Nombre", eMullo.IdCelular);
            return View(eMullo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] EMullo eMullo)
        {
            if (id != eMullo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eMullo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EMulloExists(eMullo.Id))
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
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Nombre", eMullo.IdCelular);
            return View(eMullo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eMullo = await _context.EMullo
                .Include(e => e.Celular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eMullo == null)
            {
                return NotFound();
            }

            return View(eMullo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eMullo = await _context.EMullo.FindAsync(id);
            if (eMullo != null)
            {
                _context.EMullo.Remove(eMullo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EMulloExists(int id)
        {
            return _context.EMullo.Any(e => e.Id == id);
        }
    }
}
