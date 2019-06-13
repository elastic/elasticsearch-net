using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatFielddataRecord> CatFielddata(this IElasticClient client,Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatFielddataRecord> CatFielddata(this IElasticClient client,ICatFielddataRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(this IElasticClient client,
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(this IElasticClient client,ICatFielddataRequest request,
			CancellationToken ct = default
		);
	}

}
