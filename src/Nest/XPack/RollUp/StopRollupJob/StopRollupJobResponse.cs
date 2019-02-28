using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IStopRollupJobResponse : IResponse
	{
		[DataMember(Name ="stopped")]
		bool Stopped { get; set; }
	}

	public class StopRollupJobResponse : ResponseBase, IStopRollupJobResponse
	{
		public bool Stopped { get; set; }
	}
}
