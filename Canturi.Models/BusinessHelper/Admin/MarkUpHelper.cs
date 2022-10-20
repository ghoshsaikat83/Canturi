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
    public class MarkUpHelper
    {
        public int AddUpdMarkUp(MarkUpModels model)
        {
            int result = -1;
            try
            {
                SqlParameter prmMarkUpId = SqlHelper.CreateParameter("@MarkUpId", model.MarkUpId);
                SqlParameter prmPriceFrom = SqlHelper.CreateParameter("@PriceFrom", model.PriceFrom);
                SqlParameter prmPriceTo = SqlHelper.CreateParameter("@PriceTo", model.PriceTo);
                SqlParameter prmMarkUpPercentage = SqlHelper.CreateParameter("@MarkUpPercentage", model.MarkUpPercentage);
                SqlParameter prmMarkUpAmount = SqlHelper.CreateParameter("@MarkUpAmount", model.MarkUpAmount);
                SqlParameter prmMarkUpTax = SqlHelper.CreateParameter("@MarkUpTax", model.MarkUpTax);
                SqlParameter prmIsInHand = SqlHelper.CreateParameter("@IsInHand", model.IsInHand);
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", model.DiamondID);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {prmMarkUpId,prmPriceFrom,prmPriceTo,
                                              prmMarkUpPercentage,prmMarkUpAmount, prmMarkUpTax, 
                                              prmIsInHand,prmDiamondID,
                                              prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdMarkUp", allParams);

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


        public DataSet GetMarkUp(MarkUpModels model)
        {
            try
            {
                SqlParameter prmMarkUpId = SqlHelper.CreateParameter("@MarkUpId", model.MarkUpId);
                SqlParameter prmPriceFrom = SqlHelper.CreateParameter("@PriceFrom", model.PriceFrom);
                SqlParameter prmPriceTo = SqlHelper.CreateParameter("@PriceTo", model.PriceTo);
                SqlParameter prmMarkUpPercentage = SqlHelper.CreateParameter("@MarkUpPercentage", model.MarkUpPercentage);
                SqlParameter prmMarkUpAmount = SqlHelper.CreateParameter("@MarkUpAmount", model.MarkUpAmount);
                SqlParameter prmMarkUpTax = SqlHelper.CreateParameter("@MarkUpTax", model.MarkUpTax);
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", model.DiamondID);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {prmMarkUpId,prmPriceFrom,prmPriceTo,
                                              prmMarkUpPercentage,prmMarkUpAmount, prmMarkUpTax, prmDiamondID,
                                              prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag, prmErr };
                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUpdMarkUp", allParams);

            }
            catch
            {
                return null;
            }
        }


        public List<MarkUpModels> GetMarkUpList(MarkUpModels model)
        {
            List<MarkUpModels> listMarkup = new List<MarkUpModels>();
            try
            {
                DataSet ds = GetMarkUp(model);
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    listMarkup.Add(new MarkUpModels
                    {
                        MarkUpAmount = item["MarkUpAmount"].ToString(),
                        MarkUpId = item["MarkUpId"].ToString(),
                        PriceFrom = item["PriceFrom"].ToString(),
                        PriceTo = item["PriceTo"].ToString(),
                        MarkUpPercentage = item["MarkUpPercentage"].ToString(),
                        MarkUpTax = item["MarkUpTax"].ToString()
                    });
                }

                return listMarkup;
            }
            catch
            {
                return listMarkup;
            }
        }

        public string GetCanturiDiamondFinalPrice(string price, string diamondID)
        {
            string finalPrice = "0";
            try
            {

                Canturi.Models.BusinessHelper.FrontEnd.DiamondHelper objDiamondHelper = new Canturi.Models.BusinessHelper.FrontEnd.DiamondHelper();
                DataSet ds = objDiamondHelper.GetDiamondBySelection("SELECT dbo.Udf_GetCanturiPrice(" + price + ",'AUD','CANTURI'," + diamondID + ")");
                finalPrice= ds.Tables[0].Rows[0][0].ToString();
            }
            catch 
            {
                
            }
            return finalPrice;
        }

        public MarkUpModels GetMarkUpById(MarkUpModels model)
        {
            MarkUpModels objMarkUpModels = new MarkUpModels();
            try
            {
                DataSet dsMarkup = GetMarkUp(model);
                if (dsMarkup != null)
                {
                    if (dsMarkup.Tables[0].Rows.Count > 0)
                    {
                        objMarkUpModels.MarkUpId = dsMarkup.Tables[0].Rows[0]["MarkUpId"].ToString();
                        objMarkUpModels.PriceFrom = dsMarkup.Tables[0].Rows[0]["PriceFrom"].ToString();
                        objMarkUpModels.PriceTo = dsMarkup.Tables[0].Rows[0]["PriceTo"].ToString();
                        objMarkUpModels.MarkUpPercentage = dsMarkup.Tables[0].Rows[0]["MarkUpPercentage"].ToString();
                        objMarkUpModels.MarkUpAmount = dsMarkup.Tables[0].Rows[0]["MarkUpAmount"].ToString();
                        objMarkUpModels.MarkUpTax = dsMarkup.Tables[0].Rows[0]["MarkUpTax"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objMarkUpModels;
        }
    }
}

