using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vaistine.Data;

namespace Vaistine.Areas.Cags.ViewComponents
{
    public class CagSubCompaniesVC : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public CagSubCompaniesVC(ApplicationDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// возвращает входные документы по id склада
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            ViewBag.CagId = id;
            var cags = _db.Cags.Where(x => x.ParentId == id);
            var c = cags.Count();
            return View(await cags.ToListAsync());
        }
    }
}
