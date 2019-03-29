﻿// Copyright © Kris Penner. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.


using System;
using KodeAid;
using KodeAid.Azure.Storage;
using KodeAid.Security.Cryptography.X509Certificates;
using KodeAid.Security.Secrets;
using KodeAid.Storage;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureBlobStorageClientServiceCollectionExtensions
    {
        public static void AddAzureBlobStorageKeyValueStore(this IServiceCollection services, string configurationSection = nameof(AzureBlobStorageClient))
        {
            ArgCheck.NotNull(nameof(services), services);
            ArgCheck.NotNullOrEmpty(nameof(configurationSection), configurationSection);

            services.AddTransient<IKeyValueReadOnlyStore>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>()?.GetSection(configurationSection)
                    ?? throw new InvalidOperationException($"Azure blob store configuration section '{configurationSection}' not found.");
                var options = new AzureBlobStorageClientOptions();
                configuration.Bind(options);

                if (options.SecretStore == null)
                {
                    options.SecretStore = serviceProvider.GetService<ISecretReadOnlyStore>();
                }

                return new AzureBlobStorageClient(options);
            });

            services.AddTransient<IKeyValueStore>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>()?.GetSection(configurationSection)
                    ?? throw new InvalidOperationException($"Azure blob store configuration section '{configurationSection}' not found.");
                var options = new AzureBlobStorageClientOptions();
                configuration.Bind(options);

                if (options.SecretStore == null)
                {
                    options.SecretStore = serviceProvider.GetService<ISecretReadOnlyStore>();
                }

                return new AzureBlobStorageClient(options);
            });
        }

        public static void AddAzureBlobStorageKeyValueStore(this IServiceCollection services, Action<AzureBlobStorageClientOptionsBuilder> setup)
        {
            ArgCheck.NotNull(nameof(services), services);
            ArgCheck.NotNull(nameof(setup), setup);

            services.AddTransient<IKeyValueReadOnlyStore>(serviceProvider =>
            {
                var builder = new AzureBlobStorageClientOptionsBuilder();
                setup(builder);
                var options = builder.Build();

                if (options.SecretStore == null)
                {
                    options.SecretStore = serviceProvider.GetService<ISecretReadOnlyStore>();
                }

                return new AzureBlobStorageClient(options);
            });

            services.AddTransient<IKeyValueStore>(serviceProvider =>
            {
                var builder = new AzureBlobStorageClientOptionsBuilder();
                setup(builder);
                var options = builder.Build();

                if (options.SecretStore == null)
                {
                    options.SecretStore = serviceProvider.GetService<ISecretReadOnlyStore>();
                }

                return new AzureBlobStorageClient(options);
            });
        }

        public static void AddAzureBlobStoragePublicCertificateStore(this IServiceCollection services, string configurationSection = "AzureBlobStoragePublicCertificateStore")
        {
            ArgCheck.NotNull(nameof(services), services);
            ArgCheck.NotNullOrEmpty(nameof(configurationSection), configurationSection);

            services.AddTransient<IPublicCertificateStore>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>()?.GetSection(configurationSection)
                    ?? throw new InvalidOperationException($"Azure blob store configuration section '{configurationSection}' not found.");
                var options = new AzureBlobStorageClientOptions();
                configuration.Bind(options);

                if (options.SecretStore == null)
                {
                    options.SecretStore = serviceProvider.GetService<ISecretReadOnlyStore>();
                }

                return new AzureBlobStorageClient(options);
            });
        }

        public static void AddAzureBlobStoragePublicCertificateStore(this IServiceCollection services, Action<AzureBlobStorageClientOptionsBuilder> setup)
        {
            ArgCheck.NotNull(nameof(services), services);
            ArgCheck.NotNull(nameof(setup), setup);

            services.AddTransient<IPublicCertificateStore>(serviceProvider =>
            {
                var builder = new AzureBlobStorageClientOptionsBuilder();
                setup(builder);
                var options = builder.Build();

                if (options.SecretStore == null)
                {
                    options.SecretStore = serviceProvider.GetService<ISecretReadOnlyStore>();
                }

                return new AzureBlobStorageClient(options);
            });
        }
    }
}