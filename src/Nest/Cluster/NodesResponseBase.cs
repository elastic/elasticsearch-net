using System.Runtime.Serialization;

namespace Nest
{
	public abstract class NodesResponseBase : ResponseBase, INodesResponse
	{
		public NodeStatistics NodeStatistics { get; internal set; }
	}

	public interface INodesResponse : IResponse
	{
		[DataMember(Name = "_nodes")]
		NodeStatistics NodeStatistics { get; }
	}

	[DataContract]
	public class NodeStatistics
	{
		[DataMember(Name = "failed")]
		public int Failed { get; internal set; }

		[DataMember(Name = "successful")]
		public int Successful { get; internal set; }

		[DataMember(Name = "total")]
		public int Total { get; internal set; }

		//TODO map failures
	}
}
