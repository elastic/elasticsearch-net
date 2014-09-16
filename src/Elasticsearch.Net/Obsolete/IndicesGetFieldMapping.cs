using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
#pragma warning disable 0618
	using IndicesGetFieldMappingSelector = Func<IndicesGetFieldMappingRequestParameters , IndicesGetFieldMappingRequestParameters >;
#pragma warning restore 0618

	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsRequestParameters")]
	public class IndicesGetFieldMappingRequestParameters : GetFieldMappingRequestParameters { }

	public static class IndicesExistsAliasClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<T> IndicesGetFieldMappingForAll<T>(this IElasticsearchClient client, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAll<T>(field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesGetFieldMappingForAllAsync<T>(this IElasticsearchClient client, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAllAsync<T>(field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesGetFieldMappingForAll(this IElasticsearchClient client, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAll(field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesGetFieldMappingForAllAsync(this IElasticsearchClient client, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAllAsync(field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<T> IndicesGetFieldMapping<T>(this IElasticsearchClient client, string index, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMapping<T>(index, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesGetFieldMappingAsync<T>(this IElasticsearchClient client, string index, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingAsync<T>(index, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesGetFieldMapping(this IElasticsearchClient client, string index, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMapping(index, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesGetFieldMappingAsync(this IElasticsearchClient client, string index, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingAsync(index, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<T> IndicesGetFieldMappingForAll<T>(this IElasticsearchClient client, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAll<T>(type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesGetFieldMappingForAllAsync<T>(this IElasticsearchClient client, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAllAsync<T>(type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesGetFieldMappingForAll(this IElasticsearchClient client, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAll(type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesGetFieldMappingForAllAsync(this IElasticsearchClient client, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingForAllAsync(type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<T> IndicesGetFieldMapping<T>(this IElasticsearchClient client, string index, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMapping<T>(index, type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesGetFieldMappingAsync<T>(this IElasticsearchClient client, string index, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingAsync<T>(index, type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesGetFieldMapping(this IElasticsearchClient client, string index, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMapping(index, type, field, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetFieldMappingRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesGetFieldMappingAsync(this IElasticsearchClient client, string index, string type, string field, IndicesGetFieldMappingSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<IndicesGetFieldMappingRequestParameters, GetFieldMappingRequestParameters>(requestParameters);
			return client.IndicesGetFieldMappingAsync(index, type, field, selector);
		}



	}
}
