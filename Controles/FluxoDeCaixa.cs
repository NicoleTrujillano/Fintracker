using FinTracker.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Controles
{
    public partial class FluxoDeCaixa : UserControl
    {
        

        public FluxoDeCaixa()
        {
            InitializeComponent();
            prepararDesign();
        }

        private void prepararDesign()
        {
            //instanciando o label do cabeçalho e aplicando o estilo dele
            Label headerLabel = new Label
            {
                Text = "Fluxo de caixa",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Height = 40, // Altura do Label
            };
            // Adiciona o Label ao panel do dataGridView
            panel1.Controls.Add(headerLabel);
            //faz do dataGrid preencher todo espaço do panel
            dgv.Dock = DockStyle.Fill;
            // Desabilita os estilos visuais do cabeçalho (para conseguir colocar estilo proprio)
            dgv.EnableHeadersVisualStyles = false;
            // Alterando estilo do cabeçalho
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Green; // Cor de fundo
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; //cor da fonte
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Estilo da fonte
            //alterando fonte do dataGridView
            dgv.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
        }

        public async void pegarDados(List<(int Ano, int Mes)> datas)
        {
            MySqlConnection con = await MetodosDB.conexao();
            MySqlConnection con2 = await MetodosDB.conexao();
            String cmdEntradas = "SELECT p.Nome AS nome_produto, \n";
            String cmdSaidas = "SELECT descricao as 'nome saida', ";
            for(int i=0; i< datas.Count; i++)
            {
                if (i == datas.Count - 1)
                {
                    cmdEntradas += $"SUM(CASE WHEN DATE_FORMAT(v.data_transacao, '%Y-%m') = '{datas[i].Ano}-{datas[i].Mes}' THEN iv.preco ELSE 0 END) \n";
                    cmdSaidas += $"SUM(CASE WHEN DATE_FORMAT(data, '%Y-%m') = '{datas[i].Ano}-{datas[i].Mes}' THEN valor ELSE 0 END) \n";
                    break;
                }
                cmdEntradas += $"SUM(CASE WHEN DATE_FORMAT(v.data_transacao, '%Y-%m') = '{datas[i].Ano}-{datas[i].Mes}' THEN iv.preco ELSE 0 END), \n";
                cmdSaidas += $"SUM(CASE WHEN DATE_FORMAT(data, '%Y-%m') = '{datas[i].Ano}-{datas[i].Mes}' THEN valor ELSE 0 END), \n";
            }
            cmdEntradas += "FROM venda v \nJOIN itensVenda iv ON v.id_Venda = iv.id_Venda \nJOIN produto p ON iv.id_Produto = p.id_Produto \nGROUP BY p.Nome;";
            cmdSaidas += "FROM pagamento v GROUP BY descricao;";


            MySqlCommand cmd1 = new MySqlCommand(cmdEntradas, con);
            MySqlCommand cmd2 = new MySqlCommand(cmdSaidas, con2);

            MySqlDataReader readerEntradas = (MySqlDataReader) await cmd1.ExecuteReaderAsync();
            MySqlDataReader readerSaidas = (MySqlDataReader) await cmd2.ExecuteReaderAsync();
            
            List<(String nomeProd, List<decimal> valorMes)> linhasEntradas = new List<(string nomeProd, List<decimal> valorMes)>();
            List<(String nomeSaida, List<decimal> valorMes)> linhasSaidas = new List<(string nomeSaida, List<decimal> valorMes)>();
            
            //lê cada linha
            while (readerEntradas.Read())
            {
                List<decimal> receitasMensais = new List<decimal>();
                // Loop para percorrer todas as colunas da linha atual
                for (int i = 1; i < readerEntradas.FieldCount; i++)//comeca do 1 porque na coluna 0 esta o nome do produto
                    receitasMensais.Add(readerEntradas.GetDecimal(i));

                linhasEntradas.Add( (readerEntradas.GetString(0), receitasMensais) );//preenche linhas de entrada
            }
            //lê cada linha
            while (readerSaidas.Read())
            {
                List<decimal> receitasMensais = new List<decimal>();
                // Loop para percorrer todas as colunas da linha atual
                for (int i = 1; i < readerSaidas.FieldCount; i++)//comeca do 1 porque na coluna 0 esta o nome do produto
                    receitasMensais.Add(readerSaidas.GetDecimal(i));
                if(readerSaidas.IsDBNull(0))//se o nome da saida for null
                {
                    linhasSaidas.Add( ("-", receitasMensais) );//preenche linhas de saida
                    continue;
                }
                linhasSaidas.Add( (readerSaidas.GetString(0), receitasMensais) );//preenche linhas de saida
            }
            preencherDataGrid(linhasEntradas, linhasSaidas, datas);
        }

        private void preencherDataGrid(List<(String nomeProd, List<decimal> valorMes)> linhasEntradas, List<(String nomeSaida, List<decimal> valorMes)> linhasSaidas, List<(int Ano, int Mes)> datas)
        {
            List<String> dataParaColunaDoFluxo = new List<string>();
            List<decimal> totalMes = new List<decimal>();
            for (int i = 0; i < datas.Count; i++)
            {
                var cultura = new CultureInfo("pt-BR");
                dataParaColunaDoFluxo.Add(cultura.DateTimeFormat.GetMonthName(datas[i].Mes) + " de " + datas[i].Ano);
            }
            dgv.Columns.Add("col1", "Entradas");
            for (int i = 0; i < linhasEntradas[0].valorMes.Count; i++)
            {
                dgv.Columns.Add($"col{i + 2}", dataParaColunaDoFluxo[i]);
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                totalMes.Add(0);
            }
            for(int i=0; i < linhasEntradas.Count; i++)
            {
                // Adiciona uma nova linha em branco e pega indice
                int rowIndex = dgv.Rows.Add();

                dgv.Rows[rowIndex].Cells[0].Value = linhasEntradas[i].nomeProd;

                // Insere cada valor em sua respectiva coluna
                for (int col = 0; col < linhasEntradas[i].valorMes.Count; col++)
                {
                    dgv.Rows[rowIndex].Cells[col+1].Value = linhasEntradas[i].valorMes[col];
                    totalMes[col] += decimal.Parse(dgv.Rows[rowIndex].Cells[col+1].Value.ToString());
                }
            }

            // Adiciona uma nova linha em branco e pega indice
            int indexLinhaTotalEntrada = dgv.Rows.Add();

            dgv.Rows[indexLinhaTotalEntrada].Cells[0].Value = "TOTAL ENTRADAS";

            // Insere cada valor em sua respectiva coluna
            for (int col = 0; col < totalMes.Count; col++)
            {
                dgv.Rows[indexLinhaTotalEntrada].Cells[col + 1].Value = totalMes[col];
            }

            // Mudar a cor de fundo e o estilo da fonte da nova linha
            DataGridViewRow row = dgv.Rows[indexLinhaTotalEntrada];
            row.DefaultCellStyle.BackColor = Color.Green; // Altera a cor de fundo
            row.DefaultCellStyle.ForeColor = Color.White; // Altera a cor do texto
            row.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Altera a fonte


            // Criar uma nova linha para seção de saidas
            DataGridViewRow linhaSaidas = (DataGridViewRow)dgv.Rows[0].Clone();
            // Copiar valores do cabeçalho
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                linhaSaidas.Cells[i].Value = dgv.Columns[i].HeaderText; // Copia o cabeçalho
            }
            // Mudar o nome da primeira coluna
            linhaSaidas.Cells[0].Value = "Saídas"; // Altera o valor da primeira coluna

            // Adiciona a nova linha ao DataGridView e pega indice
            int indexLinhaSaida = dgv.Rows.Add(linhaSaidas);

            // Mudar a cor de fundo e o estilo da fonte da nova linha
            DataGridViewRow row1 = dgv.Rows[indexLinhaSaida];
            row1.DefaultCellStyle.BackColor = Color.Red; // Altera a cor de fundo
            row1.DefaultCellStyle.ForeColor = Color.White; // Altera a cor do texto
            row1.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Altera a fonte


            for (int i = 0; i < linhasSaidas.Count; i++)
            {
                // Adiciona uma nova linha em branco
                int rowIndex = dgv.Rows.Add();

                dgv.Rows[rowIndex].Cells[0].Value = linhasSaidas[i].nomeSaida;

                // Insere cada valor em sua respectiva coluna
                for (int col = 0; col < linhasSaidas[i].valorMes.Count; col++)
                {
                    dgv.Rows[rowIndex].Cells[col + 1].Value = linhasSaidas[i].valorMes[col];
                }
            }

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
        }
    }
}
