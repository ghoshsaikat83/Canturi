using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Canturi.Models.BusinessHelper.Admin
{
    public class SettingHelper
    {
        public int AddUpdSettings(string settingName, string settingValue, int flag)
        {
            int result = -1;

            try
            {
                SqlParameter prmSettingId = SqlHelper.CreateParameter("@SettingId", 0);
                SqlParameter prmSettingName = SqlHelper.CreateParameter("@SettingName", settingName);
                SqlParameter prmSettingValue = SqlHelper.CreateParameter("@SettingValue", settingValue);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", 1);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy",AdminSessionData.AdminUserId);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", HttpContext.Current.Request.UserHostAddress);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmSettingId, prmSettingName, prmSettingValue, prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdSettings", allParams);

                if (prmErr.Value != null)
                {
                    result = (int)prmErr.Value;
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public DataTable GetSettings(int flag)
        {
            try
            {
                //SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                //SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", model.ConsultantName);
                //SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                //SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                //SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { 
                                               //prmConsultantId, prmConsultantName, prmStatus, prmCreatedBy, prmCreatedFromIp,
                                               prmFlag, prmErr };
                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetSettings", allParams).Tables[0];
            }
            catch
            {
                return null;
            }
            
        }
    }
}
