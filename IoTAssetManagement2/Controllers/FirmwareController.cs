//01100001
using Microsoft.AspNetCore.Mvc;
using IoTAssetManagement2.Data;
using IoTAssetManagement2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Controllers
{
    public class FirmwareController : Controller
    {
        //db
        private readonly ApplicationDbContext _context;

        //cons_inj
        public FirmwareController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Firmware list -- move to other tasks while queries process
        public async Task<IActionResult> Index()
        {
            var firmware = await _context.Firmware.ToListAsync();

            return View(firmware);
        }

        // GET: Firmware/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Firmware/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Firmware firmware)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firmware);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(firmware);
        }

        // GET: Firmware/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmware = await _context.Firmware.FindAsync(id);

            if (firmware == null)
            {
                return NotFound();
            }

            return View(firmware);
        }

        // POST: Firmware/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Firmware firmware)
        {
            if (id != firmware.FirmwareId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firmware);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Firmware.Any(e => e.FirmwareId == firmware.FirmwareId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(firmware);
        }

        // GET: Firmware/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmware = await _context.Firmware
                .FirstOrDefaultAsync(f => f.FirmwareId == id);

            if (firmware == null)
            {
                return NotFound();
            }

            return View(firmware);
        }

        // POST: Firmware/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firmware = await _context.Firmware.FindAsync(id);

            if (firmware != null)
            {
                _context.Firmware.Remove(firmware);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
