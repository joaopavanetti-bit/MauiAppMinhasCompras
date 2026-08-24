using MauiAppMinhasCompras.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto produto)
        {
            return _conn.InsertAsync(produto);
        }

        public Task<int> Update(Produto produto)
        {
            return _conn.UpdateAsync(produto);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>()
                        .DeleteAsync(produto => produto.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string texto)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";

            return _conn.QueryAsync<Produto>(sql, $"%{texto}%");
        }
    }
}