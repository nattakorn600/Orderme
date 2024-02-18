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
    public partial class InfoAndReviewPage : ContentPage
    {
		public ObservableCollection<test> Data = new ObservableCollection<test>()
		{
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
			new test (){ t1="อร่อยมาก", t2="stared.png", t3="stared.png", t4="stared.png", t5="stared.png", t6="stared.png", t7="5/5" },
		};

		private int pagebutton = 0;
		private int pixels = 50;
		public InfoAndReviewPage(ObservableCollection<Review> data)
        {
            InitializeComponent();
			Reviews.ItemsSource = data;
		}

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}