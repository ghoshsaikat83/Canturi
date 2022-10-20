using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class ConsultantModels : CommonModels
    {
        public long ConsultantId { get; set; }

        [Required(ErrorMessage = "Consultant name required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 50.")]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessage = "Consultant name not valid.")]
        [DataType(DataType.Text)]
        [Display(Name = "Consultant name :")]
        public string ConsultantName { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Password not valid (Only a-zA-Z0-9~!@$%^&*_-?).")]        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Store required.")]        
        [Display(Name = "Store")]
        public string StoreId { get; set; }

        public string StoreName { get; set; }

    }
}
