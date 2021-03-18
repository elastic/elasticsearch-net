// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Tests.Core.ManagedElasticsearch.Tasks
{
	public class EnsureWatcherActionConfigurationInElasticsearchYaml : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var configFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.yml");
			var lines = File.ReadAllLines(configFile).ToList();
			var saveFile = false;

			var v = cluster.ClusterConfiguration.Version;
			var prefix = v.Major >= 5 ? "xpack.notification" : "watcher.actions";
			var postfix = v.Major >= 5 ? string.Empty : ".service";

			// set up for Watcher Slack action
			if (!lines.Any(line => line.StartsWith($"{prefix}.slack{postfix}:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					$"{prefix}.slack{postfix}:",
					"  account:",
					"    monitoring:",
					"      message_defaults:",
					"        from: x-pack",
					string.Empty
				});

				saveFile = true;
			}

			// set up for Watcher PagerDuty action
			if (!lines.Any(line => line.StartsWith($"{prefix}.pagerduty{postfix}:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					$"{prefix}.pagerduty{postfix}:",
					"  account:",
					"    my_pagerduty_account:",
					"      event_defaults:",
					"        description: pager_duty",
					//"      service_api_key: pager_duty_service_api_key",
					string.Empty
				});

				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(configFile, lines);
			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(EnsureWatcherActionConfigurationInElasticsearchYaml)}}} {(saveFile ? "saved" : "skipped saving")} watcher config [{configFile}]");
		}
	}
}
