using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboMB12
{
    public static class Pdf
    {
        /// <summary>
        /// Creation du PDF
        /// </summary>
        /// <param name="ligne"></param>
        /// <returns></returns>
        public static string CreatePDF(int ligne, DataTable csvData, string textBox_titre_attestation, string textBox_message, string ExecutablePath)
        {
            string RAISON_SOCIALE = csvData.Rows[ligne].ItemArray[1].ToString();
            string ADRESSE_LIGNE1 = csvData.Rows[ligne].ItemArray[2].ToString();
            string CP = csvData.Rows[ligne].ItemArray[3].ToString();
            string VILLE = csvData.Rows[ligne].ItemArray[4].ToString();
            string CIVILITE = csvData.Rows[ligne].ItemArray[8].ToString();
            string INTERLOCUTEUR = csvData.Rows[ligne].ItemArray[9].ToString();
            string HEURE = csvData.Rows[ligne].ItemArray[10].ToString();

            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = $"abonnement {RAISON_SOCIALE}";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            DrawImage(gfx, "logo.jpg", 50, 50, 197, 170);

            // Create a font
            XFont font_normal = new XFont("Trebuchet MS", 12, XFontStyle.Regular);
            XFont font_titre = new XFont("Trebuchet MS", 14, XFontStyle.Bold);

            // Bloc adresse
            //gfx.DrawString(CIVILITE + " " + INTERLOCUTEUR, font_normal, XBrushes.Black, new XRect(350, 100, 0, 0), XStringFormats.Default);
            gfx.DrawString(RAISON_SOCIALE, font_normal, XBrushes.Black, new XRect(350, 120, 0, 0), XStringFormats.Default);
            gfx.DrawString(ADRESSE_LIGNE1, font_normal, XBrushes.Black, new XRect(350, 140, 0, 0), XStringFormats.Default);
            gfx.DrawString(CP + " " + VILLE, font_normal, XBrushes.Black, new XRect(350, 160, 0, 0), XStringFormats.Default);

            // Titre
            gfx.DrawString(textBox_titre_attestation, font_titre, XBrushes.Black, new XRect(240, 300, 0, 0), XStringFormats.Default);

            string message = textBox_message;
            message = message.Replace("{RAISON_SOCIALE}", RAISON_SOCIALE);
            message = message.Replace("{ADRESSE_LIGNE1}", ADRESSE_LIGNE1);
            message = message.Replace("{CP}", CP);
            message = message.Replace("{VILLE}", VILLE);
            message = message.Replace("{HEURE}", HEURE);
            message = message.Replace("{CIVILITE}", CIVILITE);
            message = message.Replace("{INTERLOCUTEUR}", INTERLOCUTEUR);

            XRect rect = new XRect(50, 350, 500, 400);
            //gfx.DrawRectangle(XBrushes.SeaShell, rect);

            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(message, font_normal, XBrushes.Black, rect, XStringFormats.TopLeft);
            //gfx.DrawString(message, font_normal, XBrushes.Black, rect, XStringFormats.TopLeft);

            // Save the document...

            string filename = $"{textBox_titre_attestation}-{ligne}-{RAISON_SOCIALE}.pdf";

            filename = CleanBadChar(filename);

            System.IO.Directory.CreateDirectory(@".\Temp\");

            document.Save(@".\Temp\" + filename);

            //Process.Start(filename);
            //return @".\Temp\" + filename;

            return Path.GetDirectoryName(ExecutablePath) + @"\Temp\" + filename;
        }

        /// <summary>
        /// Ajout image dans PDF
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="jpegSamplePath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        static private void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

        /// <summary>
        /// Nettoyage des caractéres spéciaux
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static private string CleanBadChar(string filename)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                filename = filename.Replace(c.ToString(), " ");
            }

            return filename;
        }
    }
}