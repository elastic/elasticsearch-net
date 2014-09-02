using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
#pragma warning disable 0618
	using IndicesRecoveryStatusSelector = Func<IndicesRecoveryRequestParameters, IndicesRecoveryRequestParameters>;
#pragma warning restore 0618

	[Obsolete("Scheduled to be removed in 2.0, renamed to TypeExistsRequestParameters")]
	public class IndicesRecoveryRequestParameters : RecoveryStatusRequestParameters { }

	public static class IndicesRecoveryStatusClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static ElasticsearchResponse<T> IndicesRecoveryForAll<T>(this IElasticsearchClient client, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecoveryForAll<T>(selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesRecoveryForAllAsync<T>(this IElasticsearchClient client, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecoveryForAllAsync<T>(selector);
			
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesRecoveryForAll(this IElasticsearchClient client, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecoveryForAll(selector);
			
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesRecoveryForAllAsync(this IElasticsearchClient client, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecoveryForAllAsync(selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static ElasticsearchResponse<T> IndicesRecovery<T>(this IElasticsearchClient client, string index, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecovery<T>(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesRecoveryAsync<T>(this IElasticsearchClient client, string index, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecoveryAsync<T>(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesRecovery(this IElasticsearchClient client, string index, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecovery(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of RecoveryStatusRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesRecoveryAsync(this IElasticsearchClient client, string index, IndicesRecoveryStatusSelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<IndicesRecoveryRequestParameters, RecoveryStatusRequestParameters>(requestParameters);
			return client.IndicesRecoveryAsync(index, selector);
		}
	}
}
