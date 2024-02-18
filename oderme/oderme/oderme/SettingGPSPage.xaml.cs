using oderme.Services;
using oderme.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace oderme
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingGPSPage : ContentPage
	{
		bool IsUp = true;
		double H = 100;
		double S = 2.5;
		public SettingGPSPage()
		{
			InitializeComponent();
			DependencyService.Get<IGpsDependencyService>().OpenSettings();
			Device.StartTimer(TimeSpan.FromMilliseconds(5), (Func<bool>)(() =>
			{
				if(DependencyService.Get<IGpsDependencyService>().IsGpsEnable())
				{
					Navigation.PopAsync();
					return false;
				}
				else
				{
					RunCiecle(cir1);
					RunCiecle(cir2);
					RunCiecle(cir3);
					RunPin();
					return true;
				}
			}));
		}
		void OpenLocation(object sender, EventArgs e)
		{
			DependencyService.Get<IGpsDependencyService>().OpenSettings();
		}
		void RunPin()
		{
			if(IsUp)
			{
				if(S <= 1)
				{
					IsUp = false;
					S = 1;
				}
				else
				{
					S = S * 0.98;
					H += S;
				}
			}
			else
			{
				if (H <= 80)
				{
					IsUp = true;
					S = 2.5;
				}
				else
				{
					S = S * 1.025;
					H -= S;
				}
			}
			pin.Margin = new Thickness(0,0,0,H);
		}
		void RunCiecle(Image e)
		{
			if (e.WidthRequest > 600)
			{
				e.WidthRequest = 0;
				e.Opacity = 0.32;
			}
			else
			{
				e.WidthRequest += 3;
				e.Opacity -= 0.0027;
			}
		}
		protected override bool OnBackButtonPressed()
		{
			DependencyService.Get<ICloseApplication>().closeApplication();
			return false;
		}

        private void OnBackBtn(object sender, EventArgs e)
        {
			Navigation.PopAsync();
        }
    }
}