﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AboMB12.Properties;
using FastMember;
using Microsoft.VisualBasic.FileIO;

namespace AboMB12
{
    /// <summary>
    /// Form1
    /// </summary>
    public partial class Form1 : Form
    { /// <summary>
      /// Stockage des attestations (infos du CSV)
      /// </summary>
        private Dictionary<int, Attestation> attestations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            //this.webBrowser1.Navigate("");

            //Application.DoEvents();

            //this.webBrowser1.Document.OpenNew(false).Write(@"<html><body><div id=""editable"">Edit this text</div></body></html>");

            ////'turns off document body editing
            //foreach (HtmlElement item in this.webBrowser1.Document.All)
            //{
            //    item.SetAttribute("unselectable", "on");
            //    item.SetAttribute("contenteditable", "true");
            //    item.SetAttribute("width", this.Width + "px");
            //    item.SetAttribute("height", "100%");
            //    item.SetAttribute("contenteditable", "true");
            //}

            ////'turns on edit mode
            ////this.webBrowser1.ActiveXInstance = "On";
            ////'stops right click->Browse View
            //this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
        }

        /// <summary>
        /// Import du CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_import_csv_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogCSV.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // TODO : correction du chargement quand le fichier est ouver dans Excel
                //string TempFile = Path.Combine(Path.GetTempFileName(), Path.GetRandomFileName());
                //File.Copy(openFileDialogCSV.FileName, TempFile);
                //ImporterFichier(TempFile);
                this.ImporterFichier(this.openFileDialogCSV.FileName);
            }
        }

        /// <summary>
        /// Import du CSV
        /// </summary>
        /// <param name="fileName"></param>
        private void ImporterFichier(string fileName)
        {
            //if (VerifierFichier(fileName))
            if (true)
            {
                // recup depuis fichier
                this.attestations = CsvTools.GetObjectFromCSVFile(fileName);

                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(this.attestations.Values, "RaisonSociale", "AdresseLigne1", "AdresseCP", "AdresseVille", "Civilite", "Interlocuteur", "Email", "Heure"))
                {
                    table.Load(reader);
                }

                this.dataGridView1.DataSource = table;
                this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                this.dataGridView1.Refresh();

                // Ajout des boutons de manipulation
                this.AjouterBouttonGridview();

                // Changer le label du bouton
                this.bnt_generer.Visible = true;
            }
            else
            {
                // Format non conforme
                MessageBox.Show(
                    "Le fichier n'est pas conforme. La 1er ligne doit être : sCliCode, sCliRaisonSoc, sCliAdresse1Ligne, sCliAdresse1CodePos, sCliAdresse1Ville, sContact.Tel, sContact.Portable, sContact.EMail, heures",
                    "Critical Warning",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign,
                    true);

                // Changer le label du bouton
                this.bnt_generer.Visible = false;
            }
        }

        /// <summary>
        /// Ajout des boutons de traitement au GV
        /// </summary>
        private void AjouterBouttonGridview()
        {
            // Si elle n'existe pas
            if (this.dataGridView1.Columns[0].HeaderText != "Action")
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                this.dataGridView1.Columns.Insert(0, btn);
                btn.HeaderText = "Action";
                btn.Text = "Mail + PDF";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;

                btn = new DataGridViewButtonColumn();
                this.dataGridView1.Columns.Insert(1, btn);
                btn.HeaderText = "Action pdf";
                btn.Text = "PDF";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;
            }
        }

        /// <summary>
        /// Gestion du click sur les boutons du GV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex  == this.dataGridView1.ColumnCount - 1)
                if (e.ColumnIndex >= 0)
                {
                    int ligne = e.RowIndex;
                    KeyValuePair<int, Attestation> attestation = new KeyValuePair<int, Attestation>(ligne, this.attestations[ligne]);

                    if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Action")
                    {
                        string chemin_pdf = Pdf.CreatePDF(attestation, this.textBox_titre_attestation.Text, this.textBox_message.Text, Application.ExecutablePath);
                        OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, attestation.Value.Email, this.textBox_sujet_mail.Text, this.textBox_message_mail.Text);
                    }

                    if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Action pdf")
                    {
                        string chemin_pdf = Pdf.CreatePDF(attestation, this.textBox_titre_attestation.Text, this.textBox_message.Text, Application.ExecutablePath);
                        System.Diagnostics.Process.Start(chemin_pdf);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur : " + ex.StackTrace,
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Generation des attestations PDF + brouillon email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bnt_generer_tous_brouillons_Click(object sender, EventArgs e)
        {
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = this.attestations.Count - 1;

            int idxProgress = 0;
            foreach (var attestation in this.attestations)
            {
                this.progressBar1.Value = idxProgress;
                idxProgress++;

                string chemin_pdf = Pdf.CreatePDF(attestation, this.textBox_titre_attestation.Text, this.textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, attestation.Value.Email, this.textBox_sujet_mail.Text, this.textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {idxProgress} mails.", "Terminé", MessageBoxButtons.OK);
            this.progressBar1.Value = 0;
        }

        /// <summary>
        /// Generation des attestations PDF + email + envoi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_envoyer_TOUT_Click(object sender, EventArgs e)
        {
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = this.attestations.Count - 1;

            int idxProgress = 0;
            foreach (var attestation in this.attestations)
            {
                this.progressBar1.Value = idxProgress;
                idxProgress++;

                string chemin_pdf = Pdf.CreatePDF(attestation, this.textBox_titre_attestation.Text, this.textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJAndSend(chemin_pdf, attestation.Value.Email, this.textBox_sujet_mail.Text, this.textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {idxProgress} mails.", "Terminé", MessageBoxButtons.OK);
            this.progressBar1.Value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox_message.Text = AboMB12.Properties.Settings.Default.Corps_PDF;
            this.textBox_message_mail.Text = AboMB12.Properties.Settings.Default.Corps_Mail;
            this.textBox_sujet_mail.Text = AboMB12.Properties.Settings.Default.Sujet_Mail;
            this.textBox_titre_attestation.Text = AboMB12.Properties.Settings.Default.Titre_PDF;

            // Vidage repertoire temporaire
            try
            {
                System.IO.Directory.CreateDirectory(@".\Temp\");
                System.IO.DirectoryInfo di = new DirectoryInfo(@".\Temp\");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Sauvegarde modèle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSauveMessage_Click(object sender, EventArgs e)
        {
            AboMB12.Properties.Settings.Default.Corps_PDF = this.textBox_message.Text;
            AboMB12.Properties.Settings.Default.Corps_Mail = this.textBox_message_mail.Text;
            AboMB12.Properties.Settings.Default.Sujet_Mail = this.textBox_sujet_mail.Text;
            AboMB12.Properties.Settings.Default.Titre_PDF = this.textBox_titre_attestation.Text;

            Settings.Default.Save();
        }

        private void Form1_Unload(object sender, FormClosingEventArgs e)
        {
            this.ButtonSauveMessage_Click(sender, e);
        }

        private void OpenFileDialogCSV_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}