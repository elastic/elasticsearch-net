using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector);

		/// <inheritdoc />
		ChangePasswordResponse ChangePassword(IChangePasswordRequest request);

		/// <inheritdoc />
		Task<ChangePasswordResponse> ChangePasswordAsync(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector) =>
			ChangePassword(selector.InvokeOrDefault(new ChangePasswordDescriptor()));

		/// <inheritdoc />
		public ChangePasswordResponse ChangePassword(IChangePasswordRequest request) =>
			DoRequest<IChangePasswordRequest, ChangePasswordResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ChangePasswordResponse> ChangePasswordAsync(
			Func<ChangePasswordDescriptor, IChangePasswordRequest> selector,
			CancellationToken ct = default
		) => ChangePasswordAsync(selector.InvokeOrDefault(new ChangePasswordDescriptor()), ct);

		/// <inheritdoc />
		public Task<ChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IChangePasswordRequest, ChangePasswordResponse, ChangePasswordResponse>(request, request.RequestParameters, ct);
	}
}
