using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.KiranService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new KiranService()
            };
            ServiceBase.Run(ServicesToRun);
            
            // uncomment to run service to get all data in csv
            /*
            DiamondHelper objHelper = new DiamondHelper();
            objHelper.KiranDiamond();
            */
        }
    }
}
