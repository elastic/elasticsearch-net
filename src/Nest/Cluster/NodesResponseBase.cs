using System.Runtime.Serialization;

namespace Nest
{
	public abstract class NodesResponseBase : ResponseBase
	{
		[DataMember(Name = "_nodes")]
		public NodeStatistics NodeStatistics { get; internal set; }
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
