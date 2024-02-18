using oderme.Models;
using oderme.Services;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.Shop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenQrPage : ContentPage
	{
		bool onfirst = true;
		string namefile = "";
		public GenQrPage(ShopInfo shopInfo)
		{
			InitializeComponent();
			ShopName.Text = shopInfo.Name;
		}
		private void QrCodeCreate()
		{
			try
			{
				string shopidlength = Application.Current.Properties["shop_id"].ToString().Length.ToString();
				string tablelength = Table.Text.Length.ToString();

				if (Application.Current.Properties["shop_id"].ToString().Length < 10)
				{
					shopidlength = "0" + shopidlength.ToString();
				}
				if (Table.Text.Length < 10)
				{
					tablelength = "0" + tablelength.ToString();
				}
				namefile = shopidlength + Application.Current.Properties["shop_id"].ToString() + tablelength + Table.Text;
				QrCodes.Source = Application.Current.Properties["domain"]+"/odermeApp/shop/genqrcode.php?data=" + namefile;
				Tables.Text = Table.Text;
				AlertText.IsVisible = false;
				DOC.IsVisible = false;
				BG.IsVisible = false;		
			}
			catch {  }
		}
		private async void DownloadQr(object sender, EventArgs e)
		{
			Uri image_url_format = new Uri(Application.Current.Properties["domain"]+ "/odermeApp/shop/dlqrcode.php?data=" + namefile);
			WebClient webClient = new WebClient();
			
			byte[] img = webClient.DownloadData(image_url_format);
			await DependencyService.Get<IFileHelper>().SaveFileToDefaultLocation(DateTime.Now.ToString("MMddyyyyHHmmss") + ".jpg", img);
			await DisplayAlert("", "ดาวน์โหลดเสร็จสิ้น", "OK");
		}

		private void BackButton(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private void ReFreshQr(object sender, EventArgs e)
		{
			BG.IsVisible = true;
			DOC.IsVisible = true;
		}
		private void Cancel(object sender, EventArgs e)
		{
			if(onfirst)
			{
				Navigation.PopAsync();
			}
			else
			{
				BG.IsVisible = false;
				DOC.IsVisible = false;
			}
		}
		private void Create(object sender, EventArgs e)
		{
			if(Table.Text != null && Table.Text != "")
			{
				QrCodeCreate();
			}
			else
			{
				AlertText.IsVisible = true;
			}
			onfirst = false;
		}
	}
}