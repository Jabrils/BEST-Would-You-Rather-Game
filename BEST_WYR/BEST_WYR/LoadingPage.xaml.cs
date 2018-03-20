using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Runtime.CompilerServices;


namespace BEST_WYR
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await WYRDownloadService.DownloadWYRs();

            Application.Current.MainPage = new MainPage();
        }

    }

}
