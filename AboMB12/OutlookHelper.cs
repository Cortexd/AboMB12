using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboMB12
{
    /// <summary>
    /// OutlookHelper
    /// </summary>
    internal static class OutlookHelper
    {
        /// <summary>
        /// Creation des mail outlook
        /// </summary>
        /// <param name="chemin_pdf"></param>
        /// <param name="adresse_mail"></param>
        /// <param name="textBox_sujet_mail"></param>
        /// <param name="textBox_message_mail"></param>
        public static void CreateMailOutlookAvecPJ(string chemin_pdf, string adresse_mail, string textBox_sujet_mail, string textBox_message_mail)
        {
            StringBuilder msgBuilder = new StringBuilder();

            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();

            Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)outlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            mail.Subject = textBox_sujet_mail;
            mail.To = adresse_mail;
            mail.HTMLBody = textBox_message_mail;

            mail.Attachments.Add(chemin_pdf);
            mail.Save();

            // mail.Display(true);

            //        Outlook.NameSpace omNamespace = outlookApp.GetNamespace("MAPI");
            //        Outlook.Recipient omUser = omNamespace.CreateRecipient("test");
            //        omUser.Resolve();
            //        Outlook.MAPIFolder doissierBrouillon = omNamespace.GetSharedDefaultFolder(omUser, Outlook.OlDefaultFolders.olFolderDrafts)
            // Dim omMailItem As Outlook.MailItem = CType(omDrafts.Items.Add, Outlook.MailItem)
            // With omMailItem
            //    .SentOnBehalfOfName = "otheruser@mail.com"
            //    .To = "bill@gates.com"
            //    .Subject = "Test"
            //    .Body = "Test email"
            //    .Save()
            //    .Move(omDrafts)
        }

        /// <summary>
        /// CreateMail
        /// </summary>
        /// <param name="chemin_pdf"></param>
        /// <param name="adresse_mail"></param>
        /// <param name="textBox_sujet_mail"></param>
        /// <param name="textBox_message_mail"></param>
        internal static void CreateMailOutlookAvecPJAndSend(string chemin_pdf, string adresse_mail, string textBox_sujet_mail, string textBox_message_mail)
        {
            StringBuilder msgBuilder = new StringBuilder();

            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();

            Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)outlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            mail.Subject = textBox_sujet_mail;
            mail.To = adresse_mail;
            mail.Body = textBox_message_mail;

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
                    //   mail.Save();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}