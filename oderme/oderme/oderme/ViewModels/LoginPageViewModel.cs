using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using oderme;
using oderme.Models;
using oderme.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using oderme.Views;
using System.Net.Http;
using System.IO;
using oderme.Views.User;
using oderme.Controls;

public class LoginPageViewModel
{
    string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
    public ICommand OnLoginFacebook { get; set; }
    public ICommand OnLoginGoogle { get; set; }

    IFacebookClient _facebookService = CrossFacebookClient.Current;
    IGoogleClientManager _googleService = CrossGoogleClient.Current;
   
    public LoginPageViewModel()
    {
        OnLoginFacebook = new Command(async () => await LoginFacebookAsync());
        OnLoginGoogle = new Command(async () => await LoginGoogleAsync());
    }
    
    async Task LoginFacebookAsync()
    {
        try
        {

            if (_facebookService.IsLoggedIn)
            {
                _facebookService.Logout();
            }

            EventHandler<FBEventArgs<string>> userDataDelegate = null;

            userDataDelegate = async (object sender, FBEventArgs<string> e) =>
            {
                switch (e.Status)
                {
                    case FacebookActionStatus.Completed:
                        var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                        var socialLoginData = new NetworkAuthData
                        {
                            Id = facebookProfile.Id,
                            Email = facebookProfile.Email,
                            Picture = facebookProfile.Picture.Data.Url,
                            Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                            Type = "Facebook"
                        };
                        //await App.Current.MainPage.Navigation.PushAsync(new HomePage(socialLoginData));
                        UploadData(socialLoginData);
                        break;
                    case FacebookActionStatus.Canceled:
                        await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                        break;
                    case FacebookActionStatus.Error:
                        await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                        break;
                    case FacebookActionStatus.Unauthorized:
                        await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                        break;
                }

                _facebookService.OnUserData -= userDataDelegate;
            };

            _facebookService.OnUserData += userDataDelegate;

            string[] fbRequestFields = { "email", "first_name", "picture", "gender", "last_name" };
            string[] fbPermisions = { "email" };
            await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    async Task LoginGoogleAsync()
    {
        try
        {
            if (!string.IsNullOrEmpty(_googleService.AccessToken))
            {
                //Always require user authentication
                _googleService.Logout();
            }

            EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
            userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
            {
                switch (e.Status)
                {
                    case GoogleActionStatus.Completed:
#if DEBUG
                        var googleUserString = JsonConvert.SerializeObject(e.Data);
                        Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                        var socialLoginData = new NetworkAuthData
                        {
                            Id = e.Data.Id,
                            Email = e.Data.Email,
                            Picture = e.Data.Picture.AbsoluteUri,
                            Name = e.Data.Name,
                            Type = "Google"
                        };

                        //await App.Current.MainPage.Navigation.PushAsync(new HomePage(socialLoginData));
                        UploadData(socialLoginData);
                        break;
                    case GoogleActionStatus.Canceled:
                        await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                        break;
                    case GoogleActionStatus.Error:
                        await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                        break;
                    case GoogleActionStatus.Unauthorized:
                        await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                        break;
                }

                _googleService.OnLogin -= userLoginDelegate;
            };

            _googleService.OnLogin += userLoginDelegate;

            await _googleService.LoginAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    async void UploadData(NetworkAuthData Data)
	{
        using (var cl = new HttpClient())
        {
            var formcontent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("id",Data.Id),
                new KeyValuePair<string, string>("email",Data.Email),
                new KeyValuePair<string,string>("name",Data.Name),
                new KeyValuePair<string, string>("picture",Data.Picture),
                new KeyValuePair<string, string>("type",Data.Type),
            });

            var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                "/odermeApp/register/login.php?", formcontent);

            request.EnsureSuccessStatusCode();

            var response = await request.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<Account>(response);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
            File.WriteAllText(_fileName, json);

            Application.Current.Properties["user_id"] = res.Id;
            Application.Current.Properties["user_email"] = res.Email;
            Application.Current.Properties["user_name"] = res.Name;
            Application.Current.Properties["user_phone"] = res.Phone;
            Application.Current.Properties["user_picture"] = res.Picture;
            Application.Current.Properties["social_id"] = res.Social_Id;
            Application.Current.Properties["shop_id"] = res.Shop_Id;
            Application.Current.Properties["social_type"] = res.Type;

            App.Current.MainPage = new TransitionNavigationPage(new SlidePageUser());
        }
    }
}