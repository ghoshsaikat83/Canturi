using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class SaveSelectonModels : CommonModels
    {
        [Required(ErrorMessage = "Client name required.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 20.")]
        [RegularExpression("^([a-zA-Z0-9 ]+)$", ErrorMessage = "Client name not valid.")]        
        [DataType(DataType.Text)]
        [Display(Name = "Client name :")]
        public string ClientName { get; set; }

        //[Required]
        public string ClientSurName { get; set; }

        [Required(ErrorMessage = "Phone required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Phone range should be between 1 - 50")]        
        //[RegularExpression(@"([1-9][0-9]*)", ErrorMessage = "Phone must be a Number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date required.")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Consultant email required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100.")]
        [RegularExpression(@"(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*", ErrorMessage = "Valid email id required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Consultant email")]
        public string ConsultantEmail{ get; set; }

        public string Currency { get; set; }

        public string SaveSelectionId { get; set; }

        public string ConsultantId { get; set; }

        public string ConsultantName { get; set; }

        public string SelectedDiamonds { get; set; }
        
        public string SelectedDiamondsQuery { get; set; }
    }
}
