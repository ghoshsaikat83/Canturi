using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class CaratSizeSettingModel : CommonModels
    {
        public int CaratSizeSetting { get; set; }

        [Display(Name = "Minimum carat size:")]
        [Required(ErrorMessage = "Minimum carat size required.")]
        [Range(0.21, 22.74, ErrorMessage = "Minimum carat size should be between 0.21 to 22.74")]
        public decimal MinimumCaratSize { get; set; }

        [Display(Name = "Maximum carat size:")]
        [Required(ErrorMessage = "Maximum carat size required.")]
        [Range(0.21, 22.74, ErrorMessage = "Maximum carat size should be between 0.21 to 22.74")]
        public decimal MaximumCaratSize { get; set; }
    }
}
