namespace Nest
{
	public class MoveClusterRerouteCommandDescriptor
	{
		internal MoveClusterRerouteCommand Command = new MoveClusterRerouteCommand();

		public MoveClusterRerouteCommandDescriptor Index(string index)
		{
			this.Command.Index = index;
			return this;
		}

		public MoveClusterRerouteCommandDescriptor Shard(int shard)
		{
			this.Command.Shard = shard;
			return this;
		}

		public MoveClusterRerouteCommandDescriptor FromNode(string fromNode)
		{
			this.Command.FromNode = fromNode;
			return this;
		}

		public MoveClusterRerouteCommandDescriptor ToNode(string toNode)
		{
			this.Command.ToNode = toNode;
			return this;
		}
	}
}
