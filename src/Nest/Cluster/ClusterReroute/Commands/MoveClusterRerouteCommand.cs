using Newtonsoft.Json;

namespace Nest
{
	public interface IMoveClusterRerouteCommand : IClusterRerouteCommand
	{
		[JsonProperty("from_node")]
		string FromNode { get; set; }

		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("shard")]
		int? Shard { get; set; }

		[JsonProperty("to_node")]
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

		public MoveClusterRerouteCommandDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public MoveClusterRerouteCommandDescriptor Index<T>() where T : class => Assign(typeof(T), (a, v) => a.Index = v);

		public MoveClusterRerouteCommandDescriptor Shard(int? shard) => Assign(shard, (a, v) => a.Shard = v);

		public MoveClusterRerouteCommandDescriptor FromNode(string fromNode) => Assign(fromNode, (a, v) => a.FromNode = v);

		public MoveClusterRerouteCommandDescriptor ToNode(string toNode) => Assign(toNode, (a, v) => a.ToNode = v);
	}
}
