namespace ctorx.Core.SmtpMail
{
    public class SmtpSettings
    {
        /// <summary>
        /// Gets or sets the SmtpHost
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Gets or sets the SmtpPort
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Gets or sets the SmtpUsername
        /// </summary>
        public string SmtpUsername { get; set; }

        /// <summary>
        /// Gets or sets the SmtpPassword
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Gets or sets the SmtpEnableSsl
        /// </summary>
        public bool SmtpEnableSsl { get; set; }
    }
}