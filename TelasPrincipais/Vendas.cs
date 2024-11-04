using FinTracker.AlternativeTelas;
using FinTracker.BD;
using FinTracker.Controles;
using FinTracker.Interfaces;
using FinTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.TelasPrincipais
{
    public partial class Vendas : Form
    {
        VendaRepository vendaRepository = new VendaRepository();
        List<Venda> listaVendas = null;
        List<int> idsVendaSelecionada = new List<int>();
        List<int> indexVendasSelecionadas = new List<int>();

        private int paginaAtual = 1, totalPaginas = 1;
        public Vendas()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            cmbbox_OPVendas.SelectedIndex = 0;
            cmbPagamento.SelectedIndex = 0;
            atualizarNavegacao(paginaAtual, totalPaginas);
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private async void atualizarProdutos(Object sender, EventArgs e)
        {
            if (rdbTodosProdutos.Checked)
            {
                flowLayoutPanel1.Controls.Clear();
                return;
            }
            ProdutoRepository p = new ProdutoRepository();
            List<String> produtos = await p.GetNomeProdutos();
            foreach(String x in produtos)
            {
                CheckBox check = new CheckBox();
                check.Text = x;
                flowLayoutPanel1.Controls.Add(check);
            }
        }

        public async void pegarVendas()
        {
            listaVendas = await vendaRepository.GetVendas();
            totalPaginas = listaVendas.Count / 10;
            if (totalPaginas < 1)
                totalPaginas = 1;
            atualizarNavegacao(paginaAtual, totalPaginas);
            atualizarTabela();
        }


        private async void atualizarTabela()
        {
            if (listaVendas == null)
            {
                pegarVendas();
                return;
            }
            //remove os controles da tabela
            tabela.Controls.Clear();
            //limpa definições de estilo das linhas
            tabela.RowStyles.Clear();
            tabela.RowCount = 1;
            tabela.Height = 72;
            int limitePagina = paginaAtual * 10;
            for (int i=limitePagina-10; i < limitePagina; i++)
            {
                //adiciona a altura de +1 linha na tabela
                tabela.Height += 72;
                //adiciona linha a tabela (com heigth 72)
                tabela.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));

                //cria a VendaRow
                VendaRow venda = new VendaRow(listaVendas[i], this);

                //faz a venda preencher todo espaço da linha
                venda.Dock = DockStyle.Fill;

                // adiciona a venda a tabela, na coluna 0 e na linha 1 antes da ultima criada
                tabela.Controls.Add(venda, 0, tabela.RowCount - 1);
                //adiciona mais uma linha a tabela
                tabela.RowCount += 1;
                //se for a ultima linha
                if (i + 1 >= listaVendas.Count)
                {
                    //remove a ultima linha criada
                    tabela.RowCount -= 1;
                    tabela.Height -= 72;
                    break;
                }
            }
            foreach (Control control in tabela.Controls)
            {
                //pega a VendaRow de cada linha da tabela
                VendaRow venda = (VendaRow)control;
                //adiciona o metodo verSelecionados ao panelDoCheck de cada venda
                venda.Controls["panelDoCheck"].MouseClick += verSelecionados;
                //pega o checkBox da VendaRow de cada linha da tabela
                Control checkDaVenda = venda.Controls["panelDoCheck"].Controls["checkBox1"];
                //adiciona o metodo para contagem de selecionados a cada checkBox
                checkDaVenda.MouseClick += verSelecionados;
            }
        }


        private void verSelecionados(object sender, EventArgs e)
        {
            int qtdSelecionada = 0;
            for (int i = 0; i < tabela.RowCount; i++)
            {
                //pega venda na linha atual
                VendaRow venda = (VendaRow)tabela.GetControlFromPosition(0, i);
                if (venda == null)
                    return;
                //se venda esta selecionada
                if (venda.Checked())
                {
                    qtdSelecionada++; //aumenta qtdSelecionada
                    if (idsVendaSelecionada.Contains(venda.dadosVenda.IdVenda))
                        continue;
                    idsVendaSelecionada.Add(venda.dadosVenda.IdVenda);
                    indexVendasSelecionadas.Add(i);
                }
                else
                {
                    idsVendaSelecionada.Remove(venda.dadosVenda.IdVenda);
                    indexVendasSelecionadas.Remove(i);
                }
            }
            //atualiza texto de linhas selecionadas
            lblPaginasSelecionadas.Text = $"{qtdSelecionada} de {tabela.RowCount} linhas selecionada(s).";

        }

        private void atualizarNavegacao(int paginaAtual, int totalPaginas)
        {
            //se total paginas cabe dentro do limite dos quadrados de navegacao
            if (totalPaginas <= 8)
            {
                if (totalPaginas < 8)
                {
                    pnlPage8.Visible = false;
                    if (totalPaginas < 7)
                    {
                        pnlPage7.Visible = false;
                        if (totalPaginas < 6)
                        {
                            pnlPage6.Visible = false;
                            if (totalPaginas < 5)
                            {
                                pnlPage5.Visible = false;
                                if (totalPaginas < 4)
                                {
                                    pnlPage4.Visible = false;
                                    if (totalPaginas < 3)
                                    {
                                        pnlPage3.Visible = false;
                                        if (totalPaginas < 2)
                                        {
                                            pnlPage2.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Se a página atual está muito perto do início, exibir as primeiras 6 páginas
            else if (paginaAtual <= 6)
            {
                lblPage2.Text = "2";
                lblPage3.Text = "3";
                lblPage4.Text = "4";
                lblPage5.Text = "5";
                lblPage6.Text = "6";
                lblPage7.Text = "...";
                lblPage8.Text = totalPaginas.ToString();
            }
            // Se a página atual está muito perto do fim, exibir as últimas 6 páginas
            else if (paginaAtual >= totalPaginas - 5)
            {
                lblPage2.Text = "...";
                lblPage3.Text = (totalPaginas - 5).ToString();
                lblPage4.Text = (totalPaginas - 4).ToString();
                lblPage5.Text = (totalPaginas - 3).ToString();
                lblPage6.Text = (totalPaginas - 2).ToString();
                lblPage7.Text = (totalPaginas - 1).ToString();
                lblPage8.Text = totalPaginas.ToString();
            }
            // Se página atual no meio, exibir página atual e mais algumas ao redor
            else
            {
                // Exibir 1 página antes da página atual e 2 após
                lblPage2.Text = "...";
                lblPage3.Text = (paginaAtual - 1).ToString();
                lblPage4.Text = (paginaAtual).ToString();
                lblPage5.Text = (paginaAtual + 1).ToString();
                lblPage6.Text = (paginaAtual + 2).ToString();
                lblPage7.Text = "...";
                lblPage8.Text = totalPaginas.ToString();
            }
            //limpar estilo da antiga pagina selecionada
            limparPaginasSelecionadas();
            //mudar cor da atual pagina selecionada
            selecionarPaginaAtual();
            //preenche a tabela com dados da pagina selecionada
            atualizarTabela();
            //atualiza label de qtd de linhas selecionadas
            verSelecionados(null, null);
        }
        private void limparPaginasSelecionadas()
        {
            pnlPage1.BackColor = Color.Transparent; pnlPage2.BackColor = Color.Transparent; pnlPage3.BackColor = Color.Transparent; pnlPage4.BackColor = Color.Transparent;
            pnlPage5.BackColor = Color.Transparent; pnlPage6.BackColor = Color.Transparent; pnlPage7.BackColor = Color.Transparent; pnlPage8.BackColor = Color.Transparent;
        }
        private void selecionarPaginaAtual()
        {
            if (lblPage1.Text == paginaAtual.ToString())
                pnlPage1.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage2.Text == paginaAtual.ToString())
                pnlPage2.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage3.Text == paginaAtual.ToString())
                pnlPage3.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage4.Text == paginaAtual.ToString())
                pnlPage4.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage5.Text == paginaAtual.ToString())
                pnlPage5.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage6.Text == paginaAtual.ToString())
                pnlPage6.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage7.Text == paginaAtual.ToString())
                pnlPage7.BackColor = Color.FromArgb(230, 230, 230);
            else if (lblPage8.Text == paginaAtual.ToString())
                pnlPage8.BackColor = Color.FromArgb(230, 230, 230);
        }

        public void paginaMouseIn(Object sender, EventArgs e)
        {
            Panel a = sender as Panel;
            if (a.Controls[0].Text == paginaAtual.ToString())
                return;
            a.BackColor = Color.FromArgb(247, 247, 242);
        }
        public void paginaMouseLeave(Object sender, EventArgs e)
        {
            Panel a = sender as Panel;
            if (a.Controls[0].Text == paginaAtual.ToString())
                return;
            a.BackColor = Color.Transparent;
        }

        private void selecionarPaginaPeloQuadrado(Object sender, EventArgs e)
        {
            Panel quadrado = sender as Panel;
            String numeroDoPanel = "";
            if (sender == pnlPage1)
                numeroDoPanel = lblPage1.Text;
            else if (sender == pnlPage2)
                numeroDoPanel = lblPage2.Text;
            else if (sender == pnlPage3)
                numeroDoPanel = lblPage3.Text;
            else if (sender == pnlPage4)
                numeroDoPanel = lblPage4.Text;
            else if (sender == pnlPage5)
                numeroDoPanel = lblPage5.Text;
            else if (sender == pnlPage6)
                numeroDoPanel = lblPage6.Text;
            else if (sender == pnlPage7)
                numeroDoPanel = lblPage7.Text;
            else if (sender == pnlPage8)
                numeroDoPanel = lblPage8.Text;
            if (int.TryParse(numeroDoPanel, out int n))
            {
                if (n == paginaAtual)
                    return;
                paginaAtual = n;
                atualizarNavegacao(paginaAtual, totalPaginas);
            }
        }
        private void selecionarPaginaPeloNumeroDoQuadrado(Object sender, EventArgs e)
        {
            Label lblClicada = sender as Label;
            if (int.TryParse(lblClicada.Text, out int n))
            {
                if (n == paginaAtual)
                    return;
                paginaAtual = n;
                atualizarNavegacao(paginaAtual, totalPaginas);
            }
        }
        private void proximaPagina_Click(object sender, EventArgs e)
        {
            if (paginaAtual == totalPaginas)
                return;
            paginaAtual++;
            atualizarNavegacao(paginaAtual, totalPaginas);
        }

        private void lblFiltrar_Click(object sender, EventArgs e)
        {
            pnlFiltros.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFiltros.Visible = false;
        }

        private void btn_AdVenda_Click(object sender, EventArgs e)
        {
            VendaFormManipulacao vendaForm = new VendaFormManipulacao(this);
            vendaForm.Show();
        }

        private async void btn_DelVenda_Click(object sender, EventArgs e)
        {
            if (idsVendaSelecionada.Count < 1)
            {
                MessageBox.Show("Selecione as vendas que deseja excluir.");
                return;
            }
            //foreach (int i in idVendaSelecionada) { }
            if (MessageBox.Show($"{idsVendaSelecionada.Count} venda(s) selecionada(s).\n" +
                "Deseja excluir todas vendas selecionadas?",
                "Deseja excluir as vendas permanentemente ?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool funfou = await MetodosDB.deleteVendaById(idsVendaSelecionada);
                if (funfou)
                {
                    MessageBox.Show("Exclusao realizada com sucesso.");
                    pegarVendas();
                }
                else
                    MessageBox.Show("Erro ao excluir vendas.");
            }
        }

        private void btn_EditVenda_Click(object sender, EventArgs e)
        {
            if(idsVendaSelecionada.Count > 1 || idsVendaSelecionada.Count < 1)
            {
                MessageBox.Show($"Selecione 1 venda para edição.\n{idsVendaSelecionada.Count} vendas selecionadas.");
                return;
            }
            foreach(int i in indexVendasSelecionadas)
            {
                //pega venda na linha atual
                VendaRow venda = (VendaRow)tabela.GetControlFromPosition(0, i);
                venda.abrirEdicao(null,null);
            }
        }

        private void paginaAnterior_Click(object sender, EventArgs e)
        {
            if (paginaAtual == 1)
                return;
            paginaAtual--;
            atualizarNavegacao(paginaAtual, totalPaginas);
        }
    }
}
