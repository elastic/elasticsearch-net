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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null) =>
			GetLicense(selector.InvokeOrDefault(new GetLicenseDescriptor()));

		/// <inheritdoc />
		public IGetLicenseResponse GetLicense(IGetLicenseRequest request) =>
			DoRequest<IGetLicenseRequest, GetLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetLicenseResponse> GetLicenseAsync(
			Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken ct = default
		) => GetLicenseAsync(selector.InvokeOrDefault(new GetLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetLicenseRequest, IGetLicenseResponse, GetLicenseResponse>(request, request.RequestParameters, ct);
	}
}
