using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity
{
    public class StringResourceModel : CommonModels
    {
        
        public int PageSize { get; set; }
        public int SerialNumber { get; set; }
        public string MessageClass { get; set; }
        public bool CanEdit { get; set; }
        public string ModifiedOn { get; set; }
        public int StringResourceId { get; set; }
        public int StringResourceMasterId { get; set; }
        public string Name { get; set; }


        [Display(Name = "Template :")]
        public string Title { get; set; }

        [Display(Name = "Content :")]
        public string ConfigValue { get; set; }

        public string TableType { get; set; }
        public int Id { get; set; }
    }
}
