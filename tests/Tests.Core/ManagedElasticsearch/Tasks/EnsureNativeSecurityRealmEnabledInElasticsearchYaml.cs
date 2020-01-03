using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;

namespace Tests.Core.ManagedElasticsearch.Tasks
{
	public class EnsureNativeSecurityRealmEnabledInElasticsearchYaml : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var configFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.yml");
			var lines = File.ReadAllLines(configFile).ToList();
			var modifiedLines = new List<string>();
			var modified = false;

			foreach (var line in lines)
			{
				modifiedLines.Add(line);

				if (line.Contains("realms:"))
				{
					var collection = cluster.ClusterConfiguration.Version >= "7.0.0-alpha1"
						? new[]
						{
							"        native.native1:",
							"          order: 2"
						}
						: new[]
						{
							"        native1:",
							"          type: native",
							"          order: 2"
						};

					modifiedLines.AddRange(collection);
					modified = true;
				}
			}

			if (!modified)
			{
				var collection = cluster.ClusterConfiguration.Version >= "7.0.0-alpha1"
					? new[]
					{
						string.Empty,
						"xpack:",
						"  security:",
						"    authc:",
						"      realms:",
						"        native1:",
						"          type: native",
						"          order: 0",
						string.Empty
					}
					: new[]
					{
						string.Empty,
						"xpack:",
						"  security:",
						"    authc:",
						"      realms:",
						"        native.native1:",
						"          order: 0",
						string.Empty
					};

				modifiedLines.AddRange(collection);
			}

			File.WriteAllLines(configFile, modifiedLines);
			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureNativeSecurityRealmEnabledInElasticsearchYaml)}}} native security realm [{configFile}]");
		}
	}
}
