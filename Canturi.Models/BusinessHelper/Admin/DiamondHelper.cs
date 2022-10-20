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
    public class DiamondHelper
    {
        private String _conString = String.Empty;
        public DiamondHelper()
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

        public int AddUpdCanturiDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                objDiamondModels.EyeClean = "NO";
                if (objDiamondModels.IsClean)
                {
                    objDiamondModels.EyeClean = "YES";
                }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmVideoURL = SqlHelper.CreateParameter("@VideoURL", objDiamondModels.DiamondVideo);


                SqlParameter prmAudValue = SqlHelper.CreateParameter("@AudValue", objDiamondModels.AudValue);
                SqlParameter prmIsCurrencyLock = SqlHelper.CreateParameter("@IsCurrencyLock", objDiamondModels.IsCurrencyLock);
                SqlParameter prmIsMarkupLock = SqlHelper.CreateParameter("@IsMarkupLock", objDiamondModels.IsMarkupLock);

                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate, prmVideoURL,
                                                prmAudValue,prmIsCurrencyLock,prmIsMarkupLock,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdCanturiDiamond", allParams);

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

        public int AddUpdVenusJewelsDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                //objDiamondModels.EyeClean = "NO";
                // if (objDiamondModels.IsClean)
                //{
                //    objDiamondModels.EyeClean = "YES";
                // }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmVideoURL = SqlHelper.CreateParameter("@VideoURL", objDiamondModels.DiamondVideo);
                SqlParameter prmCertURL = SqlHelper.CreateParameter("@CertURL", objDiamondModels.CertURL);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmVideoURL, prmCertURL, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdVenusJewelsDiamond", allParams);

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

        public int AddUpdPanacheJewelsDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                //objDiamondModels.EyeClean = "NO";
                // if (objDiamondModels.IsClean)
                //{
                //    objDiamondModels.EyeClean = "YES";
                // }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdPanacheDiamond", allParams);

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

        public int AddUpdYDVashDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);

                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);

                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);
                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);
                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);
                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmVideoURL = SqlHelper.CreateParameter("@VideoURL", objDiamondModels.DiamondVideo);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage,
                                                prmLotNumber, prmShape,prmCarat,
                                                prmColor, prmClarity,prmCut,
                                                prmPolish, prmSymmetry,prmFluorescence,
                                                prmDepth, prmTable, prmMeasurements,
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,
                                                prmFlag, prmVideoURL, prmErr
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdYDVashDiamond", allParams);

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

        public int AddUpdHariKrishnaJewelsDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                //objDiamondModels.EyeClean = "NO";
                // if (objDiamondModels.IsClean)
                //{
                //    objDiamondModels.EyeClean = "YES";
                // }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmVideoURL = SqlHelper.CreateParameter("@VideoURL", objDiamondModels.DiamondVideo);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmVideoURL, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdHariKrishnaDiamond", allParams);

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

        public int AddUpdFineStarDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                //objDiamondModels.EyeClean = "NO";
                // if (objDiamondModels.IsClean)
                //{
                //    objDiamondModels.EyeClean = "YES";
                // }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmVideoURL = SqlHelper.CreateParameter("@VideoURL", objDiamondModels.DiamondVideo);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmVideoURL, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdFineStarDiamond", allParams);

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

        public int AddUpdKapuGemsJewelsDiamond(DiamondModels objDiamondModels)
        {
            int result = -1;

            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);

                SqlParameter prmSupplier = SqlHelper.CreateParameter("@Supplier", objDiamondModels.Supplier);

                //objDiamondModels.EyeClean = "NO";
                // if (objDiamondModels.IsClean)
                //{
                //    objDiamondModels.EyeClean = "YES";
                // }
                SqlParameter prmEyeClean = SqlHelper.CreateParameter("@EyeClean", objDiamondModels.EyeClean);
                SqlParameter prmPerCaratPrice = SqlHelper.CreateParameter("@PerCaratPrice", objDiamondModels.PerCaratPrice);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmRowNumber = SqlHelper.CreateParameter("@RowNumber", objDiamondModels.PageNumber);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmSupplier,prmEyeClean,prmPerCaratPrice,
                                                prmDiamondCertificate,prmRowNumber,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr 
                                           };
                SqlHelper.ExecuteNonQuery(_conString, CommandType.StoredProcedure, "Usp_AddUpdKapuJemsDiamond", allParams);

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

        public DataSet GetCanturiDiamondDataSet(DiamondModels objDiamondModels)
        {
            try
            {
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);
                SqlParameter prmCarat = SqlHelper.CreateParameter("@Carat", objDiamondModels.Carat);
                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.Depth);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.Table);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPrice = SqlHelper.CreateParameter("@Price", objDiamondModels.Price);
                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);
                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter[] allParams = {    
                                                prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCarat, 
                                                prmColor, prmClarity,prmCut, 
                                                prmPolish, prmSymmetry,prmFluorescence, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPrice,
                                                prmDiamondCertificate,prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr 
                                           };
                return SqlHelper.ExecuteDataset(_conString, CommandType.StoredProcedure, "Usp_GetCanturiDiamond", allParams);

            }
            catch
            {
                return null;
            }

        }

        public List<DiamondModels> GetDiamonds(DiamondModels objDiamondModels)
        {
            List<DiamondModels> listDiamondModels = new List<DiamondModels>();
            try
            {

                DataSet dsDiamondModels = GetCanturiDiamondDataSet(objDiamondModels);

                if (dsDiamondModels != null)
                {
                    if (dsDiamondModels.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow item in dsDiamondModels.Tables[0].Rows)
                        {
                            bool IsEyClean = false;
                            string EyeClean = item["EyeClean"].ToString();
                            if (EyeClean == "YES")
                                IsEyClean = true;
                            listDiamondModels.Add(new DiamondModels
                            {
                                //ConsultantId = Convert.ToInt64(item["ConsultantId"].ToString()),
                                //ConsultantName = item["ConsultantName"].ToString()
                                DiamondId = item["DiamondID"].ToString(),
                                SkuDiamondId = item["SkuDiamondID"].ToString(),
                                DiamondImage = item["DiamondImage"].ToString(),
                                LotNumber = item["LotNumber"].ToString(),
                                Shape = item["Shape"].ToString(),
                                Carat = Convert.ToDouble(item["Carat"]),
                                Color = item["Color"].ToString(),
                                Clartiy = item["Clarity"].ToString(),
                                Cut = item["Cut"].ToString(),
                                Polish = item["Polish"].ToString(),
                                Symmetry = item["Symmetry"].ToString(),
                                Flourescence = item["Fluorescence"].ToString(),
                                Depth = item["Depth"].ToString(),
                                Table = item["Table"].ToString(),
                                Measurements = item["Measurements"].ToString(),
                                Ratio = item["Ratio"].ToString(),
                                Lab = item["LAB"].ToString(),
                                Price = Convert.ToDouble(item["Price"]),
                                DiamondCertificate = item["DiamondCertificate"].ToString(),
                                Supplier = item["Supplier"].ToString(),
                                EyeClean = item["EyeClean"].ToString(),
                                IsClean = IsEyClean,
                                CreatedOn = item["CreatedOn"].ToString(),
                                AudValue = Convert.ToDecimal(item["AudValue"]),
                                IsCurrencyLock = Convert.ToBoolean(item["IsCurrencyLock"]),
                                IsMarkupLock = Convert.ToBoolean(item["IsMarkupLock"]),
                                DiamondVideo = item["VideoURL"].ToString()
                            });
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return listDiamondModels;
        }
    }
}
