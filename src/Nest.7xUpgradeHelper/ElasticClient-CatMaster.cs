using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatMasterRecord> CatMaster(this IElasticClient client,Func<CatMasterDescriptor, ICatMasterRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatMasterRecord> CatMaster(this IElasticClient client,ICatMasterRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatMasterRecord>> CatMasterAsync(this IElasticClient client,
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatMasterRecord>> CatMasterAsync(this IElasticClient client,ICatMasterRequest request, CancellationToken ct = default
		);
	}

}
