using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Validates a machine learning job
		/// </summary>
		public static ValidateJobResponse ValidateJob<T>(this IElasticClient client,Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector) where T : class;

		/// <inheritdoc />
		public static ValidateJobResponse ValidateJob(this IElasticClient client,IValidateJobRequest request);

		/// <inheritdoc />
		public static Task<ValidateJobResponse> ValidateJobAsync<T>(this IElasticClient client,Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<ValidateJobResponse> ValidateJobAsync(this IElasticClient client,IValidateJobRequest request, CancellationToken ct = default);
	}

}
