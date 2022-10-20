using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class DiamondModels : CommonModels
    {

        public string DiamondId { get; set; }

        public string SkuDiamondId { get; set; }

        
        public string DiamondImage { get; set; }

        
        public string LotNumber { get; set; }

        public string LargePrice { get; set; }

        public string Shape { get; set; }

        public string ShapeMin { get; set; }
        public string ShapeMax { get; set; }

        public string CaratMin { get; set; }
        public string CaratMax { get; set; }

        public string Color { get; set; }
        public string ColorMin { get; set; }
        public string ColorMax { get; set; }


        public string Clartiy { get; set; }
        public string ClartiyLeft { get; set; }
        public string ClartiyRight { get; set; }
        public string ClartiyMin { get; set; }
        public string ClartiyMax { get; set; }

        public string Cut { get; set; }
        public string CutMin { get; set; }
        public string CutMax { get; set; }

        public string Polish { get; set; }
        public string PolishMin { get; set; }
        public string PolishMax { get; set; }

        public string Symmetry { get; set; }
        public string SymmetryMin { get; set; }
        public string SymmetryMax { get; set; }

        public string Flourescence { get; set; }
        public string FlourescenceMin { get; set; }
        public string FlourescenceMax { get; set; }

        public string DepthMin { get; set; }
        public string DepthMax { get; set; }

        public string TableMin { get; set; }
        public string TableMax { get; set; }

        public string Measurements { get; set; }

        public string Ratio { get; set; }

        public string Lab { get; set; }

        public string PriceMin { get; set; }
        public string PriceMax { get; set; }

        
        public string DiamondCertificate { get; set; }

        public string Currency { get; set; }

        public string EyeClean { get; set; }

        public string SortByColumn { get; set; }

        public string SortOrder { get; set; }

        public string SearchByData { get; set; }
    }
}
