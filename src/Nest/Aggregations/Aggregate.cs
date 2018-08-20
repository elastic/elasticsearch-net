using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// Aggregation response for an aggregation request
	/// </summary>
	[ExactContractJsonConverter(typeof(AggregateJsonConverter))]
	public interface IAggregate
	{
		//TODO this public set is problematic
		/// <summary>
		/// Metadata for the aggregation
		/// </summary>
		IReadOnlyDictionary<string, object> Meta { get; set; }
	}
}
