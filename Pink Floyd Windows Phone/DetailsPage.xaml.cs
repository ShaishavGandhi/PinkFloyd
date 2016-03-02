using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Pink_Floyd_Windows_Phone.Resources;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.WindowsAzure.MobileServices;
using System.Reflection;
using System.Windows.Input;

namespace Pink_Floyd_Windows_Phone
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (CheckNetworkConnection() == false)
            {
                MessageBox.Show("Internet Connection Needed");
                NavigationService.Navigate(new Uri("/MainPage.xaml?selectedId=", UriKind.Relative));
            }
            else
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                // txtInput is a TextBox defined in XAML.
                if (!settings.Contains("userName"))
                {
                    NavigationService.Navigate(new Uri("/CheckUserName.xaml?selectedId=", UriKind.Relative));
                }
                else
                {
                    RefreshTodoItems1();
                }


            }
        }



        public class Post
        {
            public string Id { get; set; }

            [JsonProperty(PropertyName = "post_name")]
            public string PostName { get; set; }

            [JsonProperty(PropertyName = "user_id")]
            public string User { get; set; }


        }

        public static bool CheckNetworkConnection()
        {
            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        private MobileServiceCollection<Post, Post> items1;

        private IMobileServiceTable<Post> postTable = App.MobileService.GetTable<Post>();

        private async void RefreshTodoItems1()
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                items1 = await postTable

                    .ToCollectionAsync();
                ListItems.ItemsSource = items1;
                ThisIsOutProgressBar.Visibility = Visibility.Collapsed;

            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }


        }


        private void ListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var name = e.AddedItems[0];
            Type myType = name.GetType();
            String str = "";
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            String[] str2 = new string[5];
            int i = 0;
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(name, null);
                propValue = propValue.ToString();
                str2.SetValue(propValue, i);
                i++;
                // Do something with propValue
            }

            var id = str2[0];
            var postName = str2[1];
            NavigationService.Navigate(new Uri("/CommentsPage.xaml?selectedId=" + id + "&post=" + postName, UriKind.Relative));
        }

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
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshTodoItems1();
            //throw new NotImplementedException();
        }

        private void AddPostImage_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddPost.xaml?selectedId=", UriKind.Relative));
        }
    }

    public class PostsData
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Guid { get; set; }
    }
}