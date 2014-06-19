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
using System.IO;
using System.Text;
using System.Runtime.Serialization;

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
           
            List<String> sucharyDoDodania = new List<String>();
            List<String> sucharyPoDodania = new List<String>();
            String dane = e.Result;
            String[] suchary = dane.Split('-');
            String wyjscie = "";
            foreach (String suchar in suchary)
            {
                sucharyDoDodania.Add(suchar);
               
            }
            Dane.suchary = sucharyDoDodania;

            sucharyDoDodania = Dane.suchary;

            foreach (string dupa in Dane.suchary)
            {
                wyjscie = wyjscie + "\n" + dupa;
            }

            MessageBox.Show(wyjscie);
        }

        private void buttonPobieraj_Click(object sender, RoutedEventArgs e)
        {
            pobierzSuchary();
        }
      


    }
}