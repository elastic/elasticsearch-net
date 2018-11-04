using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TypeExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IExistsResponse TypeExists(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null);

		/// <inheritdoc />
		IExistsResponse TypeExists(ITypeExistsRequest request);

		/// <inheritdoc />
		Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse TypeExists(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null) =>
			TypeExists(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, types)));

		/// <inheritdoc />
		public IExistsResponse TypeExists(ITypeExistsRequest request) =>
			Dispatcher.Dispatch<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse>(
				request,
				new TypeExistConverter(DeserializeExistsResponse),
				(p, d) => LowLevelDispatch.IndicesExistsTypeDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			TypeExistsAsync(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, types)), cancellationToken);

		/// <inheritdoc />
		public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				cancellationToken,
				new TypeExistConverter(DeserializeExistsResponse),
				(p, d, c) => LowLevelDispatch.IndicesExistsTypeDispatchAsync<ExistsResponse>(p, c)
			);
	}
}
