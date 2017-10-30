using AppFlorDeLiz.Dal;
using AppFlorDeLiz.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesListPage : ContentPage
    {
        private ClienteDAL clienteDAL = new ClienteDAL();

        public ClientesListPage()
        {
            InitializeComponent();
            BtnNovoItem.Clicked += BtnNovoItemClick;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lvClientes.ItemsSource = clienteDAL.GetAll();
        }

        private async void BtnNovoItemClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientesCRUDPage(new Cliente()));
        }

        public async void OnAlterarClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var cliente = mi.CommandParameter as Cliente;
            await Navigation.PushAsync(new ClientesCRUDPage(cliente));
        }

        public async void OnRemoverClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var cliente = mi.CommandParameter as Cliente;
            var opcao = await DisplayAlert("Confirmação de exclusão",
                "Confirma excluir o cliente " + cliente.Nome.ToUpper() + "?", "Sim", "Não");
            if (opcao)
            {
                clienteDAL.DeleteById((long)cliente.ClienteId);
                this.lvClientes.ItemsSource = clienteDAL.GetAll();
            }
        }
    }
}