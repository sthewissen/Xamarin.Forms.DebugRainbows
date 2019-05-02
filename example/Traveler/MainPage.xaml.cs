using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;
using Xamarin.Forms;

namespace Traveler
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel
            {
                Destinations = new List<Destination>() {
                 new Destination
                 {
                     Title = "Ulun Danu Beratan Temple",
                     ImageUrl = "bali.jpg",
                     Rating = 4.4f,
                     Votes = 3829
                 },
                 new Destination
                 {
                     Title = "Isola d'Elba",
                     ImageUrl = "elba.jpg",
                     Rating = 4.9f,
                     Votes = 9783
                 }
                }
            };
        }
    }
}
