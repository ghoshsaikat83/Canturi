using System.Web.Mvc;

namespace Canturi.Web.Areas.SecureAdmin
{
    public class SecureAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SecureAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SecureAdmin_default",
                "SecureAdmin/{controller}/{action}/{id}",
                new { controller = "LogOn", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
