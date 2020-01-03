using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;
using ProcNet;
using ProcNet.Std;

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
