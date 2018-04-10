using System.IO;
using System.Linq;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;
using Elastic.Managed.FileSystem;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
    public static class SecurityRealms
    {
        public const string FileRealm = "file1";

        public const string PkiRealm = "pki1";

    }
	public class EnsureSecurityRealms : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var configFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.yml");
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
