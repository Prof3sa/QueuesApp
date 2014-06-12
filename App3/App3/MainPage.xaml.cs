using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
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
using System.ComponentModel;
using System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private CollectionQueuesViewModel model;
        private DispatcherTimer timer;

        //private delegate void UpdateEveryMinute(CollectionQueuesViewModel model);
        //private UpdateEveryMinute every;

//        private void EveryMinute(CollectionQueuesViewModel model)
  //      {
    //        while (true) { model.QueryAllTimes(); }
      //  }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            model = CollectionQueuesViewModel.GetInstance();
            this.CollectionView.ItemsSource = model.Queues;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += new EventHandler<object>(dispatch_handler);
            timer.Start();
            //System.Threading.Timer timer = new System.Threading.Timer();
        }

        private void dispatch_handler(object sender, object e)
        {
            model.QueryAllTimes();
            //AppNameTextBlock.Text = "Updated Times";
        }

        

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            //while(true)
            //{
               // model.QueryAllTimes();
            //}

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
             //NavigationService.Navigate(new Uri("/SecondPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BankingQueueViewer));
        }
    }
}
