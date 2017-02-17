using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(NodeConfiguration config, INodeFileSystem fileSystem, string[] serverSettings);
	}
}
