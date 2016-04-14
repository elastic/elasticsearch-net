using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null);

		/// <inheritdoc/>
		IPostLicenseResponse PostLicense(IPostLicenseRequest request);

		/// <inheritdoc/>
		Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null);

		/// <inheritdoc/>
		Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request);
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
				this.LowLevelDispatch.LicensePostDispatch<PostLicenseResponse>
			);

		/// <inheritdoc/>
		public Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null) =>
			this.PostLicenseAsync(selector.InvokeOrDefault(new PostLicenseDescriptor()));

		/// <inheritdoc/>
		public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request) =>
			this.Dispatcher.DispatchAsync<IPostLicenseRequest, PostLicenseRequestParameters, PostLicenseResponse, IPostLicenseResponse>(
				request,
				this.LowLevelDispatch.LicensePostDispatchAsync<PostLicenseResponse>
			);
	}
}
