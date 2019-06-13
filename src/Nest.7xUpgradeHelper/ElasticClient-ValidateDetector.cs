using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Validates a detector for a machine learning job
		/// </summary>
		public static ValidateDetectorResponse ValidateDetector<T>(this IElasticClient client,Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector) where T : class;

		/// <inheritdoc />
		public static ValidateDetectorResponse ValidateDetector(this IElasticClient client,IValidateDetectorRequest request);

		/// <inheritdoc />
		public static Task<ValidateDetectorResponse> ValidateDetectorAsync<T>(this IElasticClient client,Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<ValidateDetectorResponse> ValidateDetectorAsync(this IElasticClient client,IValidateDetectorRequest request,
			CancellationToken ct = default
		);
	}

}
