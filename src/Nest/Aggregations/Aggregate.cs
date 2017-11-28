using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// Represents the result of an aggregation on the response
	/// </summary>
	[ExactContractJsonConverter(typeof(AggregateJsonConverter))]
	public interface IAggregate
	{
		//TODO this public set is problematic
		IReadOnlyDictionary<string, object> Meta { get; set; }
	}
}
