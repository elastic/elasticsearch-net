using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class StopRollupJobResponse : ResponseBase
	{
		[DataMember(Name ="stopped")]
		public bool Stopped { get; set; }
	}
}
