using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		public static ReindexOnServerResponse ReindexOnServer(this IElasticClient client,Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		public static ReindexOnServerResponse ReindexOnServer(this IElasticClient client,IReindexOnServerRequest request);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		public static Task<ReindexOnServerResponse> ReindexOnServerAsync(this IElasticClient client,Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector,
			CancellationToken ct = default
		);

		/// <summary>
		/// Reindexes documents from one or more indices to another. the reindex operation takes place
		/// within Elasticsearch
		/// </summary>
		public static Task<ReindexOnServerResponse> ReindexOnServerAsync(this IElasticClient client,IReindexOnServerRequest request,
			CancellationToken ct = default
		);
	}

}
