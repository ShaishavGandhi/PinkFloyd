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
using System.Windows.Input;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Pink_Floyd_Windows_Phone
{
    public partial class CommentsPage : PhoneApplicationPage
    {
        public CommentsPage()
        {
            InitializeComponent();
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

        private MobileServiceCollection<Comments, Comments> items;

        private string id;

        private IMobileServiceTable<Comments> commentTable = App.MobileService.GetTable<Comments>();
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {


            if (NavigationContext.QueryString.ContainsKey("selectedId"))
            {
                id = NavigationContext.QueryString["selectedId"];
                // etc ...
            }
            if (NavigationContext.QueryString.ContainsKey("post"))
            {
                string postNameLocal = NavigationContext.QueryString["post"];
                // etc ...
                Block.Text = postNameLocal;
            }
            RefreshTodoItems();
        }

        private async void RefreshTodoItems()
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                items = await commentTable.
                    Where(comments => comments.Post == id)

                    .ToCollectionAsync();
                ListItems.ItemsSource = items;

                ThisIsAnotherProgressBar.Visibility = Visibility.Collapsed;

            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
                ThisIsAnotherProgressBar.Visibility = Visibility.Collapsed;
            }


        }

        private void CommenTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            CommenTextBox.Text = "";
        }

        private void CommenTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            CommenTextBox.Text = "Enter comment";


        }

        //private void UserNameBox_OnGotFocus(object sender, RoutedEventArgs e)
        //{
        //    UserNameBox.Text = "Username";
        //}

        //private void UserNameBox_OnLostFocus(object sender, RoutedEventArgs e)
        //{
        //    UserNameBox.Text = "";
        //}
        private async void SendImage_OnTap(object sender, GestureEventArgs e)
        {
            
            string uid = "";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("userName"))
            {
                uid =
               IsolatedStorageSettings.ApplicationSettings["userName"] as string;
            }
            string comment = CommenTextBox.Text;
            //string uid= IsolatedStorageSettings.ApplicationSettings["userData"] as string;

            if (comment.Length == 0 || comment.Length > 450 || comment=="Enter comment")
            {
                MessageBox.Show("Please check your input");
            }
            else
            {
                ThisIsAnotherProgressBar.Visibility = Visibility.Visible;
                Comments cm = new Comments() { Comment = comment, User = uid, Post = id };
                ;
                await commentTable.InsertAsync(cm);

                RefreshTodoItems();
                NoOfCharactersBlock.Text = "450/450";
            }
        }

        private void CommenTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            int length = CommenTextBox.Text.Length;
            length = 450 - length;
            string newLength = length.ToString();
            NoOfCharactersBlock.Text = newLength + "/450";
        }
    }
}