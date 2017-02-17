using System.IO;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem) =>
			WriteFileIfNotExist(Path.Combine(fileSystem.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}