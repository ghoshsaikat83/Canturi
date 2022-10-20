using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessHelper.CommonHelper
{
    public static class PagerHelper
    {
        public static string DisplayPaging(int totalRowsCount, int pageSize, int pageNo, string pageUrl, int status)
        {

            StringBuilder sb = new StringBuilder();
            int totalPage = 1;
            int currentPageNo = pageNo;

            int pageCount = totalRowsCount / pageSize;

            totalPage = (int)((totalRowsCount % pageSize == 0) ? pageCount : pageCount + 1);
            if (totalRowsCount < pageSize)
            {
                totalPage = totalPage - 1;
            }


            if (totalPage > 1)
            {
                int start = (pageNo - 1) * pageSize + 1;
                int end = (((pageNo - 1) * pageSize + 1) + pageSize) - 1;

                sb.Append("<div class='result-nav'>");
                //sb.Append("<div class='listing'>" + start.ToString() + " – " + end.ToString() + " of " + totalRowsCount.ToString() + " listings</div>");

                //sb.Append("<div class='pagination-wrap'>");
                //sb.Append("<div class='listing'></div>");
                sb.Append("<ul>");                
                sb.Append(AddPrevPage(totalPage, pageNo, currentPageNo, pageUrl, status));
                if ((totalPage - pageNo) < 10)
                {
                    if (totalPage < 10)
                    {
                        for (int i = 0, j = totalPage - 1; i < totalPage; i++, j--)
                        {
                            //if (i == (totalPage - 1))
                            //{
                            sb.Append(AddPage((totalPage - j), (totalPage - j).ToString(), true, currentPageNo, pageUrl, status));
                            //}
                            //else
                            //{
                            //    sb.Append(AddPage((totalPage - j), (totalPage - j).ToString(), true, currentPageNo));
                            //}
                        }
                    }
                    else
                    {
                        sb.Append(AddPage(totalPage - 9, (totalPage - 9).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 8, (totalPage - 8).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 7, (totalPage - 7).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 6, (totalPage - 6).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 5, (totalPage - 5).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 4, (totalPage - 4).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 3, (totalPage - 3).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 2, (totalPage - 2).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage - 1, (totalPage - 1).ToString(), true, currentPageNo, pageUrl, status));
                        sb.Append(AddPage(totalPage, totalPage.ToString(), true, currentPageNo, pageUrl, status));
                    }
                }
                else
                {
                    sb.Append(AddPage(pageNo, pageNo.ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 1, (pageNo + 1).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 2, (pageNo + 2).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 3, (pageNo + 3).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 4, (pageNo + 4).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 5, (pageNo + 5).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 6, (pageNo + 6).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 7, (pageNo + 7).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 8, (pageNo + 8).ToString(), true, currentPageNo, pageUrl, status));
                    sb.Append(AddPage(pageNo + 9, (pageNo + 9).ToString(), true, currentPageNo, pageUrl, status));

                }
                sb.Append(AddNextPage(totalPage, pageNo, currentPageNo, pageUrl, status));



                sb.Append("</ul>");
                //sb.Append("</div>");
                sb.Append("</div>");
            }
            else
            {
                sb.Clear();
            }

            return sb.ToString();
        }

        private static string AddPrevPage(int totalPage, int pageNo, int currentPageNo, string pageUrl, int status)
        {
            string prevPager = "";
            if (pageNo == 1)
            {
                prevPager = AddPage(1, "<", false, currentPageNo, pageUrl, status);
                prevPager = prevPager + AddPage(1, "First", false, currentPageNo, pageUrl, status);
            }
            else
            {
                prevPager = AddPage(1, "<< FIRST", true, currentPageNo, pageUrl, status);
                prevPager = prevPager + AddPage(pageNo - 1, "<", true, currentPageNo, pageUrl, status);

                
            }
            return prevPager;
        }

        private static string AddNextPage(int totalPage, int pageNo, int currentPageNo, string pageUrl, int status)
        {
            string nextPager = "";
            if (pageNo == totalPage)
            {
                // nextPager = "<a class='next' href='javascript:void(0)'  onclick=\"javascript:return GetSearchData(" + pageNo.ToString() + ");\" >NEXT >></a>";
                //nextPager = AddPage(totalPage, "NEXT >>", false, currentPageNo);
            }
            else
            {
                nextPager = "<li><a class='next' href='javascript:void(0)'  onclick=\"javascript:return GetSearchData(" + (pageNo + 1).ToString() + "," + status + ");\" >></a></li>";
                nextPager = nextPager + "<li><a class='next' href='javascript:void(0)'  onclick=\"javascript:return GetSearchData(" + (totalPage).ToString() + "," + status + ");\" >LAST >></a></li>";
                //nextPager = "<li><a class='next' href='" + pageUrl + (pageNo + 1).ToString() + "'  >NEXT >></a></li>";

                //nextPager = AddPage(pageNo + 1, "NEXT >>", true, currentPageNo);
            }
            return nextPager;
        }

        private static string AddPage(int pageNo, string dispText, bool addLink, int currentPageNo, string pageUrl, int status)
        {
            StringBuilder sb = new StringBuilder();

            if (addLink)
            {
                sb.Append("<li>");
                if (currentPageNo == pageNo)
                {
                    sb.Append("<a  class='active' style='cursor:default;' href='javascript:void(0)' onclick=\"javascript:return GetSearchData(" + pageNo.ToString() + "," + status + ");\">" + dispText.ToString() + "</a>");
                    //sb.Append("<a  class='active' style='cursor:default;' href='" + pageUrl + pageNo.ToString() + "' >" + dispText.ToString() + "</a>");
                }
                else
                {
                    sb.Append("<a href='javascript:void(0)'  onclick=\"javascript:return GetSearchData(" + pageNo.ToString() + "," + status + ");\" >" + dispText.ToString() + "</a>");
                    //sb.Append("<a href='" + pageUrl + pageNo.ToString() + "' >" + dispText.ToString() + "</a>");
                }
                sb.Append("</li>");
            }
            return sb.ToString();
        }

    }
}
