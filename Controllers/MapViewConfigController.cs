using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeltaPlan2100API.Models;

namespace DeltaPlan2100API.Controllers
{
    public class MapViewConfigController : Controller
    {
        private readonly delta_plan_2100_appContext _context;

        public MapViewConfigController(delta_plan_2100_appContext context)
        {
            _context = context;
        }

        // GET: MapViewConfig
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblMapViewConfig.ToListAsync());
        }

        // GET: MapViewConfig/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMapViewConfig = await _context.TblMapViewConfig
                .FirstOrDefaultAsync(m => m.AutoId == id);
            if (tblMapViewConfig == null)
            {
                return NotFound();
            }

            return View(tblMapViewConfig);
        }

        // GET: MapViewConfig/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MapViewConfig/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutoId,TableName,ColumnName,AliasName,ViewSerial")] TblMapViewConfig tblMapViewConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMapViewConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMapViewConfig);
        }

        // GET: MapViewConfig/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMapViewConfig = await _context.TblMapViewConfig.FindAsync(id);
            if (tblMapViewConfig == null)
            {
                return NotFound();
            }
            return View(tblMapViewConfig);
        }

        // POST: MapViewConfig/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutoId,TableName,ColumnName,AliasName,ViewSerial")] TblMapViewConfig tblMapViewConfig)
        {
            if (id != tblMapViewConfig.AutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMapViewConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMapViewConfigExists(tblMapViewConfig.AutoId))
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
            return View(tblMapViewConfig);
        }

        // GET: MapViewConfig/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMapViewConfig = await _context.TblMapViewConfig
                .FirstOrDefaultAsync(m => m.AutoId == id);
            if (tblMapViewConfig == null)
            {
                return NotFound();
            }

            return View(tblMapViewConfig);
        }

        // POST: MapViewConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMapViewConfig = await _context.TblMapViewConfig.FindAsync(id);
            _context.TblMapViewConfig.Remove(tblMapViewConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMapViewConfigExists(int id)
        {
            return _context.TblMapViewConfig.Any(e => e.AutoId == id);
        }
    }
}
