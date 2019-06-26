using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class ClusterPutSettingsResponse : ResponseBase
	{
		[DataMember(Name ="acknowledged")]
		public bool Acknowledged { get; internal set; }
		[DataMember(Name ="persistent")]
		public IReadOnlyDictionary<string, object> Persistent { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
		[DataMember(Name ="transient")]
		public IReadOnlyDictionary<string, object> Transient { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
