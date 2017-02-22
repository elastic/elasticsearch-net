using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_5_2_0
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
