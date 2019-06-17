using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.License.GetBasicStatus(), please update this usage.")]
		public static GetBasicLicenseStatusResponse GetBasicLicenseStatus(this IElasticClient client,
			Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null
		)
			=> client.License.GetBasicStatus(selector);

		[Obsolete("Moved to client.License.GetBasicStatus(), please update this usage.")]
		public static GetBasicLicenseStatusResponse GetBasicLicenseStatus(this IElasticClient client, IGetBasicLicenseStatusRequest request)
			=> client.License.GetBasicStatus(request);

		[Obsolete("Moved to client.License.GetBasicStatusAsync(), please update this usage.")]
		public static Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(this IElasticClient client,
			Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.GetBasicStatusAsync(selector, ct);

		[Obsolete("Moved to client.License.GetBasicStatusAsync(), please update this usage.")]
		public static Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(this IElasticClient client,
			IGetBasicLicenseStatusRequest request, CancellationToken ct = default
		)
			=> client.License.GetBasicStatusAsync(request, ct);
	}
}
