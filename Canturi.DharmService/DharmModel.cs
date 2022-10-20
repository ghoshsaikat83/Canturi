using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.DharmService
{
    public class DharmResponseModel
    {
        public string IsValidUser { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
        public DataTable DataList { get; set; }
    }
}
