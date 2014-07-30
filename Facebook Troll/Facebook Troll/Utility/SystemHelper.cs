using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public class SystemHelper
    {
        public static void CountForReview(int numStartToAsk)
        {
            IsolatedStorageSettings.ApplicationSettings["askforreview"] = false;

            int started = 0;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("started"))
            {
                started = (int)IsolatedStorageSettings.ApplicationSettings["started"];
            }
            started++;
            IsolatedStorageSettings.ApplicationSettings["started"] = started;

            bool reviewed = false;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("reviewed"))
            {
                reviewed = (bool)IsolatedStorageSettings.ApplicationSettings["reviewed"];
            }

            if (reviewed == false && started >= numStartToAsk)
            {
                IsolatedStorageSettings.ApplicationSettings["askforreview"] = true;
            }
        }

        public static void AskForReview(string appName)
        {
            var askforReview = (bool)IsolatedStorageSettings.ApplicationSettings["askforreview"];
            if (askforReview)
            {
                //make sure we only ask once! 
                IsolatedStorageSettings.ApplicationSettings["askforreview"] = false;
                var ok = MessageBox.Show(
                    string.Format("Cảm ơn bạn đã sử dụng {0}, Hãy đánh giá 5 sao cho ứng dụng và comment chức năng mà bạn muốn ở phiên bản kế tiếp.", appName),
                    "Hãy đánh giá 5 sao!", MessageBoxButton.OKCancel);
                if (ok == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["reviewed"] = true;
                    var marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                }
            }
        }

        public static string GetAppID()
        {
            string appID = (from manifest in
                                System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App")
                            select
                            manifest).SingleOrDefault().Attribute("ProductID").Value;
            return appID;
        }

        public static string GetAppTitle()
        {
            string appTitle = (from manifest in
                                System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App")
                            select
                            manifest).SingleOrDefault().Attribute("Title").Value;
            return appTitle;
        }

        public static string GetAppAuthor()
        {
            string appAuthor = (from manifest in
                                System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App")
                            select
                            manifest).SingleOrDefault().Attribute("Author").Value;
            return appAuthor;
        }

        public static void UpdateVersion()
        {
            MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
            marketplaceDetailTask.ContentIdentifier = SystemHelper.GetAppID();
            marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
            marketplaceDetailTask.Show();
        }

        public static void OpenLink(string link)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(link);
            webBrowserTask.Show(); 
        }
        public static void RateAndReview()
        {
            var marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        public static void ShareLink(string link)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask()
            {
                Title = "Great App! Join it now :)",
                LinkUri = new Uri("http://msdn.microsoft.com/en-us/library/windowsphone/develop/ff431744(v=vs.92).aspx", UriKind.Absolute),
                Message = "Hello, I would like to share you a great app that I used and loved.\n" + link
            };

            shareLinkTask.Show();
        }


        public static void ShareEmail(string link)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask()
            {
                Subject = "Great App! Join it now :)",
                Body = "Hello, I would like to share you a great app that I used and loved.\n" + link,
                To = "recipient@example.com",
                Cc = "",
                Bcc = ""
            };

            emailComposeTask.Show();
        }

        public static void ShareSMS(string link)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask()
            {
                Body = "Try this new application. It's great!" + link
            };

            smsComposeTask.Show();
        }

        public static bool ShowExitMessageBox(string appName)
        {
            string caption = "Thoát";
            string message = string.Format("Bạn muốn thoát khỏi {0}?", appName);
            bool cancel = MessageBoxResult.Cancel == MessageBox.Show(message, caption,
            MessageBoxButton.OKCancel);
            return cancel;
        }

    }
}
