using System.IO;
using System.Linq;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureWatcherActionConfigurationInElasticsearchYaml : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var rolesConfig = Path.Combine(fileSystem.ElasticsearchHome, "config", "elasticsearch.yml");
			var lines = File.ReadAllLines(rolesConfig).ToList();
			var saveFile = false;

			var prefix = config.ElasticsearchVersion.Major >= 5 ? "xpack.notification" : "watcher.actions";
			var postfix = config.ElasticsearchVersion.Major >= 5 ? string.Empty : ".service";

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

			if (saveFile) File.WriteAllLines(rolesConfig, lines);
		}
	}
}
