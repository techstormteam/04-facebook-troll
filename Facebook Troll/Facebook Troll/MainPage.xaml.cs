using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Facebook_Troll.Resources;
using Utility;
using Utility.InneractiveNokiaAd;
using KaraokeList2016.Utility.GoogleAd;

namespace Facebook_Troll
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static string homeUrl = "https://m.facebook.com/";
        private static string messageUrl = "https://m.facebook.com/messages/";
        private static string friendUrl = "https://m.facebook.com/friends/center/requests/?mff_nav=1";
        private static string notificationUrl = "https://m.facebook.com/notifications.php?more";

        private WebBrowserHelper wbBackKeyPress = new WebBrowserHelper();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public void GotoPage(string pageName)
        {
            this.NavigationService.Navigate(new Uri("/" + pageName, UriKind.RelativeOrAbsolute));
        }

        private void PageMain_Loaded(object sender, RoutedEventArgs e)
        {
            webBrowser.Navigate(new Uri(homeUrl, UriKind.Absolute));
            if (App.NeedShowAd)
            {
                //InneractiveAdHelper.DisplayInterstitial(LayoutRoot);
                GoogleAdHelper.DisplayBanner(banner, "ca-app-pub-7278887713829891/4420970760");
                GoogleAdHelper.DisplayBanner(banner1, "ca-app-pub-7278887713829891/5897703960");
                App.NeedShowAd = false;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string appTitle = SystemHelper.GetAppTitle();
            SystemHelper.AskForReview(appTitle);
        }

        private void WebBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            wbBackKeyPress.Navigated(e);
            progressBar.Visibility = Visibility.Collapsed;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            base.OnBackKeyPress(e);
            wbBackKeyPress.BackKeyPress(webBrowser, e);
            progressBar.Visibility = Visibility.Collapsed;
            if (e.Cancel)
            {
                return;
            }

            string caption = "Thoát";
            string appTitle = SystemHelper.GetAppTitle();
            string message = "Bạn muốn thoát khỏi " + appTitle + "?";
            e.Cancel = MessageBoxResult.Cancel == MessageBox.Show(message, caption,
            MessageBoxButton.OKCancel);

            base.OnBackKeyPress(e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            OnBackKeyPress(new System.ComponentModel.CancelEventArgs());
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            webBrowser.Navigate(new Uri(homeUrl, UriKind.Absolute));
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            webBrowser.Navigate(webBrowser.Source);
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            wbBackKeyPress.Forward(webBrowser);
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void btnRating_Click(object sender, EventArgs e)
        {
            SystemHelper.RateAndReview();
        }

        private void btnUpdateVersion_Click(object sender, EventArgs e)
        {
            SystemHelper.UpdateVersion();
        }

        private void btnShare_Click(object sender, EventArgs e)
        {

        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            GotoPage("/Views/PageAboutUs.xaml");
        }

        private void btnNews_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri(homeUrl, UriKind.Absolute));
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri(messageUrl, UriKind.Absolute));
        }

        private void btnFriends_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri(friendUrl, UriKind.Absolute));
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri(notificationUrl, UriKind.Absolute));
        }
    }
}