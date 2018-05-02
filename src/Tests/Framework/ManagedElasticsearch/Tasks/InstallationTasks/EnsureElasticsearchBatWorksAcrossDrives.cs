using System.IO;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	/// <summary>
	/// Fixes https://github.com/elastic/elasticsearch/issues/29057
	/// </summary>
	public class EnsureElasticsearchBatWorksAcrossDrives : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var config = cluster.ClusterConfiguration;
			if (config.Version < "6.2.0" || config.Version >= "6.3.0")
				return;

			var batFile = cluster.FileSystem.Binary;
			var contents = File.ReadAllLines(batFile);
			for (var i = 0; i < contents.Length; i++)
			{
				if (contents[i] != "cd \"%ES_HOME%\"") continue;

				contents[i] = "cd /d \"%ES_HOME%\"";
				break;
			}

			File.WriteAllLines(batFile, contents);
		}
	}
}
