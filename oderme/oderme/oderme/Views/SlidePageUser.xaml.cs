using Newtonsoft.Json;
using oderme.Controls;
using oderme.Models;
using oderme.Services;
using oderme.ViewModels;
using oderme.Views.User;
using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlidePageUser : CarouselPage
    {
        bool FirstLoadStat = false;
        bool TurnOnGPS = false;
        public SlidePageUser()
        {
            InitializeComponent();
            
            Device.StartTimer(TimeSpan.FromMilliseconds(100), (Func<bool>)(() =>
            {
                if(TurnOnGPS == false)
				{
                    if (DependencyService.Get<IGpsDependencyService>().IsGpsEnable())
                    {
                        if (FirstLoadStat == false)
                        {
                            FirstLoad();
                            FirstLoadStat = true;
                        }
                    }
                    else
                    {
                        Navigation.PushAsync(new SettingGPSPage());
                        TurnOnGPS = true;
                    }
                }
                return true;
            }));
        }
        async void FirstLoad()
		{
            this.CurrentPage = this.Children[1];
            BindingContext = new HomePageViewModel(this);
            Children[0] = new ProfilePage(this);
            Children[2] = new MapPageViewModel(this, null);
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            TurnOnGPS = false;
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }
		
        void SelectShop(object sender, EventArgs e)
        {
            var item = (Button)sender;
            bool IsUp = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(3), () =>
            {
                if (IsUp)
                {
                    item.Opacity += 0.01;
                    if (item.Opacity >= 0.1)
                    {
                        HomePageViewModel viewModel = BindingContext as HomePageViewModel;
                        ShopData listitem = (from itm in viewModel.shopData
                                             where itm.Id == (int)(item.CommandParameter)
                                             select itm).FirstOrDefault<ShopData>();
                        Navigation.PushAsync(new ShopPage(listitem));
                        IsUp = false;
                    }
                }
                else
                {
                    item.Opacity -= 0.01;
                    if (item.Opacity <= 0.0)
                    {
                        return false;
                    }
                }
                return true;
            });
        }
        void OnTextChange(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            ((SearchBar)sender).SearchCommand?.Execute(searchBar.Text);
        }

        private void GoToShop(object sender, EventArgs e)
        {
           // Navigation.PushAsync(new FoodDetailPage());
        }

        private void GoToScan(object sender, EventArgs e)
        {
            HomePageViewModel viewModels = BindingContext as HomePageViewModel;
            Navigation.PushAsync(new ScanPage(viewModels.shopData));
        }

        protected override bool OnBackButtonPressed()
        {
            if (this.CurrentPage != this.Children[1])
            {
                this.CurrentPage = this.Children[1];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}