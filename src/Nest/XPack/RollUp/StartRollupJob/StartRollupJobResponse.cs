using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class StartRollupJobResponse : ResponseBase
	{
		[DataMember(Name ="started")]
		public bool Started { get; set; }
	}
}
