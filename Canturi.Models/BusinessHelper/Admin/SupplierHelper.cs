using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canturi.Models.BusinessEntity.Admin;
using Canturi.Models.BusinessHelper.CommonHelper;

namespace Canturi.Models.BusinessHelper.Admin
{
    public class SupplierHelper
    {

        public List<SuppliersVideo> GetSuppliersVideoSetting(int flag)
        {
            List<SuppliersVideo> listSuppliers = new List<SuppliersVideo>();
            DataTable dt;
            try
            {

                SqlParameter paraFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter[] Allpara = { paraFlag, prmErr };
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetSuppliers", Allpara);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        ds.Dispose();
                    }
                    else
                    {
                        dt = null;
                    }
                }
                else
                {
                    dt = null;
                }
            }
            catch(Exception ex)
            {
                dt = null;
            }
            if (dt!= null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SuppliersVideo obj = new SuppliersVideo();
                        obj.RowId = Convert.ToInt32(dr["RowId"]);
                        obj.SupplierId = Convert.ToInt32(dr["SupplierId"]);
                        obj.SupplierCharCode = Convert.ToString(dr["SupplierCharCode"]);
                        obj.SupplierName = Convert.ToString(dr["SupplierName"]);
                        obj.Status = Convert.ToInt32(dr["Status"]);
                        obj.ShowVideo = Convert.ToBoolean(dr["ShowVideo"]);

                        listSuppliers.Add(obj);
                    }
                }
            }
            return listSuppliers;
        }

        public int SaveSupplierVideoSettings(int flag, string ids)
        {
            int res = 0;
            try
            {

                SqlParameter paraFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter paraIds  = SqlHelper.CreateParameter("@Ids", ids);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
                SqlParameter[] Allpara = { paraFlag, paraIds, prmErr };
                res = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_SaveSuppliersSettings", Allpara);
                if (res >= 0)
                {
                    res = Convert.ToInt32(prmErr.Value);
                }
                if (res > 0)
                {
                    //Some error occured
                    res = -1;
                }
            }
            catch(Exception ex) { }
            return res;
        }
    }
}
