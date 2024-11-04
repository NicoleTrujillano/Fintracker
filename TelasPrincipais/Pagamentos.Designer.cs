namespace FinTracker.TelasPrincipais
{
    partial class Pagamentos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pagamentos));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlVerPerfil = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblData = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv_Vendas = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbbox_OPVendas = new System.Windows.Forms.ComboBox();
            this.cmbbox_AGVendas = new System.Windows.Forms.ComboBox();
            this.pnlPesquisarCliente = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txbPesquisa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_EditVenda = new System.Windows.Forms.Button();
            this.btn_DelVenda = new System.Windows.Forms.Button();
            this.btn_AdVenda = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.pnlVerPerfil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Vendas)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlPesquisarCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlVerPerfil);
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1300, 73);
            this.panel2.TabIndex = 24;
            // 
            // pnlVerPerfil
            // 
            this.pnlVerPerfil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlVerPerfil.BackColor = System.Drawing.Color.Transparent;
            this.pnlVerPerfil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlVerPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVerPerfil.Controls.Add(this.pictureBox3);
            this.pnlVerPerfil.Controls.Add(this.label5);
            this.pnlVerPerfil.Controls.Add(this.label4);
            this.pnlVerPerfil.Controls.Add(this.pictureBox2);
            this.pnlVerPerfil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlVerPerfil.Location = new System.Drawing.Point(1030, 12);
            this.pnlVerPerfil.Name = "pnlVerPerfil";
            this.pnlVerPerfil.Size = new System.Drawing.Size(230, 53);
            this.pnlVerPerfil.TabIndex = 2;
            this.pnlVerPerfil.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlVerPerfil_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::FinTracker.Properties.Resources.icon_setaDir;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(210, 18);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(10, 16);
            this.pictureBox3.TabIndex = 63;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlVerPerfil_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Ver perfil";
            this.label5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlVerPerfil_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Nome Sobrenome";
            this.label4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlVerPerfil_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::FinTracker.Properties.Resources.icon_clientes2;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(10, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 37);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlVerPerfil_Click);
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Location = new System.Drawing.Point(29, 26);
            this.lblData.Name = "lblData";
            this.lblData.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblData.Size = new System.Drawing.Size(308, 23);
            this.lblData.TabIndex = 1;
            this.lblData.Text = "Terça, 02 de Abril de 2024";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1300, 1);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(220)))));
            this.label3.Location = new System.Drawing.Point(40, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(960, 31);
            this.label3.TabIndex = 62;
            this.label3.Text = "Espaçamento para dar o scrol horizontal e vertical correto (40pxs de margin)";
            // 
            // label49
            // 
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(220)))));
            this.label49.Location = new System.Drawing.Point(0, 76);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(36, 537);
            this.label49.TabIndex = 61;
            this.label49.Text = "Espaçamento para dar o scrol horizontal e vertical correto (40pxs de margin)";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.dgv_Vendas);
            this.panel4.Location = new System.Drawing.Point(40, 339);
            this.panel4.MinimumSize = new System.Drawing.Size(700, 274);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1219, 361);
            this.panel4.TabIndex = 60;
            // 
            // dgv_Vendas
            // 
            this.dgv_Vendas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Vendas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Vendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Vendas.Location = new System.Drawing.Point(0, 0);
            this.dgv_Vendas.Name = "dgv_Vendas";
            this.dgv_Vendas.Size = new System.Drawing.Size(1219, 321);
            this.dgv_Vendas.TabIndex = 51;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cmbbox_OPVendas);
            this.panel1.Controls.Add(this.cmbbox_AGVendas);
            this.panel1.Controls.Add(this.pnlPesquisarCliente);
            this.panel1.Location = new System.Drawing.Point(40, 273);
            this.panel1.MinimumSize = new System.Drawing.Size(700, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1219, 50);
            this.panel1.TabIndex = 59;
            // 
            // cmbbox_OPVendas
            // 
            this.cmbbox_OPVendas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbbox_OPVendas.Font = new System.Drawing.Font("Arial", 12F);
            this.cmbbox_OPVendas.FormattingEnabled = true;
            this.cmbbox_OPVendas.Items.AddRange(new object[] {
            "Alfabeticamente - Comprador",
            "Alfabeticamente - Item",
            "Categoria",
            "Data",
            "Status da Venda",
            "Preço"});
            this.cmbbox_OPVendas.Location = new System.Drawing.Point(1003, 12);
            this.cmbbox_OPVendas.Name = "cmbbox_OPVendas";
            this.cmbbox_OPVendas.Size = new System.Drawing.Size(216, 26);
            this.cmbbox_OPVendas.TabIndex = 35;
            this.cmbbox_OPVendas.Text = "Ordenar por";
            // 
            // cmbbox_AGVendas
            // 
            this.cmbbox_AGVendas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbbox_AGVendas.Font = new System.Drawing.Font("Arial", 12F);
            this.cmbbox_AGVendas.FormattingEnabled = true;
            this.cmbbox_AGVendas.Items.AddRange(new object[] {
            "Nome do Comprador",
            "Nome do Item",
            "Categoria",
            "Data",
            "Status da Venda",
            "Método de Pagamento",
            "Parcelas",
            "Preço"});
            this.cmbbox_AGVendas.Location = new System.Drawing.Point(813, 12);
            this.cmbbox_AGVendas.Name = "cmbbox_AGVendas";
            this.cmbbox_AGVendas.Size = new System.Drawing.Size(169, 26);
            this.cmbbox_AGVendas.TabIndex = 34;
            this.cmbbox_AGVendas.Text = "Agrupar por";
            // 
            // pnlPesquisarCliente
            // 
            this.pnlPesquisarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPesquisarCliente.BackColor = System.Drawing.Color.Transparent;
            this.pnlPesquisarCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPesquisarCliente.BackgroundImage")));
            this.pnlPesquisarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPesquisarCliente.Controls.Add(this.pictureBox1);
            this.pnlPesquisarCliente.Controls.Add(this.txbPesquisa);
            this.pnlPesquisarCliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pnlPesquisarCliente.Location = new System.Drawing.Point(0, 0);
            this.pnlPesquisarCliente.MaximumSize = new System.Drawing.Size(426, 50);
            this.pnlPesquisarCliente.MinimumSize = new System.Drawing.Size(380, 50);
            this.pnlPesquisarCliente.Name = "pnlPesquisarCliente";
            this.pnlPesquisarCliente.Size = new System.Drawing.Size(382, 50);
            this.pnlPesquisarCliente.TabIndex = 26;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::FinTracker.Properties.Resources.icon_Search;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // txbPesquisa
            // 
            this.txbPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.txbPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbPesquisa.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPesquisa.Location = new System.Drawing.Point(59, 13);
            this.txbPesquisa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txbPesquisa.Name = "txbPesquisa";
            this.txbPesquisa.Size = new System.Drawing.Size(313, 24);
            this.txbPesquisa.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.label2.Location = new System.Drawing.Point(40, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 25);
            this.label2.TabIndex = 57;
            this.label2.Text = "Histórico de pagamentos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(39)))), ((int)(((byte)(29)))));
            this.label1.Location = new System.Drawing.Point(40, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 25);
            this.label1.TabIndex = 58;
            this.label1.Text = "Ações rápidas";
            // 
            // btn_EditVenda
            // 
            this.btn_EditVenda.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_EditVenda.BackgroundImage")));
            this.btn_EditVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EditVenda.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_EditVenda.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_EditVenda.Location = new System.Drawing.Point(410, 154);
            this.btn_EditVenda.Name = "btn_EditVenda";
            this.btn_EditVenda.Size = new System.Drawing.Size(166, 65);
            this.btn_EditVenda.TabIndex = 56;
            this.btn_EditVenda.Text = "Editar dados de venda";
            this.btn_EditVenda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_EditVenda.UseVisualStyleBackColor = true;
            // 
            // btn_DelVenda
            // 
            this.btn_DelVenda.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_DelVenda.BackgroundImage")));
            this.btn_DelVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DelVenda.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_DelVenda.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_DelVenda.Location = new System.Drawing.Point(225, 154);
            this.btn_DelVenda.Name = "btn_DelVenda";
            this.btn_DelVenda.Size = new System.Drawing.Size(166, 65);
            this.btn_DelVenda.TabIndex = 55;
            this.btn_DelVenda.Text = "Excluir venda(s)";
            this.btn_DelVenda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_DelVenda.UseVisualStyleBackColor = true;
            // 
            // btn_AdVenda
            // 
            this.btn_AdVenda.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AdVenda.BackgroundImage")));
            this.btn_AdVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AdVenda.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_AdVenda.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_AdVenda.Location = new System.Drawing.Point(40, 154);
            this.btn_AdVenda.Name = "btn_AdVenda";
            this.btn_AdVenda.Size = new System.Drawing.Size(166, 65);
            this.btn_AdVenda.TabIndex = 54;
            this.btn_AdVenda.Text = "Adicionar nova venda";
            this.btn_AdVenda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_AdVenda.UseVisualStyleBackColor = true;
            // 
            // Pagamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_EditVenda);
            this.Controls.Add(this.btn_DelVenda);
            this.Controls.Add(this.btn_AdVenda);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Pagamentos";
            this.Text = "Pagamentos";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlVerPerfil_Click);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlVerPerfil.ResumeLayout(false);
            this.pnlVerPerfil.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Vendas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlPesquisarCliente.ResumeLayout(false);
            this.pnlPesquisarCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgv_Vendas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbbox_OPVendas;
        private System.Windows.Forms.ComboBox cmbbox_AGVendas;
        private System.Windows.Forms.Panel pnlPesquisarCliente;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txbPesquisa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_EditVenda;
        private System.Windows.Forms.Button btn_DelVenda;
        private System.Windows.Forms.Button btn_AdVenda;
        private System.Windows.Forms.Panel pnlVerPerfil;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}