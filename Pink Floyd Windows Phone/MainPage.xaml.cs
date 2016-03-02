using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Pink_Floyd_Windows_Phone.Resources;
using Pink_Floyd_Windows_Phone.ViewModels;
using Microsoft.Phone.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using GoogleAds;

namespace Pink_Floyd_Windows_Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        private InterstitialAd interstitialAd;
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            interstitialAd = new InterstitialAd("ca-app-pub-7421031935026273/3765272948");
            AdRequest adRequest = new AdRequest();
            // adRequest.ForceTesting = true;
            interstitialAd.ReceivedAd += OnAdReceived;
            try
            {
                if (Convert.ToInt32(settings["ratecount"]) % 3 == 0)
                    interstitialAd.LoadAd(adRequest);
            }
            catch { }
            if (settings.Contains("ratecount") && !settings.Contains("reviewed"))
            {
                int count = Convert.ToInt32(settings["ratecount"]);
                if (count != -1)
                    count++;
                if (count % 5 == 0 && count != -1)
                {
                    //Add Dialog Code Here
                    // MessageBoxButton btn = new MessageBoxButton();

                    MessageBoxResult result = MessageBox.Show("Please take a moment to review this application. It means a lot to us :)", "Would you like to rate this application?", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

                        marketplaceReviewTask.Show();
                        settings.Add("reviewed", true);
                        settings.Save();
                    }


                }
                if (count == 5)
                    count = 0;

                settings["ratecount"] = count;
                settings.Save();
            }
            else
            {
                if (!settings.Contains("ratecount"))
                    settings.Add("ratecount", 0);
                settings.Save();
            }

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void OnAdReceived(object sender, AdEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");
            interstitialAd.ShowAd();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
             Random rnd = new Random();
            int indexNo = rnd.Next(0, 142);
            var imagebrush = new ImageBrush();
            imagebrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(App.ViewModel.Items[indexNo].Image1))
            };
            imagebrush.Opacity = 0.2;
            LayoutRoot.Background = imagebrush;

        
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;


            // Navigate to the new page
            NavigationService.Navigate(new Uri("/PivotPage1.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        public class Comments
        {
            public string Id { get; set; }

            [JsonProperty(PropertyName = "comment_name")]
            public string Comment { get; set; }

            [JsonProperty(PropertyName = "user_id")]
            public string User { get; set; }

            [JsonProperty(PropertyName = "post_id")]
            public string Post { get; set; }

        }

        public class Post
        {
            public string Id { get; set; }

            [JsonProperty(PropertyName = "post_name")]
            public string PostName { get; set; }

            [JsonProperty(PropertyName = "user_id")]
            public string User { get; set; }


        }

        // private MobileServiceCollection<Comments, Comments> items;





        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void Connect_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));
            //throw new NotImplementedException();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Search_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Search.xaml", UriKind.Relative));
        }
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }
    }
}