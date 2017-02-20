using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.AfterNodeStoppedTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(NodeConfiguration config, NodeFileSystem fileSystem);
	}
}
