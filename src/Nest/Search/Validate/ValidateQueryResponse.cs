using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class ValidateQueryResponse : ResponseBase
	{
		/// <summary>
		/// Gets the explanations if Explain() was set.
		/// </summary>
		[DataMember(Name ="explanations")]
		public IReadOnlyCollection<ValidationExplanation> Explanations { get; internal set; } = EmptyReadOnly<ValidationExplanation>.Collection;

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name ="valid")]
		public bool Valid { get; internal set; }
	}
}
