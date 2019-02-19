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
		IGetTrialLicenseStatusResponse GetTrialLicenseStatus(Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		IGetTrialLicenseStatusResponse GetTrialLicenseStatus(IGetTrialLicenseStatusRequest request);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(IGetTrialLicenseStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetTrialLicenseStatusResponse GetTrialLicenseStatus(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null
		) =>
			GetTrialLicenseStatus(selector.InvokeOrDefault(new GetTrialLicenseStatusDescriptor()));

		/// <inheritdoc />
		public IGetTrialLicenseStatusResponse GetTrialLicenseStatus(IGetTrialLicenseStatusRequest request) =>
			Dispatcher.Dispatch<IGetTrialLicenseStatusRequest, GetTrialLicenseStatusRequestParameters, GetTrialLicenseStatusResponse>(
				request,
				(p, d) => LowLevelDispatch.LicenseGetTrialStatusDispatch<GetTrialLicenseStatusResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetTrialLicenseStatusAsync(selector.InvokeOrDefault(new GetTrialLicenseStatusDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(IGetTrialLicenseStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetTrialLicenseStatusRequest, GetTrialLicenseStatusRequestParameters, GetTrialLicenseStatusResponse,
					IGetTrialLicenseStatusResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.LicenseGetTrialStatusDispatchAsync<GetTrialLicenseStatusResponse>(p, c)
				);
	}
}
