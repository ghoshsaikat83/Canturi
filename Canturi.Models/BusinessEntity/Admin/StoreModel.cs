using Canturi.Models.BusinessHelper.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Canturi.Models.BusinessEntity.Admin
{
    public class StoreModel
    {
        public StoreModel()
        {
            this.CreatedFromIp = HttpContext.Current.Request.UserHostAddress;
            this.CreatedBy = AdminSessionData.AdminUserId;
        }
        public int StoreId { get; set; }
        public string UsersToBeRemainActive { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedFromIp { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public string ModifiedFromIp { get; set; }
    }
}
