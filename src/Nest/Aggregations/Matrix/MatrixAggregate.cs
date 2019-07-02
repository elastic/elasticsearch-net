using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class MatrixAggregateBase : IAggregate
	{
		/// <inheritdoc />
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
