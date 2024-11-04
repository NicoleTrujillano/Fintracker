using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using FinTracker.Properties;
using FinTracker.BD;

namespace FinTracker.TelasPrincipais
{
    public partial class Pagamentos : Form
    {
        PagamentoRepository pagamentoRepository = new PagamentoRepository();
        public Pagamentos()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            atualizarTabela();
        }
        private async void atualizarTabela()
        {
            dgv_Vendas.DataSource = await pagamentoRepository.GetPagamentos();
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
