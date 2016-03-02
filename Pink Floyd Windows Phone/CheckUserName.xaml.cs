using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Microsoft.WindowsAzure.MobileServices;
using System.Windows.Input;
using Newtonsoft.Json;

namespace Pink_Floyd_Windows_Phone
{
    public partial class CheckUserName : PhoneApplicationPage
    {

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

        private MobileServiceCollection<Comments, Comments> items;

        private string id;

        private IMobileServiceTable<Comments> commentTable = App.MobileService.GetTable<Comments>();

        public class Post
        {
            public string Id { get; set; }

            [JsonProperty(PropertyName = "post_name")]
            public string PostName { get; set; }

            [JsonProperty(PropertyName = "user_id")]
            public string User { get; set; }


        }



        private MobileServiceCollection<Post, Post> items1;

        private IMobileServiceTable<Post> postTable = App.MobileService.GetTable<Post>();
        public CheckUserName()
        {
            InitializeComponent();
            
        }

        private async void UIElement_OnTap(object sender, GestureEventArgs e)
        {

            string uname = UserTextBox.Text;
            if(uname!="Choose Username" && uname.Length!=0){
            ThisIsAnotherAnotherProgressBar.Visibility = Visibility.Visible;
            
            try
            {
                items = await commentTable.
                    Where(comments => comments.User == uname)

                    .ToCollectionAsync();





            }
            catch (MobileServiceInvalidOperationException f)
            {
                MessageBox.Show(f.Message, "Error loading items", MessageBoxButton.OK);
            }
            try
            {
                items1 = await postTable.
                    Where(post => post.User == uname)

                    .ToCollectionAsync();





            }
            catch (MobileServiceInvalidOperationException f)
            {
                MessageBox.Show(f.Message, "Error loading items", MessageBoxButton.OK);
            }
            if (items.Count > 0 || items1.Count > 0)
            {
                ThisIsAnotherAnotherProgressBar.Visibility = Visibility.Collapsed;
                MessageBox.Show("Sorry username is already taken. Choose another");
            }
            else
            {
                ThisIsAnotherAnotherProgressBar.Visibility = Visibility.Collapsed;
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                settings.Add("userName", uname);
                settings.Save();
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedId=", UriKind.Relative));

            }
            }
        }

        private void UserTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserTextBox.Text = "Choose Username";
        }

        private void UserTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            UserTextBox.Text = "";
        }
    }
}