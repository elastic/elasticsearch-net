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

		// TODO: Remove when https://github.com/elastic/elasticsearch-net-abstractions/pull/17 is published
		private static void ExecuteBinary(EphemeralClusterConfiguration config, IConsoleLineWriter writer, string binary, string description,
			StartedHandler startedHandler, params string[] arguments
		)
		{
			var command = $"{{{binary}}} {{{string.Join(" ", arguments)}}}";
			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} starting process [{description}] {command}");

			var timeout = TimeSpan.FromSeconds(420);
			var processStartArguments = new StartArguments(binary, arguments)
			{
				Environment = new Dictionary<string, string>
				{
					{ config.FileSystem.ConfigEnvironmentVariableName, config.FileSystem.ConfigPath },
					{ "ES_HOME", config.FileSystem.ElasticsearchHome }
				}
			};

			var result = Proc.Start(processStartArguments, timeout, new ConsoleOutWriter(), startedHandler);
			if (!result.Completed)
				throw new Exception($"Timeout while executing {description} exceeded {timeout}");

			if (result.ExitCode != 0)
				throw new Exception($"Expected exit code 0 but received ({result.ExitCode}) while executing {description}: {command}");

			var errorOut = result.ConsoleOut.Where(c => c.Error).ToList();
			// this manifested when calling certgen on versions smaller then 5.2.0
			if (errorOut.Any() && config.Version < "5.2.0")
				errorOut = errorOut.Where(e => !e.Line.Contains("No log4j2 configuration file found")).ToList();
			if (errorOut.Any(e => !string.IsNullOrWhiteSpace(e.Line)))
				throw new Exception($"Received error out with exitCode ({result.ExitCode}) while executing {description}: {command}");

			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} finished process [{description}] {{{result.ExitCode}}}");
		}
	}
}
