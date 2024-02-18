using Newtonsoft.Json;
using oderme.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace oderme.ViewModels
{
	public class OrderViewModel : INotifyPropertyChanged
	{
		public OrderViewModel()
		{
            Order = new ObservableCollection<ObservableCollection<Orders>>();
            GetAllNewsAsync(list =>
            {
                foreach (ObservableCollection<Orders> item in list)
                {
                    Order.Add(item);
                }
            });
        }
        public async Task GetAllNewsAsync(Action<IEnumerable<IEnumerable<Orders>>> action)
        {
            var uri = new Uri(App.Current.Properties["domain"] +
                    "/odermeApp/user/getorders.php?user_id=" + App.Current.Properties["user_id"]);
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<Orders>>>(await response.Content.ReadAsStringAsync());
                action(list);
            }
        }
        private ObservableCollection<ObservableCollection<Orders>> _order;
        public ObservableCollection<ObservableCollection<Orders>> Order
        {
            get { return _order; }
            set
            {
                _order = value;
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
