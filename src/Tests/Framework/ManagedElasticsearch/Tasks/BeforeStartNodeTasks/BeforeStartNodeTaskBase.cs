using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.BeforeStartNodeTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(NodeConfiguration config, NodeFileSystem fileSystem, string[] serverSettings);
	}
}
