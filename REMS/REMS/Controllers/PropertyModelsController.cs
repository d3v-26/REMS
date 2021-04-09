using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using REMS.Data;
using REMS.Models;

namespace REMS.Controllers
{
    public class PropertyModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyModelsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin, Agent")]
        // GET: PropertyModels
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var props = from c in _context.PropertyModel select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                props = props.Where(x => x.name.Contains(searchString) || x.address.Contains(searchString));
                ViewBag.visible = true;
            }
            else
            {
                ViewBag.visible = false;
            }
            return View(await props.AsNoTracking().ToListAsync());
        }
        [Authorize(Roles = "Admin, Agent")]
        // GET: PropertyModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyModel = await _context.PropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyModel == null)
            {
                return NotFound();
            }

            return View(propertyModel);
        }
        
        [Authorize(Roles="Admin, Agent")]
        public async Task<IActionResult> Verify(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyModel = await _context.PropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyModel == null)
            {
                return NotFound();
            }
            propertyModel.propertyStatus = PropertyStatus.Verified;
            _context.Update(propertyModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Unfit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyModel = await _context.PropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyModel == null)
            {
                return NotFound();
            }
            propertyModel.propertyStatus = PropertyStatus.Unfit;
            _context.Update(propertyModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private bool PropertyModelExists(int id)
        {
            return _context.PropertyModel.Any(e => e.id == id);
        }
    }
}
