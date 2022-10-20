using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class SuppliersVideo : CommonModels
    {
        public int RowId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCharCode { get; set; }
        public string SupplierName { get; set; }
        public bool ShowVideo { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedFromIp { get; set; }

    }
}
