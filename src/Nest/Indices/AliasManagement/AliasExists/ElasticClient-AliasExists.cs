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
		ExistsResponse AliasExists(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		ExistsResponse AliasExists(IAliasExistsRequest request);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		Task<ExistsResponse> AliasExistsAsync(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		Task<ExistsResponse> AliasExistsAsync(IAliasExistsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExistsResponse AliasExists(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null) =>
			AliasExists(selector.InvokeOrDefault(new AliasExistsDescriptor(name)));

		/// <inheritdoc />
		public ExistsResponse AliasExists(IAliasExistsRequest request) =>
			DoRequest<IAliasExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExistsResponse> AliasExistsAsync(
			Names name,
			Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => AliasExistsAsync(selector.InvokeOrDefault(new AliasExistsDescriptor(name)), cancellationToken);

		/// <inheritdoc />
		public Task<ExistsResponse> AliasExistsAsync(IAliasExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAliasExistsRequest, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
