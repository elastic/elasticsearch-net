using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.License.StartBasic(), please update this usage.")]
		public static StartBasicLicenseResponse StartBasicLicense(this IElasticClient client,
			Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null
		)
			=> client.License.StartBasic(selector);

		[Obsolete("Moved to client.License.StartBasic(), please update this usage.")]
		public static StartBasicLicenseResponse StartBasicLicense(this IElasticClient client, IStartBasicLicenseRequest request)
			=> client.License.StartBasic(request);

		[Obsolete("Moved to client.License.StartBasicAsync(), please update this usage.")]
		public static Task<StartBasicLicenseResponse> StartBasicLicenseAsync(this IElasticClient client,
			Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.StartBasicAsync(selector, ct);

		[Obsolete("Moved to client.License.StartBasicAsync(), please update this usage.")]
		public static Task<StartBasicLicenseResponse> StartBasicLicenseAsync(this IElasticClient client, IStartBasicLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.StartBasicAsync(request, ct);
	}
}
