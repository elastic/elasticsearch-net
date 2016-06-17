using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector);

		/// <inheritdoc/>
		IChangePasswordResponse ChangePassword(IChangePasswordRequest request);

		/// <inheritdoc/>
		Task<IChangePasswordResponse> ChangePasswordAsync(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector) =>
			this.ChangePassword(selector.InvokeOrDefault(new ChangePasswordDescriptor()));

		/// <inheritdoc/>
		public IChangePasswordResponse ChangePassword(IChangePasswordRequest request) =>
			this.Dispatcher.Dispatch<IChangePasswordRequest, ChangePasswordRequestParameters, ChangePasswordResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackSecurityChangePasswordDispatch<ChangePasswordResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IChangePasswordResponse> ChangePasswordAsync(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ChangePasswordAsync(selector.InvokeOrDefault(new ChangePasswordDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IChangePasswordRequest, ChangePasswordRequestParameters, ChangePasswordResponse, IChangePasswordResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityChangePasswordDispatchAsync<ChangePasswordResponse>(p, d, c)
			);
	}
}
