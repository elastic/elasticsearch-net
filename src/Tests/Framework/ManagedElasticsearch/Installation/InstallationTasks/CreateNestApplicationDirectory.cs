using System.IO;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class CreateNestApplicationDirectory : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			if (!Directory.Exists(fileSystem.RoamingFolder))
				Directory.CreateDirectory(fileSystem.RoamingFolder);
		}
	}
}
