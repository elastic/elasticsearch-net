using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetCategoriesResponse : IResponse
	{
		[JsonProperty("categories")]
		IReadOnlyCollection<CategoryDefinition> Categories { get; }

		[JsonProperty("count")]
		long Count { get; }
	}

	public class GetCategoriesResponse : ResponseBase, IGetCategoriesResponse
	{
		public IReadOnlyCollection<CategoryDefinition> Categories { get; internal set; } = EmptyReadOnly<CategoryDefinition>.Collection;
		public long Count { get; internal set; }
	}
}
