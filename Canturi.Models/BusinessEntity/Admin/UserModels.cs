using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class UserModels
    {
        [Required(ErrorMessage = "First name required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100.")]
        [RegularExpression("^([a-zA-Z0-9' ']+)$", ErrorMessage = "First name not valid.")]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100.")]
        [RegularExpression("^([a-zA-Z0-9' ']+)$", ErrorMessage = "Last name not valid.")]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email id required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Valid email id required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email id")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Username required.")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 16.")]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessage = "Username not valid.")]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Password not valid (Only a-zA-Z0-9~!@$%^&*_-?).")]
        //[Compare("ConfirmPassword", ErrorMessage = "Password and confirm password should be same.")]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm password required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Confirm password not valid (Only a-zA-Z0-9~!@$%^&*_-?).")]
        [Compare("Password", ErrorMessage = "Confirm password and password should be same.")]
        [DataType(DataType.Text)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role required.")]
        public int RoleId { get; set; }

        [Display(Name = "Store")]
        public string StoreId { get; set; }

        public string StoreName { get; set; }

        public string SearchKeywords { get; set; }


        public int Flag { get; set; }



        public int UserId { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedFromIp { get; set; }
        public string Message { get; set; }
        public int IsShowMessage { get; set; }
        public int PageSize { get; set; }
        public int SerialNumber { get; set; }
        public List<System.Web.Mvc.SelectListItem> ListStatus { get; set; }

        public string CreatedOn { get; set; }
        public string RoleName { get; set; }
        public bool EnableDisable { get; set; }
        public string MessageClass { get; set; }


        [Display(Name = "Select role")]
        public List<RoleModels> ListRole { get; set; }
    }
}
