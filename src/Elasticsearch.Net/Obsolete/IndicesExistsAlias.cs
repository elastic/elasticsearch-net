using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	#pragma warning disable 0618
	using IndicesExistsAliasSelector = Func<IndicesExistsAliasRequestParameters, IndicesExistsAliasRequestParameters>;
	#pragma warning restore 0618
	
	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsRequestParameters")]
	public class IndicesExistsAliasRequestParameters : AliasExistsRequestParameters { }

	public static class IndicesGetFieldMappingClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<T> IndicesExistsAlias<T>(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesExistsAliasAsync<T>(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync<T>(index, name, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesExistsAlias(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias(index, name, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesExistsAliasAsync(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync(index, name, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<T> IndicesExistsAlias<T>(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias<T>(index, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesExistsAliasAsync<T>(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync<T>(index, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesExistsAlias(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias(index, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesExistsAliasAsync(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync(index, selector);
		}
	
	}
}
