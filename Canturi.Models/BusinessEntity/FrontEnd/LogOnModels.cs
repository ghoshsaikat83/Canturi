using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class LogOnModels:CommonModels
    {
        [Required(ErrorMessage = "User name required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 50.")]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessage = "User name not valid.")]
        [DataType(DataType.Text)]
        [Display(Name = "User name :")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9]+)$", ErrorMessage = "Password not valid")]
        [DataType(DataType.Password)]
        [Display(Name = "Password :")]
        public string Password { get; set; }

        public string Message { get; set; }
    }
}
