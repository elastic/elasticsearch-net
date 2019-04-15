using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null);

		/// <inheritdoc />
		GetLicenseResponse GetLicense(IGetLicenseRequest request);

		/// <inheritdoc />
		Task<GetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetLicenseResponse GetLicense(Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null) =>
			GetLicense(selector.InvokeOrDefault(new GetLicenseDescriptor()));

		/// <inheritdoc />
		public GetLicenseResponse GetLicense(IGetLicenseRequest request) =>
			DoRequest<IGetLicenseRequest, GetLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetLicenseResponse> GetLicenseAsync(
			Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken ct = default
		) => GetLicenseAsync(selector.InvokeOrDefault(new GetLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetLicenseRequest, GetLicenseResponse>(request, request.RequestParameters, ct);
	}
}
