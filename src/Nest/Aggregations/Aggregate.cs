using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Represents the result of an aggregation on the response
	/// </summary>
	[ExactContractJsonConverter(typeof(AggregateJsonConverter))]
	public interface IAggregate
	{
		IDictionary<string, object> Meta { get; set; }
	}
}
