using Tests.Framework;

namespace Tests.Framework
{
	public static class ClientCall
	{
		public static ClientCallAssertations OnCluster(VirtualizedCluster cluster) => new ClientCallAssertations(cluster);
	}
}