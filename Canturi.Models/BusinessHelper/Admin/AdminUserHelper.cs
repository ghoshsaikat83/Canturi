using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Canturi.Models.BusinessEntity.Admin;
using System.Data.SqlClient;
using Canturi.Models.BusinessHelper.CommonHelper;
using System.Data;

namespace Canturi.Models.BusinessHelper.Admin
{
    public class AdminUserHelper
    {
        private String _conString = String.Empty;
        public AdminUserHelper()
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

        public int UpdateAdminPassword(ChangePassword objChangePassword)
        {
            int result = -1;

            try
            {
                SqlParameter prmUserId = SqlHelper.CreateParameter("@UserId", objChangePassword.UserId);
                SqlParameter prmPassword = SqlHelper.CreateParameter("@Password", objChangePassword.CurrentPassword);
                SqlParameter prmNewPassword = SqlHelper.CreateParameter("@NewPassword", objChangePassword.NewPassword);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objChangePassword.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmUserId, prmPassword, prmNewPassword, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpAdminUser", allParams);

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

        public int UpdateAdminTheme(int flag, string themeName, int userId)
        {
            int result = -1;

            try
            {
                SqlParameter prmThemeName = SqlHelper.CreateParameter("@ThemeName", themeName);
                SqlParameter prmUserId = SqlHelper.CreateParameter("@UserId", userId);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmUserId, prmThemeName, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpAdminUser", allParams);

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

        //get data from datasource using SPROC (sample data access layer)
        public List<UserModels> GetAdminUserByStatus(UserModels objUserModels)
        {
            var sqlCon = new SqlConnection(_conString);
            sqlCon.Open();

            var sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "usp_GetAdminUser";
            sqlCmd.Connection = sqlCon;

            sqlCmd.Parameters.AddWithValue("@Flag", objUserModels.Flag);
            sqlCmd.Parameters.AddWithValue("@Status", objUserModels.Status);
            sqlCmd.Parameters.AddWithValue("@SearchKeywords", objUserModels.SearchKeywords);

            var list = new List<UserModels>();
            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new UserModels();
                    p.FirstName = reader["firstName"].ToString();
                    p.LastName = reader["lastName"].ToString();
                    p.UserName = reader["loginId"].ToString();
                    p.EmailId = reader["EmailId"].ToString();
                    p.RoleName = reader["roleName"].ToString();
                    p.CreatedOn = reader["addedon"].ToString();
                    p.UserId = Convert.ToInt32(reader["userId"]);
                    list.Add(p);
                }
            }

            sqlCon.Close();

            return list;
        }

        public int ChangeAdminPassword(ref ChangePasswordModel model)
        {
            SqlCommand Cmd;
            string msg = "";
            int err = 0;
            SqlConnection Con = new SqlConnection(SqlHelper.GetConnectionString());
            Cmd = new SqlCommand("Usp_ChangePassword", Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@userId", SqlDbType.VarChar, 100);
            Cmd.Parameters.Add("@oldPassword", SqlDbType.VarChar, 100);
            Cmd.Parameters.Add("@newPassword", SqlDbType.VarChar, 100);
            Cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("@err", SqlDbType.Int).Direction = ParameterDirection.Output;
            Cmd.Parameters["@userId"].Value = model.CreatedBy;
            Cmd.Parameters["@oldPassword"].Value = model.OldPassword;
            Cmd.Parameters["@newPassword"].Value = model.NewPassword;
            try
            {
                Con.Open();
                Cmd.ExecuteNonQuery();
                msg = (string)Cmd.Parameters["@msg"].Value;
                err = (int)Cmd.Parameters["@err"].Value;
                model.Message = msg;
                return err;


            }
            catch (SqlException ex)
            {
                throw new Exception("Database Error" + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error" + ex.Message, ex);
            }
            finally
            {
                Con.Close();
                Cmd.Dispose();
            }
        }


        public int PerformActionOnUser(UserModels objUserModel)
        {
            int result = -1;

            try
            {
                SqlParameter prmUserId = SqlHelper.CreateParameter("@UserId", objUserModel.UserId);
                SqlParameter prmLoginId = SqlHelper.CreateParameter("@LoginId", objUserModel.UserName);
                SqlParameter prmPassword = SqlHelper.CreateParameter("@Password", objUserModel.Password);
                SqlParameter prmFirstName = SqlHelper.CreateParameter("@FirstName", objUserModel.FirstName);
                SqlParameter prmLastName = SqlHelper.CreateParameter("@LastName", objUserModel.LastName);
                SqlParameter prmEmailId = SqlHelper.CreateParameter("@EmailId", objUserModel.EmailId);
                SqlParameter prmRoleId = SqlHelper.CreateParameter("@RoleId", objUserModel.RoleId);
                
                if (objUserModel.RoleId == 1)
                {
                    objUserModel.Status = 1;
                }
                SqlParameter prmStoreId = SqlHelper.CreateParameter("@StoreId", objUserModel.StoreId);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", objUserModel.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objUserModel.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objUserModel.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objUserModel.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = { prmUserId, prmLoginId, prmPassword, prmFirstName, prmLastName, prmEmailId, prmRoleId, 
                                               prmStoreId,prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpAdminUser", allParams);

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

        public UserModels GetUserById(UserModels objUserModel)
        {
            try
            {
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objUserModel.Flag);
                SqlParameter prmUserId = SqlHelper.CreateParameter("@UserId", objUserModel.UserId);
                SqlParameter[] allParams = { prmFlag, prmUserId };
                SqlDataReader drReader = SqlHelper.ExecuteReader(_conString, CommandType.StoredProcedure, "usp_GetAdminUser", allParams);
                if (drReader.HasRows)
                {
                    if (drReader.Read())
                    {
                        objUserModel.UserId = Convert.ToInt32(drReader["userId"].ToString());
                        objUserModel.FirstName = drReader["firstName"].ToString();
                        objUserModel.LastName = drReader["lastName"].ToString();
                        objUserModel.UserName = drReader["loginId"].ToString();
                        objUserModel.EmailId = drReader["EmailId"].ToString();
                        objUserModel.RoleId = Convert.ToInt32(drReader["roleId"].ToString());
                        objUserModel.Status = 0;
                        if (Convert.ToBoolean(drReader["userStatus"].ToString()))
                        {
                            objUserModel.Status = 1;
                        }
                        objUserModel.Password = drReader["password"].ToString();
                        objUserModel.ConfirmPassword = drReader["password"].ToString();
                        objUserModel.StoreId = drReader["StoreId"].ToString();
                        objUserModel.StoreName = drReader["StoreName"].ToString();
                    }
                    else
                    {
                        drReader.Close();
                    }
                }
                drReader.Close();

                return objUserModel;
            }
            catch
            {
                throw;
            }
        }
    }
}
