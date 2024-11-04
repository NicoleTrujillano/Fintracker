using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.BD
{
    public class FornecedorRepository
    {
        public FornecedorRepository()
        {

        }

        public async void AddFornecedor(object fornecedor)
        {
            Cliente f = (Cliente)fornecedor;
            MySqlConnection conn = await MetodosDB.conexao();
                string query = "INSERT INTO fornecedor (Nome, Data_de_Cadastro, CNPJ, Endereco, Bairro, Cidade, Estado, CEP, Telefone, Email, Status) " +
                               "VALUES (@Nome, @DataCadastro, @CNPJ, @Endereco, @Bairro, @Cidade, @Estado, @CEP, @Telefone, @Email, @Status)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome",f.Nome);
                cmd.Parameters.AddWithValue("@DataCadastro", f.Data_de_Cadastro);
                cmd.Parameters.AddWithValue("@CNPJ", f.CNPJ);
                cmd.Parameters.AddWithValue("@Endereco", f.Endereco);
                cmd.Parameters.AddWithValue("@Bairro", f.Bairro);
                cmd.Parameters.AddWithValue("@Cidade", f.Cidade);
                cmd.Parameters.AddWithValue("@Estado", f.Estado);
                cmd.Parameters.AddWithValue("@CEP", f.CEP);
                cmd.Parameters.AddWithValue("@Telefone", f.Telefone);
                cmd.Parameters.AddWithValue("@Email", f.Email);
                cmd.Parameters.AddWithValue("@Status", f.Status);
                await cmd.ExecuteNonQueryAsync();
            
        }

        public async Task<DataTable> GetFornecedores()
        {
            try 
            {
                MySqlConnection conn = await MetodosDB.conexao();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM fornecedor", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                await da.FillAsync(dt);
                return dt; 
            }catch(Exception e)
            {
                return null;
            }
        }

        public async void UpdateFornecedor(object fornecedor)
        {
            Cliente f = (Cliente)fornecedor;
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "UPDATE fornecedor SET Nome = @Nome, CNPJ = @CNPJ, Endereco = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, CEP = @CEP, Telefone = @Telefone, Email = @Email, Status = @Status WHERE id_Fornecedor = @idFornecedor";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idFornecedor", f.id_Cliente);
            cmd.Parameters.AddWithValue("@Nome", f.Nome);
            cmd.Parameters.AddWithValue("@CNPJ", f.CNPJ);
            cmd.Parameters.AddWithValue("@Endereco", f.Endereco);
            cmd.Parameters.AddWithValue("@Bairro", f.Bairro);
            cmd.Parameters.AddWithValue("@Cidade", f.Cidade);
            cmd.Parameters.AddWithValue("@Estado", f.Estado);
            cmd.Parameters.AddWithValue("@CEP", f.CEP);
            cmd.Parameters.AddWithValue("@Telefone", f.Telefone);
            cmd.Parameters.AddWithValue("@Email", f.Email);
            cmd.Parameters.AddWithValue("@Status", f.Status);
            await cmd.ExecuteNonQueryAsync();
        }

        public async void DeleteFornecedor(int idFornecedor)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "DELETE FROM fornecedor WHERE id_Fornecedor = @idFornecedor";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
            await cmd.ExecuteNonQueryAsync();
        }

        public void DeleteListaFornecedor(List<int> idFornecedores)
        {
            foreach (int id in idFornecedores)
                DeleteFornecedor(id);
        }


        public async Task<DataTable> pesquisarEmTudo(string strBusca)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "select * from fornecedor " +
                $"WHERE id_Fornecedor = '{strBusca}' or Nome LIKE '%{strBusca}%' or CNPJ LIKE '%{strBusca}%' or Endereco LIKE '%{strBusca}%' or Bairro LIKE '%{strBusca}%' or " +
                $"Cidade LIKE '%{strBusca}%' or Estado LIKE '%{strBusca}%' or CEP LIKE '%{strBusca}%' or Telefone LIKE '%{strBusca}%' or Email LIKE '%{strBusca}%' or " +
                $"Status LIKE '%{strBusca}%' or Data_de_Cadastro like '%{strBusca}%'";

            try
            {

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                await da.FillAsync(dt);
                return dt;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
            return null;
        }
    }
}
