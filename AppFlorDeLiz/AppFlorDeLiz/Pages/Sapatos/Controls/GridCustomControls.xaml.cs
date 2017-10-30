using AppFlorDeLiz.Dal;
using AppFlorDeLiz.Modelo;
using AppFlorDeLiz.Pages.Colecoes;
using Plugin.Media;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFlorDeLiz.Pages.Sapatos.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridCustomControls : Grid
    {
        private byte[] bytesFoto;
        private Sapato sapato = null;

        public GridCustomControls()
        {
            InitializeComponent();
        }

        private async void OnTapLookForTipos(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ColecoesSearchPage(idTipo, nomeTipo));
        }

        public async void OnAlbumClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            var stream = file.GetStream();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            fotoAlbum.Source = ImageSource.FromStream(() =>
            {
                var s = file.GetStream();
                file.Dispose();
                return s;
            });
            bytesFoto = memoryStream.ToArray();
        }

        private async void OnTappedSaveItem(object sender, EventArgs args)
        {
            var dal = new SapatoDAL();
            if (this.sapato == null)
            {
                this.sapato = new Sapato();
            }
            this.sapato.Nome = nome.Text;
            this.sapato.Descricao = descricao.Text;
            this.sapato.Preco = Convert.ToDouble(preco.Text);
            this.sapato.ColecaoId = Convert.ToInt32(idTipo.Text);
            this.sapato.Foto = bytesFoto;

            if (this.sapato.SapatoId == null)
            {
                dal.Add(this.sapato);
                await App.Current.MainPage.DisplayAlert("Inserção de item", "Item inserido com sucesso", "Ok");
            }
            else
            {
                dal.Update(this.sapato);
                await App.Current.MainPage.DisplayAlert("Atualização de item", "Item atualizado com sucesso", "Ok");
                await Navigation.PopAsync();
            }

            ClearControls();
        }

        private void ClearControls()
        {
            nome.Text = string.Empty;
            descricao.Text = string.Empty;
            preco.Text = string.Empty;
            idTipo.Text = string.Empty;
            nomeTipo.Text = "Selecione o Tipo do Item";
            bytesFoto = null;
            fotoAlbum.Source = null;
        }

        public void PopularControles(Sapato sapato)
        {
            this.sapato = sapato;
            nome.Text = this.sapato.Nome;
            descricao.Text = this.sapato.Descricao;
            preco.Text = this.sapato.Preco.ToString("N");
            if (this.sapato.Foto != null)
            {
                fotoAlbum.Source = ImageSource.FromStream(() => new MemoryStream(this.sapato.Foto));
                bytesFoto = this.sapato.Foto;
            }
            nomeTipo.Text = this.sapato.Colecao.Nome;
            idTipo.Text = this.sapato.ColecaoId.ToString();
        }
    }
}
