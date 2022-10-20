using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class DiamondModels : CommonModels
    {
        public DiamondModels()
        {
            this.IsCurrencyLock = true;
            this.IsMarkupLock = true;
        }
        public string DiamondId { get; set; }

        public string SkuDiamondId { get; set; }

        [Display(Name = "Diamond Image")]
        public string DiamondImage { get; set; }

        [Display(Name = "LOT #")]
        [Required(ErrorMessage = "Lot number required.")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 16.")]
        [RegularExpression("^([a-zA-Z0-9]+)$", ErrorMessage = "Lot number not valid.")]
        public string LotNumber { get; set; }

        [Display(Name = "Shape")]
        [Required(ErrorMessage = "Shape required.")]
        public string Shape { get; set; }

        [Display(Name = "Carat")]
        [Required(ErrorMessage = "Carat required.")]
        //[StringLength(5, MinimumLength = 1, ErrorMessage = "Carat range should be between 1 - 5")]
        //[MaxLength(6, ErrorMessage = "Carat range should be between 1 - 5")]
        //[RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Carat must be a Number.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid carat, Carat must be a Number.")]
        public double Carat { get; set; }

        [Display(Name = "Color")]
        [Required(ErrorMessage = "Color required.")]
        public string Color { get; set; }

        [Display(Name = "Clartiy")]
        [Required(ErrorMessage = "Clartiy required.")]
        public string Clartiy { get; set; }

        [Display(Name = "Cut")]
        [Required(ErrorMessage = "Cut required.")]
        public string Cut { get; set; }

        [Display(Name = "Polish")]
        [Required(ErrorMessage = "Polish required.")]
        public string Polish { get; set; }

        [Display(Name = "Symmetry")]
        [Required(ErrorMessage = "Symmetry required.")]
        public string Symmetry { get; set; }

        [Display(Name = "Fluorescence")]
        [Required(ErrorMessage = "Fluorescence required.")]
        public string Flourescence { get; set; }

        [Display(Name = "Depth")]
        [Required(ErrorMessage = "Depth required.")]
        public string Depth { get; set; }

        [Display(Name = "Table")]
        [Required(ErrorMessage = "Table required.")]
        public string Table { get; set; }

        [Display(Name = "Measurements")]
        [Required(ErrorMessage = "Measurements required.")]
        public string Measurements { get; set; }

        [Display(Name = "Ratio")]
        [Required(ErrorMessage = "Ratio required.")]
        public string Ratio { get; set; }

        [Display(Name = "Lab")]
        [Required(ErrorMessage = "Lab required.")]
        public string Lab { get; set; }

        [Display(Name = "Price per carat")]
        [Required(ErrorMessage = "Price per carat required.")]
        //[StringLength(10, MinimumLength = 1, ErrorMessage = "Price should be between 1 - 10")]
        //[MaxLength(10, ErrorMessage = "Price should be between 1 - 10")]
        //[RegularExpression(@"([1-9][0-9]*)", ErrorMessage = "Price must be a Number.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price per carat, Price per carat must be a Number.")]
        public double Price { get; set; }

        [Display(Name = "Diamond certificate")]
        public string DiamondCertificate { get; set; }

        public string Supplier { get; set; }

        public string EyeClean { get; set; }

        public bool IsClean { get; set; }

        public double PerCaratPrice { get; set; }

        public MarkUpModels Markup { get; set; }

        public List<MarkUpModels> OtherMarkupList { get; set; }

        public string AudFinalPrice { get; set; }

        [Display(Name = "In Aud")]
        [Required(ErrorMessage = "In Aud required.")]
        [Range(0, double.MaxValue, ErrorMessage = "In Aud must be a Number.")]
        public Nullable<decimal> AudValue { get; set; }


        [Display(Name = "Currency Lock")]
        public bool IsCurrencyLock { get; set; }

        [Display(Name = "Mark up Lock")]
        public bool IsMarkupLock { get; set; }

        // Changed in June for Video Task
        [Display(Name = "Upload Video")]
        public string DiamondVideo { get; set; }

        public string CertURL { get; set; }

        //public string DiamondId { get; set; }
        //public string SkuDiamondId { get; set; }
        //public string DiamondImage { get; set; }
        //public string Location { get; set; }
        //public string RefNumber { get; set; }
        //public string PricePerCarat { get; set; }
        //public string Carat { get; set; }
        //public string Shape { get; set; }
        //public string Cut { get; set; }
        //public string Color { get; set; }
        //public string ColorShade { get; set; }
        //public string Clarity { get; set; }
        //public string Polish { get; set; }
        //public string LAB { get; set; }
        //public string Symmetry { get; set; }
        //public string Fluorescence { get; set; }
        //public string Eyeclean { get; set; }
        //public string LengthWeightRatio { get; set; }
        //public string RapPer { get; set; }
        //public string Rappaport { get; set; }
        //public string Luster { get; set; }
        //public string HA { get; set; }
        //public string TD_per { get; set; }
        //public string Tableper { get; set; }
        //public string CrHeight { get; set; }
        //public string CrAngle { get; set; }
        //public string PvHeight { get; set; }
        //public string PvAngle { get; set; }
        //public string TI { get; set; }
        //public string TOI { get; set; }
        //public string BIS { get; set; }
        //public string BIC { get; set; }
        //public string OPCR { get; set; }
        //public string OPPV { get; set; }
        //public string OPTA { get; set; }
        //public string ExCR { get; set; }
        //public string ExPV { get; set; }
        //public string GC { get; set; }
        //public string Culate { get; set; }
        //public string TD_MN { get; set; }
        //public string KeyToSymbols { get; set; }
        //public string ReportComment { get; set; }
        //public string GirdleDesc { get; set; }
        //public string DiamondCertificate { get; set; }
    }
}
