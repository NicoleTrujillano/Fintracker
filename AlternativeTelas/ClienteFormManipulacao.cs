using FinTracker.BD;
using FinTracker.Models;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinTracker.AlternativeTelas
{
    public partial class ClienteFormManipulacao : Form
    {
        Cliente clie_ou_fornecedor = new Cliente();
        Fornecedores forn = null;
        Clientes clie = null;
        public ClienteFormManipulacao(object tela, Object paraEdicao)
        {
            InitializeComponent();
            mtxbCnpj.Culture = System.Globalization.CultureInfo.InvariantCulture; //fazendo com que leia . como . nao como ,
            
            try
            {
                forn = (Fornecedores)tela;
            }
            catch(Exception e)
            {
                clie = (Clientes)tela;
            }
            
            if (forn != null)
            {
                
                if (paraEdicao != null)
                {
                    this.Text = "Atualizando fornecedor";
                    lblTitulo.Text = "Atualizando fornecedor";
                    Cliente fornecedor = (Cliente)paraEdicao;
                    preencherCampos(fornecedor);
                    clie_ou_fornecedor = (fornecedor);
                }
                else
                {
                    this.Text = "Adicionando fornecedor";
                    lblTitulo.Text = "Adicionando novo fornecedor";
                    btnConsultarValores.Visible = false;
                }
            }
            else if (paraEdicao != null)
            {

                this.Text = "Atualizando cliente";
                lblTitulo.Text = "Atualizando cliente";
                Cliente cliente = (Cliente)paraEdicao;
                preencherCampos(cliente);
                clie_ou_fornecedor = (cliente);
            }
            else
            {
                btnConsultarValores.Visible = false;
            }
        }
        private void preencherCampos(Cliente a)
        {
            mtxbCnpj.Text = a.CNPJ;
            txbNome.Text = a.Nome;
            txbEndereco.Text = a.Endereco;
            txbBairro.Text = a.Bairro;
            mtxbCep.Text = a.CEP;
            txbCidade.Text = a.Cidade;
            txbEstado.Text = a.Estado;
            mtxbTelefone.Text = a.Telefone;
            txbEmail.Text = a.Email;
            if(a.Status == "Desativo")
                rdbDesativo.Checked = true;

        }
        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!camposOk())
            {
                MessageBox.Show("Preencha corretamente todos os campos!");
                return;
            }

            if (lblTitulo.Text.ToLower().Contains("atualizando"))
            {
                if(clie != null)
                {
                    ClienteRepository c = new ClienteRepository();
                    pegarDados();
                    c.UpdateCliente(clie_ou_fornecedor);
                    MessageBox.Show("Cliente atualizado.");
                    clie.atualizarTabela();
                    Thread.Sleep(1200);
                    this.Close();
                }
                else
                {
                    FornecedorRepository f = new FornecedorRepository();
                    pegarDados();
                    f.UpdateFornecedor(clie_ou_fornecedor);
                    limparCampos();
                    MessageBox.Show("Fornecedor atualizado.");
                    Thread.Sleep(1200);
                    forn.atualizarTabela();
                    this.Close();
                }
            }
            else if(forn != null)
            {
                FornecedorRepository f = new FornecedorRepository();
                pegarDados();
                f.AddFornecedor(clie_ou_fornecedor);
                limparCampos();
                MessageBox.Show("Fornecedor adicionado.");
                Thread.Sleep(1200);
                forn.atualizarTabela();
            }
            else
            {
                ClienteRepository c = new ClienteRepository();
                pegarDados();
                c.AddCliente(clie_ou_fornecedor);
                limparCampos();
                MessageBox.Show("Cliente adicionado.");
                Thread.Sleep(1200);
                clie.atualizarTabela();
            }

        }

        private void pegarDados()
        {
            clie_ou_fornecedor.CNPJ = Regex.Replace(mtxbCnpj.Text, @"[./-]", "");
            clie_ou_fornecedor.Nome = txbNome.Text;
            clie_ou_fornecedor.Endereco = txbEndereco.Text;
            clie_ou_fornecedor.Bairro = txbBairro.Text;
            clie_ou_fornecedor.CEP = mtxbCep.Text;
            clie_ou_fornecedor.Cidade = txbCidade.Text;
            clie_ou_fornecedor.Estado = txbEstado.Text;
            clie_ou_fornecedor.Telefone = mtxbTelefone.Text;
            clie_ou_fornecedor.Email = txbEmail.Text;
            clie_ou_fornecedor.Status = rdbAtivo.Checked ? rdbAtivo.Text : rdbDesativo.Text;
            clie_ou_fornecedor.Data_de_Cadastro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void limparCampos()
        {
            mtxbCnpj.Text = "";
            txbNome.Text ="";
            txbEndereco.Text ="";
            txbBairro.Text ="";
            mtxbCep.Text ="";
            txbCidade.Text ="";
            txbEstado.Text ="";
            mtxbTelefone.Text ="";
            txbEmail.Text ="";
            mtxbCnpj.Focus();
        }
        private bool camposOk()
        {
            if (Regex.Replace(mtxbCnpj.Text, @"[./()_-]", "").Trim().Length < 14)
                return false;
            else if (txbNome.Text.Trim() == "")
                return false;
            else if (txbEndereco.Text.Trim() == "")
                return false;
            else if (txbBairro.Text.Trim() == "")
                return false;
            else if (mtxbCep.Text.Trim().Length < 9)
                return false;
            else if (txbCidade.Text.Trim() == "")
                return false;
            else if (txbEstado.Text.Trim().Length <2)
                return false;
            else if (mtxbTelefone.Text.Length < 15)
                return false;
            else if (txbEmail.Text.Trim() == "")
                return false;
            return true;
        }

        private void btnConsultarValores_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                    $"ID: {clie_ou_fornecedor.id_Cliente}\n" +
                    $"Nome: {clie_ou_fornecedor.Nome}\n" +
                    $"CNPJ: {clie_ou_fornecedor.CNPJ}\n" +
                    $"Endereco: {clie_ou_fornecedor.Endereco}\n" +
                    $"Bairro: {clie_ou_fornecedor.Bairro}\n" +
                    $"Cidade: {clie_ou_fornecedor.Cidade}\n" +
                    $"Estado: {clie_ou_fornecedor.Estado}\n" +
                    $"CEP: {clie_ou_fornecedor.CEP}\n" +
                    $"Data de Cadastro: {clie_ou_fornecedor.Data_de_Cadastro}\n" +
                    $"Telefone: {clie_ou_fornecedor.Telefone}\n" +
                    $"Email: {clie_ou_fornecedor.Email}\n" +
                    $"Status: {clie_ou_fornecedor.Status}\n");
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }
    }
}
