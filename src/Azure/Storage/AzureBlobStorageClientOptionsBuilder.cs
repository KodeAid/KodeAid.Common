﻿// Copyright © Kris Penner. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.


using System;
using KodeAid.Security.Secrets;
using Microsoft.WindowsAzure.Storage;

namespace KodeAid.Azure.Storage
{
    public sealed class AzureBlobStorageClientOptionsBuilder
    {
        private CloudStorageAccount _storageAccount;
        private string _connectionString;
        private string _sasToken;
        private string _accountName;
        private string _endpointSuffix;
        private ISecretReadOnlyStore _secretStore;
        private string _connectionStringSecretName;
        private string _sasTokenSecretName;
        private string _containerName;
        private string _defaultDirectoryRelativeAddress;
        private TimeSpan? _leaseDuration = AzureBlobStorageClientOptions.DefaultLeaseDuration;
        private bool _useSnapshots;

        public AzureBlobStorageClientOptions Build()
        {
            return new AzureBlobStorageClientOptions()
            {
                AccountName = _accountName,
                ConnectionString = _connectionString,
                ConnectionStringSecretName = _connectionStringSecretName,
                ContainerName = _containerName,
                DefaultDirectoryRelativeAddress = _defaultDirectoryRelativeAddress,
                EndpointSuffix = _endpointSuffix,
                LeaseDuration = _leaseDuration,
                SecretStore = _secretStore,
                SharedAccessSignature = _sasToken,
                SharedAccessSignatureSecretName = _sasTokenSecretName,
                StorageAccount = _storageAccount,
                UseSnapshots = _useSnapshots,
            };
        }

        public AzureBlobStorageClientOptionsBuilder WithStorageAccount(CloudStorageAccount account)
        {
            _storageAccount = account;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithSharedAccessSignature(string sasToken, string accountName, string endpointSuffix = null)
        {
            _sasToken = sasToken;
            _accountName = accountName;
            _endpointSuffix = endpointSuffix;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithSecretConnectionString(string connectionStringSecretName, ISecretReadOnlyStore secretStore = null)
        {
            _connectionStringSecretName = connectionStringSecretName;
            _secretStore = secretStore;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithSecretSharedAccessSignature(string sasTokenSecretName, string accountName, ISecretReadOnlyStore secretStore = null, string endpointSuffix = null)
        {
            _sasTokenSecretName = sasTokenSecretName;
            _accountName = accountName;
            _secretStore = secretStore;
            _endpointSuffix = endpointSuffix;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithContainer(string containerName)
        {
            _containerName = containerName;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithDefaultDirectoryRelativeAddress(string directoryRelativeAddress)
        {
            _defaultDirectoryRelativeAddress = directoryRelativeAddress;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder WithLeaseDuration(TimeSpan duration)
        {
            _leaseDuration = duration;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder DisableLeases()
        {
            _leaseDuration = null;
            return this;
        }

        public AzureBlobStorageClientOptionsBuilder UseSnapshots(bool useSnapshots)
        {
            _useSnapshots = useSnapshots;
            return this;
        }
    }
}