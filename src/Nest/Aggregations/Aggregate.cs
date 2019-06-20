using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Aggregation response for an aggregation request
	/// </summary>
	[JsonFormatter(typeof(AggregateFormatter))]
	public interface IAggregate
	{
		/// <summary>
		/// Metadata for the aggregation
		/// </summary>
		IReadOnlyDictionary<string, object> Meta { get; }
	}
}
