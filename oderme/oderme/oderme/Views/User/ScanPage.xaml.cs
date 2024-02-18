using oderme.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        ObservableCollection<ShopData> shopdata = new ObservableCollection<ShopData>();
        string correctString = "";
        string oldid = "";
        public ScanPage(ObservableCollection<ShopData> data)
        {
            InitializeComponent();
            shopdata = data;
        }
        private void QrScan(ZXing.Result result)
        {
            int cksum = 0;
            int startindex = 6;
            int endindex = 0;
            string curshop = "";
            string table = "";
            if (result.Text.Substring(0, 4) == "6036")
			{
                endindex = int.Parse(result.Text.Substring(4, 2));  
                curshop = result.Text.Substring(6, endindex);
                startindex = startindex + endindex;
                endindex = int.Parse(result.Text.Substring(startindex, 2));
                startindex = startindex + 2;
                table = result.Text.Substring(startindex, endindex);
                startindex = startindex + endindex;

                for (int i = 0; i < startindex; i++)
                {
                    cksum = cksum + int.Parse(result.Text.Substring(i, 1));
                }

                if (result.Text.Substring(startindex) == cksum.ToString())
				{
                    correctString = curshop;
                    Application.Current.Properties["current_shop"] = curshop;
                    Application.Current.Properties["table"] = table;
                }
            }
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            correctString = "";
            oldid = "";
            Device.StartTimer(TimeSpan.FromMilliseconds(25), () =>
            {
                if (correctString != oldid)
                {
                    oldid = correctString;
                    foreach (var shop in shopdata)
                    {
                        if (shop.Id.ToString() == Application.Current.Properties["current_shop"].ToString())
                        {
                            Navigation.PushAsync(new ShopPage(shop));
                            return false;
                        }
                    }
                }
                return true;
            });
        }
		private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}