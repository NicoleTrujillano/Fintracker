using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinTracker.BD
{
    public class VendaRepository
    {

        public VendaRepository()
        {
        }

        public async Task<bool> AddVenda(int idCliente, string status, string data, string metodo, int parcelas)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                string query = $"insert into venda (id_Cliente, Status, data_transacao, Metodo, parcelas) values ({idCliente}, '{status}', '{data}', '{metodo}', {parcelas})";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                await cmd.ExecuteNonQueryAsync();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public async Task<List<Venda>> GetVendas()
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                String query = "select v.id_Venda, c.Nome, v.Metodo, count(iv.id_Produto), v.data_transacao, sum(iv.preco), v.id_cliente, v.parcelas, v.status from venda v " +
                    "inner join cliente c on c.id_Cliente = v.id_Cliente inner join itensVenda iv on iv.id_Venda = v.id_Venda group by v.id_Venda";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<Venda> vendas = new List<Venda>();
                while (reader.Read())
                {
                    Venda venda = new Venda
                    {
                        IdVenda = reader.GetInt16(0),
                        nomeCliente = reader.GetString(1),
                        Metodo = reader.GetString(2),
                        QuantidadeProdutos = reader.GetInt32(3),
                        DataTransacao = reader.GetDateTime(4),
                        TotalPreco = reader.GetDecimal(5),
                        IdCliente = reader.GetInt32(6),
                        Parcelas = reader.GetInt16(7),
                        Status = reader.GetString(8)
                    };
                    vendas.Add(venda);
                }
                return vendas;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async void UpdateVendaAsync(Venda venda)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                string query = $"update venda set id_Cliente = {venda.IdCliente}, Status = '{venda.Status}', data_transacao = '{venda.data}', " +
                    $"Metodo='{venda.Metodo}', parcelas={venda.Parcelas} where id_venda = {venda.IdVenda};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteVenda(int idVenda)
        {
            
        }
    }
}
