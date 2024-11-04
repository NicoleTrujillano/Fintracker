using FinTracker.TelasPrincipais;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Telas
{
    public partial class Importar : Form
    {
        public Importar()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
        }
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string b = "";
                foreach (string a in files)
                    b = b + a + "\n";
                MessageBox.Show(b);
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void btnFileToExport_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); dataGridView1.Columns.Clear();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                var rows = MiniExcel.Query<UserAccount>(op.FileName);

                var columns = MiniExcel.GetColumns(op.FileName, true);//pegando as colunas
                foreach (string campo in columns)
                    dataGridView1.Columns.Add(campo, campo);

                foreach (UserAccount user in rows)
                {
                    dataGridView1.Rows.Add(user.RA, user.Turma, user.Pronome, user.Matricula, user.Aluno, user.Curso);
                }
            }
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
    public class UserAccount
    {
        public Guid ID { get; set; }
        public string RA { get; set; }
        public string Turma { get; set; }
        public string Pronome { get; set; }
        public string Matricula { get; set; }
        public string Aluno { get; set; }
        public string Curso { get; set; }
    }
}
