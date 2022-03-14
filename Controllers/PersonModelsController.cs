using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonSimpleCURD.Models;

namespace PersonSimpleCURD.Controllers
{
    [Authorize]
    public class PersonModelsController : Controller
    {
        private readonly PersonDbContext _context;

        public PersonModelsController(PersonDbContext context)
        {
            _context = context;
        }

        [ResponseCache(Duration =600)]
        // GET: PersonModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.persons.ToListAsync());
        }
        [ResponseCache(Duration = 60, VaryByQueryKeys =new string[] { "id" })]
        // GET: PersonModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personModel = await _context.persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personModel == null)
            {
                return NotFound();
            }

            return View(personModel);
        }

        // GET: PersonModels/AddOrEdit
        public async Task<IActionResult> AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new PersonModel());
            }
            else
            {
                var personModel = await _context.persons.FindAsync(id);
                if (personModel == null)
                {
                    return NotFound();
                }
                return View(personModel);

            }
        }

        // POST: PersonModels/AddOrEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,Age")] PersonModel personModel)
        {
            

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(personModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(personModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonModelExists(personModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { isvalid = true, html = Helper.RenderRazorViewToString(this, "_personsList", _context.persons.ToList() ) });
            }
            return Json(new { isvalid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", personModel) });
        }

        // GET: PersonModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personModel = await _context.persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personModel == null)
            {
                return NotFound();
            }

            return View(personModel);
        }

        // POST: PersonModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personModel = await _context.persons.FindAsync(id);
            _context.persons.Remove(personModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonModelExists(int id)
        {
            return _context.persons.Any(e => e.Id == id);
        }
    }
}
