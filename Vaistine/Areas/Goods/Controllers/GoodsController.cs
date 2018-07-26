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
    public class GoodsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GoodsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Goods/Goods
        public IActionResult Index()
        {
            return View();
        }

        // GET: Goods/Goods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _db.Goods
                .Include(g => g.Category)
                .Include(g => g.MainComponent)
                .Include(g => g.Unit)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (good == null)
            {
                return NotFound();
            }

            return View(good);
        }

    }
}
