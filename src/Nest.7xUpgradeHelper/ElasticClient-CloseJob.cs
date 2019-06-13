using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Closes a machine learning job.
		/// A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
		/// </summary>
		public static CloseJobResponse CloseJob(this IElasticClient client,Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null);

		/// <inheritdoc />
		public static CloseJobResponse CloseJob(this IElasticClient client,ICloseJobRequest request);

		/// <inheritdoc />
		public static Task<CloseJobResponse> CloseJobAsync(this IElasticClient client,Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CloseJobResponse> CloseJobAsync(this IElasticClient client,ICloseJobRequest request, CancellationToken ct = default);
	}

}
