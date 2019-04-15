using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		GetTrialLicenseStatusResponse GetTrialLicenseStatus(Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		GetTrialLicenseStatusResponse GetTrialLicenseStatus(IGetTrialLicenseStatusRequest request);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(IGetTrialLicenseStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetTrialLicenseStatusResponse GetTrialLicenseStatus(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null
		) =>
			GetTrialLicenseStatus(selector.InvokeOrDefault(new GetTrialLicenseStatusDescriptor()));

		/// <inheritdoc />
		public GetTrialLicenseStatusResponse GetTrialLicenseStatus(IGetTrialLicenseStatusRequest request) =>
			DoRequest<IGetTrialLicenseStatusRequest, GetTrialLicenseStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetTrialLicenseStatusAsync(selector.InvokeOrDefault(new GetTrialLicenseStatusDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(IGetTrialLicenseStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetTrialLicenseStatusRequest, GetTrialLicenseStatusResponse, GetTrialLicenseStatusResponse>(request, request.RequestParameters, ct);
	}
}
