using Newtonsoft.Json;

namespace Nest
{
	public interface IAllocateClusterRerouteCommand : IClusterRerouteCommand
	{
		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("shard")]
		int Shard { get; set; }

		[JsonProperty("node")]
		string Node { get; set; }

		[JsonProperty("allow_primary")]
		bool? AllowPrimary { get; set; }
	}
	public class AllocateClusterRerouteCommand : IAllocateClusterRerouteCommand
	{
		public string Name => "allocate";

		public IndexName Index { get; set; }

		public int Shard { get; set; }

		public string Node { get; set; }

		public bool? AllowPrimary { get; set; }
	}

	public class AllocateClusterRerouteCommandDescriptor 
		: DescriptorBase<AllocateClusterRerouteCommandDescriptor, IAllocateClusterRerouteCommand>, IAllocateClusterRerouteCommand
	{
		string IClusterRerouteCommand.Name => "allocate";

		IndexName IAllocateClusterRerouteCommand.Index { get; set; }

		int IAllocateClusterRerouteCommand.Shard { get; set; }

		string IAllocateClusterRerouteCommand.Node { get; set; }

		bool? IAllocateClusterRerouteCommand.AllowPrimary { get; set; }

		public AllocateClusterRerouteCommandDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public AllocateClusterRerouteCommandDescriptor Index<T>() where T : class => Assign(a => a.Index = typeof(T));

		public AllocateClusterRerouteCommandDescriptor Shard(int shard) => Assign(a => a.Shard = shard);

		public AllocateClusterRerouteCommandDescriptor Node(string node) => Assign(a => a.Node = node);

		public AllocateClusterRerouteCommandDescriptor AllowPrimary(bool? allowPrimary = true) => Assign(a => a.AllowPrimary = allowPrimary);
	}
}
