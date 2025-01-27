using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace AboMB12
{
    /// <summary>
    /// Classe de mappage du CSV
    /// </summary>
    public class AttestationMap : ClassMap<Attestation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttestationMap"/> class.
        /// Mappage d'attestation
        /// </summary>
        public AttestationMap()
        {
            this.Map(m => m.RaisonSociale).Name("sCliRaisonSoc", "sCliRaisonSoc");
            this.Map(m => m.AdresseLigne1).Name("sCliAdresse1Ligne", "sCliAdresse1Ligne");
            this.Map(m => m.AdresseCP).Name("sCliAdresse1CodePos", "sCliAdresse1CodePos");
            this.Map(m => m.AdresseVille).Name("sCliAdresse1Ville", "sCliAdresse1Ville");
            this.Map(m => m.Civilite).Name("sContact.Civilite", "sContact.Civilite");
            this.Map(m => m.Interlocuteur).Name("sContact.Interloc", "sContact.Interloc");
            this.Map(m => m.Email).Name("sContact.EMail", "sContact.EMail");
            this.Map(m => m.Heure).Name("Heure", "Heure", "Heures");
        }
    }
}