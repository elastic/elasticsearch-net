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
		[Obsolete("Maintained for binary compatibility. Use overload that accepts Names. Will be removed in 7.0")]
		IExistsResponse AliasExists(Func<AliasExistsDescriptor, IAliasExistsRequest> selector);

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
		[Obsolete("Maintained for binary compatibility. Use overload that accepts Names. Will be removed in 7.0")]
		Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, IAliasExistsRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		Task<IExistsResponse> AliasExistsAsync(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		[Obsolete("Maintained for binary compatibility. Use overload that accepts Names. Will be removed in 7.0")]
		public IExistsResponse AliasExists(Func<AliasExistsDescriptor, IAliasExistsRequest> selector) =>
#pragma warning disable 618 // changing method signature would be binary breaking
			AliasExists(selector?.Invoke(new AliasExistsDescriptor()));
#pragma warning restore 618

		/// <inheritdoc />
		public IExistsResponse AliasExists(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null) =>
			AliasExists(selector.InvokeOrDefault(new AliasExistsDescriptor(name)));

		/// <inheritdoc />
		public IExistsResponse AliasExists(IAliasExistsRequest request) =>
			Dispatcher.Dispatch<IAliasExistsRequest, AliasExistsRequestParameters, ExistsResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesExistsAliasDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc />
		[Obsolete("Maintained for binary compatibility. Use overload that accepts Names. Will be removed in 7.0")]
		public Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, IAliasExistsRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
#pragma warning disable 618 // changing method signature would be binary breaking
			AliasExistsAsync(selector?.Invoke(new AliasExistsDescriptor()), cancellationToken);
#pragma warning restore 618

		/// <inheritdoc />
		public Task<IExistsResponse> AliasExistsAsync(Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			AliasExistsAsync(selector.InvokeOrDefault(new AliasExistsDescriptor(name)), cancellationToken);


		/// <inheritdoc />
		public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IAliasExistsRequest, AliasExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesExistsAliasDispatchAsync<ExistsResponse>(p, c)
			);
	}
}
