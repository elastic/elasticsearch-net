using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// Aggregation response for an aggregation request
	/// </summary>
	[JsonFormatter(typeof(AggregateJsonFormatter))]
	public interface IAggregate
	{
		//TODO this public set is problematic
		/// <summary>
		/// Metadata for the aggregation
		/// </summary>
		IReadOnlyDictionary<string, object> Meta { get; set; }
	}
}
