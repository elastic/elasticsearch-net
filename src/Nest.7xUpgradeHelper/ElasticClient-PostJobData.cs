using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Sends data to a machine learning job for analysis.
		/// </summary>
		public static PostJobDataResponse PostJobData(this IElasticClient client,Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector);

		/// <inheritdoc />
		public static PostJobDataResponse PostJobData(this IElasticClient client,IPostJobDataRequest request);

		/// <inheritdoc />
		public static Task<PostJobDataResponse> PostJobDataAsync(this IElasticClient client,Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PostJobDataResponse> PostJobDataAsync(this IElasticClient client,IPostJobDataRequest request, CancellationToken ct = default);
	}

}
