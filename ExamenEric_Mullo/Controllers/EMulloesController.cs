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

        // GET: EMulloes
        public async Task<IActionResult> Index()
        {
            var examenEric_MulloContext = _context.EMullo.Include(e => e.Celular);
            return View(await examenEric_MulloContext.ToListAsync());
        }

        // GET: EMulloes/Details/5
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

        // GET: EMulloes/Create
        public IActionResult Create()
        {
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Id");
            return View();
        }

        // POST: EMulloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] EMullo eMullo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eMullo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Id", eMullo.IdCelular);
            return View(eMullo);
        }

        // GET: EMulloes/Edit/5
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
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Id", eMullo.IdCelular);
            return View(eMullo);
        }

        // POST: EMulloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["IdCelular"] = new SelectList(_context.Celular, "Id", "Id", eMullo.IdCelular);
            return View(eMullo);
        }

        // GET: EMulloes/Delete/5
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

        // POST: EMulloes/Delete/5
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
