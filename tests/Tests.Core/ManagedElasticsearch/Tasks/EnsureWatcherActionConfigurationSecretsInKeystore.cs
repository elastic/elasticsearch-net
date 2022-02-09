// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Tests.Core.ManagedElasticsearch.Tasks
{
	public class EnsureWatcherActionConfigurationSecretsInKeystore : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var keystoreFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.keystore");
			var binary = Path.Combine(cluster.FileSystem.ElasticsearchHome, "bin", "elasticsearch-keystore" + BinarySuffix);

			if (!File.Exists(keystoreFile))
				ExecuteBinary(
					cluster.ClusterConfiguration,
					cluster.Writer,
					binary,
					"creating elasticsearch.keystore",
					"create");

			ExecuteBinary(
				cluster.ClusterConfiguration,
				cluster.Writer,
				binary,
				"add xpack.notification.slack.account.monitoring.secure_url to elasticsearch.keystore",
				input => input.WriteLine("https://hooks.slack.com/services/EXAMPLE/EXAMPLE/EXAMPLE"),
				"add", "xpack.notification.slack.account.monitoring.secure_url", "-xf");

			ExecuteBinary(
				cluster.ClusterConfiguration,
				cluster.Writer,
				binary,
				"add xpack.notification.pagerduty.account.my_pagerduty_account.secure_service_api_key to elasticsearch.keystore",
				input => input.WriteLine("dummy_key"),
				"add", "xpack.notification.pagerduty.account.my_pagerduty_account.secure_service_api_key", "-xf");

			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(EnsureWatcherActionConfigurationSecretsInKeystore)}}} added watcher action secrets to elasticsearch.keystore");
		}

	}
}
