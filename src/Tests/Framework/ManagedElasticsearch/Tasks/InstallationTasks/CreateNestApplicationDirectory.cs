using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class CreateNestApplicationDirectory : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			if (!Directory.Exists(fileSystem.RoamingFolder))
				Directory.CreateDirectory(fileSystem.RoamingFolder);
		}
	}
}
