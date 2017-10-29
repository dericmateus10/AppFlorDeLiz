using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFlorDeLiz.Dal;
using AppFlorDeLiz.Modelo;
using PCLStorage;
using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Colecoes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColecoesEditPage : ContentPage
	{
        private Colecao colecao;
        private string Foto;
        private ColecaoDAL dalColecao = new ColecaoDAL();

        public ColecoesEditPage(Colecao colecao)
        {
            InitializeComponent();
            PopularFormulario(colecao);
            RegistraClickBotaoCamera(idcolecao.Text.Trim());
            RegistraClickBotaoAlbum();
        }

        private void PopularFormulario(Colecao colecao)
        {
            this.colecao = colecao;
            idcolecao.Text = colecao.ColecaoId.ToString();
            nome.Text = colecao.Nome;
            //caminhoArquivo = produto.CaminhoArquivoFoto;
            //fotoproduto.Source = ImageSource.FromFile(produto.CaminhoArquivoFoto);
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
                var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);
                var rootFolder = FileSystem.Current.LocalStorage;
                var folderFoto = await rootFolder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);
                var setArquivoPCL = folderFoto.CreateFileAsync(getArquivoPCL.Result.Name, CreationCollisionOption.ReplaceExisting);
                var arquivoLido = getArquivoPCL.Result.ReadAllTextAsync();
                var arquivoEscrito = setArquivoPCL.Result.WriteAllTextAsync(arquivoLido.Result);
                Foto = setArquivoPCL.Result.Path;

                if (file == null)
                    return;

                fotocolecao.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
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
                        Name = "tipoitem_" + idparafoto.Trim() + ".jpg"
                    });

                if (file == null)
                    return;

                fotocolecao.Source = ImageSource.FromFile(file.Path);
                Foto = file.Path;
            };
        }

        public async void BtnGravarClick(object sender, EventArgs e)
        {
            if (nome.Text.Trim() == string.Empty)
            {
                await this.DisplayAlert("Erro",
                    "Você precisa informar o nome para o novo tipo de item do cardápio.",
                    "Ok");
            }
            else
            {
                this.colecao.Nome = nome.Text;
                //this.produto.CaminhoArquivoFoto = caminhoArquivo;

                dalColecao.Update(this.colecao);
                await Navigation.PopModalAsync();
            }
        }
    }
}