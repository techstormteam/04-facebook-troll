using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Utility;

namespace Facebook_Troll.Views
{
    public partial class PageAboutUs : PhoneApplicationPage
    {
        public PageAboutUs()
        {
            InitializeComponent();
        }

        private void lblRating_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SystemHelper.RateAndReview();
        }

        private void lblUpdateVersion_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SystemHelper.UpdateVersion();
        }

        private void lblMyLink_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SystemHelper.OpenLink("http://techstorm-solution.com/");
        }

        private void lblMyTeam_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SystemHelper.OpenLink("http://techstorm-solution.com/");
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            lblMyTeam.Text = SystemHelper.GetAppAuthor();
        }

    }
}