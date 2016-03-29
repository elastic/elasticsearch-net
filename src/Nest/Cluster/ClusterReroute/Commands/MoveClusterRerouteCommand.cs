using Newtonsoft.Json;

namespace Nest
{

	public interface IMoveClusterRerouteCommand: IClusterRerouteCommand
	{
		[JsonProperty("from_node")]
		string FromNode { get; set; }

		[JsonProperty("index")]
		IndexName Index { get; set; }

		[JsonProperty("shard")]
		int Shard { get; set; }

		[JsonProperty("to_node")]
		string ToNode { get; set; }
	}
	public class MoveClusterRerouteCommand : IMoveClusterRerouteCommand
	{
		public string Name => "move";

		public IndexName Index { get; set; }

		public int Shard { get; set; }

		public string FromNode { get; set; }

		public string ToNode { get; set; }
	}

	public class MoveClusterRerouteCommandDescriptor 
		: DescriptorBase<MoveClusterRerouteCommandDescriptor, IMoveClusterRerouteCommand>, IMoveClusterRerouteCommand
	{
		string IClusterRerouteCommand.Name => "move";

		IndexName IMoveClusterRerouteCommand.Index { get; set; }

		int IMoveClusterRerouteCommand.Shard { get; set; }

		string IMoveClusterRerouteCommand.FromNode { get; set; }

		string IMoveClusterRerouteCommand.ToNode { get; set; }

		public MoveClusterRerouteCommandDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public MoveClusterRerouteCommandDescriptor Index<T>() where T : class => Assign(a => a.Index = typeof(T));

		public MoveClusterRerouteCommandDescriptor Shard(int shard) => Assign(a => a.Shard = shard);

		public MoveClusterRerouteCommandDescriptor FromNode(string fromNode) => Assign(a => a.FromNode = fromNode);

		public MoveClusterRerouteCommandDescriptor ToNode(string toNode) => Assign(a => a.ToNode = toNode);
	}
}
