using FinTracker.AlternativeTelas;
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

namespace FinTracker.Controles
{
    public partial class VendaRow : UserControl
    {
        public Venda dadosVenda = new Venda();
        Vendas telaVendas = null;
        public VendaRow(Venda venda, Vendas telaVenda)
        {
            InitializeComponent();
            NomeCliente = venda.nomeCliente;
            MetodoPagamento = venda.Metodo;
            QuantidadeProdutos = venda.QuantidadeProdutos;
            DataTransacao = venda.DataTransacao.ToString();
            ValorTotal = venda.TotalPreco.ToString();
            dadosVenda = venda;
            this.telaVendas = telaVenda;
        }

        public string NomeCliente
        {
            get { return lblNome.Text; }
            set { lblNome.Text = value; }
        }
        public string MetodoPagamento
        {
            get { return lblPagamento.Text; }
            set { lblPagamento.Text = value; }
        }

        public int QuantidadeProdutos
        {
            get { return int.Parse(lblQtdProd.Text); }
            set { lblQtdProd.Text = value.ToString(); }
        }

        public String DataTransacao
        {
            get { return lblData.Text; }
            set { lblData.Text = value; }
        }

        public String ValorTotal
        {
            get { return lblValor.Text; }
            set { lblValor.Text = value; }
        }
        public bool Checked()
        {
            return checkBox1.Checked;
        }

        private void panelDoCheck_MouseEnter(object sender, EventArgs e)
        {
            checkBox1.BackColor = Color.LightGray;  // Efeito hover
        }

        private void panelDoCheck_MouseLeave(object sender, EventArgs e)
        {
            checkBox1.BackColor = SystemColors.Control;//deixa padrao
        }

        private void panelDoCheck_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        private void tableRow_MouseEnter(object sender, EventArgs e)
        {
            panelBorda.BorderStyle = BorderStyle.FixedSingle;
        }
        private async void tableRow_MouseLeave(object sender, EventArgs e)
        {
            panelBorda.BorderStyle = BorderStyle.None;
        }

        public void abrirEdicao(object sender, EventArgs e)
        {
            
            VendaFormManipulacao formEditarVenda = new VendaFormManipulacao(dadosVenda, telaVendas);
            formEditarVenda.Show();
        }

    }
}
