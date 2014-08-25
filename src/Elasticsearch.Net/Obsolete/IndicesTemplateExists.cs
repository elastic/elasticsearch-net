using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	#pragma warning disable 0618
	using IndicesExistsTemplateSelector = Func<IndicesExistsTemplateRequestParameters , IndicesExistsTemplateRequestParameters >;
	#pragma warning restore 0618
	
	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsRequestParameters")]
	public class IndicesExistsTemplateRequestParameters : TemplateExistsRequestParameters { }

	public static class IndicesExistsTemplateClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of IndicesExistsRequestParameters")]
		public static ElasticsearchResponse<T> IndicesExistsTemplateForAll<T>(this IElasticsearchClient client, string name, IndicesExistsTemplateSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTemplateRequestParameters, TemplateExistsRequestParameters>(requestParameters);
			return client.IndicesExistsTemplateForAll<T>(name, requestParameters);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of IndicesExistsRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesExistsTemplateForAllAsync<T>(this IElasticsearchClient client, string name, IndicesExistsTemplateSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTemplateRequestParameters, TemplateExistsRequestParameters>(requestParameters);
			return client.IndicesExistsTemplateForAllAsync<T>(name, requestParameters);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of IndicesExistsRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesExistsTemplateForAll(this IElasticsearchClient client, string name, IndicesExistsTemplateSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTemplateRequestParameters, TemplateExistsRequestParameters>(requestParameters);
			return client.IndicesExistsTemplateForAll(name, requestParameters);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of IndicesExistsRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesExistsTemplateForAllAsync(this IElasticsearchClient client, string name, IndicesExistsTemplateSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsTemplateRequestParameters, TemplateExistsRequestParameters>(requestParameters);
			return client.IndicesExistsTemplateForAllAsync(name, requestParameters);
		}
		
	
	}
}
