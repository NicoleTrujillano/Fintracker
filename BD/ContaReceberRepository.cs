using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinTracker.BD
{
    public class ContaReceberRepository
    {
        private string _connectionString;

        public ContaReceberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddContaReceber(decimal valor, int idCliente, string metodoPagamento, DateTime dataTransacao, DateTime? previsaoTermino)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Conta_a_Receber (Valor, id_Cliente, Método_de_Pagamento, Data_de_Transação, Previsão_de_Término) VALUES (@Valor, @idCliente, @MetodoPagamento, @DataTransacao, @PrevisaoTermino)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@MetodoPagamento", metodoPagamento);
                cmd.Parameters.AddWithValue("@DataTransacao", dataTransacao);
                cmd.Parameters.AddWithValue("@PrevisaoTermino", (object)previsaoTermino ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetContasReceber()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Conta_a_Receber", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateContaReceber(int idContaReceber, decimal valor, int idCliente, string metodoPagamento, DateTime dataTransacao, DateTime? previsaoTermino)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Conta_a_Receber SET Valor = @Valor, id_Cliente = @idCliente, Método_de_Pagamento = @MetodoPagamento, Data_de_Transação = @DataTransacao, Previsão_de_Término = @PrevisaoTermino WHERE id_Conta_a_Receber = @idContaReceber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idContaReceber", idContaReceber);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@MetodoPagamento", metodoPagamento);
                cmd.Parameters.AddWithValue("@DataTransacao", dataTransacao);
                cmd.Parameters.AddWithValue("@PrevisaoTermino", (object)previsaoTermino ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteContaReceber(int idContaReceber)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Conta_a_Receber WHERE id_Conta_a_Receber = @idContaReceber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idContaReceber", idContaReceber);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
