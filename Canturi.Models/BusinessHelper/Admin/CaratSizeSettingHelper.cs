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
   public class CaratSizeSettingHelper
    {

       public int AddUpdCaratSizeSetting(CaratSizeSettingModel model)
       {
           int result = -1;

           try
           {
               SqlParameter prmCaratSizeSetting = SqlHelper.CreateParameter("@CaratSizeSetting", model.CaratSizeSetting);
               SqlParameter prmMinimumCaratSize = SqlHelper.CreateParameter("@MinimumCaratSize", model.MinimumCaratSize);
               SqlParameter prmMaximumCaratSize = SqlHelper.CreateParameter("@MaximumCaratSize", model.MaximumCaratSize);
               SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
               SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
               SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
               SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
               SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

               SqlParameter[] allParams = { prmCaratSizeSetting, prmMinimumCaratSize, prmMaximumCaratSize, prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
               SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdCaratSizeSearchSetting", allParams);

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


       public CaratSizeSettingModel GetCaratSizeSettingById(CaratSizeSettingModel model)
       {
           CaratSizeSettingModel objCaratSizeSettingModel = new CaratSizeSettingModel();
           try
           {
               SqlParameter prmCaratSizeSetting = SqlHelper.CreateParameter("@CaratSizeSetting", model.CaratSizeSetting);
               SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
               SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
               SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
               SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
               SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

               SqlParameter[] allParams = { prmCaratSizeSetting, prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
               DataSet dsCaratSizeSetting = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetCaratSizeSearchSetting", allParams);

               if (dsCaratSizeSetting != null)
               {
                   if (dsCaratSizeSetting.Tables[0].Rows.Count != 0)
                   {
                       objCaratSizeSettingModel = new CaratSizeSettingModel
                       {
                           CaratSizeSetting = Convert.ToInt32(dsCaratSizeSetting.Tables[0].Rows[0]["CaratSizeSetting"].ToString()),
                           MinimumCaratSize = Convert.ToDecimal(dsCaratSizeSetting.Tables[0].Rows[0]["MinimumCaratSize"].ToString()),
                           MaximumCaratSize = Convert.ToDecimal(dsCaratSizeSetting.Tables[0].Rows[0]["MaximumCaratSize"].ToString())
                       };
                   }
               }
           }
           catch
           {
               throw;
           }
           return objCaratSizeSettingModel;
       }

    }
}
