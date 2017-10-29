using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Dal;
using AppFlorDeLiz.Modelo;
using Plugin.Media;
using PCLStorage;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Colecoes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColecoesNewPage : ContentPage
	{
        private ColecaoDAL dalColecao = new ColecaoDAL();
        private byte[] bytesFoto;

        public ColecoesNewPage()
        {
            InitializeComponent();
            PreparaParaNovoProduto();
            RegistraClickBotaoCamera(idcolecao.Text.Trim());
            RegistraClickBotaoAlbum();
        }

        private void RegistraClickBotaoAlbum()
        {

            BtnAlbum.Clicked += async (sender, args) =>
            {

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                    return;

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                fotocolecao.Source = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });
                bytesFoto = memoryStream.ToArray();
            };
        }

        private void RegistraClickBotaoCamera(string idparafoto)
        {

            BtnCamera.Clicked += async (sender, args) =>
            {

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Não existe câmera", "A câmera não está disponível.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(
                    new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = FileSystem.Current.LocalStorage.Name,
                        Name = "tipoitem_" + idparafoto + ".jpg",
                        SaveToAlbum = true
                    });


                if (file == null)
                    return;


                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                fotocolecao.Source = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });
                bytesFoto = memoryStream.ToArray();
            };
        }

        public void BtnGravarClick(object sender, EventArgs e)
        {
            if (nome.Text.Trim() == string.Empty)
            {
                this.DisplayAlert("Erro",
                    "Você precisa informar o nome para o novo tipo de item do cardápio.",
                    "Ok");
            }
            else
            {
                dalColecao.Add(new Colecao()
                {
                    Nome = nome.Text,
                    Foto = bytesFoto

                });
                PreparaParaNovoProduto();
            }
        }

        private void PreparaParaNovoProduto()
        {
            var novoId = dalColecao.GetAll().Max(x => x.ColecaoId) + 1;
            idcolecao.Text = novoId.ToString().Trim();
            nome.Text = string.Empty;
            fotocolecao.Source = null;
        }
    }
}
