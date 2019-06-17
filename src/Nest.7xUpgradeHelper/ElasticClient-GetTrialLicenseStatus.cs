using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.License.GetTrialStatus(), please update this usage.")]
		public static GetTrialLicenseStatusResponse GetTrialLicenseStatus(this IElasticClient client,
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null
		)
			=> client.License.GetTrialStatus(selector);

		[Obsolete("Moved to client.License.GetTrialStatus(), please update this usage.")]
		public static GetTrialLicenseStatusResponse GetTrialLicenseStatus(this IElasticClient client, IGetTrialLicenseStatusRequest request)
			=> client.License.GetTrialStatus(request);

		[Obsolete("Moved to client.License.GetTrialStatusAsync(), please update this usage.")]
		public static Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(this IElasticClient client,
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.GetTrialStatusAsync(selector, ct);

		[Obsolete("Moved to client.License.GetTrialStatusAsync(), please update this usage.")]
		public static Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(this IElasticClient client,
			IGetTrialLicenseStatusRequest request,
			CancellationToken ct = default
		)
			=> client.License.GetTrialStatusAsync(request, ct);
	}
}
