using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Aggregation response for an aggregation request
	/// </summary>
	[JsonFormatter(typeof(AggregateFormatter))]
	public interface IAggregate
	{
		//TODO this public set is problematic
		/// <summary>
		/// Metadata for the aggregation
		/// </summary>
		IReadOnlyDictionary<string, object> Meta { get; set; }
	}
}
