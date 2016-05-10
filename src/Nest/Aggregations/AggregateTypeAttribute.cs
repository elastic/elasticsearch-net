using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Nest
{
	[AttributeUsage(
		AttributeTargets.Class | AttributeTargets.Interface,
		AllowMultiple = false,
		Inherited = true)
	]
	internal class AggregateTypeAttribute : Attribute
	{
		public Type Type { get; private set; }

		public AggregateTypeAttribute(Type aggregateType)
		{
			if (!typeof(IAggregate).IsAssignableFrom(aggregateType))
				throw new ArgumentException($"{nameof(aggregateType)} must be of type IAggregate.");
			this.Type = aggregateType;
		}
	}
}
