using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Dal;
using AppFlorDeLiz.Modelo;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Colecoes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColecoesListPage : ContentPage
	{
        private ColecaoDAL dalColecao = new ColecaoDAL();

        public ColecoesListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lvColecoes.ItemsSource = dalColecao.GetAll();
        }

        public async void OnAlterarClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as Colecao;
            await Navigation.PushModalAsync(new ColecoesEditPage(item));
        }

        public async void OnRemoverClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as Colecao;
            var opcao = await DisplayAlert("Confirmação de exclusão",
                "Confirma excluir o item " + item.Nome.ToUpper() + "?", "Sim", "Não");
            if (opcao)
            {
                dalColecao.DeleteById((long)item.ColecaoId);
                lvColecoes.ItemsSource = dalColecao.GetAll();
            }
        }
    }
}