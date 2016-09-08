using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null);

		/// <inheritdoc/>
		IPostLicenseResponse PostLicense(IPostLicenseRequest request);

		/// <inheritdoc/>
		Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null) =>
			this.PostLicense(selector.InvokeOrDefault(new PostLicenseDescriptor()));

		/// <inheritdoc/>
		public IPostLicenseResponse PostLicense(IPostLicenseRequest request) =>
			this.Dispatcher.Dispatch<IPostLicenseRequest, PostLicenseRequestParameters, PostLicenseResponse>(
				request,
				this.LowLevelDispatch.XpackLicensePostDispatch<PostLicenseResponse>
			);

		/// <inheritdoc/>
		public Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PostLicenseAsync(selector.InvokeOrDefault(new PostLicenseDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPostLicenseRequest, PostLicenseRequestParameters, PostLicenseResponse, IPostLicenseResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackLicensePostDispatchAsync<PostLicenseResponse>
			);
	}
}
