using FinTracker.Telas;
using FinTracker.TelasPrincipais;
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


namespace FinTracker
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            mudarTelaDoPanelPrincipal(new Home());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void trocarTela(object btnTelaAlvo)
        {
            Button button = (Button)btnTelaAlvo;
            if (button.ForeColor == Color.FromArgb(21, 39, 29))
                return;//se ja botao escolhido ja esta escolhido, nn faz nada

            Button[] buttons = { btnHome, btnClientes, btnContas, btnFornecedores, btnImportar, btnPagamentos, btnVendas };
            foreach (Button b in buttons)
            {
                b.BackColor = Color.FromArgb(21, 39, 29);
                b.ForeColor = Color.White;
            }//Resetando botoes de navegacao (como se nenhum estivesse marcado)
            
            //personalizando estilo do botao para estilo de selecionado
            button.BackColor = Color.FromArgb(233, 233, 220);
            button.ForeColor = Color.FromArgb(21, 39, 29);

            if (button.Name.Contains("Home"))
                mudarTelaDoPanelPrincipal(new Home());
            else if (button.Name.Contains("Vendas"))
                mudarTelaDoPanelPrincipal(new Vendas());            
            else if (button.Name.Contains("Pagamen"))
                mudarTelaDoPanelPrincipal(new Pagamentos());
            else if (button.Name.Contains("Contas"))
                mudarTelaDoPanelPrincipal(new Contas());
            else if (button.Name.Contains("Fornece"))
                mudarTelaDoPanelPrincipal(new Fornecedores());
            else if (button.Name.Contains("Client"))
                mudarTelaDoPanelPrincipal(new Clientes());
            else if (button.Name.Contains("Importar"))
                mudarTelaDoPanelPrincipal(new Importar());

        }

        private void mudarTelaDoPanelPrincipal(object Tela)
        {
            if (this.panelPrincipal.Controls.Count > 0)
                this.panelPrincipal.Controls.RemoveAt(0);
            Form f = Tela as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelPrincipal.Controls.Add(f);
            this.panelPrincipal.Tag = f;
            f.Show();
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnPagamentos_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnContas_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }
    }
}
