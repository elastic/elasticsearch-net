using System.Runtime.Serialization;

namespace Nest
{
	public abstract class NodesResponseBase : ResponseBase, INodesResponse
	{
		public NodeStatistics NodeStatistics { get; internal set; }
	}

	public interface INodesResponse : IResponse
	{
		[DataMember(Name ="_nodes")]
		NodeStatistics NodeStatistics { get; }
	}

	[DataContract]
	public class NodeStatistics
	{
		[JsonProperty]
		public int Failed { get; internal set; }

		[JsonProperty]
		public int Successful { get; internal set; }

		[JsonProperty]
		public int Total { get; internal set; }

		//TODO map failures
	}
}
