using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null);

		/// <inheritdoc/>
		IGetLicenseResponse GetLicense(IGetLicenseRequest request);

		/// <inheritdoc/>
		Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null) =>
			this.GetLicense(selector.InvokeOrDefault(new GetLicenseDescriptor()));

		/// <inheritdoc/>
		public IGetLicenseResponse GetLicense(IGetLicenseRequest request) =>
			this.Dispatcher.Dispatch<IGetLicenseRequest, GetLicenseRequestParameters, GetLicenseResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.LicenseGetDispatch<GetLicenseResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null) =>
			this.GetLicenseAsync(selector.InvokeOrDefault(new GetLicenseDescriptor()));

		/// <inheritdoc/>
		public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request) =>
			this.Dispatcher.DispatchAsync<IGetLicenseRequest, GetLicenseRequestParameters, GetLicenseResponse, IGetLicenseResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.LicenseGetDispatchAsync<GetLicenseResponse>(p)
			);
	}
}
