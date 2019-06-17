using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetTrialLicenseStatusResponse GetTrialLicenseStatus(this IElasticClient client,
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null
		)
			=> client.License.GetTrialStatus(selector);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetTrialLicenseStatusResponse GetTrialLicenseStatus(this IElasticClient client, IGetTrialLicenseStatusRequest request)
			=> client.License.GetTrialStatus(request);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(this IElasticClient client,
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.GetTrialStatusAsync(selector, ct);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(this IElasticClient client,
			IGetTrialLicenseStatusRequest request,
			CancellationToken ct = default
		)
			=> client.License.GetTrialStatusAsync(request, ct);
	}
}
