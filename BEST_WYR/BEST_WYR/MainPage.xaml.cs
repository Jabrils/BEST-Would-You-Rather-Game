using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace BEST_WYR
{
    public partial class MainPage : ContentPage
    {
        Random r = new Random();

        public MainPage()
        {
            InitializeComponent();

            aButton.Clicked += Next;
            bButton.Clicked += Next;

            Next(null, null);
        }


        protected override void OnDisappearing()
        {
            aButton.Clicked -= Next;
            bButton.Clicked -= Next;
        }

        void Next(object sender, EventArgs args)
        {
            var scenarioCount = WYRDownloadService.AllScenarios.Count;

            if (scenarioCount == 0)
            {
                DisplayAlert("Oh nos!", "No data to play with!", "OK");
                return;
            }

            int r1 = r.Next(0, scenarioCount);
            int r2 = r.Next(0, scenarioCount);

            while (r2 == r1)
            {
                r2 = r.Next(0, scenarioCount);
            }

            aButton.Text = WYRDownloadService.AllScenarios[r1].scene;
            bButton.Text = WYRDownloadService.AllScenarios[r2].scene;
        }
    }
}