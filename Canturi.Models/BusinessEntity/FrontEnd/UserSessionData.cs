using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public static class UserSessionData
    {
        static UserSessionData()
        {
            UserId = 0;
            UserName = "";
            Name = "";
            Currency = "AUD";
        }
        /* For the  user id */
        const string UserIdKey = "UserId";
        public static int UserId
        {
            get { return HttpContext.Current.Session[UserIdKey] != null ? (int)HttpContext.Current.Session[UserIdKey] : 0; }
            set { HttpContext.Current.Session[UserIdKey] = value; }
        }


        /* For the  user name */
        const string UserNameKey = "UserName";
        public static string UserName
        {
            get { return HttpContext.Current.Session[UserNameKey] != null ? (string)HttpContext.Current.Session[UserNameKey] : ""; }
            set { HttpContext.Current.Session[UserNameKey] = value; }
        }


        /* For the  name */
        const string NameKey = "Name";
        public static string Name
        {
            get { return HttpContext.Current.Session[NameKey] != null ? (string)HttpContext.Current.Session[NameKey] : ""; }
            set { HttpContext.Current.Session[NameKey] = value; }
        }

        /* For the  Currency  */
        const string CurrencyKey = "Currency";
        public static string Currency
        {
            get { return HttpContext.Current.Session[CurrencyKey] != null ? (string)HttpContext.Current.Session[CurrencyKey] : ""; }
            set { HttpContext.Current.Session[CurrencyKey] = value; }
        }
    }
}
