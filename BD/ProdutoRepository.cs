using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FinTracker.BD
{
    public class ProdutoRepository
    {
        private string _connectionString;

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }public ProdutoRepository()
        {
        }

        public void AddProduto(string nome, string categoria, string descricao, int quantidade, decimal valorUnitario, decimal valorTotal)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Produto (Nome, Categoria, Descrição, Quantidade, Valor_Unitário, Valor_Total) VALUES (@Nome, @Categoria, @Descrição, @Quantidade, @Valor_Unitário, @Valor_Total)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Categoria", categoria);
                cmd.Parameters.AddWithValue("@Descrição", descricao);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Valor_Unitário", valorUnitario);
                cmd.Parameters.AddWithValue("@Valor_Total", valorTotal);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetProdutos()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public async Task<List<String>> GetNomeProdutos()
        {
            MySqlConnection con = await MetodosDB.conexao();
            string query = "select nome from produto";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            List<String> resultado = new List<string>();
            while (reader.Read())
                resultado.Add(reader.GetString(0));
            return resultado;
        }

        public void UpdateProduto(int idProduto, string nome, string categoria, string descricao, int quantidade, decimal valorUnitario, decimal valorTotal)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Produto SET Nome = @Nome, Categoria = @Categoria, Descrição = @Descrição, Quantidade = @Quantidade, Valor_Unitário = @Valor_Unitário, Valor_Total = @Valor_Total WHERE id_Produto = @idProduto";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Categoria", categoria);
                cmd.Parameters.AddWithValue("@Descrição", descricao);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Valor_Unitário", valorUnitario);
                cmd.Parameters.AddWithValue("@Valor_Total", valorTotal);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduto(int idProduto)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Produto WHERE id_Produto = @idProduto";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.ExecuteNonQuery();
            }
        }

        public async Task<List<Produto>> PegarTodosProdutos()
        {
            MySqlConnection conn = await MetodosDB.conexao();
            MySqlCommand cmd = new MySqlCommand("SELECT id_Produto, Nome, Quantidade, Valor_Unitario FROM produto", conn);
            MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            List<Produto> produtos = new List<Produto>();
            while (reader.Read())
            {
                produtos.Add(new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDecimal(3)));
            }
            return produtos;
        }
    }
}
