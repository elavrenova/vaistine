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
using Vaistine.Areas.Docs.Models;
using Vaistine.Data;

namespace Vaistine.Areas.Docs.Controllers
{
    [Area("Docs")]
    public class DocHeadsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DocHeadsController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: DocHeads
        public IActionResult Index()
        {
            ViewBag.StoreId = new SelectList(_db.Stores.OrderBy(x => x.Descr), "Id", "Descr");
            ViewBag.CagId = new SelectList(_db.Cags.OrderBy(x => x.Descr), "Id", "Descr");
            return View();
        }

    }
}
