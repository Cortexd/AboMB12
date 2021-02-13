using AboMB12.Properties;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AboMB12
{
    public partial class Form1 : Form
    {
        private const string lblHeaderGVBrouillonOutlook = "Brouillon outlook";
        private const string lblButtonGVBrouillonOutlook = "Brouillon";
        private const string lblHeaderGVTestPDF = "Visualiser l'attestation";
        private const string lblButtonGVTestPDF = "Visu";

        /// <summary>
        /// Data csv
        /// </summary>
        private DataTable csvData;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chargement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_message.Text = AboMB12.Properties.Settings.Default.Corps_PDF;
            textBox_message_mail.Text = AboMB12.Properties.Settings.Default.Corps_Mail;
            textBox_sujet_mail.Text = AboMB12.Properties.Settings.Default.Sujet_Mail;
            textBox_titre_attestation.Text = AboMB12.Properties.Settings.Default.Titre_PDF;

            CleanTempDirectory();
        }

        /// <summary>
        /// Déchargement de la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Unload(object sender, FormClosingEventArgs e)
        {
            but_sauve_Click(sender, e);
        }

        /// <summary>
        /// Import du CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_csv_Click(object sender, EventArgs e)
        {
            if (openFileDialogCSV.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImporterFichier(openFileDialogCSV.FileName);
            }
        }

        /// <summary>
        /// Generer tout les brouillons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnt_generer_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = csvData.Rows.Count - 1;

            for (int i = 0; i < csvData.Rows.Count; i++)
            {
                progressBar1.Value = i;
                int ligne = i;
                string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                int colonneEmail = csvData.Columns["sContact.EMail"].Ordinal;
                OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, csvData.Rows[ligne].ItemArray[colonneEmail].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {csvData.Rows.Count} mails.", "Terminé", MessageBoxButtons.OK);
            progressBar1.Value = 0;
        }

        /// <summary>
        /// Tout envoyer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_TOUT_envoyer_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = csvData.Rows.Count - 1;

            for (int i = 0; i < csvData.Rows.Count; i++)
            {
                progressBar1.Value = i;
                int ligne = i;
                int colonneEmail = csvData.Columns["sContact.EMail"].Ordinal;
                string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJAndSend(chemin_pdf, csvData.Rows[ligne].ItemArray[colonneEmail].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {csvData.Rows.Count} mails.", "Terminé", MessageBoxButtons.OK);
            progressBar1.Value = 0;
        }

        /// <summary>
        /// Sauvegarde modèle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_sauve_Click(object sender, EventArgs e)
        {
            AboMB12.Properties.Settings.Default.Corps_PDF = textBox_message.Text;
            AboMB12.Properties.Settings.Default.Corps_Mail = textBox_message_mail.Text;
            AboMB12.Properties.Settings.Default.Sujet_Mail = textBox_sujet_mail.Text;
            AboMB12.Properties.Settings.Default.Titre_PDF = textBox_titre_attestation.Text;

            Settings.Default.Save();
        }

        /// <summary>
        /// Gestion du click sur les boutons du GV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Pou eviter le clic sur les entetes
            if (e.RowIndex >= 0)
            {
                // PDF + Mail
                if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == lblHeaderGVBrouillonOutlook)
                {
                    int ligne = e.RowIndex;
                    string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                    int colonneEmail = csvData.Columns["sContact.EMail"].Ordinal;
                    OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, csvData.Rows[ligne].ItemArray[colonneEmail].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
                }

                // PDF uniquement + ouverture automatique
                if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == lblHeaderGVTestPDF)
                {
                    int ligne = e.RowIndex;
                    string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                    System.Diagnostics.Process.Start(chemin_pdf);
                }
            }
        }

        #region Tools

        /// <summary>
        /// Nettoyage rep temporaire
        /// </summary>
        private static void CleanTempDirectory()
        {
            // Vidage repertoire temporaire
            try
            {
                Directory.CreateDirectory(@".\Temp\");
                DirectoryInfo di = new DirectoryInfo(@".\Temp\");

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
        /// Ajout des boutons de traitement au GV
        /// </summary>
        private void AjouterBouttonGridview()
        {
            // Si elle n'existe pas
            if (dataGridView1.Columns[0].HeaderText != "Action")
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Insert(0, btn);
                btn.HeaderText = lblHeaderGVBrouillonOutlook;
                btn.Text = lblButtonGVBrouillonOutlook;
                btn.Name = "btnGVBrouillonOutlook";
                btn.UseColumnTextForButtonValue = true;

                btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Insert(1, btn);
                btn.HeaderText = lblHeaderGVTestPDF;
                btn.Text = lblButtonGVTestPDF;
                btn.Name = "btnGVTestPDF";
                btn.UseColumnTextForButtonValue = true;
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

                    int index = 0;
                    foreach (string column in colFields)
                    {
                        if (!csvData.Columns.Contains(column))
                        {
                            DataColumn serialno = new DataColumn(column);
                            serialno.AllowDBNull = true;
                            csvData.Columns.Add(serialno);
                        }
                        else
                        {
                            DataColumn serialno = new DataColumn(column + index);
                            serialno.AllowDBNull = true;
                            csvData.Columns.Add(serialno);
                        }
                        index++;
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

                        csvData.Rows.Add(dr);
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

        /// <summary>
        /// Import du CSV
        /// </summary>
        /// <param name="fileName"></param>
        private void ImporterFichier(string fileName)
        {
            if (VerifierFichier(fileName))
            {
                // recup depuis fichier
                csvData = GetDataTabletFromCSVFile(fileName);

                // charge grid
                this.dataGridView1.DataSource = this.csvData;
                this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                this.dataGridView1.Refresh();
                AjouterBouttonGridview();

                // Changer le label du bouton
                bnt_generer.Visible = true;
                button_TOUT_envoyer.Visible = true;
            }
            else
            {
                // Format non conforme
                MessageBox.Show("Le fichier n'est pas conforme.",
                    "Critical Warning",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign,
                    true);

                // Changer le label du bouton
                bnt_generer.Visible = false;
                button_TOUT_envoyer.Visible = false;
            }
        }

        /// <summary>
        /// Verification de colonne du fichier
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool VerifierFichier(string fileName)
        {
            try
            {
                // 1er ligne
                // sCliCode sCliRaisonSoc   sCliAdresse1Ligne sCliAdresse1CodePos sCliAdresse1Ville sContact.Tel sContact.Portable sContact.EMail heures
                string line;

                using (var reader = new StreamReader(fileName))
                {
                    line = reader.ReadLine().ToLower();
                }

                if (!line.Contains("sCliCode".ToLower()))
                    return false;

                if (!line.Contains("sCliRaisonSoc".ToLower()))
                    return false;

                if (!line.Contains("sCliAdresse1Ligne".ToLower()))
                    return false;

                if (!line.Contains("sCliAdresse1CodePos".ToLower()))
                    return false;

                if (!line.Contains("sCliAdresse1Ville".ToLower()))
                    return false;

                if (!line.Contains("sContact.EMail".ToLower()))
                    return false;

                if (!line.Contains("sContact.Civilite".ToLower()))
                    return false;

                if (!line.Contains("sContact.Interloc".ToLower()))
                    return false;

                // contient heure
                if (!line.Contains("heur".ToLower()))
                    return false;

                return true;
            }
            catch (Exception)
            {
                // Format non conforme
                MessageBox.Show("Erreur de lecture du fichier CSV. N'est il pas ouvert dans Excel ?",
                    "Critical Warning",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign,
                    true);
                return false;
            }
        }

        #endregion Tools
    }
}