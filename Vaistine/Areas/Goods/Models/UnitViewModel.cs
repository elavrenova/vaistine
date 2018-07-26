using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vaistine.Areas.Goods.Models
{
    public class UnitViewModel
    {
        public Guid Id { get; set; }
        public string Descr { get; set; }
        public Guid? BaseUnitId { get; set; }
        public string ShortDescr { get; set; }
    }
}
