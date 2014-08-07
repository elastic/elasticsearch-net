using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	#pragma warning disable 0618
	using IndicesExistsTypeSelector = Func<IndicesExistsTypeRequestParameters, IndicesExistsTypeRequestParameters>;
	#pragma warning restore 0618
	
	[Obsolete("Scheduled to be removed in 2.0, renamed to TypeExistsRequestParameters")]
	public class IndicesExistsTypeRequestParameters : TypeExistsRequestParameters { }

	public static class IndicesExistsTypeClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of TypeExistsRequestParameters")]
		public static ElasticsearchResponse<T> IndicesExistsType<T>(
			this IElasticsearchClient client, string index, string type, IndicesExistsTypeSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTypeRequestParameters, TypeExistsRequestParameters>(requestParameters);
			return client.IndicesExistsType<T>(index, type, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of TypeExistsRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesExistsTypeAsync<T>(
			this IElasticsearchClient client, string index, string type, IndicesExistsTypeSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTypeRequestParameters, TypeExistsRequestParameters>(requestParameters);
			return client.IndicesExistsTypeAsync<T>(index, type, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of TypeExistsRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesExistsType(
			this IElasticsearchClient client, string index, string type, IndicesExistsTypeSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTypeRequestParameters, TypeExistsRequestParameters>(requestParameters);
			return client.IndicesExistsType(index, type, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of TypeExistsRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesExistsTypeAsync(
			this IElasticsearchClient client, string index, string type, IndicesExistsTypeSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTypeRequestParameters, TypeExistsRequestParameters>(requestParameters);
			return client.IndicesExistsTypeAsync(index, type, selector);
		}
	}
}
