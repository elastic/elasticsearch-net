using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
#pragma warning disable 0618
	using SnapshotGetRepositorySelector = Func<SnapshotGetRepositoryRequestParameters, SnapshotGetRepositoryRequestParameters>;
#pragma warning restore 0618

	[Obsolete("Scheduled to be removed in 2.0, renamed to GetRepositoryRequestParameters ")]
	public class SnapshotGetRepositoryRequestParameters : GetRepositoryRequestParameters { }

	public static class SnapshotGetRepositoryClientExtensions
	{

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static ElasticsearchResponse<T> SnapshotGetRepository<T>(this IElasticsearchClient client, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepository<T>(selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static Task<ElasticsearchResponse<T>> SnapshotGetRepositoryAsync<T>(this IElasticsearchClient client, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepositoryAsync<T>(selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static ElasticsearchResponse<DynamicDictionary> SnapshotGetRepository(this IElasticsearchClient client, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepository(selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> SnapshotGetRepositoryAsync(this IElasticsearchClient client, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepositoryAsync(selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static ElasticsearchResponse<T> SnapshotGetRepository<T>(this IElasticsearchClient client, string repository, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepository<T>(repository, selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static Task<ElasticsearchResponse<T>> SnapshotGetRepositoryAsync<T>(this IElasticsearchClient client, string repository, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepositoryAsync<T>(repository, selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static ElasticsearchResponse<DynamicDictionary> SnapshotGetRepository(this IElasticsearchClient client, string repository, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepository(repository, selector);
		}
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of GetRepositoryRequestParameters ")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> SnapshotGetRepositoryAsync(this IElasticsearchClient client, string repository, SnapshotGetRepositorySelector requestParameters = null)
		{
			var selector = Obsolete.UpCastSelector<SnapshotGetRepositoryRequestParameters, GetRepositoryRequestParameters>(requestParameters);
			return client.SnapshotGetRepositoryAsync(repository, selector);
		}

	}
}
