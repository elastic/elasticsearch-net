using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves machine learning job configuration information
		/// </summary>
		public static GetJobsResponse GetJobs(this IElasticClient client,Func<GetJobsDescriptor, IGetJobsRequest> selector = null);

		/// <inheritdoc />
		public static GetJobsResponse GetJobs(this IElasticClient client,IGetJobsRequest request);

		/// <inheritdoc />
		public static Task<GetJobsResponse> GetJobsAsync(this IElasticClient client,Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetJobsResponse> GetJobsAsync(this IElasticClient client,IGetJobsRequest request, CancellationToken ct = default);
	}

}
