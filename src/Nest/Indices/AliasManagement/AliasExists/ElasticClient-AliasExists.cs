using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using AliasExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		IExistsResponse AliasExists(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		IExistsResponse AliasExists(IAliasExistsRequest request);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		Task<IExistsResponse> AliasExistsAsync(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse AliasExists(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null) =>
			AliasExists(selector.InvokeOrDefault(new AliasExistsDescriptor(name)));

		/// <inheritdoc />
		public IExistsResponse AliasExists(IAliasExistsRequest request) =>
			DoRequest<IAliasExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IExistsResponse> AliasExistsAsync(
			Names name,
			Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => AliasExistsAsync(selector.InvokeOrDefault(new AliasExistsDescriptor(name)), cancellationToken);

		/// <inheritdoc />
		public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAliasExistsRequest, IExistsResponse, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
