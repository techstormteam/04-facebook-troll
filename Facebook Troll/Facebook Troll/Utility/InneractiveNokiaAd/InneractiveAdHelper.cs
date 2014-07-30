using Inneractive.Nokia.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Utility.InneractiveNokiaAd
{
    public class InneractiveAdHelper
    {

        public static Dictionary<InneractiveAd.IaOptionalParams, string> OptionalParams = CreateOptionalParams();

        public static string appId = "ThienAu_FacebookPro2015_WP";

        public static void CreateInneractiveAd(Grid grid)
        {
            InneractiveAd iaBanner = new InneractiveAd(appId, InneractiveAd.IaAdType.IaAdType_Banner, 60, OptionalParams);

            // Add event listeners to the InneractiveAd
            iaBanner.AdReceived += new InneractiveAd.IaAdReceived(InneractiveAd_AdReceived);
            iaBanner.AdFailed += new InneractiveAd.IaAdFailed(InneractiveAd_AdFailed);
            iaBanner.DefaultAdReceived += new InneractiveAd.IaDefaultAdReceived(InneractiveAd_DefaultAdReceived);
            iaBanner.AdClicked += new InneractiveAd.IaAdClicked(InneractiveAd_AdClicked);
            grid.Children.Add(iaBanner);
        }

        public static void DisplayTextAd(Grid grid, IaAdEventHandlers eventHandlers)
        {
            // Display text ad in the TextAdGrid
            // In order to maximize your revenues, we recommend allowing the application usage only if the returned value is true:
            // PAY ATTENTION:   DisplayAd() checks for connectivity, first of all,
            //                  but GetIsNetworkAvailable() always returns true at the emulator
            //if (!InneractiveAd.DisplayAd("MyCompany_MyApp", InneractiveAd.IaAdType.IaAdType_Text, GetGrid(), 60, optionalParams))
            //{
            //   MessageBox.Show("This application is free but requires an internet connection. Please configure your connectivity settings and re-try.");
            //    //NavigationService.GoBack();
            //}

            // When using the DisplayAd method you may choose to send event handlers, so you may catch the ad's events
            if (!InneractiveAd.DisplayAd(appId, InneractiveAd.IaAdType.IaAdType_Text, grid, 60, OptionalParams, eventHandlers))
            {
                System.Diagnostics.Debug.WriteLine("This application is free but requires an internet connection. Please configure your connectivity settings and re-try.");
                //NavigationService.GoBack();
            }
        }

        public static void DisplayInterstitial(Grid layoutRoot)
        {
            // Display interstitial ad using the static DisplayAd method
            // In order to maximize your revenues, we recommend allowing the application usage only if the returned value is true:
            // PAY ATTENTION:   DisplayAd() checks for connectivity, first of all,
            //                  but GetIsNetworkAvailable() always returns true at the emulator
            if (!InneractiveAd.DisplayAd(appId, InneractiveAd.IaAdType.IaAdType_Interstitial, layoutRoot, 0))
            {
                System.Diagnostics.Debug.WriteLine("This application is free but requires an internet connection. Please configure your connectivity settings and re-try.");
                //NavigationService.GoBack();
            }
        }

        public static Dictionary<InneractiveAd.IaOptionalParams, string> CreateOptionalParams()
        {
            // Watch location
            IaLocationClass iaLocation = new IaLocationClass();
            iaLocation.Done += new System.EventHandler<IaLocationEventArgs>(iaLocation_Done);
            iaLocation.StartWatchLocation();

            /**
            * Optional parameters:
            * Age - User’s age
            * Gender - User’s gender (allowed values: M, m, F, f, Male, Female)
            * Keywords - Keywords relevant to this user’s specific session (comma separated)
            * Ad alignment - Alignment of the ad within the grid
            * Optional size - optional ad size
            * Required size - required ad size
            */
            Dictionary<InneractiveAd.IaOptionalParams, string>  optionalParams = new Dictionary<InneractiveAd.IaOptionalParams, string>();
            try
            {
                optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Age, "25");
                optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Gender, "m");
                optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Keywords, "test,inneractive");
                optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Ad_Alignment, InneractiveAd.IaAdAlignment.CENTER.ToString());
                optionalParams.Add(InneractiveAd.IaOptionalParams.Key_OptionalAdWidth, "320");
                optionalParams.Add(InneractiveAd.IaOptionalParams.Key_OptionalAdHeight, "53");
                //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_RequiredAdWidth, "320");
                //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_RequiredAdHeight, "53");

                // Location hard coded values, for example:
                // optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Gps_Coordinates, "53.5422,-2.2396");
                // optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Location, "US,NY,NY");

            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("InneractiveAd: Dictionary error - Make sure there is only one value per key");
            }
            return optionalParams;
        }

        public static void iaLocation_Done(object sender, IaLocationEventArgs e)
        {
            try
            {
                // Add location, if received
                if (e != null && e.location != null)
                    OptionalParams.Add(InneractiveAd.IaOptionalParams.Key_Gps_Coordinates, e.location);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("InneractiveAd: Error: " + ex.ToString());
            }
        }

        public static void InneractiveAd_AdReceived(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd: AdReceived");
        }

        public static void InneractiveAd_DefaultAdReceived(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd: DefaultAdReceived");
        }

        public static void InneractiveAd_AdFailed(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd: AdFailed");
        }

        public static void InneractiveAd_AdClicked(object sender)
        {
            System.Diagnostics.Debug.WriteLine("InneractiveAd: AdClicked");
        }

    }
}
