using GoogleAds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KaraokeList2016.Utility.GoogleAd
{
    public class GoogleAdHelper
    {
        private static InterstitialAd interstitialAd;

        public static void DisplayInterstitial(string adUnitId) 
        {
            // NOTE: Edit "MY_AD_UNIT_ID" with your interstitial
            // ad unit id.
            interstitialAd = new InterstitialAd(adUnitId);
            // NOTE: You can edit the event handler to do something custom here. Once the
            // interstitial is received it can be shown whenever you want.
            interstitialAd.ReceivedAd += OnAdReceived;
            interstitialAd.FailedToReceiveAd += OnFailedToReceiveAd;
            interstitialAd.DismissingOverlay += OnDismissingOverlay;
            AdRequest adRequest = new AdRequest();
            adRequest.ForceTesting = true;
            interstitialAd.LoadAd(adRequest);
            //interstitialAd.ShowAd();
        }

        public static void DisplayBanner(Grid LayoutRoot, string adUnitId)
        {
            // NOTE: Edit "MY_AD_UNIT_ID" with your ad unit id.
            AdView bannerAd = new AdView
            {
                Format = AdFormats.Banner,
                AdUnitID = adUnitId
            };
            bannerAd.ReceivedAd += OnAdReceived;
            bannerAd.FailedToReceiveAd += OnFailedToReceiveAd;
            LayoutRoot.Children.Add(bannerAd);
            AdRequest adRequest = new AdRequest();
            adRequest.ForceTesting = true;
            bannerAd.LoadAd(adRequest);
        }

        private static void OnAdReceived(object sender, AdEventArgs e)
        {
            Debug.WriteLine("Received interstitial successfully. Click 'Show Interstitial'.");
        }

        private static void OnDismissingOverlay(object sender, AdEventArgs e)
        {
            Debug.WriteLine("Dismissing interstitial.");
        }

        private static void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {
            Debug.WriteLine("Failed to receive interstitial with error " + errorCode.ErrorCode);
        }
    }
}
