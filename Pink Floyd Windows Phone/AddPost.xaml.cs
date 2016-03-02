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
using Microsoft.WindowsAzure.MobileServices;
using System.Windows.Input;
using System.IO.IsolatedStorage;

namespace Pink_Floyd_Windows_Phone
{
    public partial class AddPost : PhoneApplicationPage
    {
        public AddPost()
        {
            InitializeComponent();
        }

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

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TopicNameBox.Text = "";
        }

        private void TopicNameBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TopicNameBox.Text = "Enter topic of discussion";
        }

        private async void PostSubmitButton_OnTap(object sender, GestureEventArgs e)
        {
            string postName1 = TopicNameBox.Text;
            string uid = "";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("userName"))
            {
                uid =
               IsolatedStorageSettings.ApplicationSettings["userName"] as string;
            }

            if (postName1.Length == 0 || postName1== "Enter topic of discussion")
            {
                MessageBox.Show("Please enter a valid topic of discussion");
            }

            else
            {
                ThisIsOutProgressBar.Visibility = Visibility.Visible;
                Post cm = new Post() { PostName = postName1, User = uid };

                await postTable.InsertAsync(cm);
                ThisIsOutProgressBar.Visibility = Visibility.Collapsed;
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedId=", UriKind.Relative));
            }
        }
    }
}