using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class AcknowledgedResponseBase : ResponseBase
	{
		[DataMember(Name = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}
