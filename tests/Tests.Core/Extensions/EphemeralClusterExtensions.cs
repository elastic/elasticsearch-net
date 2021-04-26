/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Security.Cryptography.X509Certificates;
 using Elastic.Elasticsearch.Ephemeral;
 using Elastic.Elasticsearch.Xunit;
 using Elasticsearch.Net;
using Nest;
using Tests.Core.Client.Settings;

namespace Tests.Core.Extensions
{
	public static class EphemeralClusterExtensions
	{
		public static ConnectionSettings CreateConnectionSettings<TConfig>(this IEphemeralCluster<TConfig> cluster)
			where TConfig : EphemeralClusterConfiguration
		{
			var clusterNodes = cluster.NodesUris(TestConnectionSettings.LocalOrProxyHost);
			//we ignore the uri's that TestConnection provides and seed with the nodes the cluster dictates.
			return new TestConnectionSettings(uris => new StaticConnectionPool(clusterNodes));
		}

		public static IElasticClient GetOrAddClient<TConfig>(
			this IEphemeralCluster<TConfig> cluster,
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null
		)
			where TConfig : EphemeralClusterConfiguration
		{
			modifySettings = modifySettings ?? (s => s);
			return cluster.GetOrAddClient(c =>
			{
				var settings = modifySettings(cluster.CreateConnectionSettings());

				var current = (IConnectionConfigurationValues)settings;
				var notAlreadyAuthenticated = current.BasicAuthenticationCredentials == null
					&& current.ApiKeyAuthenticationCredentials == null
					&& current.ClientCertificates == null;

				var noCertValidation = current.ServerCertificateValidationCallback == null;

				if (cluster.ClusterConfiguration.EnableSecurity && notAlreadyAuthenticated)
					settings = settings.BasicAuthentication(ClusterAuthentication.Admin.Username, ClusterAuthentication.Admin.Password);
				if (cluster.ClusterConfiguration.EnableSsl && noCertValidation)
				{
					//todo use CA callback instead of allowall
					// ReSharper disable once UnusedVariable
					var ca = new X509Certificate2(cluster.ClusterConfiguration.FileSystem.CaCertificate);
					settings = settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
				}
				var client = new ElasticClient(settings);
				return client;
			});
		}
	}
}
