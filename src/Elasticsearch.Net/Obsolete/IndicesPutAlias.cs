using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	#pragma warning disable 0618
	using System.Threading.Tasks;
	using IndicesPutAliasSelector = Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters>;
	#pragma warning restore 0618

	[Obsolete("Scheduled to be removed in 2.0, renamed to PutAliasRequestParameters")]
	public class IndicesPutAliasRequestParameters : PutAliasRequestParameters { }

	public static class IndicesPutAliasClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<T> IndicesPutAlias<T>(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAlias<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesPutAliasAsync<T>(this ElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasAsync<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesPutAlias(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAlias(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesPutAliasAsync(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasAsync(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<T> IndicesPutAliasForAll<T>(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAll<T>(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesPutAliasForAllAsync<T>(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAllAsync<T>(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesPutAliasForAll(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAll(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesPutAliasForAllAsync(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAllAsync(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<T> IndicesPutAliasPost<T>(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasPost<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesPutAliasPostAsync<T>(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasPostAsync<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesPutAliasPost(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasPost(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesPutAliasPostAsync(this IElasticsearchClient client, string index, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasPostAsync(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<T> IndicesPutAliasPostForAll<T>(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAll<T>(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesPutAliasPostForAllAsync<T>(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAllAsync<T>(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesPutAliasPostForAll(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAll(name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of PutAliasRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesPutAliasPostForAllAsync(this IElasticsearchClient client, string name, object body, Func<IndicesPutAliasRequestParameters, IndicesPutAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesPutAliasRequestParameters, PutAliasRequestParameters>(requestParameters);
			return client.IndicesPutAliasForAllAsync(name, selector);
		}
	}
}
