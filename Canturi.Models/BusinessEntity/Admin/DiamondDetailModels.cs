using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
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

        public bool IsRequestOrder { get; set; }

        public bool IsRequestAvailability { get; set; }

        public string LotNumber { get; set; }
        public string Shape { get; set; }
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

        public bool HasCertFile { get; set; }

    }

    public class RequestOrder
    {
        public string RequestOrderId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string JcsCustomerNumber { get; set; }

        [Required]
        public string DueDate { get; set; }

        [Required]
        public bool IsFullPayment { get; set; }

        [Required]
        public string AvailabilityDepositToken { get; set; }

        [Required]
        public string DatePaid { get; set; }

        [Required]
        public string BalanceDue { get; set; }

        [Required]
        public string DateToBePaid { get; set; }

        [Required]
        public bool IsBankTransfer { get; set; }

        [Required]
        public bool IsCreditCard { get; set; }

        [Required]
        public bool IsCash { get; set; }

        [Required]
        public bool IsOther { get; set; }

        [Required]
        public string DateDiamondPaidInFull { get; set; }

        [Required]
        public string DateOrderPaidInFull { get; set; }

        [Required]
        public string FullAmount { get; set; }

        //[Required]
        public string FullAmount1 { get; set; }

        [Required]
        public bool IsCertificateViewdByClient { get; set; }

        //[Required]
        public string DiamondPrice { get; set; }

        [Required]
        public string DesignPrice { get; set; }

        [Required]
        public string Comment { get; set; }


    }

    public class RequestAvailability
    {
        public string RequestId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string JcsCustomerNumber { get; set; }

        [Required]
        public string DueDate { get; set; }

        //[Required]
        public string DiamondPrice { get; set; }

        [Required]
        public string DesignPrice { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
