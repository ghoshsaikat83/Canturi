using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.FrontEnd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.CommonHelper;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
namespace Canturi.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/



        public ActionResult Index()
        {
           
         


            LogOnModels model = new LogOnModels();
            if (UserSessionData.UserId != 0)
            {
                return Redirect(Url.Content("~/DiamondSearch"));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LogOnModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Flag = 3;
                    LoginHelper objLoginHelper = new LoginHelper();
                    DataTable dtConsultant = objLoginHelper.GetConsultant(model);
                    if (dtConsultant != null)
                    {
                        if (dtConsultant.Rows.Count != 0)
                        {
                            if (dtConsultant.Rows[0]["Status"].ToString()=="True")
                            {
                                //if (model.Password == ConfigurationManager.AppSettings["CanturiCommonPassword"].ToString())
                                if (model.Password == dtConsultant.Rows[0]["Password"].ToString())
                                {
                                    UserSessionData.UserId = Convert.ToInt32(dtConsultant.Rows[0]["ConsultantId"].ToString());
                                    UserSessionData.UserName = dtConsultant.Rows[0]["ConsultantName"].ToString();
                                    UserSessionData.Name = dtConsultant.Rows[0]["ConsultantName"].ToString();
                                    UserSessionData.Currency = "AUD";
                                    return Redirect(Url.Content("~/DiamondSearch"));
                                }
                                else
                                {
                                    model.Message = "Please enter correct password";
                                }
                            }
                            else
                            {
                                model.Message = "Your account is Inactive. Please contact Administrator.";
                            }
                        }
                        else
                        {
                            model.Message = "Please fill correct username and password";
                        }
                    }
                    else
                    {
                        model.Message = "Please fill correct username and password";
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }

            return View(model);
        }

    }
}
