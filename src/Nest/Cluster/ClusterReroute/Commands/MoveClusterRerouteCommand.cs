using System.Runtime.Serialization;

namespace Nest
{
	public interface IMoveClusterRerouteCommand : IClusterRerouteCommand
	{
		[DataMember(Name ="from_node")]
		string FromNode { get; set; }

		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		[DataMember(Name ="shard")]
		int? Shard { get; set; }

		[DataMember(Name ="to_node")]
		string ToNode { get; set; }
	}

	public class MoveClusterRerouteCommand : IMoveClusterRerouteCommand
	{
		public string FromNode { get; set; }

		public IndexName Index { get; set; }
		public string Name => "move";

		public int? Shard { get; set; }

		public string ToNode { get; set; }
	}

	public class MoveClusterRerouteCommandDescriptor
		: DescriptorBase<MoveClusterRerouteCommandDescriptor, IMoveClusterRerouteCommand>, IMoveClusterRerouteCommand
	{
		string IMoveClusterRerouteCommand.FromNode { get; set; }

		IndexName IMoveClusterRerouteCommand.Index { get; set; }
		string IClusterRerouteCommand.Name => "move";

		int? IMoveClusterRerouteCommand.Shard { get; set; }

		string IMoveClusterRerouteCommand.ToNode { get; set; }

		public MoveClusterRerouteCommandDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public MoveClusterRerouteCommandDescriptor Index<T>() where T : class => Assign(a => a.Index = typeof(T));

		public MoveClusterRerouteCommandDescriptor Shard(int? shard) => Assign(a => a.Shard = shard);

		public MoveClusterRerouteCommandDescriptor FromNode(string fromNode) => Assign(a => a.FromNode = fromNode);

		public MoveClusterRerouteCommandDescriptor ToNode(string toNode) => Assign(a => a.ToNode = toNode);
	}
}
