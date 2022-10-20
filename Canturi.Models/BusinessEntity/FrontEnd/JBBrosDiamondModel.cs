using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class JBBrosDiamondModel
    {
        public int Srno { get; set; }
        public int RefNo { get; set; }
        public string Shape { get; set; }
        public string Availability { get; set; }
        public float Carat { get; set; }
        public float Price { get; set; }
        public float RapOff { get; set; }
        public float RapaportPrice { get; set; }
        public string Lab { get; set; }
        public string CertNo { get; set; }
        public string Color { get; set; }
        public string CS { get; set; }
        public string Purity { get; set; }
        public string Cut { get; set; }
        public string Intensity { get; set; }
        public string Overtone { get; set; }
        public string Symmetry { get; set; }
        public string Polish { get; set; }
        public string FL { get; set; }
        public string Luster { get; set; }
        public string TI { get; set; }
        public string BIS { get; set; }
        public string BIC { get; set; }
        public string OPPV { get; set; }
        public string OPCR { get; set; }
        public string OPTA { get; set; }
        public string EyeClean { get; set; }
        public string HA { get; set; }
        public string Culate { get; set; }
        public string EFCR { get; set; }
        public string EFPV { get; set; }
        public string TOI { get; set; }
        public string GirdleCondition { get; set; }
        public string FluoresentColor { get; set; }
        public string BowTie { get; set; }
        public float TotalDepthPer { get; set; }
        public float TabelPer { get; set; }
        public float Girdle { get; set; }
        public float CrownAngle { get; set; }
        public float CrownHeight { get; set; }
        public float PavalionHeight { get; set; }
        public float PavalionAngle { get; set; }
        public float TotalDept_mm { get; set; }
        public float TotalDepth_Per { get; set; }
        public float DimMinAndWidth { get; set; }
        public float DimMaxAndWidth { get; set; }
        public float Ratio { get; set; }
        public string KeytoSymbols { get; set; }
        public string Feather { get; set; }
        public string ReportComments { get; set; }

        public List<KeyValuePair<string, string>> getShape()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("CUMBR", "CUSHION MODIFIED BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("EM", "EMERALD"));
            list.Add(new KeyValuePair<string, string>("FS", "FANCY SHAPE"));
            list.Add(new KeyValuePair<string, string>("HRT", "HEART"));
            list.Add(new KeyValuePair<string, string>("MQ", "MARQUISE"));
            list.Add(new KeyValuePair<string, string>("OV", "OVAL"));
            list.Add(new KeyValuePair<string, string>("PS", "PEAR"));
            list.Add(new KeyValuePair<string, string>("PR", "PRINCESS"));
            list.Add(new KeyValuePair<string, string>("RT", "RADIANT"));
            list.Add(new KeyValuePair<string, string>("RD", "ROUND"));
            list.Add(new KeyValuePair<string, string>("SQEM", "SQUARE EMERALD"));
            list.Add(new KeyValuePair<string, string>("SQRT", "SQUARE RADIANT"));
            list.Add(new KeyValuePair<string, string>("TR", "TRILLIANT"));
            list.Add(new KeyValuePair<string, string>("SQBR", "SQUARE BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("RECBR", "RECTANGLE BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("TA", "TRIANGLE"));
            list.Add(new KeyValuePair<string, string>("ST", "STEP-TRIANGLE CUT"));
            list.Add(new KeyValuePair<string, string>("CUBR", "CUSHION BRILLIANT"));
            list.Add(new KeyValuePair<string, string>("CU", "CUSHION"));
            return list;
        }


        public List<KeyValuePair<string, string>> getCUT()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("ID", "IDEAL"));
            list.Add(new KeyValuePair<string, string>("EX+", "EXCELLENT +"));
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG+", "VERY GOOD +"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("GD+", "GOOD"));
            list.Add(new KeyValuePair<string, string>("GD+", "GOOD +"));
            list.Add(new KeyValuePair<string, string>("FR+", "FAIR +"));
            list.Add(new KeyValuePair<string, string>("FR", "FAIR"));
            list.Add(new KeyValuePair<string, string>("PR", "POOR"));
            return list;
        }

        public List<KeyValuePair<string, string>> getPolish()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("ID", "IDEAL"));
            list.Add(new KeyValuePair<string, string>("EX", "EXCELLENT"));
            list.Add(new KeyValuePair<string, string>("VG", "VERY GOOD"));
            list.Add(new KeyValuePair<string, string>("GD", "GOOD"));
            list.Add(new KeyValuePair<string, string>("FR", "FAIR"));
            list.Add(new KeyValuePair<string, string>("PR", "POOR"));
            return list;
        }

        public List<KeyValuePair<string, string>> getFloroscence()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("N", "NONE"));
            list.Add(new KeyValuePair<string, string>("F", "FAINT / VERY SLIGHT"));
            list.Add(new KeyValuePair<string, string>("M", "MEDIUM / SLIGHT"));
            list.Add(new KeyValuePair<string, string>("S", "STRONG"));
            list.Add(new KeyValuePair<string, string>("VS", "VERY STRONG"));
            return list;
        }
    }
}
