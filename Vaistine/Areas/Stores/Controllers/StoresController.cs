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
using Vaistine.Areas.Stores.Models;
using Vaistine.Data;

namespace Vaistine.Areas.Stores.Controllers
{
    [Area("Stores")]
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StoresController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Stores/Stores
        public IActionResult Index()
        {
            ViewData["ownerid"] = _db.Cags.ToList();
            return View();
        }

        public IActionResult Details(Guid id)
        {
            var store = _db.Stores
                    .Include(x=>x.Owner)
                .SingleOrDefault(x=>x.Id == id);
            if (store == null)
                return NotFound();

            return View(store);
        }

    }
}
