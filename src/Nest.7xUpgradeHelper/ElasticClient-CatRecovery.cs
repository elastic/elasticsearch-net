using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatRecoveryRecord> CatRecovery(this IElasticClient client,Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatRecoveryRecord> CatRecovery(this IElasticClient client,ICatRecoveryRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(this IElasticClient client,
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(this IElasticClient client,ICatRecoveryRequest request,
			CancellationToken ct = default
		);
	}


}
