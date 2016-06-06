using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IChangePasswordResponse ChangePassword(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector);

		/// <inheritdoc/>
		IChangePasswordResponse ChangePassword(IChangePasswordRequest request);

		/// <inheritdoc/>
		Task<IChangePasswordResponse> ChangePasswordAsync(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector);

		/// <inheritdoc/>
		Task<IChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request);
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
		public Task<IChangePasswordResponse> ChangePasswordAsync(Func<ChangePasswordDescriptor, IChangePasswordRequest> selector) =>
			this.ChangePasswordAsync(selector.InvokeOrDefault(new ChangePasswordDescriptor()));

		/// <inheritdoc/>
		public Task<IChangePasswordResponse> ChangePasswordAsync(IChangePasswordRequest request) =>
			this.Dispatcher.DispatchAsync<IChangePasswordRequest, ChangePasswordRequestParameters, ChangePasswordResponse, IChangePasswordResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackSecurityChangePasswordDispatchAsync<ChangePasswordResponse>(p, d)
			);
	}
}
