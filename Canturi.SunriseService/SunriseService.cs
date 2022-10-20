using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Canturi.SunriseService
{
    public partial class SunriseService : ServiceBase
    {
        private Timer timer = null;
        public SunriseService()
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
                DiamondHelper.LogError("Error Component Initialization - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
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
                DiamondHelper.LogError("Error Timer On Start - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
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
                    DiamondHelper.LogError("ServiceTimer_Tick - " + DateTime.Now.ToString() + " - ServiceTimer_Tick - ");
                    DiamondHelper objDiamond = new DiamondHelper();
                    objDiamond.SunriseDiamond();
                }
                this.timer.Start();
            }
            catch (Exception ex)
            {
                DiamondHelper.LogError("Error Timer Handler - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
            }
        }
    }
}
