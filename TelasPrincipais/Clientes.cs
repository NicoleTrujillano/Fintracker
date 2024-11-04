using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinTracker.AlternativeTelas;
using FinTracker.BD;
using FinTracker.Models;

namespace FinTracker.TelasPrincipais
{
    public partial class Clientes : Form
    {
        ClienteRepository clienteRepository = new ClienteRepository();
        public Clientes()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            atualizarTabela();
            // Definir o modo de ajuste de colunas para preencher todo o espaço
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 40;
            // Adiciona uma coluna delete de botão com ícone
            DataGridViewImageColumn columnDelete = new DataGridViewImageColumn();
            columnDelete.HeaderText = "";
            columnDelete.Name = "delete";
            columnDelete.Image = Properties.Resources.icon_delete; // colocando icone em cada linha
            columnDelete.MinimumWidth = 40;
            columnDelete.Width = 40;
            columnDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;//retirando autoSize dessa coluna
            // Adicionando uma coluna de botão com ícone
            DataGridViewImageColumn columnEdit = new DataGridViewImageColumn();
            columnEdit.HeaderText = "";
            columnEdit.Name = "edit";
            columnEdit.Image = Properties.Resources.icon_edit; // colocando icone em cada linha
            columnEdit.MinimumWidth = 40;
            columnEdit.Width = 40;
            columnEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;//retirando autoSize dessa coluna
            dataGridView1.Columns.Add(columnDelete);
            dataGridView1.Columns.Add(columnEdit);
            dataGridView1.ReadOnly = true;
        }
        private void pnlPesquisarCliente_Click(object sender, EventArgs e)
        {
            txbPesquisa.Focus();
        }
        public async void atualizarTabela()
        {
            dataGridView1.DataSource = await clienteRepository.GetClientes();
            //colocando colunas delete e edit que já existem em ultimo
            dataGridView1.Columns["delete"].DisplayIndex = dataGridView1.Columns.Count - 1;
            dataGridView1.Columns["edit"].DisplayIndex = dataGridView1.Columns.Count - 1;
        }
        private void btnNovoCliente_Click(object sender, EventArgs e)
        {
            ClienteFormManipulacao formManipulacao = new ClienteFormManipulacao(this, null);
            formManipulacao.Show();
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Para excluir pelo botão selecione as linhas desejadas.");
                return;
            }
            int qtdLinhasSelecionadas = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            List<int> idsExclusao = new List<int>();
            if (MessageBox.Show($"{qtdLinhasSelecionadas} clientes selecionados.\n" +
                "Deseja excluir todos cientes selecionados?",
                "Deseja excluir lista de clientes permanentemente ?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = 0; i < qtdLinhasSelecionadas; i++)
                {
                    idsExclusao.Add(int.Parse(dataGridView1.SelectedRows[i].Cells["id_Cliente"].Value.ToString()));
                }
                ClienteRepository f = new ClienteRepository();
                f.DeleteListaCliente(idsExclusao);
                Thread.Sleep(1200);
                atualizarTabela();
            }
        }

        private async void txbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (txbPesquisa.Text == "")
                atualizarTabela();
            dataGridView1.DataSource = await clienteRepository.pesquisarEmTudo(txbPesquisa.Text);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txbPesquisa.Text = "";
            atualizarTabela();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Cliente cliente = new Cliente();
            if (e.RowIndex < 0)
                return;
            cliente.id_Cliente = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id_Cliente"].Value.ToString());
            cliente.Nome = dataGridView1.Rows[e.RowIndex].Cells["Nome"].Value.ToString();
            cliente.CNPJ = dataGridView1.Rows[e.RowIndex].Cells["CNPJ"].Value.ToString();
            cliente.Endereco = dataGridView1.Rows[e.RowIndex].Cells["Endereco"].Value.ToString();
            cliente.Bairro = dataGridView1.Rows[e.RowIndex].Cells["bairro"].Value.ToString();
            cliente.Cidade = dataGridView1.Rows[e.RowIndex].Cells["cidade"].Value.ToString();
            cliente.Estado = dataGridView1.Rows[e.RowIndex].Cells["Estado"].Value.ToString();
            cliente.CEP = dataGridView1.Rows[e.RowIndex].Cells["cep"].Value.ToString();
            cliente.Telefone = dataGridView1.Rows[e.RowIndex].Cells["telefone"].Value.ToString();
            cliente.Email = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
            cliente.Data_de_Cadastro = dataGridView1.Rows[e.RowIndex].Cells["Data_de_Cadastro"].Value.ToString();
            cliente.Status = dataGridView1.Rows[e.RowIndex].Cells["status"].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["delete"])
            {
                if (
                    MessageBox.Show(
                    $"ID: {cliente.id_Cliente}\n" +
                    $"Nome: {cliente.Nome}\n" +
                    $"CNPJ: {cliente.CNPJ}\n" +
                    $"Endereco: {cliente.Endereco}\n" +
                    $"Bairro: {cliente.Bairro}\n" +
                    $"Cidade: {cliente.Cidade}\n" +
                    $"Estado: {cliente.Estado}\n" +
                    $"CEP: {cliente.CEP}\n" +
                    $"Data de Cadastro: {cliente.Data_de_Cadastro}\n" +
                    $"Telefone: {cliente.Telefone}\n" +
                    $"Email: {cliente.Email}\n", "Deseja excluir cliente permanentemente ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes
                    )
                {
                    //se concordou em excluir
                    ClienteRepository c = new ClienteRepository();
                    c.DeleteCliente(cliente.id_Cliente);
                    Thread.Sleep(1200);
                    atualizarTabela();
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["edit"])
            {
                ClienteFormManipulacao formManipulacao = new ClienteFormManipulacao(this, cliente);
                formManipulacao.Show();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["delete"].ToolTipText = "Clique aqui para excluir";
            dataGridView1.Rows[e.RowIndex].Cells["edit"].ToolTipText = "Clique aqui para editar";
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn coluna in dataGridView1.Columns)
            {
                coluna.FillWeight = 3;  // Ajusta o peso de preenchimento de todas as colunas de forma uniforme
                //coluna.MinimumWidth = 70;
            }
            dataGridView1.Columns["id_Cliente"].HeaderText = "id";//mudando titulo da coluna
            /*formatando tamanho das colunas para responsividade*/
            dataGridView1.Columns["id_Cliente"].MinimumWidth = 20;
            dataGridView1.Columns["Nome"].MinimumWidth = 40;
            dataGridView1.Columns["Data_de_Cadastro"].MinimumWidth = 100;
            dataGridView1.Columns["CNPJ"].MinimumWidth = 92;
            dataGridView1.Columns["Endereco"].MinimumWidth = 56;
            dataGridView1.Columns["bairro"].MinimumWidth = 40;
            dataGridView1.Columns["cidade"].MinimumWidth = 46;
            dataGridView1.Columns["Estado"].MinimumWidth = 42;
            dataGridView1.Columns["cep"].MinimumWidth = 60;
            dataGridView1.Columns["telefone"].MinimumWidth = 86;
            dataGridView1.Columns["email"].MinimumWidth = 40;
            dataGridView1.Columns["status"].MinimumWidth = 40;
            dataGridView1.Columns["nome"].FillWeight = 12;
            dataGridView1.Columns["endereco"].FillWeight = 9;
            dataGridView1.Columns["email"].FillWeight = 9;
        }
    }
}
