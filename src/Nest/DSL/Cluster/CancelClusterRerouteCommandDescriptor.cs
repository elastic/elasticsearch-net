namespace Nest
{
	public class CancelClusterRerouteCommandDescriptor
	{
		internal CancelClusterRerouteCommand Command = new CancelClusterRerouteCommand();

		public CancelClusterRerouteCommandDescriptor Index(string index)
		{
			this.Command.Index = index;
			return this;
		}

		public CancelClusterRerouteCommandDescriptor Shard(int shard)
		{
			this.Command.Shard = shard;
			return this;
		}

		public CancelClusterRerouteCommandDescriptor Node(string node)
		{
			this.Command.Node = node;
			return this;
		}

		public CancelClusterRerouteCommandDescriptor AllowPrimary(bool allowPrimary = true)
		{
			this.Command.AllowPrimary = allowPrimary;
			return this;
		}
	}
}
