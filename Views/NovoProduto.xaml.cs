using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_descricao.Text))
            {
                await DisplayAlert("Atenção", "Digite a descrição do produto.", "OK");
                return;
            }

            if (!double.TryParse(txt_quantidade.Text, out double quantidade) ||
                !double.TryParse(txt_preco.Text, out double preco))
            {
                await DisplayAlert("Atenção", "Digite números válidos na quantidade e no preço.", "OK");
                return;
            }

            Produto produto = new Produto
            {
                Descricao = txt_descricao.Text.Trim(),
                Quantidade = quantidade,
                Preco = preco
            };

            try
            {
                await App.Db.Insert(produto);

                await DisplayAlert("Sucesso", "Produto cadastrado com sucesso.", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Não foi possível salvar o produto.", "OK");
            }
        }
    }
}