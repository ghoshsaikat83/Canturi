using Canturi.Models.BusinessEntity.FrontEnd;
using Canturi.Models.BusinessHelper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessHelper.FrontEnd
{
    public class DiamondOrderHelper
    {

        public int AddUpdRequestAvailability(DiamondDetailModels model)
        {
            int result = -1;

            try
            {
                SqlParameter prmRequestId = SqlHelper.CreateParameter("@RequestId", model.RequestAvailabilityDetails.RequestId);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmConsultantEmail = SqlHelper.CreateParameter("@ConsultantEmail", model.RequestAvailabilityDetails.ConsultantEmail);

                SqlParameter prmDiamondId = SqlHelper.CreateParameter("@DiamondId", model.DiamondId);
                SqlParameter prmType = SqlHelper.CreateParameter("@Type", model.Type);
                SqlParameter prmCustomerName = SqlHelper.CreateParameter("@CustomerName", model.RequestAvailabilityDetails.CustomerName);
                SqlParameter prmOrderNumber = SqlHelper.CreateParameter("@OrderNumber", model.RequestAvailabilityDetails.OrderNumber);
                SqlParameter prmJcsCustomerNumber = SqlHelper.CreateParameter("@JcsCustomerNumber", model.RequestAvailabilityDetails.JcsCustomerNumber);
                SqlParameter prmDueDate = SqlHelper.CreateParameter("@DueDate", DateTime.ParseExact(model.RequestAvailabilityDetails.DueDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
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
                SqlParameter prmDiamondPrice = SqlHelper.CreateParameter("@DiamondPrice", model.TotalAmount);
                SqlParameter prmDesignPrice = SqlHelper.CreateParameter("@DesignPrice", model.RequestAvailabilityDetails.DesignPrice);
                SqlParameter prmComment = SqlHelper.CreateParameter("@Comment", model.RequestAvailabilityDetails.Comment);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);                
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    prmRequestId,prmConsultantId, prmConsultantEmail,prmDiamondId, 
                                            prmType, prmCustomerName, prmOrderNumber, 
                                            prmJcsCustomerNumber, prmDueDate, prmShape, 
                                            prmColor, prmClarity, prmCut, 
                                            prmPolish, prmFlourescence, prmMeasurements, 
                                            prmLab, prmCertificateNumber, prmLotNumber, 
                                            prmSymmetry, prmDiamondPrice, prmDesignPrice, 
                                            prmComment, prmStatus, prmCreatedBy, 
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

        public int AddUpdRequestOrder(DiamondDetailModels model)
        {
            int result = -1;

            try
            {
                SqlParameter prmRequestOrderId = SqlHelper.CreateParameter("@RequestOrderId", model.RequestOrderDetails.RequestOrderId);
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", model.ConsultantId);
                SqlParameter prmConsultantEmail = SqlHelper.CreateParameter("@ConsultantEmail", model.RequestOrderDetails.ConsultantEmail);
                SqlParameter prmDiamondId = SqlHelper.CreateParameter("@DiamondId", model.DiamondId);
                SqlParameter prmType = SqlHelper.CreateParameter("@Type", model.Type);
                SqlParameter prmCustomerName = SqlHelper.CreateParameter("@CustomerName", model.RequestOrderDetails.CustomerName);
                SqlParameter prmOrderNumber = SqlHelper.CreateParameter("@OrderNumber", model.RequestOrderDetails.OrderNumber);
                SqlParameter prmJcsCustomerNumber = SqlHelper.CreateParameter("@JcsCustomerNumber", model.RequestOrderDetails.JcsCustomerNumber);
                SqlParameter prmDueDate;
                if (String.IsNullOrEmpty(model.RequestOrderDetails.DueDate))
                {
                    prmDueDate = SqlHelper.CreateParameter("@DueDate", model.RequestOrderDetails.DueDate);
                }
                else
                {
                    prmDueDate = SqlHelper.CreateParameter("@DueDate", DateTime.ParseExact(model.RequestOrderDetails.DueDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                }

                


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
                SqlParameter prmIsFullPayment = SqlHelper.CreateParameter("@IsFullPayment", model.RequestOrderDetails.IsFullPayment);
                //SqlParameter prmAvailabilityDepositToken = SqlHelper.CreateParameter("@AvailabilityDepositToken", model.RequestOrderDetails.AvailabilityDepositToken);

                SqlParameter prmDatePaid;
                if (String.IsNullOrEmpty(model.RequestOrderDetails.DatePaid))
                {
                    prmDatePaid = SqlHelper.CreateParameter("@DatePaid", model.RequestOrderDetails.DatePaid);
                }
                else
                {
                    prmDatePaid = SqlHelper.CreateParameter("@DatePaid", DateTime.ParseExact(model.RequestOrderDetails.DatePaid, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                }

                //SqlParameter prmDatePaid = SqlHelper.CreateParameter("@DatePaid", DateTime.ParseExact(model.RequestOrderDetails.DatePaid, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                SqlParameter prmBalanceDue = SqlHelper.CreateParameter("@BalanceDue", model.RequestOrderDetails.BalanceDue);

                SqlParameter prmDateToBePaid;
                if (String.IsNullOrEmpty(model.RequestOrderDetails.DateToBePaid))
                {
                    prmDateToBePaid = SqlHelper.CreateParameter("@DateToBePaid", model.RequestOrderDetails.DateToBePaid);
                }
                else
                {
                    prmDateToBePaid = SqlHelper.CreateParameter("@DateToBePaid", DateTime.ParseExact(model.RequestOrderDetails.DateToBePaid, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                }

                //SqlParameter prmDateToBePaid = SqlHelper.CreateParameter("@DateToBePaid", DateTime.ParseExact(model.RequestOrderDetails.DateToBePaid, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                SqlParameter prmIsBankTransfer = SqlHelper.CreateParameter("@IsBankTransfer", model.RequestOrderDetails.IsBankTransfer);
                SqlParameter prmIsCreditCard = SqlHelper.CreateParameter("@IsCreditCard", model.RequestOrderDetails.IsCreditCard);
                SqlParameter prmIsCash = SqlHelper.CreateParameter("@IsCash", model.RequestOrderDetails.IsCash);
                SqlParameter prmIsOther = SqlHelper.CreateParameter("@IsOther", model.RequestOrderDetails.IsOther);
                SqlParameter prmDateDiamondPaidInFull;
                if (String.IsNullOrEmpty(model.RequestOrderDetails.DateDiamondPaidInFull))
                {
                    prmDateDiamondPaidInFull = SqlHelper.CreateParameter("@DateDiamondPaidInFull", model.RequestOrderDetails.DateDiamondPaidInFull);
                }
                else
                {
                    prmDateDiamondPaidInFull = SqlHelper.CreateParameter("@DateDiamondPaidInFull", DateTime.ParseExact(model.RequestOrderDetails.DateDiamondPaidInFull, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                }
                //SqlParameter prmDateDiamondPaidInFull = SqlHelper.CreateParameter("@DateDiamondPaidInFull", DateTime.ParseExact(model.RequestOrderDetails.DateDiamondPaidInFull, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));


                SqlParameter prmDateOrderPaidInFull;
                if (String.IsNullOrEmpty(model.RequestOrderDetails.DateOrderPaidInFull))
                {
                    prmDateOrderPaidInFull = SqlHelper.CreateParameter("@DateOrderPaidInFull", model.RequestOrderDetails.DateOrderPaidInFull);
                }
                else
                {
                    prmDateOrderPaidInFull = SqlHelper.CreateParameter("@DateOrderPaidInFull", DateTime.ParseExact(model.RequestOrderDetails.DateOrderPaidInFull, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                }
                //SqlParameter prmDateOrderPaidInFull = SqlHelper.CreateParameter("@DateOrderPaidInFull", DateTime.ParseExact(model.RequestOrderDetails.DateOrderPaidInFull, "dd/MM/yyyy", null).ToString("MM/dd/yyyy"));
                SqlParameter prmFullAmount = SqlHelper.CreateParameter("@FullAmount", model.RequestOrderDetails.FullAmount);
                SqlParameter prmFullAmount1 = SqlHelper.CreateParameter("@FullAmount1", model.RequestOrderDetails.FullAmount1);
                SqlParameter prmIsCertificateViewdByClient = SqlHelper.CreateParameter("@IsCertificateViewdByClient", model.RequestOrderDetails.IsCertificateViewdByClient);
                SqlParameter prmDiamondPrice = SqlHelper.CreateParameter("@DiamondPrice", model.RequestOrderDetails.DiamondPrice);
                SqlParameter prmDesignPrice = SqlHelper.CreateParameter("@DesignPrice", model.RequestOrderDetails.DesignPrice);
                SqlParameter prmComment = SqlHelper.CreateParameter("@Comment", model.RequestOrderDetails.Comment);
                SqlParameter prmIsClientViewing = SqlHelper.CreateParameter("@IsClientViewing", model.RequestOrderDetails.IsClientViewing);
                SqlParameter prmStatus = SqlHelper.CreateParameter("@Status", model.Status);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", model.CreatedBy);                
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", model.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", model.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);


                SqlParameter[] allParams = {  prmRequestOrderId, prmConsultantId,prmConsultantEmail, prmDiamondId, 
                                                prmType, prmCustomerName, prmOrderNumber, 
                                                prmJcsCustomerNumber, prmDueDate, prmShape, 
                                                prmColor, prmClarity, prmCut, 
                                                prmPolish, prmFlourescence, prmMeasurements, 
                                                prmLab, prmCertificateNumber, prmLotNumber, 
                                                prmSymmetry, prmIsFullPayment, 
                                                //prmAvailabilityDepositToken, 
                                                prmDatePaid, prmBalanceDue, prmDateToBePaid, 
                                                prmIsBankTransfer, prmIsCreditCard, prmIsCash, 
                                                prmIsOther, prmDateDiamondPaidInFull, prmDateOrderPaidInFull, 
                                                prmFullAmount, prmFullAmount1, prmIsCertificateViewdByClient, 
                                                prmDiamondPrice, prmDesignPrice, prmComment, 
                                                prmIsClientViewing,
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
