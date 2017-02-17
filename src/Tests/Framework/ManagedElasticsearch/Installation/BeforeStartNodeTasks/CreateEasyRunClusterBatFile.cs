using System.IO;
using System.Linq;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class CreateEasyRunClusterBatFile : BeforeStartNodeTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fs, string[] serverSettings)
		{
			var cluster = config.TypeOfCluster;
			var v = config.ElasticsearchVersion;
			var easyRunBat = Path.Combine(fs.RoamingFolder, $"run-{cluster}.bat");
			if (File.Exists(easyRunBat)) return;
			var badSettings = new[] {"node.name", "cluster.name"};
			var batSettings = string.Join(" ", serverSettings.Where(s => !badSettings.Any(s.Contains)));
			File.WriteAllText(easyRunBat, $@"elasticsearch-{v}\bin\elasticsearch.bat {batSettings}");
		}
	}
}