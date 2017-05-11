using System.IO;
using System.Linq;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureSecurityRealms : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var configFile = Path.Combine(fileSystem.ElasticsearchHome, "config", "elasticsearch.yml");
			var lines = File.ReadAllLines(configFile).ToList();
			var saveFile = false;

			// set up for Watcher HipChat action
			if (!lines.Any(line => line.Contains("file1")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack:",
					"  security:",
					"    authc:",
					"      realms:",
					$"        {SecurityRealms.FileRealm}:",
					"          type: file",
					"          order: 0",
					$"        {SecurityRealms.PkiRealm}:",
					"          type: pki",
					"          order: 1",
					string.Empty
				});
				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(configFile, lines);
		}
	}
}
