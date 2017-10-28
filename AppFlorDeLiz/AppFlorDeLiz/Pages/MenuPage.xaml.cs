using AppFlorDeLiz.Pages.Colecoes;
using AppFlorDeLiz.Pages.Sapatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void ColecoesOnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ColecoesPage());
        }

        private async void SapatosOnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SapatosPage());
        }
    }
}