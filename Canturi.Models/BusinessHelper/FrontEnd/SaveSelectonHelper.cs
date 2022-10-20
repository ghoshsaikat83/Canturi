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
    public class SaveSelectonHelper
    {
        public int AddUpdSaveSelecton(SaveSelectonModels model)
        {
            int result = -1;

            try
            {
                SqlParameter prmSaveSelectionId = SqlHelper.CreateParameter("@SaveSelectionId", model.SaveSelectionId);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmClientName = SqlHelper.CreateParameter("@ClientName", model.ClientName);
                SqlParameter prmClientSurName = SqlHelper.CreateParameter("@ClientSurName", model.ClientSurName);
                SqlParameter prmPhone = SqlHelper.CreateParameter("@Phone", model.Phone);
                SqlParameter prmDate = SqlHelper.CreateParameter("@Date", DateTime.ParseExact(model.Date, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                SqlParameter prmConsultantEmail = SqlHelper.CreateParameter("@ConsultantEmail", model.ConsultantEmail);
                SqlParameter prmCurrency = SqlHelper.CreateParameter("@Currency", model.Currency);
                SqlParameter prmSelectedDiamondsQuery = SqlHelper.CreateParameter("@SelectedDiamondsQuery", model.SelectedDiamondsQuery);

                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);


                SqlParameter[] allParams = {  prmSaveSelectionId, prmConsultantId, prmClientName, 
                                                prmClientSurName, prmPhone, prmDate,prmConsultantEmail, prmCurrency, prmSelectedDiamondsQuery,
                                                prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag,prmErr
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

        public DataSet GetSaveSelecton(SaveSelectonModels model)
        {

            try
            {
                SqlParameter prmSaveSelectionId = SqlHelper.CreateParameter("@SaveSelectionId", model.SaveSelectionId);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmClientName = SqlHelper.CreateParameter("@ClientName", model.ClientName);
                SqlParameter prmClientSurName = SqlHelper.CreateParameter("@ClientSurName", model.ClientSurName);
                SqlParameter prmPhone = SqlHelper.CreateParameter("@Phone", model.Phone);
                //SqlParameter prmDate = SqlHelper.CreateParameter("@Date", model.Date);
                SqlParameter prmCurrency = SqlHelper.CreateParameter("@Currency", model.Currency);                

                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);


                SqlParameter[] allParams = {  prmSaveSelectionId, prmConsultantId, prmClientName, 
                                                prmClientSurName, prmPhone, 
                                                //prmDate, 
                                                prmCurrency, 
                                                prmStatus, prmFlag,prmErr
                                           };

                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetSaveSelecton", allParams);

            }
            catch
            {
                return null;
            }            
        }
    }
}
