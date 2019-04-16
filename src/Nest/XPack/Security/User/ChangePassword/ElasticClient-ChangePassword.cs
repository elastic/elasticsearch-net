using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector);

		/// <inheritdoc />
		IChangePasswordResponse ChangePassword(IChangePasswordRequest request);

		/// <inheritdoc />
		Task<IChangePasswordResponse> ChangePasswordAsync(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector) =>
			ChangePassword(selector.InvokeOrDefault(new ChangePasswordDescriptor()));

		/// <inheritdoc />
		public IChangePasswordResponse ChangePassword(IChangePasswordRequest request) =>
			DoRequest<IChangePasswordRequest, ChangePasswordResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IChangePasswordResponse> ChangePasswordAsync(
			Func<ChangePasswordDescriptor, IChangePasswordRequest> selector,
			CancellationToken ct = default
		) => ChangePasswordAsync(selector.InvokeOrDefault(new ChangePasswordDescriptor()), ct);

		/// <inheritdoc />
		public Task<IChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IChangePasswordRequest, IChangePasswordResponse, ChangePasswordResponse>(request, request.RequestParameters, ct);
	}
}
