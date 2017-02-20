using System.IO;
using System.Linq;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.BeforeStartNodeTasks
{
	public class CreateEasyRunClusterBatFile : BeforeStartNodeTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fs, string[] serverSettings)
		{
			var clusterMoniker = config.ClusterMoniker;
			var v = config.ElasticsearchVersion;
			var easyRunBat = Path.Combine(fs.RoamingFolder, $"run-{clusterMoniker}.bat");
			if (File.Exists(easyRunBat)) return;
			var badSettings = new[] {"node.name", "cluster.name"};
			var batSettings = string.Join(" ", serverSettings.Where(s => !badSettings.Any(s.Contains)));
			File.WriteAllText(easyRunBat, $@"elasticsearch-{v}\bin\elasticsearch.bat {batSettings}");
		}
	}
}
