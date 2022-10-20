using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessHelper.FrontEnd
{
    public class LoginHelper
    {
        public DataTable GetConsultant(LogOnModels model)
        {
            try
            {
                
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", model.UserName);                
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter[] allParams = {  prmConsultantName, prmFlag, prmErr };
                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetConsultants", allParams).Tables[0];
            }
            catch
            {
                return null;
            }            
        }
    }
}
