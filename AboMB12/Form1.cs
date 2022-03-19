using AboMB12.Properties;
using CsvHelper;
using CsvHelper.Configuration;
using FastMember;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AboMB12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Data csv
        /// </summary>
        private DataTable CsvData;

        private List<Attestation> Attestations;

        /// <summary>
        /// Import du CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_csv_Click(object sender, EventArgs e)
        {
            if (openFileDialogCSV.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // TODO : correction du chargement quand le fichier est ouver dans Excel
                //string TempFile = Path.Combine(Path.GetTempFileName(), Path.GetRandomFileName());
                //File.Copy(openFileDialogCSV.FileName, TempFile);
                //ImporterFichier(TempFile);
                ImporterFichier(openFileDialogCSV.FileName);
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
                this.Attestations = CsvTools.GetObjectFromCSVFile(fileName).ToList();

                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(this.Attestations,
                    "RaisonSociale", "AdresseLigne1", "AdresseCP", "AdresseVille", "Civilite", "Interlocuteur", "Email", "Heure"))
                //using (var reader = ObjectReader.Create(this.Attestations))
                {
                    table.Load(reader);
                }

                // charge grid
                //this.dataGridView1.DataSource = this.CsvData;
                this.dataGridView1.DataSource = table;
                this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                this.dataGridView1.Refresh();
                AjouterBouttonGridview();

                // Changer le label du bouton
                bnt_generer.Visible = true;
            }
            else
            {
                // Format non conforme
                MessageBox.Show("Le fichier n'est pas conforme. La 1er ligne doit être : sCliCode, sCliRaisonSoc, sCliAdresse1Ligne, sCliAdresse1CodePos, sCliAdresse1Ville, sContact.Tel, sContact.Portable, sContact.EMail, heures",
                    "Critical Warning",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign,
                    true);

                // Changer le label du bouton
                bnt_generer.Visible = false;
            }
        }

        /// <summary>
        /// Ajout des boutons de traitement au GV
        /// </summary>
        private void AjouterBouttonGridview()
        {
            // Si elle n'existe pas
            if (dataGridView1.Columns[0].HeaderText != "Action")
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Insert(0, btn);
                btn.HeaderText = "Action";
                btn.Text = "Mail + PDF";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;

                btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Insert(1, btn);
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex  == this.dataGridView1.ColumnCount - 1)
            if (e.ColumnIndex >= 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Action")
                {
                    int ligne = e.RowIndex;
                    string chemin_pdf = Pdf.CreatePDF(ligne, CsvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                    OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, CsvData.Rows[ligne].ItemArray[6].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
                }
                if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Action pdf")
                {
                    int ligne = e.RowIndex;
                    string chemin_pdf = Pdf.CreatePDF(ligne, CsvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                    System.Diagnostics.Process.Start(chemin_pdf);
                }
            }
        }

        /// <summary>
        /// Chargement data table depuis cSV
        /// on vire le ligne sans heures
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static DataTable GetDataTabletFromCSVFile(string path)
        {
            DataTable csvData = new DataTable();

            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path, Encoding.Default))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn serialno = new DataColumn(column);
                        serialno.AllowDBNull = true;
                        csvData.Columns.Add(serialno);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();

                        DataRow dr = csvData.NewRow();
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == null)
                                fieldData[i] = string.Empty;

                            dr[i] = fieldData[i];
                        }

                        // On importe pas le ligne ou l'heure est vide.
                        if (!string.IsNullOrWhiteSpace(fieldData[7]))
                        {
                            csvData.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de lecture csv :" + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return csvData;
        }

        private void bnt_generer_tous_brouillons_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = CsvData.Rows.Count - 1;

            for (int i = 0; i < CsvData.Rows.Count; i++)
            {
                progressBar1.Value = i;
                int ligne = i;
                string chemin_pdf = Pdf.CreatePDF(ligne, CsvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, CsvData.Rows[ligne].ItemArray[6].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {CsvData.Rows.Count} mails.", "Terminé", MessageBoxButtons.OK);
            progressBar1.Value = 0;
        }

        private void button_envoyer_TOUT_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = CsvData.Rows.Count - 1;

            for (int i = 0; i < CsvData.Rows.Count; i++)
            {
                progressBar1.Value = i;
                int ligne = i;
                string chemin_pdf = Pdf.CreatePDF(ligne, CsvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJAndSend(chemin_pdf, CsvData.Rows[ligne].ItemArray[6].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {CsvData.Rows.Count} mails.", "Terminé", MessageBoxButtons.OK);
            progressBar1.Value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_message.Text = AboMB12.Properties.Settings.Default.Corps_PDF;
            textBox_message_mail.Text = AboMB12.Properties.Settings.Default.Corps_Mail;
            textBox_sujet_mail.Text = AboMB12.Properties.Settings.Default.Sujet_Mail;
            textBox_titre_attestation.Text = AboMB12.Properties.Settings.Default.Titre_PDF;

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
        private void buttonSauveMessage_Click(object sender, EventArgs e)
        {
            AboMB12.Properties.Settings.Default.Corps_PDF = textBox_message.Text;
            AboMB12.Properties.Settings.Default.Corps_Mail = textBox_message_mail.Text;
            AboMB12.Properties.Settings.Default.Sujet_Mail = textBox_sujet_mail.Text;
            AboMB12.Properties.Settings.Default.Titre_PDF = textBox_titre_attestation.Text;

            Settings.Default.Save();
        }

        private void Form1_Unload(object sender, FormClosingEventArgs e)
        {
            buttonSauveMessage_Click(sender, e);
        }

        private void openFileDialogCSV_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}