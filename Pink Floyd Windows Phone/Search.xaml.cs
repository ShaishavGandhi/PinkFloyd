using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Pink_Floyd_Windows_Phone.ViewModels;
using Windows.UI.Core;

namespace Pink_Floyd_Windows_Phone
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
        }





        private void SearchBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }

        private void SearchBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "Search for any song...";
        }

        //private void SearchBox_OnKeyUp(object sender, KeyEventArgs e)
        //{
        //    int index = 0;
        //    int counter = 0;
        //    string query = SearchBox.Text;

        //    ItemViewModel[] im = new ItemViewModel[100];
        //    //SearchResults[] sr = new SearchResults[100];
        //    if (query.Length > 0)
        //    {
        //        query = query.First().ToString().ToUpper() + query.Substring(1);
        //        for (index = 0; index < App.ViewModel.Items.Count; index++)
        //        {





        //            if (App.ViewModel.Items[index].LineOne.IndexOf(query) > -1)
        //            {


        //                im[counter] = new ItemViewModel();
        //                im[counter] = App.ViewModel.Items[index];
        //                counter++;
        //                MainLongListSelector.ItemsSource = im;
        //            }


        //        }


        //    }
        //}

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



        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int index = 0;
            int counter = 0;
            string query = SearchBox.Text;

            ItemViewModel[] im = new ItemViewModel[100];
            //SearchResults[] sr = new SearchResults[100];
            if (query.Length > 0)
            {
                query = query.First().ToString().ToUpper() + query.Substring(1);
                for (index = 0; index < App.ViewModel.Items.Count; index++)
                {





                    if (App.ViewModel.Items[index].LineOne.IndexOf(query) > -1)
                    {


                        im[counter] = new ItemViewModel();
                        im[counter] = App.ViewModel.Items[index];
                        counter++;
                        MainLongListSelector.ItemsSource = im;
                    }


                }


            }
        }
    }
}