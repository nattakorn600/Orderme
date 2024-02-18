using Newtonsoft.Json;
using oderme.Controls;
using oderme.Models;
using oderme.Services;
using oderme.ViewModels;
using oderme.Views;
using oderme.Views.User;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme
{
	public partial class App : Application
	{
		string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
		public App()
		{
			InitializeComponent();
			Application.Current.Properties["domain"] = "https://www.mahamhorr.com";
			Application.Current.Properties["distance"] = 1;
			Application.Current.Properties["current_shop"] = "";
			/*if (File.Exists(_fileName))
			{
				File.Delete(_fileName);
			}*/
			//MainPage = new NavigationPage(new LoginPage());
		}
		
		protected override void OnStart()
		{
			if (!(File.Exists(_fileName)))
			{
				MainPage = new NavigationPage(new LoginPage());
				//File.Create(session).Dispose();
			}
			else
			{
				Account res = new Account();
				string jsondata = File.ReadAllText(_fileName);
				res = JsonConvert.DeserializeObject<Account>(jsondata);

				Application.Current.Properties["user_id"] = res.Id;
				Application.Current.Properties["user_email"] = res.Email;
				Application.Current.Properties["user_name"] = res.Name;
				Application.Current.Properties["user_phone"] = res.Phone;
				Application.Current.Properties["user_picture"] = res.Picture;
				Application.Current.Properties["social_id"] = res.Social_Id;
				Application.Current.Properties["shop_id"] = res.Shop_Id;
				Application.Current.Properties["social_type"] = res.Type;
				Application.Current.Properties["distance"] = 1;

				MainPage = new TransitionNavigationPage(new SlidePageUser());
				//MainPage = new Controls.TransitionNavigationPage(new Views.User.Help.HelpPage()) { BarBackgroundColor = Color.FromHex("#FFF") };
				//MainPage = new AppShell();
			}
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
