using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Opens a machine learning job.
		/// A job must be opened in order for it to be ready to receive and analyze data.
		/// A job can be opened and closed multiple times throughout its lifecycle.
		/// </summary>
		public static OpenJobResponse OpenJob(this IElasticClient client,Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null);

		/// <inheritdoc />
		public static OpenJobResponse OpenJob(this IElasticClient client,IOpenJobRequest request);

		/// <inheritdoc />
		public static Task<OpenJobResponse> OpenJobAsync(this IElasticClient client,Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<OpenJobResponse> OpenJobAsync(this IElasticClient client,IOpenJobRequest request, CancellationToken ct = default);
	}

}
