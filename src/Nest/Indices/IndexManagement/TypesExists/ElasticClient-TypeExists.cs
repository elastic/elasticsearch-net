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
		IExistsResponse TypeExists(Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null);

		/// <inheritdoc />
		IExistsResponse TypeExists(ITypeExistsRequest request);

		/// <inheritdoc />
		Task<IExistsResponse> TypeExistsAsync(Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse TypeExists(Indices indices, string type, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null) =>
			TypeExists(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, type)));

		/// <inheritdoc />
		public IExistsResponse TypeExists(ITypeExistsRequest request) =>
			DoRequest<ITypeExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExistsResponse> TypeExistsAsync(
			Indices indices,
			string type,
			Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken ct = default
		) => TypeExistsAsync(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, type)), ct);

		/// <inheritdoc />
		public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ITypeExistsRequest, IExistsResponse, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
