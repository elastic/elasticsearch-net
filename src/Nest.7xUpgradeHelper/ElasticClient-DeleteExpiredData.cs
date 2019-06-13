using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes expired machine learning data.
		/// </summary>
		public static DeleteExpiredDataResponse DeleteExpiredData(this IElasticClient client,Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null);

		/// <inheritdoc />
		public static DeleteExpiredDataResponse DeleteExpiredData(this IElasticClient client,IDeleteExpiredDataRequest request);

		/// <inheritdoc />
		public static Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(this IElasticClient client,Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(this IElasticClient client,IDeleteExpiredDataRequest request,
			CancellationToken ct = default
		);
	}

}
