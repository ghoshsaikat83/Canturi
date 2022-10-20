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
    public class RoleHelper
    {
        private String _conString = String.Empty;
        public RoleHelper()
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



        //get data from datasource using SPROC (sample data access layer)
        public List<RoleModels> GetRoleByStatus(RoleModels objRoleModel)
        {
            var sqlCon = new SqlConnection(_conString);
            sqlCon.Open();

            var sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "Usp_GetRole";
            sqlCmd.Connection = sqlCon;

            sqlCmd.Parameters.AddWithValue("@Flag", objRoleModel.Flag);
            sqlCmd.Parameters.AddWithValue("@Status", objRoleModel.Status);

            var list = new List<RoleModels>();
            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new RoleModels();
                    p.RoleName = reader["roleName"].ToString();
                    p.RoleDescription = reader["roledesc"].ToString();
                    p.RoleId = Convert.ToInt32(reader["roleId"]);
                    list.Add(p);
                }
            }

            sqlCon.Close();

            return list;
        }

        public int InsertRole(RoleModels objRoleModel)
        {
            int result = -1;

            try
            {
                SqlParameter prmRoleName = SqlHelper.CreateParameter("@RoleName", objRoleModel.RoleName);
                SqlParameter prmRoleDescription = SqlHelper.CreateParameter("@RoleDescription", objRoleModel.RoleDescription);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objRoleModel.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objRoleModel.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objRoleModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmRoleName, prmRoleDescription, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdRole", allParams);

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

        public int UpdateRole(RoleModels objRoleModel)
        {
            int result = -1;

            try
            {
                SqlParameter prmRoleId = SqlHelper.CreateParameter("@RoleId", objRoleModel.RoleId);
                SqlParameter prmRoleName = SqlHelper.CreateParameter("@RoleName", objRoleModel.RoleName);
                SqlParameter prmRoleDescription = SqlHelper.CreateParameter("@RoleDescription", objRoleModel.RoleDescription);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objRoleModel.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objRoleModel.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objRoleModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmRoleId, prmRoleName, prmRoleDescription, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdRole", allParams);

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

        public int DeleteRole(RoleModels objRoleModel)
        {
            int result = -1;

            try
            {
                SqlParameter prmId = SqlHelper.CreateParameter("@RoleId", objRoleModel.RoleId);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objRoleModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter[] allParams = { prmId, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdRole", allParams);

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

        public int ChangeStatusRole(RoleModels objRoleModel)
        {
            int result = -1;

            try
            {
                SqlParameter prmRoleId = SqlHelper.CreateParameter("@RoleId", objRoleModel.RoleId);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", objRoleModel.Status);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objRoleModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter[] allParams = { prmRoleId, prmStatus, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdRole", allParams);

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

        public RoleModels GetRoleById(RoleModels objRoleModel)
        {
            try
            {
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objRoleModel.Flag);
                SqlParameter prmRoleId = SqlHelper.CreateParameter("@RoleId", objRoleModel.RoleId);
                SqlParameter[] allParams = { prmFlag, prmRoleId };
                SqlDataReader drReader = SqlHelper.ExecuteReader(_conString, CommandType.StoredProcedure, "Usp_GetRole", allParams);
                if (drReader.HasRows)
                {
                    if (drReader.Read())
                    {
                        objRoleModel.RoleId = Convert.ToInt32(drReader["RoleId"].ToString());
                        objRoleModel.RoleName = drReader["roleName"].ToString();
                        objRoleModel.RoleDescription = drReader["roledesc"].ToString();
                    }
                    else
                    {
                        drReader.Close();
                    }
                }
                drReader.Close();

                return objRoleModel;
            }
            catch
            {
                throw;
            }
        }

        public int InsertModuleOnRole(RoleModels objRoleModel)
        {
            int result = -1;

            try
            {
                SqlParameter prmRoleId = SqlHelper.CreateParameter("@RoleId", objRoleModel.RoleId);
                SqlParameter prmModuleIds = SqlHelper.CreateParameter("@ModuleIds", objRoleModel.ModuleIds);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objRoleModel.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objRoleModel.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objRoleModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmRoleId, prmModuleIds, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdRolePriv", allParams);

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
