using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null);

		/// <inheritdoc />
		IGetLicenseResponse GetLicense(IGetLicenseRequest request);

		/// <inheritdoc />
		Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null) =>
			GetLicense(selector.InvokeOrDefault(new GetLicenseDescriptor()));

		/// <inheritdoc />
		public IGetLicenseResponse GetLicense(IGetLicenseRequest request) =>
			Dispatcher.Dispatch<IGetLicenseRequest, GetLicenseRequestParameters, GetLicenseResponse>(
				request,
				(p, d) => LowLevelDispatch.LicenseGetDispatch<GetLicenseResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetLicenseAsync(selector.InvokeOrDefault(new GetLicenseDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetLicenseRequest, GetLicenseRequestParameters, GetLicenseResponse, IGetLicenseResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.LicenseGetDispatchAsync<GetLicenseResponse>(p, c)
			);
	}
}
