using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class DiamondDetailModels : CommonModels
    {
        public DiamondDetailModels()
        {
            this.RequestOrderDetails = new RequestOrder();
            this.RequestAvailabilityDetails = new RequestAvailability();
        }

        public RequestOrder RequestOrderDetails { get; set; }

        public RequestAvailability RequestAvailabilityDetails { get; set; }

        public string DiamondId { get; set; }

        public string ConsultantId { get; set; }

        //Caret-color-clarity-shape-Diamond
        public string DiamondName { get; set; }

        public string PriceCurrancy { get; set; }

        public string Price { get; set; }

        public string TotalAmount { get; set; }

        public bool IsRequestOrder { get; set; }

        public bool IsRequestAvailability { get; set; }

        public string LotNumber { get; set; }
        public string Shape { get; set; }
        public string Shape1 { get; set; }
        public string Carat { get; set; }
        public string Colour { get; set; }
        public string Cut { get; set; }
        public string Clarity { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Flourescence { get; set; }
        public string Depth { get; set; }
        public string Table { get; set; }
        public string Ratio { get; set; }
        public string Lab { get; set; }
        public string Cert { get; set; }
        public string Measurements { get; set; }
        public string Type { get; set; }
        public string EyeClean { get; set; }
        public bool HasCertFile { get; set; }

        public string Supplier { get; set; }

        public bool IsDiamondOrderd { get; set; }

        public string UrlCode { get; set; }
        public string DiamondVideo { get; set; }
        public string CertURL { get; set; }

        public string VideoURL { get; set; }
        
    }

    public class RequestOrder
    {
        public string RequestOrderId { get; set; }

        [Required(ErrorMessage = "Customer name required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer name should be between 1 - 50")]
        public string CustomerName { get; set; }

        //[Required(ErrorMessage = "Order number required.")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Order number should be between 1 - 50")]
        public string OrderNumber { get; set; }

        //[Required(ErrorMessage = "Jcs customer number required.")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Jcs customer number should be between 1 - 50")]
        public string JcsCustomerNumber { get; set; }

        [Required(ErrorMessage = "Order due date required.")]
        //[DataType(DataType.Date, ErrorMessage = "Due date must be a date.")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string DueDate { get; set; }

        [Required]
        public string IsFullPayment { get; set; }



         [Required(ErrorMessage = "Client viewing by client required.")]
        public string IsClientViewing { get; set; }

        [Required(ErrorMessage = "Availability deposit taken required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Availability deposit taken should be between 1 - 10")]
        [RegularExpression(@"([0-9]*)", ErrorMessage = "Availability deposit taken must be a Number.")]
        public string AvailabilityDepositToken { get; set; }

        [Required(ErrorMessage = "Date paid required.")]
        //[DataType(DataType.Date, ErrorMessage = "Date paid must be a date.")]
        public string DatePaid { get; set; }

        [Required(ErrorMessage = "Balance due required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Balance due should be between 1 - 10")]
        [RegularExpression(@"([0-9]*)", ErrorMessage = "Balance due must be a Number.")]
        public string BalanceDue { get; set; }

        [Required(ErrorMessage = "Date to be paid required.")]
        //[DataType(DataType.Date, ErrorMessage = "Date to be paid must be a date.")]             
        public string DateToBePaid { get; set; }

        [Required]
        public bool IsBankTransfer { get; set; }

        [Required]
        public bool IsCreditCard { get; set; }

        [Required]
        public bool IsCash { get; set; }

        [Required]
        public bool IsOther { get; set; }

        [Required(ErrorMessage = "Full Payment paid via required.")]
        public string PaymentBalanceDueVia { get; set; }

        [Required(ErrorMessage = "Date diamond paid in full required.")]
        //[DataType(DataType.Date, ErrorMessage = "Date diamond paid in full must be a date.")]                     
        public string DateDiamondPaidInFull { get; set; }

        [Required(ErrorMessage = "Date order paid in full required.")]
        //[DataType(DataType.Date, ErrorMessage = "Date order paid in full must be a date.")]                     
        public string DateOrderPaidInFull { get; set; }

        [Required(ErrorMessage = "Full amount required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Full amount should be between 1 - 10")]
        [RegularExpression(@"([0-9]*)", ErrorMessage = "Full amount must be a Number.")]
        public string FullAmount { get; set; }

        //[Required]
        public string FullAmount1 { get; set; }

         [Required(ErrorMessage = "Certificate viewd by client required.")]
        public string IsCertificateViewdByClient { get; set; }

        //[Required]
        public string DiamondPrice { get; set; }

        [Required(ErrorMessage = "Design price required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Design price should be between 1 - 10")]
        [RegularExpression(@"([0-9]*)", ErrorMessage = "Design price must be a Number.")]
        public string DesignPrice { get; set; }

        //[Required(ErrorMessage = "Comment required.")]
        [StringLength(8000, MinimumLength = 1, ErrorMessage = "Comment should be between 1 - 8000")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Consultant email required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100.")]
        [RegularExpression(@"(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*", ErrorMessage = "Valid email id required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Consultant email")]
        public string ConsultantEmail { get; set; }


    }

    public class RequestAvailability
    {
        public string RequestId { get; set; }

        [Required(ErrorMessage = "Customer name required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer name should be between 1 - 50")]
        public string CustomerName { get; set; }

        //[Required(ErrorMessage = "Order number required.")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Order number should be between 1 - 50")]
        public string OrderNumber { get; set; }

        //[Required(ErrorMessage = "Jcs customer number required.")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Jcs customer number should be between 1 - 50")]
        public string JcsCustomerNumber { get; set; }

        [Required(ErrorMessage = "Confirmation due date required.")]
        //[DataType(DataType.Date, ErrorMessage = "Due date must be a date.")]
        public string DueDate { get; set; }

        //[Required]
        public string DiamondPrice { get; set; }

        [Required(ErrorMessage = "Design price required.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Design price should be between 1 - 10")]
        [RegularExpression(@"([0-9]*)", ErrorMessage = "Design price must be a Number.")]
        public string DesignPrice { get; set; }

        //[Required(ErrorMessage = "Comment required.")]
        [StringLength(8000, MinimumLength = 1, ErrorMessage = "Comment should be between 1 - 8000")]
        public string Comment { get; set; }


        [Required(ErrorMessage = "Consultant email required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 100.")]
        [RegularExpression(@"(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*", ErrorMessage = "Valid email id required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Consultant email")]
        public string ConsultantEmail { get; set; }
    }
}
