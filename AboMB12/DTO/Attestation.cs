using CsvHelper.Configuration.Attributes;

namespace AboMB12
{
    /// <summary>
    /// Attestation
    /// </summary>
    public class Attestation
    {
        /// <summary>
        /// Gets or sets raisonSociale
        /// </summary>
        [Name("sCliRaisonSoc")]
        public string RaisonSociale { get; set; }

        /// <summary>
        /// Gets or sets adresseLigne1
        /// </summary>
        [Name("sCliAdresse1Ligne")]
        public string AdresseLigne1 { get; set; }

        /// <summary>
        /// Gets or sets adresseCP
        /// </summary>
        [Name("sCliAdresse1CodePos")]
        public string AdresseCP { get; set; }

        /// <summary>
        /// Gets or sets adresseVille
        /// </summary>
        [Name("sCliAdresse1Ville")]
        public string AdresseVille { get; set; }

        /// <summary>
        /// Gets or sets civilite
        /// </summary>
        [Name("sContact.Civilite")]
        public string Civilite { get; set; }

        /// <summary>
        /// Gets or sets interlocuteur
        /// </summary>
        [Name("sContact.Interloc")]
        public string Interlocuteur { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        [Name("sContact.EMail")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets heure
        /// </summary>
        [Name("Heure")]
        public string Heure { get; set; }
    }
}