using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessHelper.Admin
{
    public class StoreHelper
    {

        private String _conString = String.Empty;
        public StoreHelper()
        {
            try
            {
                _conString = SqlHelper.GetConnectionString();
            }
            catch
            {
                throw;
            }
        }


        public List<StoreModel> GetStores(int ? Status)
        {
            List<StoreModel> listStores = new List<StoreModel>();
            try
            {
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", 1);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {prmStatus, prmFlag, prmErr };
                DataSet dsStores = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetStores", allParams);

                if (dsStores != null)
                {
                    if (dsStores.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow item in dsStores.Tables[0].Rows)
                        {
                            listStores.Add(new StoreModel
                            {
                                StoreId = Convert.ToInt32(item["StoreId"].ToString()),
                                Name = item["Name"].ToString(),
                                CreatedBy = Convert.ToInt32(item["CreatedBy"]),
                                CreatedOn = Convert.ToDateTime(item["CreatedOn"])
                            });
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return listStores;
        }

        public List<StoreModel> GetAllStores()
        {
            List<StoreModel> listStores = new List<StoreModel>();
            try
            {
                DataSet dsStores = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.Text, "SELECT * FROM vw_GetAllStores");

                if (dsStores != null)
                {
                    if (dsStores.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow item in dsStores.Tables[0].Rows)
                        {
                            listStores.Add(new StoreModel
                            {
                                StoreId = Convert.ToInt32(item["StoreId"].ToString()),
                                Name = item["Name"].ToString(),
                                CreatedBy = Convert.ToInt32(item["CreatedBy"]),
                                CreatedOn = Convert.ToDateTime(item["CreatedOn"])
                            });
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return listStores;
        }

        public int UpdateStoreStatus(StoreModel model)
        {
            int result = -1;

            try
            {
               model.Name = GetAllStores().Where(x => x.StoreId == model.StoreId).FirstOrDefault().Name;
                SqlParameter prmStoreId = SqlHelper.CreateParameter("@StoreId", model.StoreId);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmModifiededBy = SqlHelper.CreateParameter("@ModifiedBy", model.CreatedBy);
                SqlParameter prmModifiededFromIp = SqlHelper.CreateParameter("@ModifiedFromIp", model.CreatedFromIp);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter prmUserIdToBeRemainActive=null;
                if (model.Name != null && model.Name.ToLower() == "sydney")
                {
                    prmUserIdToBeRemainActive = SqlHelper.CreateParameter("@UsersToBeRemainActive", ConfigurationManager.AppSettings["SydneyActiveConsultants"]);                    
                }
                SqlParameter[] allParams = { prmStoreId, prmStatus, prmModifiededBy, prmModifiededFromIp, prmErr, prmUserIdToBeRemainActive };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_UpdateConsultantStatus", allParams);

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
