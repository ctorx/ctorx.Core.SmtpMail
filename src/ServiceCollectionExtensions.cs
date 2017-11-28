using System;
using Microsoft.Extensions.DependencyInjection;

namespace ctorx.Core.SmtpMail
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Picnic to the application
        /// </summary>
        /// <param name="services">IServiceCollection for the application</param>
        /// <param name="options">Options for Smtp Mail</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSmtpMail(this IServiceCollection services, Action<SmtpOptions> options)
        {
            services.Configure(options);

            services.AddSingleton<IEmailSender, SmtpEmailSender>();

            return services;
        }

    }
}