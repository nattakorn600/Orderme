using Newtonsoft.Json;
using oderme.Models;
using oderme.Views.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SlideShopPage
	{
        public SlideShopPage(ShopInfo data)
		{
			InitializeComponent();

            Children[0] = new ProfileShopPage(this, data);
			Children[1] = new HomePage(this, data);
            CurrentPage = this.Children[1];
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