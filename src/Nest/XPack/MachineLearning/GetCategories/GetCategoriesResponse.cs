using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetCategoriesResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("categories")]
		IReadOnlyCollection<CategoryDefinition> Categories { get; }
	}

	public class GetCategoriesResponse : ResponseBase, IGetCategoriesResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<CategoryDefinition> Categories { get; internal set; } = EmptyReadOnly<CategoryDefinition>.Collection;
	}
}
