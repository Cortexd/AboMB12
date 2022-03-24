using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;

namespace AboMB12
{
    internal static class CsvTools
    {
        /// <summary>
        /// Verification de colonne du fichier
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool VerifierFichier(string fileName)
        {
            try
            {
                // 1er ligne
                // sCliCode sCliRaisonSoc   sCliAdresse1Ligne sCliAdresse1CodePos sCliAdresse1Ville sContact.Tel sContact.Portable sContact.EMail heures
                // sCliRaisonSoc	sCliAdresse1Ligne	sCliAdresse1CodePos	sCliAdresse1Ville	sContact.Civilite	sContact.Interloc	sContact.EMail	Heure
                string[] values;
                using (var reader = new StreamReader(fileName))
                {
                    var line = reader.ReadLine().ToLower();
                    values = line.Split(';');
                }

                int index = 0;
                if (values[index].Trim() != "sCliRaisonSoc".ToLower())
                    return false;
                index++;
                if (values[index].Trim() != "sCliAdresse1Ligne".ToLower())
                    return false;
                index++;
                if (values[index].Trim() != "sCliAdresse1CodePos".ToLower())
                    return false;
                index++;
                if (values[index].Trim() != "sCliAdresse1Ville".ToLower())
                    return false;
                index++;
                if (values[index].Trim() != "sContact.Civilite".ToLower())
                    return false;
                index++;
                if (values[index].Trim() != "sContact.Interloc".ToLower())
                    return false;
                index++;
                if (values[index].Trim() != "sContact.EMail".ToLower())
                    return false;
                index++;
                if (!values[index].Contains("heur"))
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

        public static Dictionary<int, Attestation> GetObjectFromCSVFile(string fileName)
        {
            Dictionary<int, Attestation> retour = new Dictionary<int, Attestation>();

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                };

                using (var reader = new StreamReader(fileName, Encoding.Default))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<AttestationMap>();
                    var records = csv.GetRecords<Attestation>();

                    int cleDico = 0;
                    foreach (var record in records)
                    {
                        retour.Add(cleDico, record);
                        cleDico++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de lecture csv :" + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retour;
        }
    }
}