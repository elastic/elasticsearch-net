using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetCategoriesResponse : IResponse
	{
		[DataMember(Name ="categories")]
		IReadOnlyCollection<CategoryDefinition> Categories { get; }

		[DataMember(Name ="count")]
		long Count { get; }
	}

	public class GetCategoriesResponse : ResponseBase, IGetCategoriesResponse
	{
		public IReadOnlyCollection<CategoryDefinition> Categories { get; internal set; } = EmptyReadOnly<CategoryDefinition>.Collection;
		public long Count { get; internal set; }
	}
}
