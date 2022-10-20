using Canturi.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessHelper.CommonHelper
{
    public class StringResource
    {
        private String _conString = String.Empty;

        public StringResource()
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
        public List<StringResourceModel> GetStringResource(StringResourceModel objStringResourceModel)
        {
            var sqlCon = new SqlConnection(_conString);
            sqlCon.Open();

            var sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "usp_GetStringResource";
            sqlCmd.Connection = sqlCon;

            sqlCmd.Parameters.AddWithValue("@Flag", objStringResourceModel.Flag);
            sqlCmd.Parameters.AddWithValue("@Status", objStringResourceModel.Status);
            sqlCmd.Parameters.AddWithValue("@StringResourceId", objStringResourceModel.StringResourceId);
            sqlCmd.Parameters.AddWithValue("@SearchKeywords", objStringResourceModel.SearchKeywords);

            var list = new List<StringResourceModel>();
            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new StringResourceModel();
                    p.SerialNumber = Convert.ToInt32(reader["SerialNumber"]);
                    p.Id = Convert.ToInt32(reader["Id"]);
                    p.TableType = reader["TableType"].ToString();
                    //p.VerticalId = Convert.ToInt32(reader["VerticalId"]);
                    //p.MicroSiteId = Convert.ToInt32(reader["MicroSiteId"]);
                    //p.VerticalName = reader["VerticalName"].ToString();
                    //p.MicrositeName = reader["MicrositeName"].ToString();
                    p.Title = reader["Title"].ToString();
                    p.Name = reader["Name"].ToString();
                    p.ConfigValue = reader["ConfigValue"].ToString();
                    p.CreatedOn = reader["CreatedOn"].ToString();
                    p.ModifiedOn = reader["ModifiedOn"].ToString();
                    list.Add(p);
                }
            }

            sqlCon.Close();

            return list;
        }


        public StringResourceModel GetStringResourceById(StringResourceModel objStringResourceModel)
        {
            try
            {
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objStringResourceModel.Flag);
                SqlParameter prmStringResourceId = SqlHelper.CreateParameter("@StringResourceId", objStringResourceModel.StringResourceId);
                SqlParameter prmId = SqlHelper.CreateParameter("@Id", objStringResourceModel.Id);
                SqlParameter prmTableType = SqlHelper.CreateParameter("@TableType", objStringResourceModel.TableType);
                SqlParameter[] allParams = { prmFlag, prmStringResourceId, prmId, prmTableType };
                SqlDataReader reader = SqlHelper.ExecuteReader(_conString, CommandType.StoredProcedure, "usp_GetStringResource", allParams);
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        objStringResourceModel.SerialNumber = Convert.ToInt32(reader["SerialNumber"]);
                        objStringResourceModel.Id = Convert.ToInt32(reader["Id"]);
                        objStringResourceModel.TableType = reader["TableType"].ToString();
                        objStringResourceModel.StringResourceMasterId = Convert.ToInt32(reader["StringResourceMasterId"]);
                        //objStringResourceModel.VerticalId = Convert.ToInt32(reader["VerticalId"]);
                        //objStringResourceModel.MicroSiteId = Convert.ToInt32(reader["MicroSiteId"]);
                        objStringResourceModel.Name = reader["Name"].ToString();
                        objStringResourceModel.Title = reader["Title"].ToString();
                        objStringResourceModel.ConfigValue = reader["ConfigValue"].ToString();
                        //objStringResourceModel.MicrositeName = reader["MicrositeName"].ToString();
                        //objStringResourceModel.VerticalName = reader["VerticalName"].ToString();
                        objStringResourceModel.ModifiedOn = reader["ModifiedOn"].ToString();

                    }
                }

                reader.Close();
                return objStringResourceModel;
            }
            catch
            {
                throw;
            }
        }

        public int InsertUpdateStringResource(StringResourceModel objStringResourceModel)
        {
            int result = -1;
            try
            {
                SqlParameter prmStringResourceId = SqlHelper.CreateParameter("@StringResourceId", objStringResourceModel.StringResourceId);
                SqlParameter prmId = SqlHelper.CreateParameter("@Id", objStringResourceModel.Id);
                SqlParameter prmTableType = SqlHelper.CreateParameter("@TableType", objStringResourceModel.TableType);
                SqlParameter prmConfigValue = SqlHelper.CreateParameter("@ConfigValue", objStringResourceModel.ConfigValue);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", objStringResourceModel.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objStringResourceModel.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objStringResourceModel.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objStringResourceModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { 
                                                prmStringResourceId, prmId, prmTableType,
                                                prmConfigValue, prmStatus, 
                                                prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdStringResource", allParams);

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




        // Static function for calling in front end
        /// <summary>
        /// verticalId 0(For common) or greater then zero(For vertical)
        /// microSiteId 0(For common) or greater then zero(For microSiteId)
        /// name = use from tblStringResourceMaster column "name"
        /// If new message is to be cretaed then make an entry in table "tblStringResourceMaster" and then update related vertical or microsite (For vertical or microsite)
        /// TableId - Auto Id for the corresponding TableName
        /// TableName - Table name from vertical and microsite id find out
        /// StringResource.GetStringResourceFile(0, 0, 0, "", "admin.ServiceNotActivate");  For Common message from master table
        /// </summary>
        /// <param name="verticalId"></param>
        /// <param name="microSiteId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetStringResourceFile(string name)
        {
            string configValue = "";
            string _conString = SqlHelper.GetConnectionString();
            var sqlCon = new SqlConnection(_conString);
            sqlCon.Open();

            var sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "usp_GetStringResource";
            sqlCmd.Connection = sqlCon;

            sqlCmd.Parameters.AddWithValue("@Flag", 3);            
            sqlCmd.Parameters.AddWithValue("@name", name);

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    configValue = reader["ConfigValue"].ToString();
                }
            }

            sqlCon.Close();

            return configValue;
        }

        

    }
}
