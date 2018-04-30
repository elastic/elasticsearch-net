using System.IO;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	/// <summary>
	/// Fixes https://github.com/elastic/elasticsearch/issues/29057
	/// </summary>
	public class EnsureElasticsearchBatWorksAcrossDrives : InstallationTaskBase
	{
		private readonly ElasticsearchVersion _sixTwoZero = "6.2.0";
		private readonly ElasticsearchVersion _sixThreeZero = "6.3.0";

		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			if (config.ElasticsearchVersion < _sixTwoZero || config.ElasticsearchVersion >= _sixThreeZero)
				return;

			var batFile = Path.Combine(fileSystem.ElasticsearchHome, "bin", "elasticsearch.bat");
			var contents = File.ReadAllLines(batFile);
			for (var i = 0; i < contents.Length; i++)
			{
				if (contents[i] == "cd \"%ES_HOME%\"")
				{
					contents[i] = "cd /d \"%ES_HOME%\"";
					break;
				}
			}

			File.WriteAllLines(batFile, contents);
		}
	}
}
