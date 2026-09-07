using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        private readonly ObservableCollection<Produto> produtosFiltrados = new();
        private List<Produto> todosProdutos = new();

        public ListaProduto()
        {
            InitializeComponent();
            listaProdutos.ItemsSource = produtosFiltrados;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarProdutos();
        }

        private async Task CarregarProdutos()
        {
            try
            {
                todosProdutos = await App.Db.GetAll();
                AplicarFiltro(barraPesquisa.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Não foi possível carregar os produtos.", "OK");
            }
        }

        private void PesquisarProduto(object sender, TextChangedEventArgs e)
        {
            AplicarFiltro(e.NewTextValue);
        }

        private void AplicarFiltro(string? texto)
        {
            produtosFiltrados.Clear();

            IEnumerable<Produto> resultado = todosProdutos;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                resultado = todosProdutos.Where(produto =>
                    produto.Descricao.Contains(texto.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            foreach (Produto produto in resultado)
            {
                produtosFiltrados.Add(produto);
            }
        }

        private async void AbrirCadastro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }
    }
}