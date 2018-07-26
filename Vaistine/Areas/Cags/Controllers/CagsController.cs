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
using Vaistine.Areas.Cags.Models;
using Vaistine.Data;

namespace Vaistine.Areas.Cags.Controllers
{
    [Area("Cags")]
    public class CagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        //public IActionResult Idx() => View();

        public CagsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Cags/Cags
        public async Task<IActionResult> Index()
        {

            ViewBag.ParId = new SelectList(_db.Cags.OrderBy(x => x.Descr), "Id", "Descr");
            ViewData["parentid"] = _db.Cags.ToList();
            return View();
        }

        // GET: Cags/Cags/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cag = await _db.Cags
                .Include(c => c.Parent)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cag == null)
            {
                return NotFound();
            }

            return View(cag);
        }


    }
}
