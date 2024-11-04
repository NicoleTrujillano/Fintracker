using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinTracker.AlternativeTelas;

namespace FinTracker.TelasPrincipais
{
    public partial class Perfil : Form
    {
        public Perfil()
        {
            InitializeComponent();
        }
        private void btnsHover(Object sender, EventArgs e)
        {

        }

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            pnlTelaClicada.Controls.Clear();
            EditarPerfil editarPerfilPage = new EditarPerfil();
            Form f = editarPerfilPage as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            pnlTelaClicada.Controls.Add(f);
            pnlTelaClicada.Tag = f;
            f.Show();
            pnlTela2.Visible = true;
        }

        private void btnMudarSenha_Click(object sender, EventArgs e)
        {
            pnlTelaClicada.Controls.Clear();
            
            MudarSenha mudarSenhaPage = new MudarSenha();
            Form f = mudarSenhaPage as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            pnlTelaClicada.Controls.Add(f);
            pnlTelaClicada.Tag = f;
            f.Show();
            pnlTela2.Visible = true;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            pnlTela2.Visible = false;
            pnlTelaClicada.Controls.Clear();
        }

        private void pnlTelaClicada_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
