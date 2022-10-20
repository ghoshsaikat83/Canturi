using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Canturi.JBBrosDataService
{
    public partial class JBBrosService : ServiceBase
    {
        private Timer timer = null;
        public JBBrosService()
        {
            InitializeComponent();
            try
            {
                //string ServiceTimeInterval = ConfigurationSettings.AppSettings["ServiceTimeInterval"].ToString();
                // string ServiceTimeInterval= ConfigurationManager.AppSettings["ServiceTimeInterval"].ToString();
                double Interval = 1000;
                // double Interval = Convert.ToDouble(ServiceTimeInterval);
                timer = new Timer(Interval);
                // This Event Handler Call ServiceTimer Method.
                timer.Elapsed += new ElapsedEventHandler(this.ServiceTimer_Tick);

            }
            catch (Exception ex)
            {
                StreamWriter str1 = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                str1.WriteLine("Error Component Initialization - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                str1.Close();
            }
            Diamond objDiamond = new Diamond();
            objDiamond.JBBrosDiamond();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer.AutoReset = true;
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                StreamWriter str2 = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                str2.WriteLine("Error Timer On Start - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                str2.Close();
            }

        }

        protected override void OnStop()
        {
            timer.AutoReset = false;
            timer.Enabled = false;
            timer.Stop();
        }

        private void ServiceTimer_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.timer.Stop();
                DateTime StartTime = Convert.ToDateTime(ConfigurationSettings.AppSettings["StartTime"].ToString());//Convert.ToDateTime("11:27");
                if (String.Format("{0: hh mm tt}", StartTime).Replace(" ", "") == String.Format("{0: hh mm tt}", DateTime.Now).Replace(" ", ""))
                {
                    Diamond.LogError("ServiceTimer_Tick - " + DateTime.Now.ToString() + " - ServiceTimer_Tick - ");
                    Diamond objDiamond = new Diamond();
                    objDiamond.JBBrosDiamond();
                }
                this.timer.Start();
            }
            catch (Exception ex)
            {
                StreamWriter str = new StreamWriter(@"" + ConfigurationSettings.AppSettings["ErrorFilePath"].ToString() + "", true);
                str.WriteLine("Error Timer Handler - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
                str.Close();
                str.Dispose();
            }
        }
    }
}
