using System.IO;
using System.Linq;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class EnsureWatcherActionConfigurationInElasticsearchYaml : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			if (!config.XPackEnabled) return;

			var rolesConfig = Path.Combine(fileSystem.ElasticsearchHome, "config", "elasticsearch.yml");
			var lines = File.ReadAllLines(rolesConfig).ToList();
			var saveFile = false;

			// set up for Watcher HipChat action
			if (!lines.Any(line => line.StartsWith("xpack.notification.hipchat:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack.notification.hipchat:",
					"  account:",
					"    notify-monitoring:",
					"      profile: user",
					"      user: watcher-user@example.com",
					"      auth_token: hipchat_auth_token",
					string.Empty
				});

				saveFile = true;
			}

			// set up for Watcher Slack action
			if (!lines.Any(line => line.StartsWith("xpack.notification.slack:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack.notification.slack:",
					"  account:",
					"    monitoring:",
					"      url: https://hooks.slack.com/services/foo/bar/baz",
					string.Empty
				});

				saveFile = true;
			}

			// set up for Watcher PagerDuty action
			if (!lines.Any(line => line.StartsWith("xpack.notification.pagerduty:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack.notification.pagerduty:",
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