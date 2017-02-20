using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem) =>
			WriteFileIfNotExist(Path.Combine(fileSystem.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
