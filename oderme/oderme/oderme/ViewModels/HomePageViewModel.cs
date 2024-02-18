using Newtonsoft.Json;
using oderme.Models;
using oderme.Services;
using oderme.Views;
using oderme.Views.User;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace oderme.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        CarouselPage carouselPage;
        public HomePageViewModel(CarouselPage page)
        {
            LoadingShow = true;
            carouselPage = page;
            UserPicture = Application.Current.Properties["user_picture"].ToString();
            shopData = new ObservableCollection<ShopData>();
            shopDataList = new ObservableCollection<ShopData>();
            GetAllNewsAsync(list =>
            {
                foreach (ShopData item in list)
                {
                    shopData.Add(item);
                    shopDataList.Add(item);
                }
                carouselPage.Children[2] = new MapPageViewModel(carouselPage, shopData);
                LoadingShow = false;
            });
            
            OnProfileCommand = new Command(async () => page.CurrentPage = page.Children[0]);
            OnMapCommand = new Command(async () => page.CurrentPage = page.Children[2]);
        }
  
        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
        public ICommand OnProfileCommand { get; set; }
        public ICommand OnMapCommand { get; set; }
        async Task RefreshItemsAsync()
        {
            IsRefreshing = false;
            await Task.Delay(200);
            LoadingShow = true;
            SearchText = "";
            shopData = new ObservableCollection<ShopData>();
            shopDataList = new ObservableCollection<ShopData>();
            GetAllNewsAsync(list =>
            {
                foreach (ShopData item in list)
				{
                    shopData.Add(item);
                    shopDataList.Add(item);
                }
                carouselPage.Children[2] = new MapPageViewModel(carouselPage, shopData);
                LoadingShow = false;
            });
        }
        public async Task GetAllNewsAsync(Action<IEnumerable<ShopData>> action)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var position = await Geolocation.GetLocationAsync(request);
            Geocoder geoCoder = new Geocoder();
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(new Position(position.Latitude, position.Longitude));
            string address = possibleAddresses.FirstOrDefault();
            string[] addres = address.Split(",".ToCharArray());
            CurAddress = addres[addres.Length - 1];

            var uri = new Uri(Application.Current.Properties["domain"] +
                "/odermeApp/loaddata.php?latitude=" + position.Latitude + "&longitude=" + position.Longitude +
                "&distance=" + Application.Current.Properties["distance"]);
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<ShopData>>(await response.Content.ReadAsStringAsync());
                action(list);
            }
        }
        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
        {   
            try
			{
                await Task.Delay(500);
                if(SearchText == text)
				{
                    LoadingShow = true;
                    shopData = new ObservableCollection<ShopData>();
                    if (text.Length >= 1)
                    {
                        var suggestions = shopDataList.Where(x => x.Name.ToLower().Contains(text.ToLower()));
                        foreach (var recipe in suggestions)
                            shopData.Add(recipe);
                    }
                    else
                    {
                        foreach (var recipe in shopDataList)
                            shopData.Add(recipe);
                    }
                    LoadingShow = false;
                }
            }
            catch
			{
			}
        })); 

        bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ShopData> _shopdataList;
        public ObservableCollection<ShopData> shopDataList
        {
            get { return _shopdataList; }
            set
            {
                _shopdataList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ShopData> _shopdata;
        public ObservableCollection<ShopData> shopData
        {
            get { return _shopdata; }
            set
            {
                _shopdata = value;
                OnPropertyChanged();
            }
        }
        public string UserPicture { get; set; }
        private string _curAddress;
        public string CurAddress
        {
            get { return _curAddress; }
            set
            {
                _curAddress = value;
                OnPropertyChanged();
            }
        }
        private bool _loadingShow;
        public bool LoadingShow
        {
            get { return _loadingShow; }
            set
            {
                _loadingShow = value;
                OnPropertyChanged();
            }
        }
        private string _searchtext;
        public string SearchText
        {
            get { return _searchtext; }
            set
            {
                _searchtext = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}