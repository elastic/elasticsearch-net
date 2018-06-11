using System.IO;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;

namespace Tests.Framework.ManagedElasticsearch.Tasks
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

			// set up for Watcher HipChat action
			if (!lines.Any(line => line.StartsWith($"{prefix}.hipchat{postfix}:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					$"{prefix}.hipchat{postfix}:",
					"  account:",
					"    notify-monitoring:",
					"      profile: user",
					"      auth_token: hipchat_auth_token",
					string.Empty
				});

				saveFile = true;
			}

			// set up for Watcher Slack action
			if (!lines.Any(line => line.StartsWith($"{prefix}.slack{postfix}:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					$"{prefix}.slack{postfix}:",
					"  account:",
					"    monitoring:",
					"      url: https://hooks.slack.com/services/foo/bar/baz",
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
					"      service_api_key: pager_duty_service_api_key",
					string.Empty
				});

				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(configFile, lines);
			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureWatcherActionConfigurationInElasticsearchYaml)}}} {(saveFile ? "saved" : "skipped saving")} watcher config [{configFile}]");
		}
	}
}
