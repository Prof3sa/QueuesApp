using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App3.ViewModels;
using System.Windows;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BankingQueueViewer : Page
    {

        private BankingsQueueViewModel bankingModel;
        private DispatcherTimer timer;
        private int selected;
        private QueueViewModel model;

        public BankingQueueViewer()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            bankingModel = BankingsQueueViewModel.GetInstance();
            this.BankingView.ItemsSource = bankingModel.Queues;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler<object>(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 1, 0);
            //this.BankingView.SelectionChanged += Change_Selection;
            selected = -1;
            model = null;
            
            
        }

        private void Change_Selection(object obj, SelectionChangedEventArgs e)
        {
            

            foreach(QueueViewModel Q in bankingModel.Queues )
            {
                Q.Color = "Black";
            }

            if (model == null || !model.Equals(this.BankingView.SelectedIndex))
            {
                
                selected = this.BankingView.SelectedIndex;
                model = this.BankingView.SelectedItem as QueueViewModel;
                model.Color = "Red";
                this.SwapButton.DataContext = model;
            }
            else if(model != null)
            {
                model.Color = "Black";
                selected = -1;
                model = null;
            }
            this.AppNameTextBlock.Text = "Selected an item " + selected.ToString();
            
        }

        private void Cancel_Click(object obj, RoutedEventArgs e)
        {
            selected = -1;
        }

        private void Swap_Click(object obj, RoutedEventArgs e)
        {
            selected = this.BankingView.SelectedIndex;
            if (selected > -1)
            {
                BankingsQueueViewModel banks = bankingModel;
                QueueViewModel qm = model;
                //QueueViewModel qm = this.BankingView.SelectedItem as QueueViewModel;
                bankingModel.swap(qm);
                this.AppNameTextBlock.Text = qm.Color;
                selected = -1;
            }
        }


        void dispatcherTimer_Tick(object sender, object e)
        {
            bankingModel.QueryAllTimes();
        }

        

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //this.DataContext = bankingModel.Queues;
        }

        private void CollectionQueuesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected != -1)
            {

                
                bankingModel.swap(model.ID);
                model.Color = "Black";
                selected = -1;
                this.AppNameTextBlock.Text = "SWPP";
                model = null;
            }
        }
    }
}
