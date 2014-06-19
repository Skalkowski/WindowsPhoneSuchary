using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;

namespace Suchary
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        public void pobierzSuchary()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("http://kubavic.vdl.pl/suchary.json"));
        }


        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            var rootObject = JsonConvert.DeserializeObject<RootObject>(e.Result);
            foreach (var suchar in rootObject.Suchar)
            {
              //  Console.WriteLine(suchar.suchar);
            }
        }

        private void buttonPobieraj_Click(object sender, RoutedEventArgs e)
        {
            pobierzSuchary();
        }



    }
}