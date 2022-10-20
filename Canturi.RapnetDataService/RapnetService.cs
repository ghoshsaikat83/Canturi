using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using System.Configuration;

namespace Canturi.RapnetDataService
{
    public partial class RapnetService : ServiceBase
    {
        private Timer timer = null;
        public RapnetService()
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
                Diamond.LogError("Error Component Initialization - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
            }
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
                Diamond.LogError("Error Timer On Start - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
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
                    objDiamond.CanturiDaimond();
                }
                this.timer.Start();
            }
            catch (Exception ex)
            {
                Diamond.LogError("Error Timer Handler - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
            }
        }
    }
}
