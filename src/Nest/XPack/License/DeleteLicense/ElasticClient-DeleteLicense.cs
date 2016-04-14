using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null);

		/// <inheritdoc/>
		IDeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request);

		/// <inheritdoc/>
		Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null) =>
			this.DeleteLicense(selector.InvokeOrDefault(new DeleteLicenseDescriptor()));

		/// <inheritdoc/>
		public IDeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request) =>
			this.Dispatcher.Dispatch<IDeleteLicenseRequest, DeleteLicenseRequestParameters, DeleteLicenseResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.LicenseDeleteDispatch<DeleteLicenseResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null) =>
			this.DeleteLicenseAsync(selector.InvokeOrDefault(new DeleteLicenseDescriptor()));

		/// <inheritdoc/>
		public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request) =>
			this.Dispatcher.DispatchAsync<IDeleteLicenseRequest, DeleteLicenseRequestParameters, DeleteLicenseResponse, IDeleteLicenseResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.LicenseDeleteDispatchAsync<DeleteLicenseResponse>(p)
			);
	}
}
