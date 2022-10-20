using Canturi.Models.BusinessEntity.Admin;
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
    public class ConsultantHelper
    {
        

        public int AddUpdConsultants(ConsultantModels model)
        {
            int result = -1;

            try
            {
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", model.ConsultantName);
                SqlParameter prmStoreId = SqlHelper.CreateParameter("@StoreId", model.StoreId);
                SqlParameter prmPassword = SqlHelper.CreateParameter("@Password", model.Password);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmConsultantId, prmConsultantName, prmStoreId,prmPassword, prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdConsultants", allParams);

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

        public ConsultantModels GetConsultantById(ConsultantModels model)
        {
            ConsultantModels objConsultantModels = new ConsultantModels();
            try
            {
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", model.ConsultantName);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmConsultantId, prmConsultantName, prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                DataSet dsConsultant = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetConsultants", allParams);

                if (dsConsultant != null)
                {
                    if (dsConsultant.Tables[0].Rows.Count != 0)
                    {
                        objConsultantModels = new ConsultantModels
                                                        {
                                                            ConsultantId = Convert.ToInt64(dsConsultant.Tables[0].Rows[0]["ConsultantId"].ToString()),
                                                            ConsultantName = dsConsultant.Tables[0].Rows[0]["ConsultantName"].ToString(),
                                                            Password = dsConsultant.Tables[0].Rows[0]["Password"].ToString(),
                                                            StoreId = dsConsultant.Tables[0].Rows[0]["StoreId"].ToString(),
                                                            StoreName = dsConsultant.Tables[0].Rows[0]["StoreName"].ToString()
                                                        };
                    }
                }
            }
            catch
            {
                throw;
            }
            return objConsultantModels;
        }

        public List<ConsultantModels> GetConsultants(ConsultantModels model)
        {
            List<ConsultantModels> listConsultantModels = new List<ConsultantModels>();
            try
            {
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", model.ConsultantName);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmConsultantId, prmConsultantName, prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                DataSet dsConsultant = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetConsultants", allParams);

                if (dsConsultant != null)
                {
                    if (dsConsultant.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow item in dsConsultant.Tables[0].Rows)
                        {
                            listConsultantModels.Add(new ConsultantModels
                            {
                                ConsultantId = Convert.ToInt64(item["ConsultantId"].ToString()),
                                ConsultantName = item["ConsultantName"].ToString(),
                                Password = item["Password"].ToString(),
                                StoreId = item["StoreId"].ToString(),
                                StoreName = item["StoreName"].ToString()
                            });
                        }
                      
                    }
                }
            }
            catch
            {
                throw;
            }
            return listConsultantModels;
        }
    }
}
