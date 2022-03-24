using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace AboMB12
{
    /// <summary>
    /// Classe de gestion des pdf
    /// </summary>
    public static class Pdf
    {
        /// <summary>
        /// CreatePDF
        /// </summary>
        /// <param name="attestation"></param>
        /// <param name="textBox_titre_attestation"></param>
        /// <param name="textBox_message"></param>
        /// <param name="executablePath"></param>
        /// <returns>Chemin du fichier généré</returns>
        public static string CreatePDF(KeyValuePair<int, Attestation> attestation, string textBox_titre_attestation, string textBox_message, string executablePath)
        {
            // Create a new PDF document
            PdfSharp.Pdf.PdfDocument document = new PdfDocument();
            document.Info.Title = $"abonnement {attestation.Value.RaisonSociale}";

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
            gfx.DrawString(attestation.Value.RaisonSociale, font_normal, XBrushes.Black, new XRect(350, 120, 0, 0), XStringFormats.Default);
            gfx.DrawString(attestation.Value.AdresseLigne1, font_normal, XBrushes.Black, new XRect(350, 140, 0, 0), XStringFormats.Default);
            gfx.DrawString(attestation.Value.AdresseCP + " " + attestation.Value.AdresseVille, font_normal, XBrushes.Black, new XRect(350, 160, 0, 0), XStringFormats.Default);

            // Titre
            gfx.DrawString(textBox_titre_attestation, font_titre, XBrushes.Black, new XRect(240, 300, 0, 0), XStringFormats.Default);

            string message = textBox_message;
            message = message.Replace("{RAISON_SOCIALE}", attestation.Value.RaisonSociale);
            message = message.Replace("{ADRESSE_LIGNE1}", attestation.Value.AdresseLigne1);
            message = message.Replace("{CP}", attestation.Value.AdresseCP);
            message = message.Replace("{VILLE}", attestation.Value.AdresseVille);
            message = message.Replace("{HEURE}", attestation.Value.Heure);
            message = message.Replace("{CIVILITE}", attestation.Value.Civilite);
            message = message.Replace("{INTERLOCUTEUR}", attestation.Value.Interlocuteur);

            XRect rect = new XRect(50, 350, 500, 400);

            // gfx.DrawRectangle(XBrushes.SeaShell, rect);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(message, font_normal, XBrushes.Black, rect, XStringFormats.TopLeft);

            // gfx.DrawString(message, font_normal, XBrushes.Black, rect, XStringFormats.TopLeft);
            string filename = $"{textBox_titre_attestation}-{attestation.Key}-{attestation.Value.RaisonSociale}.pdf";

            filename = CleanBadChar(filename);

            System.IO.Directory.CreateDirectory(@".\Temp\");

            document.Save(@".\Temp\" + filename);

            return Path.GetDirectoryName(executablePath) + @"\Temp\" + filename;
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
        private static void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

        /// <summary>
        /// Nettoyage des caractéres spéciaux
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>chaine nettoyée</returns>
        private static string CleanBadChar(string filename)
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