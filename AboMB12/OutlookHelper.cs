using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboMB12
{
    internal static class OutlookHelper
    {
        /// <summary>
        /// Creation des mail outlook
        /// </summary>
        /// <param name="chemin_pdf"></param>
        /// <param name="adresse_mail"></param>
        static public void CreateMailOutlookAvecPJ(string chemin_pdf, string adresse_mail, string textBox_sujet_mail, string textBox_message_mail)
        {
            StringBuilder MsgBuilder = new StringBuilder();

            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();

            Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)outlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            mail.Subject = textBox_sujet_mail;
            mail.To = adresse_mail;
            mail.HTMLBody = textBox_message_mail;

            mail.Attachments.Add(chemin_pdf);
            mail.Save();
        }

        internal static void CreateMailOutlookAvecPJAndSend(string chemin_pdf, string adresse_mail, string textBox_sujet_mail, string textBox_message_mail)
        {
            StringBuilder MsgBuilder = new StringBuilder();

            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();

            Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)outlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            mail.Subject = textBox_sujet_mail;
            mail.To = adresse_mail;
            mail.HTMLBody = textBox_message_mail;

            mail.Attachments.Add(chemin_pdf);

            try
            {
                // Envoie
                mail.Send();
            }
            catch (Exception)
            {
                try
                {
                    // Brouillon
                    mail.Save();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}