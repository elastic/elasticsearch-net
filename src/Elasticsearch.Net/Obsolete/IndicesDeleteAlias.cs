using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	#pragma warning disable 0618
	using System.Threading.Tasks;
	using IndicesDeleteAliasSelector = Func<IndicesDeleteAliasRequestParameters, IndicesDeleteAliasRequestParameters>;
	#pragma warning restore 0618

	[Obsolete("Scheduled to be removed in 2.0, renamed to DeleteAliasRequestParameters")]
	public class IndicesDeleteAliasRequestParameters : DeleteAliasRequestParameters { }
	
	public static class IndicesDeleteAliasClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of DeleteAliasRequestParameters")]
		public static ElasticsearchResponse<T> IndicesDeleteAlias<T>(this IElasticsearchClient client, string index, string name, Func<IndicesDeleteAliasRequestParameters, IndicesDeleteAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesDeleteAliasRequestParameters, DeleteAliasRequestParameters>(requestParameters);
			return client.IndicesDeleteAlias<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of DeleteAliasRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesDeleteAliasAsync<T>(this IElasticsearchClient client, string index, string name, Func<IndicesDeleteAliasRequestParameters, IndicesDeleteAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesDeleteAliasRequestParameters, DeleteAliasRequestParameters>(requestParameters);
			return client.IndicesDeleteAliasAsync<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of DeleteAliasRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesDeleteAlias(this IElasticsearchClient client, string index, string name, Func<IndicesDeleteAliasRequestParameters, IndicesDeleteAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesDeleteAliasRequestParameters, DeleteAliasRequestParameters>(requestParameters);
			return client.IndicesDeleteAlias(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of DeleteAliasRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesDeleteAliasAsync(this IElasticsearchClient client, string index, string name, Func<IndicesDeleteAliasRequestParameters, IndicesDeleteAliasRequestParameters> requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesDeleteAliasRequestParameters, DeleteAliasRequestParameters>(requestParameters);
			return client.IndicesDeleteAliasAsync(index, name, selector);
		}
	}
}
