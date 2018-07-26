using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Vaistine.Areas.Cags.Models;
using Vaistine.Areas.Docs.Models;
using Vaistine.Areas.Goods.Models;
using Vaistine.Areas.Stores.Models;
using Vaistine.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vaistine.Controllers
{
    public class KendoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KendoController(ApplicationDbContext context)
        {
            _db = context;
        }

        #region Cags
        [AllowAnonymous]
        public IActionResult GetCags([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Cags.Where(x=>x.ParentId==null)
                //.Select(x=>new CagViewModel
                //{
                //    Id = x.Id,
                //    Descr = x.Descr,
                //    ParentId = x.ParentId
                //})
                .AsNoTracking()
                .ToDataSourceResult(request));
        }

        // для дочерней таблицы
        public IActionResult GetChildCags([DataSourceRequest] DataSourceRequest request, Guid parentId)
        {
            return Json(_db.Cags.Where(x => x.ParentId == parentId)
                //.AsNoTracking()
                .Select(x => new CagViewModel
                {
                    Id = x.Id,
                    Descr = x.Descr,
                    ParentId = x.ParentId
                })
                .ToDataSourceResult(request));
        }

        [HttpPost]

        public async Task<IActionResult> CreateCag(Cag item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Add(item); await _db.SaveChangesAsync();
            }
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        //для дочерней таблицы
        public async Task<IActionResult> CreateChildCag(Cag item, Guid pId)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            ModelState["ParentId"].ValidationState = ModelValidationState.Valid;
            if (item != null)
            {
                item.ParentId = pId;
                if (ModelState.IsValid)
                {
                    _db.Add(item); await _db.SaveChangesAsync();
                }
            }
            
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [HttpPost]
        public ActionResult UpdateCag([DataSourceRequest] DataSourceRequest request, Cag item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyCag([DataSourceRequest] DataSourceRequest request, Cag item)
        {
            //if (!_db.Lots.Any(x => x.AuId == item.Id))
            //{
            _db.Remove(item);
            _db.SaveChanges();
            //}
            return Json(ModelState.ToDataSourceResult());
        }

        //public IActionResult GetChildItems([DataSourceRequest] DataSourceRequest request, Guid parentId)
        //{

        //    return Json(_db.Cags.Where(x => x.ParentId == parentId)
        //        .ToDataSourceResult(request, e => new CagViewModel
        //        {
        //            Id = e.Id,
        //            Descr = e.Descr
        //        }));
        //}

        #endregion

        #region DocHeads
        [AllowAnonymous]
        public IActionResult GetInDocs([DataSourceRequest] DataSourceRequest request, Guid StoreId)
        {
            return Json(_db.Docs.Where(x => x.ToStoreId == StoreId).ToDataSourceResult(request));
        }

        [AllowAnonymous]
        public IActionResult GetOutDocs([DataSourceRequest] DataSourceRequest request, Guid StoreId)
        {
            return Json(_db.Docs.Where(x => x.FromStoreId == StoreId).ToDataSourceResult(request));
        }

        public IActionResult GetDocs([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Docs.ToDataSourceResult(request));
        }

        public async Task<IActionResult> CreateDoc(DocHead item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Add(item); await _db.SaveChangesAsync();
            }
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        //public async Task<ActionResult> CreateInDoc (DocHead item, Guid InOlolo)
        //{
        //    ModelState["Id"].ValidationState = ModelValidationState.Valid;
        //    if (item != null && ModelState.IsValid)
        //    {
        //        item.ToStoreId = InOlolo;
        //        _db.Add(item); await _db.SaveChangesAsync();
        //    }
        //    return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        //}
        //public async Task<ActionResult> CreateOutDoc(DocHead item, Guid OutOlolo)
        //{
        //    ModelState["Id"].ValidationState = ModelValidationState.Valid;
        //    if (item != null && ModelState.IsValid)
        //    {
        //        item.FromStoreId = OutOlolo;
        //        _db.Add(item); await _db.SaveChangesAsync();
        //    }
        //    return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        //}

        [HttpPost]
        public ActionResult UpdateDoc([DataSourceRequest] DataSourceRequest request, DocHead item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyDoc([DataSourceRequest] DataSourceRequest request, DocHead item)
        {
            if (!_db.DocLines.Any(x => x.DocHeadId == item.Id))
            {
                _db.Remove(item);
                _db.SaveChanges();
            }
            return Json(ModelState.ToDataSourceResult());
        }

        #endregion

        #region Components
        [AllowAnonymous]
        public IActionResult GetComponents([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Components.ToDataSourceResult(request));
        }
        public async Task<IActionResult> CreateComponent(Component item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Add(item); await _db.SaveChangesAsync();
            }
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [HttpPost]
        public ActionResult UpdateComponent([DataSourceRequest] DataSourceRequest request, Component item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyComponent([DataSourceRequest] DataSourceRequest request, Component item)
        {
            //if (!_db.Lots.Any(x => x.AuId == item.Id))
            //{
            _db.Remove(item);
            _db.SaveChanges();
            //}
            return Json(ModelState.ToDataSourceResult());
        }

        #endregion

        #region Goods
        [AllowAnonymous]
        public IActionResult GetGoods([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Goods.ToDataSourceResult(request));
        }
        public async Task<IActionResult> CreateGood(Good item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Add(item); await _db.SaveChangesAsync();
            }
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [HttpPost]
        public ActionResult UpdateGood([DataSourceRequest] DataSourceRequest request, Good item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyGood([DataSourceRequest] DataSourceRequest request, Good item)
        {
            //if (!_db.Lots.Any(x => x.AuId == item.Id))
            //{
            _db.Remove(item);
            _db.SaveChanges();
            //}
            return Json(ModelState.ToDataSourceResult());
        }

        #endregion

        #region Units
        [AllowAnonymous]
        public IActionResult GetUnits([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Units.ToDataSourceResult(request));
        }
        public async Task<IActionResult> CreateUnit(Unit item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Add(item); await _db.SaveChangesAsync();
            }
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [HttpPost]
        public ActionResult UpdateUnit([DataSourceRequest] DataSourceRequest request, Unit item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyUnit([DataSourceRequest] DataSourceRequest request, Unit item)
        {
            //if (!_db.Lots.Any(x => x.AuId == item.Id))
            //{
            _db.Remove(item);
            _db.SaveChanges();
            //}
            return Json(ModelState.ToDataSourceResult());
        }

        #endregion

        #region Stores
        [AllowAnonymous]
        public IActionResult GetStores([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_db.Stores.ToDataSourceResult(request));
        }
        public async Task<IActionResult> CreateStore(Store item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Add(item); await _db.SaveChangesAsync();
            }
            return Json(new[] { item }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [HttpPost]
        public ActionResult UpdateStore([DataSourceRequest] DataSourceRequest request, Store item)
        {
            ModelState["Id"].ValidationState = ModelValidationState.Valid;
            if (item != null && ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyStore([DataSourceRequest] DataSourceRequest request, Store item)
        {
            if (!_db.Docs.Any(x => x.FromStoreId == item.Id) && !_db.Docs.Any(x => x.ToStoreId == item.Id))
            {
                _db.Remove(item);
                _db.SaveChanges();
            }
            return Json(ModelState.ToDataSourceResult());
        }

        #endregion
    }
}
