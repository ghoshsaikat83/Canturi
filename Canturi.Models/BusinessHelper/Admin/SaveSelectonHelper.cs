
using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessHelper.Admin
{
    public class SaveSelectonHelper
    {
        public string SaveSelectionId { get; set; }
        public string ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedFromIp { get; set; }
        public int AdminUserId { get; set; }

        public DataSet GetSaveSelecton(int flag)
        {
            try
            {
                SqlParameter prmSaveSelectionId = SqlHelper.CreateParameter("@SaveSelectionId", SaveSelectionId);
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", ConsultantName);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", ConsultantId);
                SqlParameter prmClientName = SqlHelper.CreateParameter("@ClientName", ClientName);
                SqlParameter prmAdminUserId = SqlHelper.CreateParameter("@AdminUserId", AdminUserId);
                SqlParameter prmPhone = SqlHelper.CreateParameter("@Phone", Phone);
                SqlParameter prmDate = SqlHelper.CreateParameter("@Date", Date);
                SqlParameter prmCurrency = SqlHelper.CreateParameter("@Currency", Currency);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    prmSaveSelectionId,prmConsultantName,prmConsultantId, prmClientName, 
                                               prmAdminUserId,prmDate,prmCurrency,  prmStatus, prmFlag,prmErr
                                           };

                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetSaveSelecton_A", allParams);

            }
            catch
            {
                return null;
            }
        }

        public int AddUpdSaveSelecton(int flag)
        {
            int result = -1;

            try
            {
                SqlParameter prmSaveSelectionId = SqlHelper.CreateParameter("@SaveSelectionId", SaveSelectionId);

                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);


                SqlParameter[] allParams = {  prmSaveSelectionId,prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag,prmErr
                                           };

                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdSaveSelecton", allParams);
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
    }
}
