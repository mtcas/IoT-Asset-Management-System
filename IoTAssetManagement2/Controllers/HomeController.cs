//01100110 
using System.Diagnostics;
using IoTAssetManagement2.Models;
using Microsoft.AspNetCore.Mvc;
using IoTAssetManagement2.Data;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalDevices = await _context.Devices.CountAsync();

            ViewBag.TotalFirmware = await _context.Firmware.CountAsync();

            ViewBag.TotalGroups = await _context.Groups.CountAsync();

            ViewBag.TotalDeviceTypes = await _context.DeviceTypes.CountAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
