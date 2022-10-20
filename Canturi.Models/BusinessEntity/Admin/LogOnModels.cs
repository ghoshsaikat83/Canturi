using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Canturi.Models.BusinessEntity.Admin
{
    public class LogOnModels
    {
        [Required(ErrorMessage = "User name required.")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 16.")]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessage = "User name not valid.")]
        [DataType(DataType.Text)]
        [Display(Name = "User name :")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Password not valid (Only a-zA-Z0-9~!@$%^&*_-?).")]
        [DataType(DataType.Password)]
        [Display(Name = "Password :")]
        public string Password { get; set; }
        public string Message { get; set; }

        public String _conString = String.Empty;


        public DataTable AuthenticateUser(LogOnModels objModel, out int result)
        {
            try
            {
                result = -1;
                _conString = SqlHelper.GetConnectionString();
                SqlParameter prmUserName = SqlHelper.CreateParameter("@loginId", objModel.UserName);
                SqlParameter prmPassword = SqlHelper.CreateParameter("@password", objModel.Password);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter[] allParams = { prmUserName, prmPassword, prmErr };
                DataSet ds = SqlHelper.ExecuteDataset(_conString, CommandType.StoredProcedure, "Usp_AuthenticateUser", allParams);
                if (prmErr.Value != null)
                {
                    result = (int)prmErr.Value;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable Dt = ds.Tables[0].Copy();
                    ds.Dispose();
                    return Dt;
                }
                else
                {
                    return (DataTable)null;
                }
            }
            catch
            {
                throw;
            }
            
        }
    }


    public class ForgetPasswordModel:CommonModels
    {
        [Required(ErrorMessage = "Email address required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email address must be at least 5 character long")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Valid email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address :")]
        public string Email { get; set; }

        public String _conString = String.Empty;



        public DataTable AdminPasswordReminder(ForgetPasswordModel objModel)
        {
            try
            {
                _conString = SqlHelper.GetConnectionString();
                SqlParameter prmUserName = SqlHelper.CreateParameter("@EmailId", objModel.Email);
                SqlParameter prmQFlag = SqlHelper.CreateParameter("@Flag", objModel.Flag);
                SqlParameter[] allParams = { prmUserName, prmQFlag };
                DataSet ds = SqlHelper.ExecuteDataset(_conString, CommandType.StoredProcedure, "usp_GetAdminUser", allParams);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable Dt = ds.Tables[0].Copy();
                    ds.Dispose();
                    return Dt;
                }
                else
                {
                    return (DataTable)null;
                }
            }
            catch
            {
                throw;
            }
        }

    }

    public class ChangePassword:CommonModels
    {
        [Required(ErrorMessage = "Current password required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Password not valid")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password :")]
        public string CurrentPassword { get; set; }


        [Required(ErrorMessage = "New password required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Password not valid (Only a-zA-Z0-9~!@$%^&*_-?).")]        
        [DataType(DataType.Password)]
        [Display(Name = "New password :")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm password required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 20.")]
        [RegularExpression("^([a-zA-Z0-9~!@$%^&*_?-]+)$", ErrorMessage = "Password not valid (Only a-zA-Z0-9~!@$%^&*_-?).")]
        //[Compare("NewPassword", ErrorMessage = "New password and confirm password must be same.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password :")]
        public string ConfirmPassword { get; set; }

        public int UserId { get; set; }
        
    }
}
