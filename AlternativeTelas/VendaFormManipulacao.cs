using FinTracker.BD;
using FinTracker.Interfaces;
using FinTracker.Models;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.AlternativeTelas
{
    public partial class VendaFormManipulacao : Form
    {

        List<String> produtosSelecionados = new List<string>();
        List<String> produtosDisponiveisSelecao = new List<string>();
        List<Cliente> clientes = new List<Cliente>();
        List<Produto> produtos = new List<Produto>();
        Venda dadosVenda = new Venda();
        List<ItensVenda> itensVenda = new List<ItensVenda>();
        bool ehEdicao = false;
        int idVenda = -1;
        int id_Cliente = -1;
        Vendas telaQueChamou = null;
        public VendaFormManipulacao(Vendas quemChamou)
        {
            InitializeComponent();
            cmbParcelas.SelectedIndex = 0;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy 'às' HH:mm:ss";
            if(produtos.Count<1)
                preencherProdutos();
            else
                pnlProdutos.Controls.Add(novaLinhaAddProduto());
            preencherClientes();
            telaQueChamou = quemChamou;
        }
        public VendaFormManipulacao(Venda venda, Vendas quemChamou)
        {
            InitializeComponent();
            ehEdicao = true;
            idVenda = venda.IdVenda;
            id_Cliente = venda.IdCliente;
            this.Text = "Editar venda";
            btnSalvar.Text = "Atualizar";
            lblTitulo.Text = "Editando venda";
            telaQueChamou = quemChamou;

            dateTimePicker1.CustomFormat = "dd/MM/yyyy 'às' HH:mm:ss";
            cmbMetodoPag.SelectedItem = venda.Metodo;
            cmbParcelas.SelectedIndex = venda.Parcelas-1;
            rdbPendente.Checked = venda.Status == "Concluída" ? false : true;
            dateTimePicker1.Value = venda.DataTransacao;
            preencherClientes();
            preencherProdutos();
        }

        private async Task<bool> salvar()
        {
            VendaRepository vendaRepository = new VendaRepository();
            bool adicionouVenda = await vendaRepository.AddVenda(dadosVenda.IdCliente, dadosVenda.Status, dadosVenda.data, dadosVenda.Metodo, dadosVenda.Parcelas);
            if(!adicionouVenda)
            {
                MessageBox.Show("Erro ao adicionar venda!");
                return false;
            }
            int idVenda = int.Parse(await MetodosDB.executarQuerrySimples($"select id_Venda from venda where id_Cliente = {dadosVenda.IdCliente} and " +
                $"Status = '{dadosVenda.Status}' and data_transacao = '{dadosVenda.data}' and Metodo = '{dadosVenda.Metodo}' and parcelas = {dadosVenda.Parcelas}"));
            for(int i = 0; i < itensVenda.Count; i++)
                itensVenda[i].IdVenda = idVenda;
            ItensVendaRepository itensVendaRepository = new ItensVendaRepository();
            itensVendaRepository.AddItensVenda(itensVenda);
            return true;
        }

        private void pegarValores()
        {
            int indexCliente = cmbClientes.SelectedIndex;
            String status;
            if (rdbConcluida.Checked)
                status = rdbConcluida.Text;
            else
                status = rdbPendente.Text;
            dadosVenda.IdCliente = clientes[indexCliente].id_Cliente;
            dadosVenda.data = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            dadosVenda.Parcelas = int.Parse(cmbParcelas.Text);
            dadosVenda.Metodo = cmbMetodoPag.Text;
            dadosVenda.Status = status;

            for(int i = 0; i < pnlProdutos.Controls.Count-1; i++)
            {
                int indexProduto, qtdProd,idProd=0;
                decimal valorProd = 0;
                ComboBox comboProduto = pnlProdutos.Controls[i].Controls[0] as ComboBox;
                NumericUpDown upDown = pnlProdutos.Controls[i].Controls[1] as NumericUpDown;
                foreach(Produto p in produtos)
                {
                    if (p.Nome == comboProduto.SelectedItem)
                    {
                        idProd = p.IdProduto;
                        valorProd = p.ValorUnitario;
                        break;
                    }
                }
                qtdProd = int.Parse(upDown.Value.ToString());
                decimal preco = valorProd * qtdProd;
                itensVenda.Add(new ItensVenda
                {
                    IdVenda = this.idVenda == -1 ? 0 : this.idVenda,
                    IdProduto = idProd,
                    Quantidade = qtdProd,
                    Preco = preco
                });
            }
        }

        private bool preencheuCorretamente()
        {
            if (cmbClientes.Text == "")
                return false;
            else if (cmbMetodoPag.Text == "")
                return false;
            else if (pnlProdutos.Controls.Count <= 1)
                return false;
            return true;
        }

        private async void preencherClientes()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            clientes = await clienteRepository.PegarTodosClientes();
            foreach (Cliente c in clientes)
            {
                cmbClientes.Items.Add(c.Nome);
                produtosDisponiveisSelecao.Add(c.Nome);
            }
            if (ehEdicao)
            {
                for (int i = 0; i < clientes.Count; i++)
                {
                    if (clientes[i].id_Cliente == id_Cliente)
                    {
                        cmbClientes.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void preencherProdutos()
        {
            ProdutoRepository produtosRepository = new ProdutoRepository();
            produtos = await produtosRepository.PegarTodosProdutos();
            produtosDisponiveisSelecao.Clear();
            foreach(Produto p in produtos)
                produtosDisponiveisSelecao.Add(p.Nome);
            pnlProdutos.Controls.Add(novaLinhaAddProduto());
            if (ehEdicao)
            {
                ItensVendaRepository itensVendaRepository = new ItensVendaRepository();
                List<ItensVenda> idsEQtdProdutos = await itensVendaRepository.pegarIdsProdutosNaVenda(idVenda);
                
                for(int v=0; v<idsEQtdProdutos.Count;v++)
                {
                    ItensVenda i = idsEQtdProdutos[v];
                    ComboBox comboAddProd = pnlProdutos.Controls[pnlProdutos.Controls.Count - 1].Controls[0] as ComboBox;
                    NumericUpDown upDown = pnlProdutos.Controls[pnlProdutos.Controls.Count - 1].Controls[1] as NumericUpDown;
                    foreach(Produto p in produtos)
                    {
                        if(p.IdProduto == i.IdProduto)
                        {
                            comboAddProd.SelectedItem = p.Nome;
                            break;
                        }
                    }
                    upDown.Value = i.Quantidade;
                }
            }
        }
        

        
        private Control novaLinhaAddProduto()
        {
            Panel panel = new Panel();
            panel.Width = 722;
            panel.Height = 48;
            ComboBox comboProdutos = new ComboBox();
            comboProdutos.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            comboProdutos.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProdutos.Width = 588;
            comboProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboProdutos.Location = new Point(0, 0);
            comboProdutos.Items.Add("Adicionar produto");
            if (produtos.Count < 1 && produtosSelecionados.Count < 1)
                preencherProdutos();

            foreach (String p in produtosDisponiveisSelecao)
                comboProdutos.Items.Add(p);
            
            comboProdutos.SelectedIndex =0;
            comboProdutos.SelectedIndexChanged += cmbProdutoIndexChanged;
            Label legendaQtd = new Label();
            legendaQtd.Location = new Point(634, 4);
            legendaQtd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            legendaQtd.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            legendaQtd.Text = "Qtd";
            NumericUpDown qtd = new NumericUpDown();
            qtd.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            qtd.Maximum = 100;
            qtd.Minimum = 1;
            qtd.Width = 90;
            qtd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            qtd.Location = new Point(632, 2);
            qtd.Visible = false;
            Panel borda = new Panel();
            borda.Height = 1;
            borda.BackColor = Color.Gray;
            borda.Location = new Point(0, 40);
            borda.Width = 722;
            borda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel.Controls.Add(comboProdutos);
            panel.Controls.Add(qtd);
            panel.Controls.Add(borda);
            panel.Controls.Add(legendaQtd);
            panel.Dock = DockStyle.Top;
            return panel; //retorna a linha de produtos e deixa ela ao topo
        }

        private void atualizarComboAdicionarProduto()
        {
            ComboBox comboBox = pnlProdutos.Controls[pnlProdutos.Controls.Count - 1].Controls[0] as ComboBox;
            comboBox.Items.Clear();
            comboBox.Items.Add("Adicionar produto");
            foreach (String x in produtosDisponiveisSelecao)
                comboBox.Items.Add(x);
            comboBox.SelectedIndex = 0;
        }

        private void cmbProdutoIndexChanged(Object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            Control controlePai = combo.Parent;//pai do comboBox que chamou o metodo

            if (combo.Text == "Remover produto") // se foi escolhido remover o produto
            {
                String itemRemovido = combo.Items[1].ToString();//pega o item
                //atualiza e organiza as listas 
                produtosDisponiveisSelecao.Add(itemRemovido); 
                produtosSelecionados.Remove(itemRemovido);
                produtosDisponiveisSelecao.Sort();
                produtosSelecionados.Sort();
                //refaz comboBox de adicao de produto
                atualizarComboAdicionarProduto();
                //remove controle pai do comboBox que foi selecionado a remocao
                controlePai.Dispose();//remove linha 
            }
            else if (combo.Text != "Adicionar produto" && combo.Items[0] == "Adicionar produto") //se o comboBox do produto foi o combo de adicao de produtos
            {
                //transforma o adicionar produto em Remover produto
                combo.Items[0] = "Remover produto";
                produtosSelecionados.Add(combo.Text);
                controlePai.Controls[1].Visible = true;
                controlePai.Controls[3].Dispose();//remove legendaQtd
                //remove todos os produtos e deixa apenas o produto selecionado e a opcao de remover
                for(int i=combo.Items.Count-1; i >0; i--)
                {
                    String atual = combo.Items[i].ToString();
                    string selected = combo.Text;
                    if (atual == selected)
                        continue;
                    combo.Items.RemoveAt(i);
                }
                produtosDisponiveisSelecao.Remove(combo.Text);

                pnlProdutos.Controls.Add(novaLinhaAddProduto());
            }

        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ehEdicao)
            {
                this.Text = "Atualizando...";
                btnSalvar.Text = "Atualizando...";
                btnSalvar.Enabled = false;
                pegarValores();
                VendaRepository vendaRepository = new VendaRepository();
                dadosVenda.IdVenda = this.idVenda;
                vendaRepository.UpdateVendaAsync(dadosVenda);
                ItensVendaRepository itensVendaRepository = new ItensVendaRepository();
                bool aa = await itensVendaRepository.UpdateItensVenda(itensVenda);
                telaQueChamou.pegarVendas();//atualiza tabela da tela de Vendas
                this.Text = "Editar venda";
                btnSalvar.Text = "Atualizar";
                btnSalvar.Enabled = true;
                MessageBox.Show("Venda atualizada com sucesso.");
                return;
            }
            if (!preencheuCorretamente())
            {
                MessageBox.Show("Preecha todos os campos corretamente.");
                return;
            }
            this.Text = "Salvando...";
            btnSalvar.Text = "Salvando...";
            btnSalvar.Enabled = false;
            pegarValores();
            bool a = await salvar();
            this.Text = "Nova venda";
            btnSalvar.Enabled = true;
            btnSalvar.Text = "Salvar";
            MessageBox.Show("Venda salva com sucesso.");
            telaQueChamou.pegarVendas();//atualiza tabela da tela de Vendas
            limparInputs();
        }

        private void limparInputs()
        {
            cmbClientes.SelectedIndex = -1;
            cmbMetodoPag.SelectedIndex = -1;
            cmbParcelas.SelectedIndex = 0;
            for(int i=pnlProdutos.Controls.Count-1; i > 0; i--)
                pnlProdutos.Controls[i].Dispose();
        }
    }
}
