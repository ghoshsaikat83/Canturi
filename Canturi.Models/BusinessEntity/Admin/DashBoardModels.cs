using Canturi.Models.BusinessHelper.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class DashBoardModels
    {
        public DashBoardModels()
        {
            GetNoOfOrders();
        }
        public int Flag { get; set; }
        public int ParentId { get; set; }
        public int RoleId { get; set; }
        public int TotalRegisteredMember { get; set; }
        public int IsMemberPageAccessible { get; set; }


        public int DashBoardVerticalLeadReportAccess { get; set; }
        public int DashBoardLeadsMatured { get; set; }
        public int DashBoardVerticalNewPartner { get; set; }

        public string TotalOrderToday { get; set; }
        public string TotalOrderInMonth { get; set; }
        public string TotalOrderInYear { get; set; }
        public string TotalOrder { get; set; }


        public DataTable GetModules(DashBoardModels objModel)
        {
            MenuHelper objMenuHelper = new MenuHelper();
            DataTable dt = objMenuHelper.GetAllModule(objModel);
            return dt;
        }

        public void GetNoOfOrders()
        {
            DiamondOrderHelper _orderHelper = new DiamondOrderHelper();
            DataSet ds=new DataSet();
            ds=_orderHelper.GetNumberOfOrders();
            TotalOrderToday = ds.Tables[0].Rows[0]["OrderToday"].ToString();
            TotalOrderInMonth = ds.Tables[1].Rows[0]["OrderThisMonth"].ToString();
            TotalOrderInYear = ds.Tables[2].Rows[0]["OrderThisYear"].ToString();
            TotalOrder = ds.Tables[3].Rows[0]["TotalAvOrder"].ToString();
        }

    }
}
