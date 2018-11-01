using System.Collections.Generic;

namespace Nest
{
	public abstract class MatrixAggregateBase : IAggregate
	{
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
