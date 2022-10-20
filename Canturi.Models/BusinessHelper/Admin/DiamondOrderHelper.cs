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


    public class DiamondOrderHelper
    {

        public DataSet GetRequestOrder(DiamondDetailModels model)
        {
            int result = -1;

            try
            {
                SqlParameter prmRequestId = SqlHelper.CreateParameter("@RequestId", model.RequestAvailabilityDetails.RequestId);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmDiamondId = SqlHelper.CreateParameter("@DiamondId", model.DiamondId);
                SqlParameter prmType = SqlHelper.CreateParameter("@Type", model.Type);
                SqlParameter prmCustomerName = SqlHelper.CreateParameter("@CustomerName", model.RequestAvailabilityDetails.CustomerName);
                SqlParameter prmOrderNumber = SqlHelper.CreateParameter("@OrderNumber", model.RequestAvailabilityDetails.OrderNumber);
                SqlParameter prmJcsCustomerNumber = SqlHelper.CreateParameter("@JcsCustomerNumber", model.RequestAvailabilityDetails.JcsCustomerNumber);
                SqlParameter prmDueDate = SqlHelper.CreateParameter("@DueDate", model.RequestAvailabilityDetails.DueDate);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", model.Shape);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", model.Colour);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", model.Clarity);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", model.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", model.Polish);
                SqlParameter prmFlourescence = SqlHelper.CreateParameter("@Flourescence", model.Flourescence);
                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", model.Measurements);
                SqlParameter prmLab = SqlHelper.CreateParameter("@Lab", model.Lab);
                SqlParameter prmCertificateNumber = SqlHelper.CreateParameter("@CertificateNumber", model.Cert);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", model.LotNumber);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", model.Symmetry);
                SqlParameter prmDiamondPrice = SqlHelper.CreateParameter("@DiamondPrice", model.RequestAvailabilityDetails.DiamondPrice);
                SqlParameter prmDesignPrice = SqlHelper.CreateParameter("@DesignPrice", model.RequestAvailabilityDetails.DesignPrice);
                SqlParameter prmComment = SqlHelper.CreateParameter("@Comment", model.RequestAvailabilityDetails.Comment);
                SqlParameter prmAdminUserId = SqlHelper.CreateParameter("@AdminUserId", model.AdminUserId);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    prmRequestId,prmConsultantId, prmDiamondId, 
                                            prmType, prmCustomerName, prmOrderNumber, 
                                            prmJcsCustomerNumber, prmDueDate, prmShape, 
                                            prmColor, prmClarity, prmCut, 
                                            prmPolish, prmFlourescence, prmMeasurements, 
                                            prmLab, prmCertificateNumber, prmLotNumber, 
                                            prmSymmetry, prmDiamondPrice, prmDesignPrice, 
                                            prmComment,prmAdminUserId, prmStatus, prmCreatedBy, 
                                            prmCreatedFromIp ,prmFlag,prmErr
                                           };

                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetRequestOrder", allParams);

            }
            catch
            {
                return null;
            }
        }

        public DataSet GetNumberOfOrders()
        {
            return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetNumberOfOrders");

        }

    }
}
