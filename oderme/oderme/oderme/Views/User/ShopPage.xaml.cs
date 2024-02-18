using Acr.UserDialogs;
using Newtonsoft.Json;
using oderme.Models;
using oderme.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopPage : ContentPage
    {
        ShopData Shopdata = new ShopData();
        ObservableCollection<MenuGroup> MenuData = new ObservableCollection<MenuGroup>();
        int OldSelectIndex = 0;
        public ShopPage(ShopData data)
        {
            InitializeComponent();
            LoadingData(data);
        }
        async void LoadingData(ShopData data)
		{
            LoadShow.IsRunning = true;
            await Task.Delay(500);
            LoadShow.IsRunning = false;
            BindingContext = data;
            MenuData = data.MenuGroup;
            Shopdata = data;
        }
        void SelectMenu(object sender, EventArgs e)
		{
            if(Application.Current.Properties["current_shop"].ToString() == Shopdata.Id.ToString())
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
                            MenuGroup itemgroup = (from itm in MenuData
                                                   where itm.Type == item.Text
                                                   select itm).FirstOrDefault<MenuGroup>();
                            Models.Menu menuitem = (from itm in itemgroup.MenuList
                                                    where itm.Id == (int)(item.CommandParameter)
                                                    select itm).FirstOrDefault<Models.Menu>();
                            Navigation.PushAsync(new FoodDetailPage(Shopdata, menuitem));
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
			else
			{
                DisplayAlert("","ไม่สามารถสั่งอาหารได้ โปรดแสกน QR code ที่ร้านก่อนสั่งอาหาร","ตกลง");
			}
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void RecommendMenu(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            Label label = (Label)frame.Content;

            MenuGroup group = MenuData.FirstOrDefault(a => a.Type == label.Text);
            collectionViews.ScrollTo(group, group, ScrollToPosition.Center, true);
        }

        void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {   
            if(e.CenterItemIndex != OldSelectIndex)
			{
                Frame frame = StackMenu.Children[OldSelectIndex] as Frame;
                frame.BackgroundColor = Color.FromHex("#FF7A21");
                Label lab = frame.Content as Label;
                lab.TextColor = Color.White;

                Frame frameselect = StackMenu.Children[e.CenterItemIndex] as Frame;
                frameselect.BackgroundColor = Color.White;
                Label labselect = frameselect.Content as Label;
                labselect.TextColor = Color.FromHex("#FF7A21");

                OldSelectIndex = e.CenterItemIndex;
            }
            
            /*Debug.WriteLine("HorizontalDelta: " + e.HorizontalDelta);
            Debug.WriteLine("VerticalDelta: " + e.VerticalDelta);
            Debug.WriteLine("HorizontalOffset: " + e.HorizontalOffset);
            Debug.WriteLine("VerticalOffset: " + e.VerticalOffset);
            Debug.WriteLine("FirstVisibleItemIndex: " + e.FirstVisibleItemIndex);
            Debug.WriteLine("CenterItemIndex: " + e.CenterItemIndex);
            Debug.WriteLine("LastVisibleItemIndex: " + e.LastVisibleItemIndex);*/
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            if (File.Exists(_fileName))
            {
                string jsondata = File.ReadAllText(_fileName);
                var res = JsonConvert.DeserializeObject<ObservableCollection<Models.Menu>>(jsondata);
                Frame frame = (Frame)NumberMenu.Parent;
                if (res.Count>0)
                {
                    frame.IsVisible = true;
                    NumberMenu.Text = res.Count.ToString();
                }
                else
                {
                    frame.IsVisible = false;
                    NumberMenu.Text = res.Count.ToString();
                }
            }
            else
            {
                Frame frame = (Frame)NumberMenu.Parent;
                frame.IsVisible = false;
            }
        }
		private void GoToInfoAndReview(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoAndReviewPage(Shopdata.Review));
        }

        private void GoToCart(object sender, EventArgs e)
        {
            if (Application.Current.Properties["current_shop"].ToString() == Shopdata.Id.ToString())
			{
                string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
                if(File.Exists(_fileName))
                    Navigation.PushAsync(new CartFoodPage(Shopdata));
            } 
        }
    }
}