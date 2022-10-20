using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Text;
using System.Web.Configuration;
using System.Net;
using Canturi.Models.BusinessEntity.FrontEnd;


namespace Canturi.Models.BusinessHelper.CommonHelper
{
    public class EmailSender
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string SenderDisplayName { get; set; }
        public string To { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }


        public string Host { get; set; }
        public int Port { get; set; }
        public AttachmentCollection Attachments { get; set; }
        public ArrayList AttachmentList { get; set; }
        public string MailStatus { get; set; }
        public string NetworkUserId { get; set; }
        public string NetworkUserPassword { get; set; }
        public string ErrorMessage { get; set; }

        public static string FnEmailSend(ref MailMessage email)
        {
            string res = "Ok";
            try
            {
                bool isEnableSsl = false;
                if (ConfigurationManager.AppSettings["IsEnableSsl"].ToString() == "1")
                {
                    isEnableSsl = true;
                }
                email.Body = email.Body.Replace("{YEAR}", DateTime.Now.Year.ToString());
                email.Body = email.Body.Replace("{20}", ConfigurationManager.AppSettings["WebsiteRootPath"].ToString());
                email.Subject = WebUtility.HtmlDecode(email.Subject);

                var cred = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["networkUserId"].Trim(), ConfigurationManager.AppSettings["networkUserPassword"].Trim());
                var mailClient = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"].Trim(), Convert.ToInt32(ConfigurationManager.AppSettings["networkUserPort"].Trim()))
                {
                    EnableSsl = isEnableSsl,
                    Credentials = cred
                };
                mailClient.Send(email);
            }
            catch (Exception ex)
            {
                AppError err = new AppError();
                err.LogMe(ex);
                res = "NotOk";
            }
            return res;
        }

        public static EmailSender FncSendMail(EmailSender objMail)
        {
            FileStream ojbStream = null;
            try
            {
                string returnStatus = "";
                if (!ValidateMailObject(objMail, out returnStatus))
                {
                    if (returnStatus.Length > 0)
                    {
                        objMail.MailStatus = "Mail was not sent because " + returnStatus + " was not provided.";
                    }
                    return objMail;
                }

                MailMessage msg = new MailMessage();
                msg.Subject = objMail.Subject.Trim().Length == 0 ? "No Subject" : objMail.Subject.Trim();
                msg.From = new MailAddress(objMail.From, objMail.SenderDisplayName);
                msg.To.Clear();
                msg.To.Add(objMail.To);
                msg.Bcc.Clear();
                string basepath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
                string RDURL = ConfigurationManager.AppSettings["WebsiteRootPath"].ToString();
                objMail.Body = objMail.Body.Replace("{BASEPATH}", basepath);
                objMail.Body = objMail.Body.Replace("{RDURL}", RDURL);
                objMail.Body = objMail.Body.Replace("{COPYYEAR}", DateTime.Now.Year.ToString());

                msg.Body = objMail.Body;
                if (objMail.Bcc != null && objMail.Bcc.Length > 0)
                {
                    msg.Bcc.Add(objMail.Bcc);
                }

                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;

                msg.Attachments.Clear();
                //if (objMail.Attachments != null)
                //    {
                //    if (objMail.Attachments.Count > 0)
                //        {
                //        foreach (Attachment attachmentItem in objMail.Attachments)
                //            {
                //            msg.Attachments.Add(attachmentItem);
                //            }
                //        }
                //    }

                if (objMail.AttachmentList != null)
                {
                    if (objMail.AttachmentList.Count > 0)
                    {
                        foreach (string attachmentItem in objMail.AttachmentList)
                        {
                            ojbStream = File.OpenRead(attachmentItem);

                            Attachment objAttachment = new Attachment(attachmentItem);
                            msg.Attachments.Add(new Attachment(ojbStream, Path.GetFileName(attachmentItem)));
                        }
                    }
                }

                SmtpClient sc = new SmtpClient(objMail.Host);
                sc.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"].ToUpper().Trim());
                if (ConfigurationManager.AppSettings["IsReqSSLInMail"].ToUpper().Trim() == "Y")
                {
                    sc.EnableSsl = true;
                }
                else
                {
                    sc.EnableSsl = false;
                }
                sc.Credentials = new System.Net.NetworkCredential(objMail.NetworkUserId, objMail.NetworkUserPassword);
                sc.Send(msg);
                if (ojbStream != null)
                {
                    ojbStream.Close();
                    ojbStream.Dispose();
                }
                objMail.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                objMail.ErrorMessage = ex.Message;
                //AppError objError = new AppError();
                //objError.LogMe(ex);
                if (ex.InnerException != null)
                {

                    //objError.LogMe(ex.InnerException);
                }
                if (ojbStream != null)
                {
                    ojbStream.Close();
                    ojbStream.Dispose();
                }
            }

            return objMail;
        }

        private static bool ValidateMailObject(EmailSender objMail, out string _status)
        {
            _status = "";
            try
            {

                if (objMail.To == null || objMail.To.Trim() == "")
                {
                    _status = "To";
                    return false;
                }
                if (objMail.From == null || objMail.From.Trim() == "")
                {
                    _status = "From";
                    return false;
                }
                if (objMail.Host == null || objMail.Host.Trim() == "")
                {
                    _status = "Host";
                    return false;
                }
            }
            catch
            {
                throw;
            }
            return true;
        }

        public static int FncSendAdminPasswordReminderMail(string emailAddress, string password, string UserName, string AdminName, string verticalId)
        {
            try
            {
                EmailSender objClsMail = new EmailSender();
                objClsMail.To = emailAddress;
                objClsMail.SenderDisplayName = "L4L";
                objClsMail.From = ConfigurationManager.AppSettings["FromEmail"] == null ? "" : ConfigurationManager.AppSettings["FromEmail"].ToString();
                //objClsMail.Bcc = ConfigurationManager.AppSettings["BccMailid"] == null ? "" : ConfigurationManager.AppSettings["BccMailid"].ToString();
                objClsMail.Host = ConfigurationManager.AppSettings["EmailServer"] == null ? "" : ConfigurationManager.AppSettings["EmailServer"].ToString();
                objClsMail.Port = ConfigurationManager.AppSettings["port"] == null ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                objClsMail.NetworkUserId = ConfigurationManager.AppSettings["networkUserId"] == null ? "" : ConfigurationManager.AppSettings["networkUserId"];
                objClsMail.NetworkUserPassword = ConfigurationManager.AppSettings["networkUserPassword"] == null ? "" : ConfigurationManager.AppSettings["networkUserPassword"];
                objClsMail.Subject = "Admin password reminder for L4L";

                //string fullpage = EmailTemplate.GetEmailTemplateFile(Convert.ToInt32(verticalId), 0, "admin.forgetpassword");

                string path = HttpContext.Current.Server.MapPath("~/Areas/Admin/Content/Mail/ForgetAdminPassword.htm");
                string fullpage = CommonData.readingFile(path);
                string url = ConfigurationManager.AppSettings["WebsiteRootPath"].ToString() + "admin/";


                string mailBody = fullpage.Replace("{ADMINNAME}", AdminName);
                mailBody = mailBody.Replace("{USERNAME}", UserName);
                mailBody = mailBody.Replace("{PASSWORD}", password);
                mailBody = mailBody.Replace("{URL}", url);
                objClsMail.Body = mailBody;
                FncSendMail(objClsMail);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public static string SendTestMailFromAdmin(EmailServerModel emailServerModel)
        {
            try
            {
                EmailSender emailSender = new EmailSender();
                emailSender.To = emailServerModel.EmailTo;
                emailSender.Subject = "Test Email";
                emailSender.SenderDisplayName = "Test Mail";
                emailSender.From = emailServerModel.FromEmail;
                emailSender.Host = emailServerModel.EmailServer;
                emailSender.Port = (int)emailServerModel.Port;
                emailSender.NetworkUserId = emailServerModel.NetworkUserId;
                emailSender.NetworkUserPassword = emailServerModel.NetworkUserPassword;
                emailSender.Body = "Test email";

                MailMessage msg = new MailMessage();
                msg.Subject = emailSender.Subject.Trim().Length == 0 ? "No Subject" : emailSender.Subject.Trim();
                msg.From = new MailAddress(emailSender.From, emailSender.SenderDisplayName);
                msg.To.Clear();
                msg.To.Add(emailSender.To);
                msg.Bcc.Clear();
          

                msg.Body = emailSender.Body;
               

                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;

                msg.Attachments.Clear();
                


                SmtpClient sc = new SmtpClient(emailSender.Host);
                sc.Port = Convert.ToInt32(emailSender.Port);
                if (emailServerModel.EnableSsl)
                {
                    sc.EnableSsl = true;
                }
                else
                {
                    sc.EnableSsl = false;
                }
                sc.Credentials = new System.Net.NetworkCredential(emailSender.NetworkUserId, emailSender.NetworkUserPassword);
                sc.Send(msg);
                
                emailSender = FncSendMail(emailSender);
                //return "Success";
                return emailSender.ErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
