using AppFlorDeLiz.Modelo;
using AppFlorDeLiz.ViewModel;
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
    public partial class ClientesCRUDPage : ContentPage
    {
        private ClienteViewModel clienteViewModel;

        public ClientesCRUDPage(Cliente cliente)
        {
            InitializeComponent();
            if (cliente.ClienteId == null)
            {
                Title = "Inserção de um novo cliente";
            }
            else
            {
                Title = "Alteração dos dados do cliente";
            }
            clienteViewModel = new ClienteViewModel(cliente);
            BindingContext = clienteViewModel;
            BtnVisualizarMapa.Clicked += BtnVisualizarMapaClicked;
        }

        private async void BtnVisualizarMapaClicked(object sender, EventArgs e)
        {
            var cliente = clienteViewModel.GetObjectFromView();
            var endereco = cliente.Numero + " " + cliente.Endereco + ", " + cliente.Bairro + ", " + cliente.Cidade + ", " + cliente.Estado;
            await Navigation.PushAsync(new ClientesMapPage(endereco));
        }
    }
}