using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class CategoryId : IUrlParameter
	{
		private readonly long _categoryId;

		public CategoryId(long categoryId) => _categoryId = categoryId;

		public string GetString(IConnectionConfigurationValues settings) => _categoryId.ToString(CultureInfo.InvariantCulture);

		public static implicit operator CategoryId(long categoryId) => new CategoryId(categoryId);
	}
}
