using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.SunriseService
{
    public class Diamond
    {
        int DiamondCount;
        #region Private Variable
        string _constring = string.Empty;
        #endregion

        public Diamond()
        {
            try
            {
                _constring = ConfigurationSettings.AppSettings["CanturiConnectionStr"].ToString();
            }
            catch
            {
                throw new ApplicationException("Sorry, connection to database could not made");
            }
        }

        public void SunriseDiamond()
        {
            try
            {
                string jsonData = "";
                using (WebClient wc = new WebClient())
                {
                    jsonData = wc.DownloadString(ConfigurationSettings.AppSettings["SunriseApiUrl"].ToString());
                }
                if (!String.IsNullOrEmpty(jsonData))
                {
                    //JsonFolderPath
                    string fileName = Guid.NewGuid().ToString()+".json";
                    System.IO.File.WriteAllText(ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName, jsonData);
                    LogError("Json file save: " + ConfigurationSettings.AppSettings["JsonFolderPath"].ToString() + fileName);
                }
            }
            catch (Exception ex)
            {

                LogError(ex.Message);
            }
        }


        public static void LogError(string error)
        {
            StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
            str.WriteLine(error);
            str.Close();
            str.Dispose();
        }
    }
}
