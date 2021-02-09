using AboMB12.Properties;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
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
        private DataTable csvData;

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
                string[] values;
                using (var reader = new StreamReader(fileName))
                {
                    var line = reader.ReadLine().ToLower();
                    values = line.Split(';');
                }

                if (values[0].Trim() != "sCliCode".ToLower())
                    return false;

                if (values[1].Trim() != "sCliRaisonSoc".ToLower())
                    return false;

                if (values[2].Trim() != "sCliAdresse1Ligne".ToLower())
                    return false;

                if (values[3].Trim() != "sCliAdresse1CodePos".ToLower())
                    return false;

                if (values[4].Trim() != "sCliAdresse1Ville".ToLower())
                    return false;

                if (values[5].Trim() != "sContact.Tel".ToLower())
                    return false;

                if (values[6].Trim() != "sContact.Portable".ToLower())
                    return false;

                if (values[7].Trim() != "sContact.EMail".ToLower())
                    return false;

                if (values[8].Trim() != "sContact.Civilite".ToLower())
                    return false;

                if (values[9].Trim() != "sContact.Interloc".ToLower())
                    return false;

                // contient heure
                if (!values[10].Contains("heur"))
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
            if (e.ColumnIndex > 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Action")
                {
                    int ligne = e.RowIndex;
                    string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                    OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, csvData.Rows[ligne].ItemArray[7].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
                }
                if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Action pdf")
                {
                    int ligne = e.RowIndex;
                    string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
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
                        if (!string.IsNullOrWhiteSpace(fieldData[10]))
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
            progressBar1.Maximum = csvData.Rows.Count - 1;

            for (int i = 0; i < csvData.Rows.Count; i++)
            {
                progressBar1.Value = i;
                int ligne = i;
                string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJ(chemin_pdf, csvData.Rows[ligne].ItemArray[7].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {csvData.Rows.Count} mails.", "Terminé", MessageBoxButtons.OK);
            progressBar1.Value = 0;
        }

        private void button_envoyer_TOUT_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = csvData.Rows.Count - 1;

            for (int i = 0; i < csvData.Rows.Count; i++)
            {
                progressBar1.Value = i;
                int ligne = i;
                string chemin_pdf = Pdf.CreatePDF(ligne, csvData, textBox_titre_attestation.Text, textBox_message.Text, Application.ExecutablePath);
                OutlookHelper.CreateMailOutlookAvecPJAndSend(chemin_pdf, csvData.Rows[ligne].ItemArray[7].ToString(), textBox_sujet_mail.Text, textBox_message_mail.Text);
            }

            MessageBox.Show($"Fin de génération des {csvData.Rows.Count} mails.", "Terminé", MessageBoxButtons.OK);
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
    }
}