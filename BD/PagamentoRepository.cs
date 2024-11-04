using FinTracker.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace FinTracker.BD
{
    public class PagamentoRepository
    {
        public PagamentoRepository()
        {
        }

        public async void AddPagamentoAsync(DateTime data, string metodo, string tipo, int parcelas, decimal valor)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            conn.Open();
                string query = "INSERT INTO Pagamento (Data, Método, Tipo, Parcelas, Valor) VALUES (@Data, @Metodo, @Tipo, @Parcelas, @Valor)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Data", data);
                cmd.Parameters.AddWithValue("@Metodo", metodo);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Parcelas", parcelas);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.ExecuteNonQuery();
            
        }

        public async Task<DataTable >GetPagamentos()
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM pagamento", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                await da.FillAsync(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async void UpdatePagamento(int idPagamento, DateTime data, string metodo, string tipo, int parcelas, decimal valor)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            conn.Open();
                string query = "UPDATE Pagamento SET Data = @Data, Método = @Metodo, Tipo = @Tipo, Parcelas = @Parcelas, Valor = @Valor WHERE id_Pagamento = @idPagamento";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
                cmd.Parameters.AddWithValue("@Data", data);
                cmd.Parameters.AddWithValue("@Metodo", metodo);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Parcelas", parcelas);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.ExecuteNonQuery();
            
        }

        public async void DeletePagamento(int idPagamento)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            conn.Open();
                string query = "DELETE FROM Pagamento WHERE id_Pagamento = @idPagamento";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
                cmd.ExecuteNonQuery();
            
        }
    }
}
