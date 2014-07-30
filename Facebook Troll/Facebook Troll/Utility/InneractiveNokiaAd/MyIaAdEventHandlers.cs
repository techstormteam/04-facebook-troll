
using Inneractive.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.InneractiveNokiaAd
{
    class MyIaAdEventHandlers : IaAdEventHandlers
    {
        public override void AdFailedEventHandler(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd.DisplayAd: AdFailed");
        }

        public override void AdReceivedEventHandler(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd.DisplayAd: AdReceived");
        }

        public override void DefaultAdReceivedEventHandler(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd.DisplayAd: DefaultAdReceived");
        }

        public override void AdClickedEventHandler(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd.DisplayAd: AdClicked");
        }

        public MyIaAdEventHandlers()
        {
        }
    }
}
