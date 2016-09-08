using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null);

		/// <inheritdoc/>
		IGetLicenseResponse GetLicense(IGetLicenseRequest request);

		/// <inheritdoc/>
		Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				(p, d) =>this.LowLevelDispatch.XpackLicenseGetDispatch<GetLicenseResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetLicenseAsync(selector.InvokeOrDefault(new GetLicenseDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetLicenseRequest, GetLicenseRequestParameters, GetLicenseResponse, IGetLicenseResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackLicenseGetDispatchAsync<GetLicenseResponse>(p, c)
			);
	}
}
