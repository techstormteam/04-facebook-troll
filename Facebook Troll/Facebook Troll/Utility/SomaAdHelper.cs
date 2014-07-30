using SOMAWP8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class SomaAdHelper
    {
        public static int PUB_ID = 923874581;
        public static int ADSPACE_ID = 65822408;

        //public static int PUB_ID = 0;
        //public static int ADSPACE_ID = 0;

        public static void startAd(SomaAdViewer somaAdViewer)
        {
            somaAdViewer.Pub = PUB_ID;       // Developer pub ID for testing
            somaAdViewer.Adspace = ADSPACE_ID;   // Developer adSpace ID for testing
            somaAdViewer.AdInterval = 60;
            somaAdViewer.StartAds();
        }

        public static void stopAd(SomaAdViewer somaAdViewer)
        {
            somaAdViewer.StopAds();
        }
    }
}
