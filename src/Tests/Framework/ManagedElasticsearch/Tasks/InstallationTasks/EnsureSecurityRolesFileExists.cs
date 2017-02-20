using System.IO;
using System.Linq;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureSecurityRolesFileExists : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			if (!config.XPackEnabled) return;
			var folder = config.ElasticsearchVersion.Major >= 5 ? "x-pack" : "shield";
			var rolesConfig = Path.Combine(fileSystem.ElasticsearchHome, "config", folder, "roles.yml");
			var lines = File.ReadAllLines(rolesConfig).ToList();
			var saveFile = false;

			if (!lines.Any(line => line.StartsWith("user:")))
			{
				lines.InsertRange(0, new []
				{
					"# Read-only operations on indices",
					"user:",
					"  indices:",
					"    - names: '*'",
					"      privileges:",
					"        - read",
					string.Empty
				});

				saveFile = true;
			}

			if (!lines.Any(line => line.StartsWith("power_user:")))
			{
				lines.InsertRange(0, new []
				{
					"# monitoring cluster privileges",
					"# All operations on all indices",
					"power_user:",
					"  cluster:",
					"    - monitor",
					"  indices:",
					"    - names: '*'",
					"      privileges:",
					"        - all",
					string.Empty
				});

				saveFile = true;
			}

			if (!lines.Any(line => line.StartsWith("admin:")))
			{
				lines.InsertRange(0, new []
				{
					"# All cluster rights",
					"# All operations on all indices",
					"admin:",
					"  cluster:",
					"    - all",
					"  indices:",
					"    - names: '*'",
					"      privileges:",
					"        - all",
					string.Empty
				});

				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(rolesConfig, lines);
		}
	}
}
