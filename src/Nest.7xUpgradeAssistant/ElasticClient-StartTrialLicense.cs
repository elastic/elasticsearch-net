using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.License.StartTrial(), please update this usage.")]
		public static StartTrialLicenseResponse StartTrialLicense(this IElasticClient client,
			Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null
		)
			=> client.License.StartTrial(selector);

		[Obsolete("Moved to client.License.StartTrial(), please update this usage.")]
		public static StartTrialLicenseResponse StartTrialLicense(this IElasticClient client, IStartTrialLicenseRequest request)
			=> client.License.StartTrial(request);

		[Obsolete("Moved to client.License.StartTrialAsync(), please update this usage.")]
		public static Task<StartTrialLicenseResponse> StartTrialLicenseAsync(this IElasticClient client,
			Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.StartTrialAsync(selector, ct);

		[Obsolete("Moved to client.License.StartTrialAsync(), please update this usage.")]
		public static Task<StartTrialLicenseResponse> StartTrialLicenseAsync(this IElasticClient client, IStartTrialLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.StartTrialAsync(request, ct);
	}
}
