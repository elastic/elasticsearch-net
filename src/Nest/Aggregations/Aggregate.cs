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

	[AttributeUsage(
		AttributeTargets.Class | AttributeTargets.Interface, 
		AllowMultiple = false, 
		Inherited = true)
	]
	public class AggregateTypeAttribute : Attribute
	{
		public string Name { get; private set; }

		public AggregateTypeAttribute(Type aggregateType)
		{
			if (!typeof(IAggregate).IsAssignableFrom(aggregateType))
				throw new ArgumentException("Type is not an IAggregate");
			Name = aggregateType.FullName;
		}
	}
}
