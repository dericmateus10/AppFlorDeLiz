using AppFlorDeLiz.Dal;
using AppFlorDeLiz.HelperControls;
using AppFlorDeLiz.Modelo;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Sapatos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SapatosPage : ContentPage
    {
        private ColecaoDAL dalColecao = new ColecaoDAL();
        private SapatoDAL dalSapato = new SapatoDAL();

        public SapatosPage()
        {
            InitializeComponent();
            this.lvSapatos.ItemsSource = GetDataByGroup();
            BtnNovoItem.Clicked += BtnNovoItemClick;
        }

        private async void BtnNovoItemClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SapatosNewPage());
        }

        private Collection<ListViewGrouping<Colecao, Sapato>> GetDataByGroup()
        {
            var dadosAgrupados = new Collection<ListViewGrouping<Colecao, Sapato>>();
            var tipos = dalColecao.GetAllWithChildren();
            foreach (var tipo in tipos)
            {
                dadosAgrupados.Add(new ListViewGrouping<Colecao, Sapato>(tipo, tipo.Itens));
            }
            return dadosAgrupados;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lvSapatos.BeginRefresh();
            lvSapatos.ItemsSource = GetDataByGroup();
            lvSapatos.EndRefresh();
        }

        public async void OnAlterarClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as Sapato;
            await Navigation.PushAsync(new SapatosEditPage(item));
        }

        public async void OnRemoverClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as Sapato;
            var opcao = await DisplayAlert("Confirmação de exclusão",
                "Confirma excluir o item " + item.Nome.ToUpper() + "?", "Sim", "Não");
            if (opcao)
            {
                dalSapato.DeleteById((long)item.SapatoId);
                this.lvSapatos.ItemsSource = GetDataByGroup();
            }
        }
    }
}