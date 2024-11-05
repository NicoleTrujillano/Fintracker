using FinTracker.AlternativeTelas;
using FinTracker.Interfaces;
using FinTracker.TelasPrincipais;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinTracker.Telas
{
    public partial class Home : Form
    {
        private String[] tabelasConfirmadas = null;//variavel para identificar tabelas que foram confirmadas em informações por periodo de tempo

        public Home()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de MMMMM \de yyyy.");
            
            anoDoPeriodo.MaxDate = DateTime.Today;
            cmbReceitaUltimo.SelectedIndex = 0;
            cmbTransacoesNeste.SelectedIndex = 0;
            tbData1.Text = DateTime.Today.AddMonths(-1).ToString("dd/MM/yyyy");
            tbData2.Text = DateTime.Today.ToString("dd/MM/yyyy");
            CarregarDadosAsync();
            buscarInfoPorPeriodo_Click(null, null);
            atualizaGrafico(null, null);
            
            addLabelTip(receitaAllTime, "Receita de todas as vendas + todas contas recebidas.");
            addLabelTip(despesasAllTime, "Soma de todas despesas (sem contar impostos).");
            //atualizarReceitaDoUltimo(null,null);
            atualizaTransacoesPendentes(null, null);
        }
        private async void CarregarDadosAsync()
        {
            qtdClientes.Text = await MetodosDB.executarQuerrySimples("select count(id_cliente) from cliente");
            qtdFornecedores.Text = await MetodosDB.executarQuerrySimples("select count(id_fornecedor) from fornecedor");
            qtdVendas.Text = await MetodosDB.executarQuerrySimples("select count(id_venda) from venda");
            qtdPagamentos.Text = await MetodosDB.executarQuerrySimples("select count(*) from pagamento");
        }

        private async Task<String> somarResultDeMultiplasQuerrys(String[] querrys)
        {
            return await MetodosDB.somarResultDeMultiplasQuerrys(querrys);
        }
        private void addLabelTip(object componente,String tip)
        {
            System.Windows.Forms.ToolTip dica = new System.Windows.Forms.ToolTip();
            dica.SetToolTip((Label)componente, tip);
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        /*
         * 
         * Bloco de codigo para informações por periodo de tempo
         *
         */

        private void panelFiltros_SizeChanged(object sender, EventArgs e)
        {
            if(panelFiltros.Width < 800)
                btnFiltrar.Location = new Point(btnFiltrar.Location.X,15);
            else
                btnFiltrar.Location = new Point(btnFiltrar.Location.X, 69);
        }

        private void numberOnlyInTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*metodo para permitir que o usuario digite apenas numeros no textBox*/

            // Verifica se a tecla pressionada nao é um número | backspace | '/' 
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;//bloqueia a entrada
        }

        private void inputData_TextChanged(object sender, EventArgs e)
        {
            /*metodo para formatar o estilo da data no textBox*/

            //peegando qual textBox solicitou o metodo
            System.Windows.Forms.TextBox input = (System.Windows.Forms.TextBox)sender;
            //pegando texto do textBox sem '/'
            string texto = input.Text.Replace("/", "").Trim();

            //se o texto for menor igual a 2, exibi apenas os caracteres iniciais (exclui a barra automaticamente)
            //exemplo: se input.text == "21/5" e o usuario deu backspace o texto recebe apenas "21"
            if (texto.Length <= 2)
                input.Text = texto;
            //se o texto for maior que dois e menor igual a 4, pode por a / no terceiro elemento da string texto (que é o texto do input sem '/')
            else if (texto.Length <= 4)
            {
                // Exibir dia/mês: 0107 -> 01/07
                input.Text = texto.Insert(2, "/");
            }
            //se o texto for maior que 4, pode por a / no terceiro e quinto elemento da string texto (que é o texto do input sem '/')
            else if (texto.Length > 4)
            {
                // Exibir dia/mês/ano: 11042024 -> 11/04/2024
                input.Text = texto.Insert(2, "/").Insert(5, "/");
            }

            // Mover o cursor para o final do texto
            input.SelectionStart = input.Text.Length;
        }

        public async void atualizarInfoPorPeriodo(String[] tabelasAlvo, int paginaAtual)
        {
            //atualiza as bolas de navegacao
            pnlPaginasPeriodoTempo.Width = tabelasAlvo.Length * 22;
            pnlPaginasPeriodoTempo.Location = new Point(panel4.Width - pnlPaginasPeriodoTempo.Width - 3, 0);
            /*selecionando a primeira pagina de navegacao*/
            if (paginaAtual == 0)
            {
                limparPaginasSelecionadas();
                pagina1infoPeriodo.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"shapes_backgrounds\miniCircleBlack.png");
                pagina1infoPeriodo.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
            String[][] tituloCard = new string[1][];
            /*pegando as datas na forma correta do mySqL*/
            String data1 = DateTime.Parse(tbData1.Text).ToString("yyyy-MM-dd");
            String data2 = DateTime.Parse(tbData2.Text).ToString("yyyy-MM-dd");

            if (tabelasAlvo[paginaAtual].ToLower() == "venda")
            {
                tituloCard[0] = new string[]{
                    "Valor total de vendas",
                    "Valor médio de venda",
                    "Vendas canceladas",
                    "Vendas concluídas",
                    "Vendas pendentes",
                    "Nº de produdos únicos vendidos",
                    "Nº Clientes com vendas registradas neste período",
                    "Quantidade de produtos vendidos" };

                limparValorCards();
                trocarTituloCards(tituloCard);

                valorCard1.Text =  "R$ " + await MetodosDB.executarQuerrySimples("SELECT sum(preco) FROM itensVenda iv " +
                    "inner join venda v on v.id_Venda = iv.id_Venda " +
                    $"where data_transacao between '{data1}' and '{data2}';");
                valorCard2.Text = "R$ " + await MetodosDB.executarQuerrySimples(
                    "SELECT FORMAT(AVG(total_venda), 2) " +
                    "FROM (" +
                    "SELECT iv.id_Venda, SUM(preco) AS total_venda FROM itensVenda iv " +
                    "inner join venda v on v.id_Venda = iv.id_Venda " +
                    $"where data_transacao between '{data1}' and '{data2}' GROUP BY iv.id_Venda)" +
                    "AS subquery;");
                valorCard3.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Venda) FROM venda where lower(Status) like 'cancelada' " +
                    $"and data_transacao between '{data1}' and '{data2}';");
                valorCard4.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Venda) FROM venda where lower(Status) like 'concluida' " +
                    $"and data_transacao between '{data1}' and '{data2}';");
                valorCard5.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Venda) FROM venda where lower(Status) like 'pendente' " +
                    $"and data_transacao between '{data1}' and '{data2}';");
                valorCard6.Text = await MetodosDB.executarQuerrySimples("SELECT count(distinct iv.id_Produto) FROM itensVenda iv " +
                    "inner join venda v on v.id_Venda = iv.id_Venda " +
                    $"where data_transacao between '{data1}' and '{data2}';");
                valorCard7.Text = await MetodosDB.executarQuerrySimples("SELECT count(distinct id_Cliente) FROM venda " +
                    $"where data_transacao between '{data1}' and '{data2}';");
                valorCard8.Text = await MetodosDB.executarQuerrySimples("SELECT sum(quantidade) FROM itensVenda iv " +
                    $"inner join venda v on v.id_Venda = iv.id_Venda where data_transacao between '{data1}' and '{data2}';");
                
            }
            else if (tabelasAlvo[paginaAtual].ToLower() == "pagamento")
            {
                tituloCard[0] = new string[]{
                    "Quantidade pagamentos feitos",
                    "Valor total dos pagamentos realizados",
                    "Valor médio de pagamento",
                    "Maior pagamento realizado",
                    "Menor pagamento realizado",
                    "Método de pagamento mais frequente",
                    "Método de pagamento menos frequente",
                    "" };
                limparValorCards();
                trocarTituloCards(tituloCard);
                valorCard1.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Pagamento) FROM pagamento " +
                    $"where Data between '{data1}' and '{data2}';");

                valorCard2.Text = "R$ " + await MetodosDB.executarQuerrySimples("SELECT sum(Valor) FROM pagamento " +
                    $"where Data between '{data1}' and '{data2}';");

                valorCard3.Text = "R$ " + await MetodosDB.executarQuerrySimples($"SELECT FORMAT(avg(Valor), 2) FROM pagamento where Data between '{data1}' and '{data2}';");

                valorCard4.Text = "R$ " + await MetodosDB.executarQuerrySimples($"SELECT max(Valor) FROM pagamento where Data between '{data1}' and '{data2}';");

                valorCard5.Text = "R$ " + await MetodosDB.executarQuerrySimples($"SELECT min(Valor) FROM pagamento where Data between '{data1}' and '{data2}';");

                String[] valorDoisUltimos2Cards = await MetodosDB.getFirstAndLastOfQuery(
                    $"SELECT distinct Metodo, count(Metodo) 'frequencia' FROM pagamento where date(Data) between '{data1}' and '{data2}' " +
                    "GROUP BY Metodo ORDER BY frequencia DESC;");
                valorCard6.Text = valorDoisUltimos2Cards[0];
                valorCard7.Text = valorDoisUltimos2Cards[1];

            }
            else if (tabelasAlvo[paginaAtual].ToLower() == "cliente")
            {
                tituloCard[0] = new string[]{
                    "Total de clientes da empresa",
                    "Clientes cadastrados neste periodo",
                    "Quantidade de clientes ativos",
                    "Quantidade de clientes inativos",
                    "% de clientes ativos",
                    "% de clientes inativos",
                    "Cidade com mais clientes",
                    "Cidade com menos clientes" };
                limparValorCards();
                trocarTituloCards(tituloCard);

                valorCard1.Text = await MetodosDB.executarQuerrySimples($"SELECT count(id_Cliente) FROM cliente where date(data_de_Cadastro)  <= '{data2}';");

                valorCard2.Text = await MetodosDB.executarQuerrySimples($"SELECT count(id_Cliente) FROM cliente where date(data_de_Cadastro)  between '{data1}' and '{data2}';");

                valorCard3.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Cliente) FROM cliente where lower(Status) like 'ativo' " +
                    $"and date(data_de_Cadastro)  <= '{data2}';");

                valorCard4.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Cliente) FROM cliente where lower(Status) like 'inativo' " +
                    $"and date(data_de_Cadastro)  <= '{data2}';");

                valorCard5.Text = await MetodosDB.executarQuerrySimples("SELECT FORMAT((COUNT(CASE WHEN status = 'ativo' THEN 1 END) / COUNT(*)) * 100, 2) FROM cliente " +
                    $"where date(Data_de_Cadastro) <= '{data2}';");

                valorCard6.Text = await MetodosDB.executarQuerrySimples("SELECT FORMAT((COUNT(CASE WHEN status = 'inativo' THEN 1 END) / COUNT(*)) * 100, 2) FROM cliente " +
                    $"where date(Data_de_Cadastro) <= '{data2}';");

                String[] valorDoisUltimos2Cards = await MetodosDB.getFirstAndLastOfQuery(
                    $"select distinct cidade, count(Cidade) 'frequencia' from cliente where date(Data_de_Cadastro) <= '{data2}' " +
                    "group by Cidade order by frequencia DESC;");
                valorCard7.Text = valorDoisUltimos2Cards[0];
                valorCard8.Text = valorDoisUltimos2Cards[1];

            }
            else if (tabelasAlvo[paginaAtual].ToLower() == "fornecedor")
            {
                tituloCard[0] = new string[]{
                    "Quantidade de fornecedores",
                    "Fornecedores cadastrados neste periodo",
                    "Quantidade de fornecedores ativos",
                    "Quantidade de fornecedores inativos",
                    "% de fornecedores ativos",
                    "% de fornecedores inativos",
                    "Cidade com mais fornecedores",
                    "Cidade com menos fornecedores" };
                limparValorCards();
                trocarTituloCards(tituloCard);

                valorCard1.Text = await MetodosDB.executarQuerrySimples($"SELECT count(*) FROM fornecedor where date(data_de_Cadastro)  <= '{data2}';");
                valorCard2.Text = await MetodosDB.executarQuerrySimples($"SELECT count(id_Fornecedor) FROM fornecedor where date(data_de_Cadastro)  between '{data1}' and '{data2}';");
                valorCard3.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Fornecedor) FROM fornecedor where lower(Status) like 'ativo' " +
                    $"and date(data_de_Cadastro)  <= '{data2}';");
                valorCard4.Text = await MetodosDB.executarQuerrySimples("SELECT count(id_Fornecedor) FROM fornecedor where lower(Status) like 'inativo' " +
                    $"and date(data_de_Cadastro)  <= '{data2}';");
                valorCard5.Text = await MetodosDB.executarQuerrySimples("SELECT FORMAT((COUNT(CASE WHEN status = 'ativo' THEN 1 END) / COUNT(*)) * 100, 2)  FROM fornecedor " +
                    $"where date(Data_de_Cadastro) <= '{data2}';");
                valorCard6.Text = await MetodosDB.executarQuerrySimples("SELECT FORMAT((COUNT(CASE WHEN status = 'inativo' THEN 1 END) / COUNT(*)) * 100, 2)  FROM fornecedor " +
                    $"where date(Data_de_Cadastro) <= '{data2}';");
                String[] valorDoisUltimos2Cards = await MetodosDB.getFirstAndLastOfQuery(
                    $"select distinct cidade, count(Cidade) 'frequencia' from fornecedor where date(Data_de_Cadastro) <= '{data2}' " +
                    "group by Cidade order by frequencia DESC");
                valorCard7.Text = valorDoisUltimos2Cards[0];
                valorCard8.Text = valorDoisUltimos2Cards[1];
            }
        }

        private void trocarTituloCards(String[][] tituloCard)
        {
            tituloCard1.Text = tituloCard[0][0];
            tituloCard2.Text = tituloCard[0][1];
            tituloCard3.Text = tituloCard[0][2];
            tituloCard4.Text = tituloCard[0][3];
            tituloCard5.Text = tituloCard[0][4];
            tituloCard6.Text = tituloCard[0][5];
            tituloCard7.Text = tituloCard[0][6];
            tituloCard8.Text = tituloCard[0][7];
        }

        private void limparValorCards()
        {
            valorCard1.Text = "";
            valorCard2.Text = "";
            valorCard3.Text = "";
            valorCard4.Text = "";
            valorCard5.Text = "";
            valorCard6.Text = "";
            valorCard7.Text = "";
            valorCard8.Text = "";
        }

        private void buscarInfoPorPeriodo_Click(object sender, EventArgs e)
        {
            int qtd = 0, atual = 0;
            if (chekVendas.Checked)
                qtd++;
            if (chekPagamentos.Checked)
                qtd++;
            if (chekClientes.Checked)
                qtd++;
            if (chekFornecedores.Checked)
                qtd++;
            String[] tabelasSelecionadas = new string[qtd];
            if (chekVendas.Checked)
            {
                tabelasSelecionadas[atual] = "venda";
                atual++;
            }
            if (chekPagamentos.Checked)
            {
                tabelasSelecionadas[atual] = ("pagamento");
                atual++;
            }
            if (chekClientes.Checked)
            {
                tabelasSelecionadas[atual] = ("cliente");
                atual++;
            }
            if (chekFornecedores.Checked)
            {
                tabelasSelecionadas[atual] = ("fornecedor");
            }
            tabelasConfirmadas = tabelasSelecionadas;
            atualizarInfoPorPeriodo(tabelasSelecionadas, 0);
        }

        private void mudarPagina(object sender, EventArgs e)
        {
            PictureBox quemChamou = (PictureBox)sender;
            string imagem = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"shapes_backgrounds\miniCircleBlack.png");
            if (pnlPaginasPeriodoTempo.Width == 22)
                return;
            if (pagina1infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom) // se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {
                if (quemChamou == proximaPagina)
                    selecionaPaginaEspecifica(pagina2infoPeriodo);
                else
                {
                    
                    if(pnlPaginasPeriodoTempo.Width == 22 * 2)
                    {
                        selecionaPaginaEspecifica(pagina2infoPeriodo);
                        return;
                    }
                    else if(pnlPaginasPeriodoTempo.Width == 22 * 3)
                    {
                        selecionaPaginaEspecifica(pagina3infoPeriodo);
                        return;
                    }
                    selecionaPaginaEspecifica(pagina4infoPeriodo);
                }
                return;
            }
            if (pagina2infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom)// se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {

                if (quemChamou == proximaPagina)
                {
                    if(pnlPaginasPeriodoTempo.Width > 22*2)
                        selecionaPaginaEspecifica(pagina3infoPeriodo);
                    else
                        selecionaPaginaEspecifica(pagina1infoPeriodo);
                }
                else
                    selecionaPaginaEspecifica(pagina1infoPeriodo);
                return;
            }
            if (pagina3infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom)// se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {
                if (quemChamou == proximaPagina)
                {
                    if (pnlPaginasPeriodoTempo.Width > 22 * 3)
                        selecionaPaginaEspecifica(pagina4infoPeriodo);
                    else
                        selecionaPaginaEspecifica(pagina1infoPeriodo);
                }
                else
                    selecionaPaginaEspecifica(pagina2infoPeriodo);
                return;
            }
            if (pagina4infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom)// se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {
                if (quemChamou == proximaPagina)
                    selecionaPaginaEspecifica(pagina1infoPeriodo);
                else
                    selecionaPaginaEspecifica(pagina3infoPeriodo);
                return;
            }
        }
        private void limparPaginasSelecionadas()
        {
            string imagem = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"shapes_backgrounds\miniCircleGray.png");
            pagina1infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina2infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina3infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina4infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina1infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
            pagina2infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
            pagina3infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
            pagina4infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void mudarPaginaNoClickBolinha(object sender, EventArgs e)
        {
            PictureBox objeto = (PictureBox)sender;
            if(objeto.SizeMode != PictureBoxSizeMode.Zoom)
            {
                limparPaginasSelecionadas();
                selecionaPaginaEspecifica(objeto);
            }
        }
        private void selecionaPaginaEspecifica(PictureBox bolinhaAlvo)
        {
            limparPaginasSelecionadas();
            bolinhaAlvo.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"shapes_backgrounds\miniCircleBlack.png");
            bolinhaAlvo.SizeMode = PictureBoxSizeMode.Zoom;
            //String[] tabelasSelecionadas = pegarTabelasSelecionadas();
            int pagina = 0;//começa na pagina 1
            if (bolinhaAlvo == pagina2infoPeriodo)
                pagina = 1;
            else if (bolinhaAlvo == pagina3infoPeriodo)
                pagina = 2;
            else if (bolinhaAlvo == pagina4infoPeriodo)
                pagina = 3;
            atualizarInfoPorPeriodo(tabelasConfirmadas, pagina);
        }

        /*
         * 
         * Bloco de codigo para Relatório de entradas e saidas
         *
         */
        

        private async void atualizaGrafico(Object sender, EventArgs e)
        {
            // Limpa séries e áreas do gráfico se já existirem
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Cria área do gráfico
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // Cria duas séries (duas barras por coluna)
            Series saida = new Series("Total de saídas");
            Series entrada = new Series("Total de entradas");
            // Define que as séries serão do tipo coluna (barras verticais)
            saida.ChartType = SeriesChartType.Column;
            entrada.ChartType = SeriesChartType.Column;
            //instanciando os possiveis dados de x
            String[] meses = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            String[] trimestres = { "Jan Fev Mar", "Abr Mai Jun", "Jul Ago Set", "Out Nov Dez" };
            String[] semestre = { "1º semestre", "2º semestre" };
            
            String ano = anoDoPeriodo.Value.ToString("yyyy");
            
            if (rdbMes.Checked)
            {
                for (int i = 0; i < 12; i++)
                {
                    String entradaAtual = await somarResultDeMultiplasQuerrys(new string[]
                    {   
                        "SELECT sum(preco) 'valor total de vendas' FROM itensVenda iv inner join venda v on v.id_Venda = iv.id_Venda " +
                        $"where date_format(v.data_transacao, '%m') = '{(i+1):D2}' " +
                        $"and year(data_transacao) = {ano};"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where date_format(Data_de_Transacao,'%m') = '{(i+1):D2}' " +
                        $"and year(Data_de_Transacao) = {ano}"
                    });

                    String saidaAtual = await somarResultDeMultiplasQuerrys(new String[]
                    {
                        $"select sum(Valor) from conta_a_pagar where id_Pagamento is null and date_format(Data_de_Transacao, '%m') = '{(i + 1):D2}' " +
                        $"and year(Data_de_Transacao) = {ano}"
                        ,
                        $"select sum(Valor) from pagamento where date_format(Data, '%m') = '{(i + 1):D2}' and year(Data) = {ano}"
                    });
                    if (entradaAtual != "" && entradaAtual != null)
                        entrada.Points.AddXY(meses[i], double.Parse(entradaAtual));
                    else
                        entrada.Points.AddXY(meses[i], 0.0);
                    if(saidaAtual != "" && saidaAtual != null)
                        saida.Points.AddXY(meses[i], double.Parse(saidaAtual));
                    else
                        saida.Points.AddXY(meses[i], 0.0);

                }
            }
            
            if(rdbTrimestre.Checked)
            {
                for (int i = 0; i < 4; i++)
                {
                    int volta = i + 1;
                    int mesFim = volta * 3;
                    String entradaAtual = await somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(preco) 'valor total de vendas' FROM itensVenda iv inner join venda v on v.id_Venda = iv.id_Venda " +
                        $"where date_format(v.data_transacao, '%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}' " +
                        $"and year(data_transacao) = {ano};"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where date_format(Data_de_Transacao,'%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}' " +
                        $"and year(Data_de_Transacao) = {ano}"
                    });

                    String saidaAtual = await somarResultDeMultiplasQuerrys(new String[]
                    {
                        $"select sum(Valor) from conta_a_pagar where id_Pagamento is null and date_format(Data_de_Transacao, '%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}' " +
                        $"and year(Data_de_Transacao) = {ano}"
                        ,
                        $"select sum(Valor) from pagamento where date_format(Data, '%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}' " +
                        $"and year(Data) = {ano}"
                    });

                    entrada.Points.AddXY(trimestres[i], double.Parse(entradaAtual));
                    if (saidaAtual != "")
                        saida.Points.AddXY(trimestres[i], double.Parse(saidaAtual));
                    else
                        saida.Points.AddXY(meses[i], 0.0);
                }
            }
            
            else if(rdbSemestre.Checked)
            {
                for (int i = 0; i < 2; i++)
                {
                    int mesFim = (i+1) * 6;
                    String entradaAtual = await somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(preco) 'valor total de vendas' FROM itensVenda iv inner join venda v on v.id_Venda = iv.id_Venda " +
                        $"where date_format(v.data_transacao, '%m') between '{(mesFim-5):D2}' and '{(mesFim):D2}' " +
                        $"and year(data_transacao) = {ano};"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where date_format(Data_de_Transacao,'%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}' " +
                        $"and year(Data_de_Transacao) = {ano}"
                    });

                    String saidaAtual = await somarResultDeMultiplasQuerrys(new String[]
                    {
                        $"select sum(Valor) from conta_a_pagar where id_Pagamento is null and year(Data_de_Transacao) = {ano} " +
                        $"and date_format(Data_de_Transacao, '%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}'"
                        ,
                        $"select sum(Valor) from pagamento where date_format(Data, '%m') between '{(mesFim-5):D2}' and '{(mesFim):D2}' " +
                        $"and year(Data) = {ano}"
                    });

                    entrada.Points.AddXY(semestre[i], double.Parse(entradaAtual));
                    if (saidaAtual != "")
                        saida.Points.AddXY(semestre[i], double.Parse(saidaAtual));
                    else
                        saida.Points.AddXY(meses[i], 0.0);
                }
            }
            
            //colocando os valores das legendas Y no formato de Moeda
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "C2";  // "C2" para exibir como moeda com 2 casas decimais
            //mudando cor das colunas
            saida.Color = Color.FromArgb(182, 2, 2);//setando cor das colunas vermelhas
            entrada.Color = Color.FromArgb(17, 134, 50);//setando cor das colunas verdes
            //configura largura das colunas
            saida["PointWidth"] = "0.6";
            entrada["PointWidth"] = "0.6";
            chartArea.AxisX.MajorGrid.Enabled = false; // Desabilita gridlines no eixo X ()
            // Adiciona as séries no gráfico
            chart1.Series.Clear();
            chart1.Series.Add(saida);
            chart1.Series.Add(entrada);
            //removendo legendas
            chart1.Legends.Clear();
            chartArea.AxisX.Interval = 1; // Define intervalo no eixo X para espaçar as categorias
            //permite saber o valor de cada coluna ao repousar mouse sobre ela
            chart1.Series[0].ToolTip = "R$ #VALY{N2}";
            chart1.Series[1].ToolTip = "R$ #VALY{N2}";
        }


        /*
         * 
         * Bloco de codigo para seção dos 3 cards
         *
         */

        private async void changeVisibityDadosTodosTempos(object sender, EventArgs e)
        {
            PictureBox queChamou = (PictureBox)sender;
            if (queChamou == verReceita)
            {
                if (receitaAllTime.Text == "-")
                {
                    receitaAllTime.Text = "R$ ";
                    verReceita.BackgroundImage = Properties.Resources.icon_olhoFechadoBranco_1_;
                    receitaAllTime.Text += await somarResultDeMultiplasQuerrys(new string[]
                    {
                        "SELECT sum(preco) 'valor total de vendas' FROM itensVenda iv inner join venda v on v.id_Venda = iv.id_Venda ",
                        "select sum(valor) from conta_a_receber"
                    });
                }
                else
                {
                    receitaAllTime.Text = "-";
                    verReceita.BackgroundImage = Properties.Resources.icon_olhoAberto;
                }
            }
            if (queChamou == verSaidas)
            {
                if (despesasAllTime.Text == "-")
                {
                    despesasAllTime.Text = "R$ ";
                    verSaidas.BackgroundImage = Properties.Resources.icon_olhoFechadoBranco_1_;
                    despesasAllTime.Text += await somarResultDeMultiplasQuerrys(new string[]
                    {   //total pagos + total a pagar
                        "select sum(Valor) from conta_a_pagar where id_Pagamento is null and year(Data_de_Transacao)",
                        "select sum(Valor) from pagamento"
                    });
                }
                else
                {
                    despesasAllTime.Text = "-";
                    verSaidas.BackgroundImage = Properties.Resources.icon_olhoAberto;
                }
            }
        }

        private async void atualizarReceitaDoUltimo(object sender, EventArgs e)
        {
            
            String mes = "", ano="";
            if (cmbReceitaUltimo.Text == "Mês")
            {
                mes = DateTime.Today.AddMonths(-1).ToString("MM");
                ano = DateTime.Today.AddMonths(-1).ToString("yyyy");
            }
            else if (cmbReceitaUltimo.Text == "Bimestre")
            {
                mes = DateTime.Today.AddMonths(-2).ToString("MM");
                ano = DateTime.Today.AddMonths(-2).ToString("yyyy");
            }
            else if (cmbReceitaUltimo.Text == "Trimestre")
            {
                mes = DateTime.Today.AddMonths(-3).ToString("MM");
                ano = DateTime.Today.AddMonths(-3).ToString("yyyy");
            }
            else if (cmbReceitaUltimo.Text == "Semestre")
            {
                mes = DateTime.Today.AddMonths(-6).ToString("MM");
                ano = DateTime.Today.AddMonths(-6).ToString("yyyy");
            }
            else if (cmbReceitaUltimo.Text == "Ano")
            {
                mes = DateTime.Today.AddYears(-1).ToString("MM");
                ano = DateTime.Today.AddYears(-1).ToString("yyyy");
            }
            String entradas = await somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(preco) 'valor total de vendas' FROM itensVenda iv inner join venda v on v.id_Venda = iv.id_Venda " +
                        $"where date_format(v.data_transacao, '%m') between '{mes}' and '{DateTime.Now.Month:D2}' " +
                        $"and year(data_transacao) between {ano} and {DateTime.Now.Year};"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where date_format(Data_de_Transacao,'%m') between '{mes}' and '{DateTime.Now.Month:D2}' " +
                        $"and year(Data_de_Transacao) between {ano} and {DateTime.Now.Year}"
                    });

            String saidas = await somarResultDeMultiplasQuerrys(new String[]
                    {
                        $"select sum(Valor) from conta_a_pagar where id_Pagamento is null and year(Data_de_Transacao) between {ano} and {DateTime.Now.Year} " +
                        $"and date_format(Data_de_Transacao, '%m') between '{mes}' and '{DateTime.Now.Month:D2}'"
                        ,
                        $"select sum(Valor) from pagamento where date_format(Data, '%m') between '{mes}' and '{DateTime.Now.Month:D2}' " +
                        $"and year(Data) between {ano} and {DateTime.Now.Year}"
                    });


            entradasPeriodo.Text = "R$ " + entradas;
            saidasPeriodo.Text = "R$ " + saidas;
            lucroPeriodo.Text = "R$ " + (double.Parse(entradas) - double.Parse(saidas));
        }

        private async void atualizaTransacoesPendentes(object sender, EventArgs e)
        {
            String mes = "", ano="";
            if (cmbTransacoesNeste.Text == "Mês")
            {
                mes = DateTime.Today.AddMonths(-1).ToString("MM");
                ano = DateTime.Today.AddMonths(-1).ToString("yyyy");
            }
            else if (cmbTransacoesNeste.Text == "Bimestre")
            {
                mes = DateTime.Today.AddMonths(-2).ToString("MM");
                ano = DateTime.Today.AddMonths(-2).ToString("yyyy");
            }
            else if (cmbTransacoesNeste.Text == "Trimestre")
            {
                mes = DateTime.Today.AddMonths(-3).ToString("MM");
                ano = DateTime.Today.AddMonths(-3).ToString("yyyy");
            }
            else if (cmbTransacoesNeste.Text == "Semestre")
            {
                mes = DateTime.Today.AddMonths(-6).ToString("MM");
                ano = DateTime.Today.AddMonths(-6).ToString("yyyy");
            }
            else if (cmbTransacoesNeste.Text == "Ano")
            {
                mes = DateTime.Today.AddYears(-1).ToString("MM");
                ano = DateTime.Today.AddYears(-1).ToString("yyyy");
            }

            String str1 = await MetodosDB.executarQuerrySimples(
                $"SELECT sum(Valor) FROM conta_a_pagar where month(Data_de_Transacao) >= '{mes}' and year(Data_de_Transacao) >= {ano} " +
                $"and day(Previsao_de_Termino) >= {DateTime.Today.Day} and month(Previsao_de_Termino) >= '{DateTime.Now.Month:D2}' and year(Previsao_de_Termino) >= {DateTime.Now.Year};");
            String str2 = await MetodosDB.executarQuerrySimples(
                $"SELECT sum(Valor) FROM conta_a_receber where month(Data_de_Transacao) >= '{mes}' and year(Data_de_Transacao) >= {ano} " +
                $"and day(Previsao_de_Termino) >= {DateTime.Today.Day} and month(Previsao_de_Termino) >= '{DateTime.Now.Month:D2}' and year(Previsao_de_Termino) >= {DateTime.Now.Year};");
            double aPagar=0, aReceber=0;
            if (double.TryParse(str1, out aPagar));
            if (double.TryParse(str2, out aReceber));
            if(aPagar==0 && aReceber==0)
                circularProgressBar1.Value = 0;
            else
                circularProgressBar1.Value = (int)(((aPagar / aReceber) * 100)/2);
            aPagarNeste.Text = "R$ " + aPagar;
            aReceberNeste.Text = "R$ " + aReceber;
            
        }


        /*
         * 
         * Bloco de codigo para Fluxo de caixa
         *
         */
        private void dateTimePickerInicio_ValueChanged(object sender, EventArgs e)
        {
            // Garante que a data mínima do DateTimePicker final não seja menor que a do inicial
            dateTimePickerFim.MinDate = dateTimePickerInicio.Value.AddMonths(1);
        }

        private void dateTimePickerFim_ValueChanged(object sender, EventArgs e)
        {
            // Garante que a data do DateTimePicker final seja sempre igual ou posterior ao inicial
            if (dateTimePickerFim.Value < dateTimePickerInicio.Value)
            {
                MessageBox.Show("A data final não pode ser menor que a data inicial.", "Data Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerFim.MinDate = dateTimePickerInicio.Value.AddMonths(1);
            }
        }

        private void btnConfirmarDataFluxoCaixa_Click(object sender, EventArgs e)
        {
            var resultado = new List<(int Ano, int Mes)>();

            // Loop para adicionar cada par de ano e mês no intervalo
            DateTime dataAtual = new DateTime(dateTimePickerInicio.Value.Year, dateTimePickerInicio.Value.Month, 1);
            while (dataAtual <= dateTimePickerFim.Value)
            {
                resultado.Add((dataAtual.Year, dataAtual.Month));
                dataAtual = dataAtual.AddMonths(1); // Incrementa o mês
            }

            fluxoDeCaixa1.pegarDados(resultado);
        }
    }
}
