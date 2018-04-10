using System.IO;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class EnsureSecurityRolesFileExists : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			//2.x tests only use prebaked roles
			var v = cluster.ClusterConfiguration.Version;
			if (v.Major < 5) return;
			var folder = v.Major >= 5 ? "x-pack" : "shield";
			var rolesConfig = Path.Combine(cluster.FileSystem.ConfigPath, folder, "roles.yml");

			if (!File.Exists(rolesConfig))
			{
				Directory.CreateDirectory(Path.Combine(cluster.FileSystem.ConfigPath, folder));
				File.WriteAllText(rolesConfig, string.Empty);
			}

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
