using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Modelo;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Sapatos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SapatosEditPage : ContentPage
    {
        public SapatosEditPage(Sapato sapato)
        {
            InitializeComponent();
            gridControl.PopularControles(sapato);
        }
    }
}