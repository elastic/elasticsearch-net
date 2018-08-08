using System;
using System.Security.Cryptography.X509Certificates;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit;
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
			Func<ConnectionSettings, ConnectionSettings> modifySettings = null)
			where TConfig : EphemeralClusterConfiguration
		{
			modifySettings = modifySettings ?? (s => s);
			return cluster.GetOrAddClient(c =>
			{
				var settings = modifySettings(cluster.CreateConnectionSettings());

				var current = (IConnectionConfigurationValues) settings;
				var notAlreadyAuthenticated = current.BasicAuthenticationCredentials == null && current.ClientCertificates == null;
				var noCertValidation = current.ServerCertificateValidationCallback == null;

				if (cluster.ClusterConfiguration.EnableSecurity && notAlreadyAuthenticated)
					settings = settings.BasicAuthentication(ClusterAuthentication.Admin.Username, ClusterAuthentication.Admin.Password);
				if (cluster.ClusterConfiguration.EnableSsl && noCertValidation)
				{
					//todo use CA callback instead of allowall
					var ca = new X509Certificate2(cluster.ClusterConfiguration.FileSystem.CaCertificate);
					settings = settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
				}
				var client = new ElasticClient(settings);
				return client;
			});
		}
	}
}
