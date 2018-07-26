using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vaistine.Areas.Goods.Models;
using Vaistine.Data;

namespace Vaistine.Areas.Goods.Controllers
{
    [Area("Goods")]
    public class UnitsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UnitsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Goods/Units
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Units.Include(u => u.BaseUnit);
            ViewData["baseunitid"] = _db.Units.ToList();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Goods/Units/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _db.Units
                .Include(u => u.BaseUnit)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

    }
}
