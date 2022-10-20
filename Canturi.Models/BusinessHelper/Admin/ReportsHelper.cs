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
    public class ReportsHelper
    {

        public string RequestId { get; set; }
        public string ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public string CustomerName { get; set; }
        
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Status { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public string RequestOrderId { get; set; }                
        public int CreatedBy { get; set; }
        public string CreatedFromIp { get; set; }
        public int AdminUserId { get; set; }

        public DataSet GetRequestAvailability(int flag)
        {
            try
            {
                SqlParameter prmRequestId = SqlHelper.CreateParameter("@RequestId", RequestId);
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", ConsultantName);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", ConsultantId);
                SqlParameter prmCustomerName = SqlHelper.CreateParameter("@CustomerName", CustomerName);
                SqlParameter prmAdminUserId = SqlHelper.CreateParameter("@AdminUserId", AdminUserId);
                SqlParameter prmDateFrom = SqlHelper.CreateParameter("@DateFrom", DateFrom);
                SqlParameter prmDateTo = SqlHelper.CreateParameter("@DateTo", DateTo);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    prmRequestId,prmConsultantName,prmConsultantId, prmCustomerName, 
                                               prmAdminUserId,prmDateTo,  prmStatus, prmFlag,prmErr
                                           };

                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetRequestAvailability", allParams);

            }
            catch
            {
                return null;
            }
        }


        public DataSet GetOrders(int flag)
        {
            try
            {
                SqlParameter prmRequestId = SqlHelper.CreateParameter("@RequestOrderId", RequestId);
                SqlParameter prmConsultantName = SqlHelper.CreateParameter("@ConsultantName", ConsultantName);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", ConsultantId);
                SqlParameter prmCustomerName = SqlHelper.CreateParameter("@CustomerName", CustomerName);
                SqlParameter prmAdminUserId = SqlHelper.CreateParameter("@AdminUserId", AdminUserId);
                SqlParameter prmDateFrom = SqlHelper.CreateParameter("@DateFrom", DateFrom);
                SqlParameter prmDateTo = SqlHelper.CreateParameter("@DateTo", DateTo);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    prmRequestId,prmConsultantName,prmConsultantId, prmCustomerName, prmAdminUserId,
                                            prmDateTo,  prmStatus, prmFlag,prmErr
                                           };

                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetRequestOrder", allParams);

            }
            catch
            {
                return null;
            }
        }


        public int AddUpdRequestAvailability(int flag)
        {
            int result = -1;

            try
            {
                SqlParameter prmRequestId = SqlHelper.CreateParameter("@RequestId", RequestId);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    prmRequestId, prmStatus, prmCreatedBy, 
                                            prmCreatedFromIp ,prmFlag,prmErr
                                           };
                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdRequestAvailability", allParams);
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

        public int AddUpdRequestOrder(int flag)
        {
            int result = -1;

            try
            {
                SqlParameter prmRequestOrderId = SqlHelper.CreateParameter("@RequestOrderId", RequestOrderId);
                
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);


                SqlParameter[] allParams = {  prmRequestOrderId, 
                                                prmStatus, prmCreatedBy, prmCreatedFromIp, prmFlag,prmErr
                                           };

                SqlHelper.ExecuteNonQuery(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_AddUpdRequestOrder", allParams);
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
