using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TypeExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	// TODO should we keep this around in 7.x
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ExistsResponse TypeExists(Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null);

		/// <inheritdoc />
		ExistsResponse TypeExists(ITypeExistsRequest request);

		/// <inheritdoc />
		Task<ExistsResponse> TypeExistsAsync(Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ExistsResponse> TypeExistsAsync(ITypeExistsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExistsResponse TypeExists(Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null) =>
			TypeExists(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, type)));

		/// <inheritdoc />
		public ExistsResponse TypeExists(ITypeExistsRequest request) =>
			DoRequest<ITypeExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExistsResponse> TypeExistsAsync(
			Indices indices,
			string type,
			Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken ct = default
		) => TypeExistsAsync(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, type)), ct);

		/// <inheritdoc />
		public Task<ExistsResponse> TypeExistsAsync(ITypeExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ITypeExistsRequest, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
