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
    public class DiamondHelper
    {

        public DataSet GetDiamondById(string sqlQuery)
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.Text, sqlQuery.ToString());
            }
            catch( Exception ex)
            {

                return null;
            }

        }

        public DataSet GetDiamondBySelection(string sqlQuery)
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.Text, sqlQuery.ToString());
            }
            catch
            {
                return null;
            }

        }

        public DataSet GetDiamondSearch1(DiamondModels objDiamondModels)
        {

            StringBuilder sqlQuery = new StringBuilder();
            try
            {
                int cMin = 0, cMax = 0;
                cMin = Convert.ToInt32(objDiamondModels.ClartiyMin);// -1; 
                cMax = Convert.ToInt32(objDiamondModels.ClartiyMax);// -1; 


                sqlQuery.Append("DECLARE @TotalRecords INT ");

                sqlQuery.Append("DECLARE @MarkupMinPrice DECIMAL ");
                sqlQuery.Append("DECLARE @MarkupMaxPrice DECIMAL ");
                sqlQuery.Append("SET @MarkupMinPrice = 0 ");
                sqlQuery.Append("SET @MarkupMaxPrice = 0 ");
                sqlQuery.Append("SELECT @MarkupMinPrice=MIN(PriceFrom) FROM tblMarkUp  ");
                sqlQuery.Append("SELECT @MarkupMaxPrice=MAX(PriceTo) FROM tblMarkUp ");
                String[] strRapnetCodes = { "1", "11", "12" };
                if (String.IsNullOrEmpty(objDiamondModels.SearchByData))
                {
                    GetDiamondsFromAllSuppliers(objDiamondModels, sqlQuery, cMin);
                }
                else
                {
                    if (strRapnetCodes.Contains(objDiamondModels.SearchByData))//Rapnet diamonds 
                    {
                        GetDiamondsFromRapnet(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "2")//"JBBROS"
                    {
                        GetDiamondsFromJbBros(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "3")//"VENUS"
                    {
                        GetDiamondsFromVenus(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "4")//"CANTURI"
                    {
                        GetDiamondsFromCanturi(objDiamondModels, sqlQuery, cMin);
                    }
                    //else if (objDiamondModels.SearchByData == "5")//"PANACHE"
                    //{
                    //    GetDiamondsFromPananche(objDiamondModels, sqlQuery, cMin);
                    //}
                    else if (objDiamondModels.SearchByData == "5")//"YDVASH"
                    {
                        GetDiamondsFromYDVash(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "6")//"HARIKRISHNA"
                    {
                        GetDiamondsFromHariKrishna(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "7")//"FINESTAR"
                    {
                        GetDiamondsFromFineStar(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "8")//"CDINESH"
                    {
                        GetDiamondsFromCDINESH(objDiamondModels, sqlQuery, cMin);
                    }
                    //Replaced by Prashant on 20 Nov 2020 
                    //else if (objDiamondModels.SearchByData == "9")//"KAPUGEMS" 
                    //{ 
                    //    GetDiamondsFromKapuGems(objDiamondModels, sqlQuery, cMin); 
                    //} 
                    else if (objDiamondModels.SearchByData == "9")//"GlowStar"
                    {
                        GetDiamondsFromGlowStar(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "10")//"SRK"
                    {
                        GetDiamondsFromSrk(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "13")//"KIRANGEMS"
                    {
                        GetDiamondsFromKiranGems(objDiamondModels, sqlQuery, cMin);
                    }
                    else if (objDiamondModels.SearchByData == "14")//"Dharam"
                    {
                        GetDiamondsFromDharamDiamonds(objDiamondModels, sqlQuery, cMin);
                    }
                    else
                    {
                        GetDiamondsFromAllSuppliers(objDiamondModels, sqlQuery, cMin);
                    }
                }




                SqlConnection con = new SqlConnection(SqlHelper.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 720000;
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery.ToString();

                //SqlDataReader reader= cmd.ExecuteReader();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;//SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.Text, sqlQuery.ToString());


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void GetDiamondsFromRapnet(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            string tmpCondtion = "";
            if (objDiamondModels.SearchByData == "1")
            {
                tmpCondtion = " WHERE UPPER(Supplier) NOT IN ('N.E.R. JEWELRY INC.','OFER MIZRAHI DIAMONDS, INC.') AND  LAB ='GIA' ";
            }
            else if (objDiamondModels.SearchByData == "11")
            {
                tmpCondtion = " WHERE UPPER(Supplier) IN ('OFER MIZRAHI DIAMONDS, INC.') AND  LAB ='GIA' ";
            }
            else if (objDiamondModels.SearchByData == "12")
            {
                tmpCondtion = " WHERE UPPER(Supplier) IN ('N.E.R. JEWELRY INC.') AND  LAB ='GIA' ";
            }
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");




            if (!String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
            {
                sqlQuery.Append(" SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond " + tmpCondtion + " ");
            }
            else
            {

                if (!Convert.ToBoolean(objDiamondModels.EyeClean))
                {
                    sqlQuery.Append(" SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond " + tmpCondtion + " ");
                }
                else
                {
                    if (cMin > 4)
                    {
                        sqlQuery.Append(" SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond " + tmpCondtion + " ");
                    }
                }

            }


            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }




            if (!String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
            {

                sqlQuery.Append(" SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond " + tmpCondtion + " ");
            }
            else
            {

                if (!Convert.ToBoolean(objDiamondModels.EyeClean))
                {

                    sqlQuery.Append(" SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond " + tmpCondtion + " ");
                }
                else
                {
                    if (cMin > 4)
                    {

                        sqlQuery.Append(" SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond " + tmpCondtion + " ");
                    }
                }

            }




            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void GetDiamondsFromJbBros(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append(" SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblJBBrosDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);


            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }



            sqlQuery.Append(" SELECT *,'JBBros' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond  WHERE LAB ='GIA' ");



            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }


        private static void GetDiamondsFromVenus(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");




            sqlQuery.Append("  SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblVenusJewelsDiamond  WHERE LAB ='GIA' ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }



            sqlQuery.Append(" SELECT *,'Venus' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }


        private static void GetDiamondsFromCanturi(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append("SELECT " + GetCanturiCoulmns() + ",dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE UPPER(LAB) IN ('GIA','GSL','-') ");


            sqlQuery.Append(" ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT " + GetCanturiCoulmns() + ",'Canturi' AS TableType,dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE UPPER(LAB) IN ('GIA','GSL','-') ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }


        //private static void GetDiamondsFromPananche(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        //{
        //    sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");




        //    sqlQuery.Append(" SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblPanacheDiamond  WHERE LAB ='GIA' ");
        //    sqlQuery.Append("  ) T1 ");
        //    SearchCondition(objDiamondModels, sqlQuery);







        //    if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
        //    {
        //        sqlQuery.Append("	SELECT * FROM ( ");
        //        sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
        //    }
        //    else
        //    {
        //        sqlQuery.Append("	SELECT * FROM ( ");
        //        sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
        //    }



        //    sqlQuery.Append(" SELECT *,'Panache' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond  WHERE LAB ='GIA' ");


        //    sqlQuery.Append(" )Tmp ");

        //    SearchCondition(objDiamondModels, sqlQuery);

        //    sqlQuery.Append(" )T1 ");

        //    sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
        //    sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        //}

        private static void GetDiamondsFromYDVash(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");




            sqlQuery.Append(" SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblYDVashDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }



            sqlQuery.Append(" SELECT *,'YDVash' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void GetDiamondsFromHariKrishna(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");



            sqlQuery.Append("SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblHariKrishnaDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'HariKrishna' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond  WHERE LAB ='GIA' ");



            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }


        private static void GetDiamondsFromFineStar(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");



            sqlQuery.Append("  SELECT *,dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblFineStarDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'FineStar' AS TableType,dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblFineStarDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void GetDiamondsFromCDINESH(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");

            sqlQuery.Append("SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblCDINESHDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'CDINESH' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void GetDiamondsFromKapuGems(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append("SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblKapuGemsDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'KAPUGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond  WHERE LAB ='GIA'  ");

            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void GetDiamondsFromSunrise(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append("  SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblSunriseDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'SUNRISE' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblSunriseDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }




        // Add this for Client SRK

        private static void GetDiamondsFromSrk(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");



            sqlQuery.Append("  SELECT *,dbo.Udf_GetPriceCalculation(Price,'AUD',Carat) AS Amount FROM tblSrkDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" ) T1 ");
            NewSearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'SRK' AS TableType,dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblSrkDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            NewSearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }
       
        private static void GetDiamondsFromKiranGems(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append("  SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblKiranGemsDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'KIRANGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void GetDiamondsFromDharamDiamonds(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append("  SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblDharmDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append("  ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'Dharm' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        //Added by Prashant on 20 Nov 2020
        private static void GetDiamondsFromGlowStar(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");


            sqlQuery.Append("SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblGlowStarDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            sqlQuery.Append(" SELECT *,'GlowStar' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblGlowStarDiamond  WHERE LAB ='GIA'  ");

            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        public static string GetCanturiCoulmns()
        {
            string columns = "DiamondID,SkuDiamondID,DiamondImage,LotNumber,Shape,Carat,Color,Clarity,Cut,Polish,Symmetry,Fluorescence" +
                            ",Depth,[Table],Measurements,Ratio,LAB,Price,DiamondCertificate,HasCertFile,IsSoldOut,ActiveInd,Supplier,EyeClean,PerCaratPrice,"+
                            "CreatedOn,CreatedBy,CreatedFromIp,IsDiamondOrderd,VideoURL,CertURL";
            return columns;
        }

        private static void GetDiamondsFromAllSuppliers(DiamondModels objDiamondModels, StringBuilder sqlQuery, int cMin)
        {
            sqlQuery.Append("SELECT @TotalRecords=COUNT(1) FROM ( ");

            sqlQuery.Append("SELECT *,dbo.Udf_GetPriceCalculation(Price,'AUD',Carat) AS Amount FROM tblSrkDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT " + GetCanturiCoulmns()+ ",dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE UPPER(LAB) IN ('GIA','GSL','-') ");

            //If I click on Clarity bar on the LEFT (see below) and I DON’T tick yes in the EYE CLEAN box. The “Are you sure…” message appears. If I click YES to the message, NO RAPNET diamonds should come up in the results.

            //if (String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
            //{
            //    if (!Convert.ToBoolean(objDiamondModels.EyeClean))
            //    {
            //        sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond ");
            //    }
            //    else
            //    {
            //        if (cMin > 4)
            //        {
            //            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond ");
            //        }
            //    }
            //}
            //else
            //{
            //    sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond ");

            //}

            if (!String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
            {
                sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond WHERE LAB ='GIA' ");
            }
            else
            {

                if (!Convert.ToBoolean(objDiamondModels.EyeClean))
                {
                    sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond  WHERE LAB ='GIA' ");
                }
                else
                {
                    if (cMin > 4)
                    {
                        sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblRapnetDiamond  WHERE LAB ='GIA' ");
                    }
                }

            }






            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblJBBrosDiamond  WHERE LAB ='GIA' ");

            //sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblPanacheDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblDharmDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblYDVashDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblHariKrishnaDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPriceCalculation(Price,'AUD',Carat) AS Amount FROM tblFineStarDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblCDINESHDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblKapuGemsDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append("union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblKiranGemsDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append(" union all SELECT *,dbo.Udf_GetPrice(Price,'AUD') AS Amount FROM tblVenusJewelsDiamond  WHERE LAB ='GIA' ) T1 ");
            SearchCondition(objDiamondModels, sqlQuery);







            if (String.IsNullOrEmpty(objDiamondModels.LargePrice))
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY " + objDiamondModels.SortByColumn + " " + objDiamondModels.SortOrder + ") AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }
            else
            {
                sqlQuery.Append("	SELECT * FROM ( ");
                sqlQuery.Append(" SELECT ROW_NUMBER() over (ORDER BY Carat desc) AS Sno, @TotalRecords  AS TotalRecords,* FROM ( ");
            }


            
            sqlQuery.Append(" SELECT *,'SRK' AS TableType,dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblSrkDiamond  WHERE LAB ='GIA' ");
            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT " + GetCanturiCoulmns() + ",'Canturi' AS TableType,dbo.Udf_GetCanturiPrice(Price,'" + UserSessionData.Currency.ToUpper() + "','CANTURI',DiamondID) AS Amount FROM tblCanturiDiamond WHERE UPPER(LAB) IN ('GIA','GSL','-') ");

            
            
            if (!String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
            {
                sqlQuery.Append(" union all ");
                sqlQuery.Append(" SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond  WHERE LAB ='GIA' ");
            }
            else
            {

                if (!Convert.ToBoolean(objDiamondModels.EyeClean))
                {
                    sqlQuery.Append(" union all ");
                    sqlQuery.Append(" SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond  WHERE LAB ='GIA' ");
                }
                else
                {
                    if (cMin > 4)
                    {
                        sqlQuery.Append(" union all ");
                        sqlQuery.Append(" SELECT *,'Rapnet' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblRapnetDiamond  WHERE LAB ='GIA' ");
                    }
                }

            }


            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'JBBros' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblJBBrosDiamond  WHERE LAB ='GIA' ");


            //sqlQuery.Append(" union all ");
            //sqlQuery.Append(" SELECT *,'Panache' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblPanacheDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'Dharm' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblDharmDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'YDVash' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblYDVashDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'HariKrishna' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblHariKrishnaDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'FineStar' AS TableType,dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) AS Amount FROM tblFineStarDiamond  WHERE LAB ='GIA' ");

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'CDINESH' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblCDINESHDiamond  WHERE LAB ='GIA' ");

            

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'KAPUGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKapuGemsDiamond   WHERE LAB ='GIA' ");

            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'KIRANGEMS' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblKiranGemsDiamond   WHERE LAB ='GIA' ");


            sqlQuery.Append(" union all ");
            sqlQuery.Append(" SELECT *,'Venus' AS TableType,dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') AS Amount FROM tblVenusJewelsDiamond  WHERE LAB ='GIA' ");


            sqlQuery.Append(" )Tmp ");

            SearchCondition(objDiamondModels, sqlQuery);

            sqlQuery.Append(" )T1 ");

            sqlQuery.Append(" WHERE T1.Sno >((" + objDiamondModels.PageNumber + " - 1) * " + objDiamondModels.PageSize + ")");
            sqlQuery.Append(" AND  T1.Sno <= (" + objDiamondModels.PageNumber + " * " + objDiamondModels.PageSize + ")");
        }

        private static void SearchCondition(DiamondModels objDiamondModels, StringBuilder sqlQuery)
        {
            sqlQuery.Append("	WHERE 1=1 ");
            //sqlQuery.Append(" AND LAB ='GIA' ");

            if (objDiamondModels.Shape != "''")
            {
                sqlQuery.Append(" AND Shape IN (" + objDiamondModels.Shape + ") ");
            }

            if (objDiamondModels.Shape.ToUpper() == "ROUND")
            {
                sqlQuery.Append(" AND REPLACE(Cut,'+','') IN (" + objDiamondModels.Cut + ") ");
            }


            sqlQuery.Append(" AND Carat BETWEEN " + objDiamondModels.CaratMin + " AND " + objDiamondModels.CaratMax + " ");



            if (objDiamondModels.Shape.ToUpper().Contains("ROUND"))
            {
                sqlQuery.Append(" AND ISNULL(Cut,'Good') IN (" + objDiamondModels.Cut + ") ");
            }




            sqlQuery.Append(" AND ISNULL(Fluorescence,'') IN (" + objDiamondModels.Flourescence + ") ");

            if (Convert.ToBoolean(objDiamondModels.EyeClean))
            {


                if (!String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
                {
                    sqlQuery.Append(" AND ((REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyLeft + ") and ISNULL(EyeClean, '') IN ('yes') AND 'TableType'!='R') ");
                    sqlQuery.Append(" or (REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyRight + ") and ISNULL(EyeClean, '') IN ('yes','e0',''))) ");
                }
                else
                {
                    sqlQuery.Append(" AND REPLACE(Clarity,'+','') IN (" + objDiamondModels.Clartiy + ") ");
                    if (Convert.ToBoolean(objDiamondModels.EyeClean))
                    {
                        sqlQuery.Append(" AND ISNULL(EyeClean,'yes') IN ('yes','e0','') ");
                    }
                    else
                    {
                        //eye clean yes including as per suggested by jaennie -- https://newmediaguru.basecamphq.com/projects/13179000-canturi-com-czz01_01/posts/99534986/comments#comment_362183858
                        sqlQuery.Append(" AND ISNULL(EyeClean,'') IN ('yes','no','') ");

                    }
                }



            }
            else
            {
                sqlQuery.Append(" AND ((REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyLeft + ") and ISNULL(EyeClean, '') IN ('yes','no','')) ");
                sqlQuery.Append(" or (REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyRight + ") and ISNULL(EyeClean, '') IN ('yes','e0',''))) ");
            }





            sqlQuery.Append(" AND Polish IN (" + objDiamondModels.Polish + ") ");
            sqlQuery.Append(" AND dbo.Udf_GetPrice(Price,'" + UserSessionData.Currency.ToUpper() + "') BETWEEN " + objDiamondModels.PriceMin + " AND " + objDiamondModels.PriceMax + " ");

            sqlQuery.Append(" AND REPLACE(Color,'+','') IN (" + objDiamondModels.Color + ") ");

            sqlQuery.Append(" AND LEN(Color) IN (1, 2)");

            sqlQuery.Append(" AND Symmetry IN (" + objDiamondModels.Symmetry + ") ");
            sqlQuery.Append(" AND Amount != 0 ");





            sqlQuery.Append(" AND ISNULL(Cut,'') NOT LIKE '%fair%'");
            sqlQuery.Append(" AND ISNULL(Cut,'') NOT LIKE '%poor%'");

            sqlQuery.Append(" AND Amount BETWEEN @MarkupMinPrice AND @MarkupMaxPrice");
            sqlQuery.Append(" AND IsDiamondOrderd=0");


            //else
            //{

            //}
            //if (objDiamondModels.EyeClean == "true")
            //{
            //    sqlQuery.Append(" AND ISNULL(EyeClean,'yes') IN ('yes','e0','') ");
            //}
            //else
            //{
            //    sqlQuery.Append(" AND ISNULL(EyeClean,'yes') IN ('no','e1','e2') ");
            //}
        }

        private static void NewSearchCondition(DiamondModels objDiamondModels, StringBuilder sqlQuery)
        {
            sqlQuery.Append("	WHERE 1=1 ");
            //sqlQuery.Append(" AND LAB ='GIA' ");

            if (objDiamondModels.Shape != "''")
            {
                sqlQuery.Append(" AND Shape IN (" + objDiamondModels.Shape + ") ");
            }

            if (objDiamondModels.Shape.ToUpper() == "ROUND")
            {
                sqlQuery.Append(" AND REPLACE(Cut,'+','') IN (" + objDiamondModels.Cut + ") ");
            }


            sqlQuery.Append(" AND Carat BETWEEN " + objDiamondModels.CaratMin + " AND " + objDiamondModels.CaratMax + " ");



            if (objDiamondModels.Shape.ToUpper().Contains("ROUND"))
            {
                sqlQuery.Append(" AND ISNULL(Cut,'Good') IN (" + objDiamondModels.Cut + ") ");
            }




            sqlQuery.Append(" AND ISNULL(Fluorescence,'') IN (" + objDiamondModels.Flourescence + ") ");

            if (Convert.ToBoolean(objDiamondModels.EyeClean))
            {


                if (!String.IsNullOrEmpty(objDiamondModels.ClartiyLeft))
                {
                    sqlQuery.Append(" AND ((REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyLeft + ") and ISNULL(EyeClean, '') IN ('yes') AND 'TableType'!='R') ");
                    sqlQuery.Append(" or (REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyRight + ") and ISNULL(EyeClean, '') IN ('yes','e0',''))) ");
                }
                else
                {
                    sqlQuery.Append(" AND REPLACE(Clarity,'+','') IN (" + objDiamondModels.Clartiy + ") ");
                    if (Convert.ToBoolean(objDiamondModels.EyeClean))
                    {
                        sqlQuery.Append(" AND ISNULL(EyeClean,'yes') IN ('yes','e0','') ");
                    }
                    else
                    {
                        //eye clean yes including as per suggested by jaennie -- https://newmediaguru.basecamphq.com/projects/13179000-canturi-com-czz01_01/posts/99534986/comments#comment_362183858
                        sqlQuery.Append(" AND ISNULL(EyeClean,'') IN ('yes','no','') ");

                    }
                }



            }
            else
            {
                sqlQuery.Append(" AND ((REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyLeft + ") and ISNULL(EyeClean, '') IN ('yes','no','')) ");
                sqlQuery.Append(" or (REPLACE(Clarity, '+', '') IN (" + objDiamondModels.ClartiyRight + ") and ISNULL(EyeClean, '') IN ('yes','e0',''))) ");
            }





            sqlQuery.Append(" AND Polish IN (" + objDiamondModels.Polish + ") ");
            sqlQuery.Append(" AND dbo.Udf_GetPriceCalculation(Price,'" + UserSessionData.Currency.ToUpper() + "',Carat) BETWEEN " + objDiamondModels.PriceMin + " AND " + objDiamondModels.PriceMax + " ");

            sqlQuery.Append(" AND REPLACE(Color,'+','') IN (" + objDiamondModels.Color + ") ");

            sqlQuery.Append(" AND LEN(Color) IN (1, 2)");

            sqlQuery.Append(" AND Symmetry IN (" + objDiamondModels.Symmetry + ") ");
            sqlQuery.Append(" AND Amount != 0 ");





            sqlQuery.Append(" AND ISNULL(Cut,'') NOT LIKE '%fair%'");
            sqlQuery.Append(" AND ISNULL(Cut,'') NOT LIKE '%poor%'");

            sqlQuery.Append(" AND Amount BETWEEN @MarkupMinPrice AND @MarkupMaxPrice");
            sqlQuery.Append(" AND IsDiamondOrderd=0");



        }




        public DataSet GetDiamondSearch(DiamondModels objDiamondModels)
        {
            try
            {
                SqlParameter prmConsultantId = SqlHelper.CreateParameter("@ConsultantId", objDiamondModels.AdminUserId);
                SqlParameter prmDiamondID = SqlHelper.CreateParameter("@DiamondID", objDiamondModels.DiamondId);
                SqlParameter prmSkuDiamondID = SqlHelper.CreateParameter("@SkuDiamondID", objDiamondModels.SkuDiamondId);
                SqlParameter prmDiamondImage = SqlHelper.CreateParameter("@DiamondImage", objDiamondModels.DiamondImage);
                SqlParameter prmLotNumber = SqlHelper.CreateParameter("@LotNumber", objDiamondModels.LotNumber);
                SqlParameter prmShape = SqlHelper.CreateParameter("@Shape", objDiamondModels.Shape);


                SqlParameter prmCaratMin = SqlHelper.CreateParameter("@CaratMin", objDiamondModels.CaratMin);
                SqlParameter prmCaratMax = SqlHelper.CreateParameter("@CaratMax", objDiamondModels.CaratMax);

                SqlParameter prmColor = SqlHelper.CreateParameter("@Color", objDiamondModels.Color);
                SqlParameter prmColorMin = SqlHelper.CreateParameter("@ColorMin", objDiamondModels.ColorMin);
                SqlParameter prmColorMax = SqlHelper.CreateParameter("@ColorMax", objDiamondModels.ColorMax);

                SqlParameter prmClarity = SqlHelper.CreateParameter("@Clarity", objDiamondModels.Clartiy);
                SqlParameter prmClarityMin = SqlHelper.CreateParameter("@ClarityMin", objDiamondModels.ClartiyMin);
                SqlParameter prmClarityMax = SqlHelper.CreateParameter("@ClarityMax", objDiamondModels.ClartiyMax);

                SqlParameter prmCut = SqlHelper.CreateParameter("@Cut", objDiamondModels.Cut);
                SqlParameter prmCutMin = SqlHelper.CreateParameter("@CutMin", objDiamondModels.CutMin);
                SqlParameter prmCutMax = SqlHelper.CreateParameter("@CutMax", objDiamondModels.CutMax);

                SqlParameter prmPolish = SqlHelper.CreateParameter("@Polish", objDiamondModels.Polish);
                SqlParameter prmPolishMin = SqlHelper.CreateParameter("@PolishMin", objDiamondModels.PolishMin);
                SqlParameter prmPolishMax = SqlHelper.CreateParameter("@PolishMax", objDiamondModels.PolishMax);

                SqlParameter prmSymmetry = SqlHelper.CreateParameter("@Symmetry", objDiamondModels.Symmetry);
                SqlParameter prmSymmetryMin = SqlHelper.CreateParameter("@SymmetryMin", objDiamondModels.SymmetryMin);
                SqlParameter prmSymmetryMax = SqlHelper.CreateParameter("@SymmetryMax", objDiamondModels.SymmetryMax);

                SqlParameter prmFluorescence = SqlHelper.CreateParameter("@Fluorescence", objDiamondModels.Flourescence);
                SqlParameter prmFluorescenceMin = SqlHelper.CreateParameter("@FluorescenceMin", objDiamondModels.FlourescenceMin);
                SqlParameter prmFluorescenceMax = SqlHelper.CreateParameter("@FluorescenceMax", objDiamondModels.FlourescenceMax);


                SqlParameter prmDepth = SqlHelper.CreateParameter("@Depth", objDiamondModels.DepthMin);
                SqlParameter prmTable = SqlHelper.CreateParameter("@Table", objDiamondModels.TableMin);



                SqlParameter prmMeasurements = SqlHelper.CreateParameter("@Measurements", objDiamondModels.Measurements);
                SqlParameter prmRatio = SqlHelper.CreateParameter("@Ratio", objDiamondModels.Ratio);

                SqlParameter prmLAB = SqlHelper.CreateParameter("@LAB", objDiamondModels.Lab);



                SqlParameter prmPriceMin = SqlHelper.CreateParameter("@PriceMin", objDiamondModels.PriceMin);
                SqlParameter prmPriceMax = SqlHelper.CreateParameter("@PriceMax", objDiamondModels.PriceMax);

                SqlParameter prmDiamondCertificate = SqlHelper.CreateParameter("@DiamondCertificate", objDiamondModels.DiamondCertificate);

                SqlParameter prmCurrency = SqlHelper.CreateParameter("@Currency", objDiamondModels.Currency);

                SqlParameter prmSortByColumn = SqlHelper.CreateParameter("@SortByColumn", objDiamondModels.SortByColumn);
                SqlParameter prmSortOrder = SqlHelper.CreateParameter("@SortOrder", objDiamondModels.SortOrder);

                SqlParameter prmCreatedBy = SqlHelper.CreateParameter("@CreatedBy", objDiamondModels.CreatedBy);
                SqlParameter prmCreatedFromIp = SqlHelper.CreateParameter("@CreatedFromIp", objDiamondModels.CreatedFromIp);
                SqlParameter prmFlag = SqlHelper.CreateParameter("@Flag", objDiamondModels.Flag);
                SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);

                SqlParameter prmPgNumber = SqlHelper.CreateParameter("@PgNumber", objDiamondModels.PageNumber);
                SqlParameter prmPgSize = SqlHelper.CreateParameter("@PgSize", objDiamondModels.PageSize);
                SqlParameter prmTotalRecords = SqlHelper.CreateParameter("@TotalRecords", -1, ParameterDirection.Output);


                SqlParameter[] allParams = {
                                                prmConsultantId,prmDiamondID, prmSkuDiamondID, prmDiamondImage, 
                                                prmLotNumber, prmShape,prmCaratMin,prmCaratMax, 
                                                prmColorMin,prmColorMax, prmClarityMin,prmClarityMax,prmCutMin,prmCutMax, 
                                                prmPolishMin,prmPolishMax, prmSymmetryMin,prmSymmetryMax,prmFluorescenceMin,prmFluorescenceMax, 
                                                prmDepth, prmTable, prmMeasurements,  
                                                prmRatio,prmLAB,  prmPriceMin,prmPriceMax,
                                                prmDiamondCertificate,prmCurrency,
                                                prmSortByColumn,prmSortOrder,
                                                prmCreatedBy, prmCreatedFromIp,  
                                                prmFlag, prmErr ,prmPgNumber,prmPgSize,prmTotalRecords
                                           };
                return SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetSearchDiamond", allParams);

            }
            catch(Exception ex)
            {
                return new DataSet();
            }

        }

        public List<DiamondModels> GetDiamonds(DiamondModels objDiamondModels)
        {
            List<DiamondModels> listDiamondModels = new List<DiamondModels>();
            try
            {

                DataSet dsDiamondModels = GetDiamondSearch(objDiamondModels);

                if (dsDiamondModels != null)
                {
                    if (dsDiamondModels.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow item in dsDiamondModels.Tables[0].Rows)
                        {
                            listDiamondModels.Add(new DiamondModels
                            {
                                //ConsultantId = Convert.ToInt64(item["ConsultantId"].ToString()),
                                //ConsultantName = item["ConsultantName"].ToString()
                                DiamondId = item["DiamondID"].ToString(),
                                SkuDiamondId = item["SkuDiamondID"].ToString(),
                                DiamondImage = item["DiamondImage"].ToString(),
                                LotNumber = item["LotNumber"].ToString(),
                                Shape = item["Shape"].ToString(),
                                CaratMin = item["Carat"].ToString(),
                                ColorMin = item["Color"].ToString(),
                                ClartiyMin = item["Clarity"].ToString(),
                                CutMin = item["Cut"].ToString(),
                                PolishMin = item["Polish"].ToString(),
                                SymmetryMin = item["Symmetry"].ToString(),
                                FlourescenceMin = item["Fluorescence"].ToString(),
                                DepthMin = item["Depth"].ToString(),
                                TableMin = item["Table"].ToString(),
                                Measurements = item["Measurements"].ToString(),
                                Ratio = item["Ratio"].ToString(),
                                Lab = item["LAB"].ToString(),
                                PriceMin = item["Price"].ToString(),
                                DiamondCertificate = item["DiamondCertificate"].ToString(),
                                CreatedOn = item["CreatedOn"].ToString()
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

        public static bool ShowHideDiamondVideo(string SupplierCode, List<string> AllowedSuppliers)
        {
            try
            {
                return AllowedSuppliers.Contains(SupplierCode.ToLower());
            }
            catch
            { return false; }
        }

        public static List<string> GetVideoAllowedSuppliers()
        {
            List<string> lstIds = new List<string>();
            DataTable dt = new DataTable();
            SqlParameter paraFlag = SqlHelper.CreateParameter("@Flag", 2);
            SqlParameter prmErr = SqlHelper.CreateParameter("@Err", -1, ParameterDirection.Output);
            SqlParameter[] Allpara = { paraFlag, prmErr };
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetSuppliers", Allpara);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                lstIds.Add(Convert.ToString(dr["SupplierId"]));
            }
             return lstIds;
        }
    }
}
