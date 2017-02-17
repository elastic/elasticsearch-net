using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(NodeConfiguration config, INodeFileSystem fileSystem);
	}
}
