using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Interfaces
{
    internal static class MetodosDB
    {
        public static async Task<MySqlConnection> conexao()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("Server=db4free.net;Database=bd_fintracker;User Id=manuelagadelho;Password=Ma14082002.");
                await con.OpenAsync();
                return con;
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            return null;
        }

        public static async Task<String> executarQuerrySimples(String querry)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            MySqlCommand command = new MySqlCommand(querry, con);
            var resultado = await command.ExecuteScalarAsync();
            String teste = resultado.ToString();
            if (resultado != null)
                return resultado.ToString();
            return null;
        }

        public static async Task<String> getTotalEntradasEntre(String dataInicio, String dataFim)
        {

            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            String query = "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                "inner join produto p on p.id_Produto = v.id_Produto " +
                "inner join pagamento pag on pag.id_Pagamento = v.id_Pagamento " +
                $"where pag.Data between '{dataInicio}' and '{dataFim}' " +
                "group by v.id_Venda";
            MySqlCommand command = new MySqlCommand(query, con);
            //pegando o resultado do comando sql
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            //declarando variavel para somatorio dos resultados
            double totalEntradas = 0;
            //se tem dados no reader (se consulta retornou dados)
            if (reader.HasRows)
            {
                //faz a leitura de cada linha
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        continue;
                    totalEntradas += reader.GetDouble(0);//pega o valor da coluna 0 (1ª coluna) e soma em totalEntradas
                }
            }
            //adiciona em totalEntradas a soma total dos valores na tabela contas a receber se estiver entre data dos parametros deta funcao
            totalEntradas += double.Parse(await executarQuerrySimples($"SELECT sum(valor) FROM conta_a_receber where Previsao_de_Termino >= '{dataFim}'"));

            return totalEntradas.ToString();
        }
        public static async Task<String> getTotalSaidasEntre(String dataInicio, String dataFim)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            //declarando variavel para somatorio dos resultados
            double totalSaidas = 0;
            //adiciona em totalEntradas a soma total dos valores na tabela contas a receber se estiver entre data dos parametros deta funcao
            String a = await executarQuerrySimples(
                $"SELECT sum(valor) FROM conta_a_pagar where date_format(Previsao_de_Termino, '%Y-%m-%d')  <= '{dataFim}' " +
                $"or date_format(Data_de_Transacao, '%Y-%m-%d')  >= '{dataInicio}'");
            if(a!="")
                totalSaidas += double.Parse(a);

            return totalSaidas.ToString();
        }

        public static async Task<String[]> getFirstAndLastOfQuery(String query)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            if (reader == null)
                return null;
            String primeiroValor = "", ultimoValor = "";
            // Verifica se há linhas para serem lidas
            if (await reader.ReadAsync())
                primeiroValor = reader[0].ToString(); // Pegar a primeira célula da primeira linha
            // Continuar a ler até a última linha
            while (await reader.ReadAsync())
                ultimoValor = reader[0].ToString(); // Pegar a primeira célula da linha atual até chegar na ultima
            return new String[] { primeiroValor, ultimoValor };
        }

        public static async Task<bool> deleteVendaById(List<int> idsVenda)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                foreach (int id in idsVenda)
                {
                    MySqlCommand cmdDeletarItensVenda = new MySqlCommand($"delete from itensVenda where id_Venda = {id};", conn);
                    MySqlCommand cmdDeletarVenda = new MySqlCommand($"delete from venda where id_Venda = {id}", conn);
                    await cmdDeletarItensVenda.ExecuteNonQueryAsync();
                    await cmdDeletarVenda.ExecuteNonQueryAsync();
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
