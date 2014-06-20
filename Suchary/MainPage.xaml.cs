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

namespace Suchary
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            if (settings.Contains("suchary")) Dane.suchary = (List<String>) settings["suchary"];
            if (!Dane.suchary.Any())
            {
                textBlockSuchary.Text = "Najpierw ściągnij suchary";
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
            String[] sucharyTab = e.Result.Split('@');
            String wyjscie = "";
            foreach (String suchar in sucharyTab)
            {
                Dane.suchary.Add(suchar);
            }

            if (settings.Contains("suchary"))
            {
                settings.Remove("suchary");
                settings.Add("suchary", Dane.suchary);
                settings.Save();
                MessageBox.Show("nadpisałem suchary");
            }
            else
            {
                settings.Add("suchary", Dane.suchary);
                settings.Save();
                MessageBox.Show("stworzylem suchary");
            }


            //foreach (string dupa in Dane.suchary)
            //{
            //    wyjscie = wyjscie + "\n\n" + dupa;
            //}

            //MessageBox.Show(wyjscie);
        }

        private void buttonPobieraj_Click(object sender, RoutedEventArgs e)
        {
            pobierzSuchary();
        }

        private void buttonNastepny_Click(object sender, RoutedEventArgs e)
        {
            Random wylosowana = new Random();
            if (Dane.suchary.Any())
                textBlockSuchary.Text = Dane.suchary[wylosowana.Next(0,Dane.suchary.Count)];
            else
                textBlockSuchary.Text = "Najpierw ściągnij suchary";
        }
    }
}