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
using System.IO.IsolatedStorage;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using System.Diagnostics;

namespace Suchary
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        List<String> sucharyList = new List<string>();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            if (settings.Contains("suchary")) Dane.suchary = (List<String>)settings["suchary"];
            if (!Dane.suchary.Any())
            {
                textBlockSuchary.Text = "Najpierw ściągnij suchary";
                buttonNastepny.IsEnabled = false;
            }
            else
            {

                foreach (String suchar in Dane.suchary)
                {
                    sucharyList.Add(suchar);
                }

                losuj();
            }

        }

        private void buttonPobieraj_Click(object sender, RoutedEventArgs e)
        {
            ToastPrompt toast = new ToastPrompt
            {
                Background = new SolidColorBrush(Colors.Red),
                Message = "Brak połączenia internetowego."
            };

            if (checkNetworkConnection())
            {
                pobierzSuchary();
                
                textBlockSuchary.Text = "Losuj suchara :)";
                buttonNastepny.IsEnabled = true;
            }
            else
                toast.Show();
        }

        private void buttonNastepny_Click(object sender, RoutedEventArgs e)
        {
            if (sucharyList.Any())
                losuj();
            else
            {
                foreach (String suchar in Dane.suchary)
                {
                    sucharyList.Add(suchar);
                }
            }
        }

        public void losuj()
        {
            
            Random random = new Random();
            int wylosowana = random.Next(0, sucharyList.Count);
            textBlockSuchary.Text = sucharyList[wylosowana];
            sucharyList.RemoveAt(wylosowana);
            int wielkosc = sucharyList.Count;
            if (!sucharyList.Any())
            {
                foreach (String suchar in Dane.suchary)
                {
                    sucharyList.Add(suchar);
                }
            }
        }

        public void pobierzSuchary()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("http://project-midas.com/michal/suchary.txt"));
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            ToastPrompt toast = new ToastPrompt
            {
                Background = new SolidColorBrush(Colors.Green),
                Message = "Suchary zostały pobrane"
            };
            String[] sucharyTab = e.Result.Split('@');
            String wyjscie = "";
            Dane.suchary.Clear();
            foreach (String suchar in sucharyTab)
            {
                Dane.suchary.Add(suchar);
                sucharyList.Add(suchar);
            }
            if (settings.Contains("suchary"))
            {
                settings.Remove("suchary");
                settings.Add("suchary", Dane.suchary);
                settings.Save();
            }
            else
            {
                settings.Add("suchary", Dane.suchary);
                settings.Save();
            }
            toast.Show();
        }


        public static bool checkNetworkConnection()
        {
            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }


    }

}