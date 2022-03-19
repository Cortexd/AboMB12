using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboMB12
{
    public class Attestation
    {
        [Name("sCliRaisonSoc")]
        public string RaisonSociale { get; set; }

        [Name("sCliAdresse1Ligne")]
        public string AdresseLigne1 { get; set; }

        [Name("sCliAdresse1CodePos")]
        public string AdresseCP { get; set; }

        [Name("sCliAdresse1Ville")]
        public string AdresseVille { get; set; }

        [Name("sContact.Civilite")]
        public string Civilite { get; set; }

        [Name("sContact.Interloc")]
        public string Interlocuteur { get; set; }

        [Name("sContact.EMail")]
        public string Email { get; set; }

        [Name("Heure")]
        public string Heure { get; set; }
    }

    public class AttestationMap : ClassMap<Attestation>
    {
        public AttestationMap()
        {
            Map(m => m.RaisonSociale).Name("sCliRaisonSoc", "sCliRaisonSoc");
            Map(m => m.AdresseLigne1).Name("sCliAdresse1Ligne", "sCliAdresse1Ligne");
            Map(m => m.AdresseCP).Name("sCliAdresse1CodePos", "sCliAdresse1CodePos");
            Map(m => m.AdresseVille).Name("sCliAdresse1Ville", "sCliAdresse1Ville");
            Map(m => m.Civilite).Name("sContact.Civilite", "sContact.Civilite");
            Map(m => m.Interlocuteur).Name("sContact.Interloc", "sContact.Interloc");
            Map(m => m.Email).Name("sContact.EMail", "sContact.EMail");
            Map(m => m.Heure).Name("Heure", "Heure");
        }
    }
}