using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public abstract class MetricAggregateBase : IAggregate
	{
		[DataMember(Name = "meta")]
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
