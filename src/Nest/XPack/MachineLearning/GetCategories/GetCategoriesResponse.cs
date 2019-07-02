using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetCategoriesResponse : ResponseBase
	{
		[DataMember(Name ="categories")]
		public IReadOnlyCollection<CategoryDefinition> Categories { get; internal set; } = EmptyReadOnly<CategoryDefinition>.Collection;

		[DataMember(Name ="count")]
		public long Count { get; internal set; }
	}
}
