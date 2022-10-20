using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System;
using Canturi.Models.BusinessHelper.Admin;
using System.Configuration;
using System.Data.SqlClient;
using Canturi.Models.BusinessHelper.CommonHelper;
using System.Data;
using Canturi.Models.BusinessEntity.FrontEnd;


namespace Canturi.Web.App_Start
{

    public class AdminSessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            // check  sessions here
            if (filterContext.ActionDescriptor.ActionName.ToLower() != "fileuploads")
            {
                if (AdminSessionData.AdminUserId == 0)
                {
                    filterContext.Result = new RedirectResult("~/SecureAdmin/LogOn");
                    return;
                }                
                else if (!CheckModule(1, HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString()))
                {
                    filterContext.Result = new RedirectResult("~/SecureAdmin/NoAccess");
                }
                else
                {
                    // FnCheckVerticalAdmin();
                }
            }

            base.OnActionExecuting(filterContext);
        }
        private static bool CheckModule(int flag, string navigateUrl)
        {
            bool isValid = false;
            var sqlCon = new SqlConnection(SqlHelper.GetConnectionString());
            sqlCon.Open();

            var sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "usp_GetModulesByUserId";
            sqlCmd.Connection = sqlCon;

            sqlCmd.Parameters.AddWithValue("@Flag", flag);
            sqlCmd.Parameters.AddWithValue("@AdminUserId", AdminSessionData.AdminUserId);
            sqlCmd.Parameters.AddWithValue("@NavigateUrl", "/" + navigateUrl + "/");

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    isValid = true;
                }
            }

            sqlCon.Close();
            if (navigateUrl.ToLower() == "Dashboard".ToLower() || navigateUrl.ToLower() == "ChangePassword".ToLower() || navigateUrl.ToLower() == "noaccess".ToLower())
            {
                isValid = true;
            }


            return isValid;
        }

        public static DataTable GetDataByModuleName(int flag, string domainName)
        {
            var sqlCon = new SqlConnection(SqlHelper.GetConnectionString());
            sqlCon.Open();

            var sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "usp_GetDataByModuleName";
            sqlCmd.Connection = sqlCon;

            sqlCmd.Parameters.AddWithValue("@Flag", flag);
            sqlCmd.Parameters.AddWithValue("@DomainName", domainName);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);
            return dt;
        }

    }

    public class FrontEndSessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // check  sessions here
            if (filterContext.ActionDescriptor.ActionName.ToLower() != "fileuploads")
            {
                if (UserSessionData.UserId == 0)
                {
                    filterContext.Result = new RedirectResult("~/");
                    return;
                }
                //if (FrontEndSessionData.IsFreeMember==false)
                //{
                //    filterContext.Result = new RedirectResult("~/common/accessdenied.aspx");
                //    return;
                //}
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class RequireHttpsAttribute : System.Web.Mvc.RequireHttpsAttribute
    {
        public bool RequireSecure = false;

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (RequireSecure)
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["RequireHttps"]))
                {
                    base.OnAuthorization(filterContext);
                }
                else
                {
                    // for local machine
                    if (filterContext.HttpContext.Request.IsSecureConnection)
                    {
                        HandleNonHttpRequest(filterContext);
                    }
                }
            }
            else
            {
                // non secure requested
                if (filterContext.HttpContext.Request.IsSecureConnection)
                {
                    HandleNonHttpRequest(filterContext);
                }
            }
        }

        protected virtual void HandleNonHttpRequest(AuthorizationContext filterContext)
        {
            if (String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                // redirect to HTTP version of page
                string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(url);
            }

        }
    }


}