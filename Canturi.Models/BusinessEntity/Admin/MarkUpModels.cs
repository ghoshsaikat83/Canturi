using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class MarkUpModels:CommonModels
    {
        public string MarkUpId { get; set; }

        [Required(ErrorMessage="Price From required.")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Price From range should be between 1 - 15")]
        //[RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Price From must be a Number.")]
        [RegularExpression(@"([1-9][0-9]*)", ErrorMessage = "Price From must be a Number.")]
        public string PriceFrom { get; set; }

        [Required(ErrorMessage = "Price To required.")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Price To range should be between 1 - 15")]
        //[RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Price to must be a Number.")]
        [RegularExpression(@"([1-9][0-9]*)", ErrorMessage = "Price to must be a Number.")]
        public string PriceTo { get; set; }

        [Required(ErrorMessage = "Mark up percentage required.")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Mark up percentage range should be between 1 - 2")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Mark up percentage must be a Number.")]
        public string MarkUpPercentage { get; set; }

        [Required(ErrorMessage = "Mark up amount required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mark up amount range should be between 1 - 10")]
        //[RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Mark up amount must be a Number.")]
        [RegularExpression(@"([1-9][0-9]*)", ErrorMessage = "Mark up amount must be a Number.")]
        public string MarkUpAmount { get; set; }

        [Required(ErrorMessage = "Markup Tax required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Markup Tax should be between 1 - 10")]
        //[RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Markup Tax must be a Number.")]
        [RegularExpression(@"([1-9][0-9]*)", ErrorMessage = "Markup Tax must be a Number.")]
        public string MarkUpTax { get; set; }

        public int DiamondID { get; set; }

        public Boolean IsInHand { get; set; }
    }
}
