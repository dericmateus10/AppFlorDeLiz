using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Dal;
using AppFlorDeLiz.Modelo;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Colecoes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColecoesSearchPage : ContentPage
	{
        private ColecaoDAL dalColecao = new ColecaoDAL();
        private Label displayValue;
        private Label keyValue;
        private IEnumerable<Colecao> itens;

        public ColecoesSearchPage(Label keyValue, Label displayValue)
        {
            InitializeComponent();
            itens = dalColecao.GetAll();
            this.keyValue = keyValue;
            this.displayValue = displayValue;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            lvTipos.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                lvTipos.ItemsSource = itens;
            else
                lvTipos.ItemsSource = itens.Where(i => i.Nome.Contains(e.NewTextValue));

            lvTipos.EndRefresh();
        }

        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            var item = (o as ListView).SelectedItem as Colecao;
            this.keyValue.Text = item.ColecaoId.ToString();
            this.displayValue.Text = item.Nome;
            await Navigation.PopAsync();
        }
    }
}