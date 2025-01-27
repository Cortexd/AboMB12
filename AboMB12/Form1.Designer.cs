namespace AboMB12
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_import_csv = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bnt_generate_all_draft = new System.Windows.Forms.Button();
            this.openFileDialogCSV = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogRTF = new System.Windows.Forms.OpenFileDialog();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.textBox_titre_attestation = new System.Windows.Forms.TextBox();
            this.textBox_sujet_mail = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_message_mail = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.but_sauve = new System.Windows.Forms.Button();
            this.btn_generate_and_send_all = new System.Windows.Forms.Button();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_import_csv
            // 
            this.btn_import_csv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_import_csv.Location = new System.Drawing.Point(12, 7);
            this.btn_import_csv.Name = "btn_import_csv";
            this.btn_import_csv.Size = new System.Drawing.Size(139, 28);
            this.btn_import_csv.TabIndex = 0;
            this.btn_import_csv.Text = "Importer fichier CSV";
            this.btn_import_csv.UseVisualStyleBackColor = true;
            this.btn_import_csv.Click += new System.EventHandler(this.Btn_import_csv_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Location = new System.Drawing.Point(12, 41);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(1470, 234);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // bnt_generate_all_draft
            // 
            this.bnt_generate_all_draft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_generate_all_draft.Location = new System.Drawing.Point(951, 7);
            this.bnt_generate_all_draft.Name = "bnt_generate_all_draft";
            this.bnt_generate_all_draft.Size = new System.Drawing.Size(262, 28);
            this.bnt_generate_all_draft.TabIndex = 3;
            this.bnt_generate_all_draft.Text = "Générer tout les brouillons";
            this.bnt_generate_all_draft.UseVisualStyleBackColor = true;
            this.bnt_generate_all_draft.Visible = false;
            this.bnt_generate_all_draft.Click += new System.EventHandler(this.Bnt_generer_tous_brouillons_Click);
            // 
            // openFileDialogCSV
            // 
            this.openFileDialogCSV.Filter = "Fichiers CSV|*.csv|Tous les fichiers|*.*";
            // 
            // openFileDialogRTF
            // 
            this.openFileDialogRTF.Filter = "Fichiers RTF|*.rtf";
            // 
            // textBox_message
            // 
            this.textBox_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_message.Location = new System.Drawing.Point(47, 52);
            this.textBox_message.Multiline = true;
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_message.Size = new System.Drawing.Size(603, 327);
            this.textBox_message.TabIndex = 8;
            // 
            // textBox_titre_attestation
            // 
            this.textBox_titre_attestation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_titre_attestation.Location = new System.Drawing.Point(47, 22);
            this.textBox_titre_attestation.Name = "textBox_titre_attestation";
            this.textBox_titre_attestation.Size = new System.Drawing.Size(603, 22);
            this.textBox_titre_attestation.TabIndex = 9;
            // 
            // textBox_sujet_mail
            // 
            this.textBox_sujet_mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_sujet_mail.Location = new System.Drawing.Point(62, 19);
            this.textBox_sujet_mail.Name = "textBox_sujet_mail";
            this.textBox_sujet_mail.Size = new System.Drawing.Size(231, 22);
            this.textBox_sujet_mail.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_message_mail);
            this.groupBox1.Controls.Add(this.textBox_sujet_mail);
            this.groupBox1.Location = new System.Drawing.Point(674, 290);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 380);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Sujet";
            // 
            // textBox_message_mail
            // 
            this.textBox_message_mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_message_mail.Location = new System.Drawing.Point(9, 55);
            this.textBox_message_mail.Multiline = true;
            this.textBox_message_mail.Name = "textBox_message_mail";
            this.textBox_message_mail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_message_mail.Size = new System.Drawing.Size(373, 315);
            this.textBox_message_mail.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_titre_attestation);
            this.groupBox2.Controls.Add(this.textBox_message);
            this.groupBox2.Location = new System.Drawing.Point(12, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(656, 389);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PDF";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Texte";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Titre";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(157, 7);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(788, 28);
            this.progressBar1.TabIndex = 14;
            // 
            // but_sauve
            // 
            this.but_sauve.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.but_sauve.Location = new System.Drawing.Point(674, 676);
            this.but_sauve.Name = "but_sauve";
            this.but_sauve.Size = new System.Drawing.Size(388, 40);
            this.but_sauve.TabIndex = 15;
            this.but_sauve.Text = "Sauvegarde des modèles mail et attestation";
            this.but_sauve.UseVisualStyleBackColor = true;
            this.but_sauve.Click += new System.EventHandler(this.ButtonSauveMessage_Click);
            // 
            // btn_generate_and_send_all
            // 
            this.btn_generate_and_send_all.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_generate_and_send_all.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_generate_and_send_all.Location = new System.Drawing.Point(1220, 7);
            this.btn_generate_and_send_all.Name = "btn_generate_and_send_all";
            this.btn_generate_and_send_all.Size = new System.Drawing.Size(262, 28);
            this.btn_generate_and_send_all.TabIndex = 16;
            this.btn_generate_and_send_all.Text = "Générer et tout envoyer";
            this.btn_generate_and_send_all.UseVisualStyleBackColor = true;
            this.btn_generate_and_send_all.Visible = false;
            this.btn_generate_and_send_all.Click += new System.EventHandler(this.Button_envoyer_TOUT_Click);
            // 
            // webBrowser2
            // 
            this.webBrowser2.Location = new System.Drawing.Point(1080, 303);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(402, 413);
            this.webBrowser2.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 676);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1503, 759);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.webBrowser2);
            this.Controls.Add(this.btn_generate_and_send_all);
            this.Controls.Add(this.but_sauve);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bnt_generate_all_draft);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_import_csv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "AboMB12";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Unload);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_import_csv;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bnt_generate_all_draft;
        private System.Windows.Forms.OpenFileDialog openFileDialogCSV;
        private System.Windows.Forms.OpenFileDialog openFileDialogRTF;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.TextBox textBox_titre_attestation;
        private System.Windows.Forms.TextBox textBox_sujet_mail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_message_mail;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button but_sauve;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_generate_and_send_all;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

