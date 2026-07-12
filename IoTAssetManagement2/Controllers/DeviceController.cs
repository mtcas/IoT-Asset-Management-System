//01010100
using Microsoft.AspNetCore.Mvc;
using IoTAssetManagement2.Data;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var devices = await _context.Devices
                //eager loading, changed because objects werent showing--fetch device && devicetype
                .Include(d => d.DeviceType)
                .Include(d => d.Firmware)
                .Include(d => d.Group)
                .ToListAsync();

            return View(devices);
        }
    }
}
