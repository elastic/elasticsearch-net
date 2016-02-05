using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
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
