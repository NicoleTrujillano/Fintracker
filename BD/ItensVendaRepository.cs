using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.BD
{
    public class ItensVendaRepository
    {
        public async void AddItensVenda(List<ItensVenda> itensVendas)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                foreach(ItensVenda i in itensVendas)
                {
                    string query = $"insert into itensVenda values ({i.IdVenda}, {i.IdProduto}, {i.Quantidade}, {i.Preco.ToString().Replace(',','.')})";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    await cmd.ExecuteNonQueryAsync();
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async Task<List<ItensVenda>> pegarIdsProdutosNaVenda(int id_venda)
        {
            List<ItensVenda> idsProdutos = new List<ItensVenda>();
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                
                string query = $"SELECT id_Produto, quantidade FROM itensVenda where id_Venda = {id_venda}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader =  (MySqlDataReader) await cmd.ExecuteReaderAsync();
                while (reader.Read()) 
                {
                    ItensVenda i = new ItensVenda
                    {
                        IdProduto = reader.GetInt32(0),
                        Quantidade = reader.GetInt32(1)
                    };
                    idsProdutos.Add(i);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return idsProdutos;
        }

        public async Task<bool> UpdateItensVenda(List<ItensVenda> itensVenda)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                MySqlCommand cmdDeletarTudo = new MySqlCommand($"delete from itensVenda where id_Venda = {itensVenda[0].IdVenda};", conn);
                await cmdDeletarTudo.ExecuteNonQueryAsync();
                for(int v=0;v<itensVenda.Count;v++)
                {
                    ItensVenda i = itensVenda[v];
                    string query = $"insert into itensVenda values ({i.IdVenda}, {i.IdProduto}, {i.Quantidade}, {i.Preco.ToString().Replace(',', '.')})";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    await cmd.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
