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
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ComponentsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Goods/Components
        public async Task<IActionResult> Index()
        {
            return View(await _db.Components.ToListAsync());
        }

        // GET: Goods/Components/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _db.Components
                .SingleOrDefaultAsync(m => m.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

    }
}
