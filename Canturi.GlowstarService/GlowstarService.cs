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

namespace Canturi.GlowstarService
{
    public partial class GlowstarService : ServiceBase
    {
        private Timer timer = null;
        public GlowstarService()
        {
            Glowstar.LogError("Error Timer Handler - " + DateTime.Now.ToString() + " - Service start");


            InitializeComponent();
            try
            {
                double Interval = 1000*60;
                timer = new Timer(Interval);
                // This Event Handler Call ServiceTimer Method.
                timer.Elapsed += new ElapsedEventHandler(this.ServiceTimer_Tick);
                timer.Interval = Interval; // Run service every 1 minute ///

            }
            catch (Exception ex)
            {
                Glowstar.LogError("Error Component Initialization - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                //StreamWriter str1 = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //str1.WriteLine("Error Component Initialization - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                //str1.Close();
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
                Glowstar.LogError("Error Timer On Start - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());

                //StreamWriter str2 = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //str2.WriteLine("Error Timer On Start - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.StackTrace.ToString());
                //str2.Close();
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
                DateTime StartTime = Convert.ToDateTime(ConfigurationManager.AppSettings["StartTime"].ToString());//Convert.ToDateTime("11:27");
                                                                                                                  //if (String.Format("{0: hh mm}", StartTime) == String.Format("{0: hh mm}", DateTime.Now))

                Glowstar.LogError("ServiceTimer_Tick - " + DateTime.Now.ToString() + " - ServiceTimer_Tick - ");
                
                if (String.Format("{0: hh mm tt}", StartTime).Replace(" ", "") == String.Format("{0: hh mm tt}", DateTime.Now).Replace(" ", ""))
                {
                    Glowstar objDiamond = new Glowstar();
                    objDiamond.CGlowstarDiamond();
                }
                this.timer.Start();
            }
            catch (Exception ex)
            {
                Glowstar.LogError("Error Timer Handler - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());

                //StreamWriter str = new StreamWriter(@"" + ConfigurationManager.AppSettings["ErrorFilePath"].ToString() + "", true);
                //str.WriteLine("Error Timer Handler - " + DateTime.Now.ToString() + " - " + ex.Message.ToString() + " - " + ex.Source.ToString());
                //str.Close();
                //str.Dispose();
            }
        }
    }
}
