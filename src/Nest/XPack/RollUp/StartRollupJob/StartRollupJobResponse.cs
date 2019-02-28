using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IStartRollupJobResponse : IResponse
	{
		[DataMember(Name ="started")]
		bool Started { get; set; }
	}

	public class StartRollupJobResponse : ResponseBase, IStartRollupJobResponse
	{
		public bool Started { get; set; }
	}
}
