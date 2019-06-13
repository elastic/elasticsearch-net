using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TypeExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	// TODO should we keep this around in 7.x
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static ExistsResponse TypeExists(this IElasticClient client,Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null);

		/// <inheritdoc />
		public static ExistsResponse TypeExists(this IElasticClient client,ITypeExistsRequest request);

		/// <inheritdoc />
		public static Task<ExistsResponse> TypeExistsAsync(this IElasticClient client,Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ExistsResponse> TypeExistsAsync(this IElasticClient client,ITypeExistsRequest request, CancellationToken ct = default);
	}

}
