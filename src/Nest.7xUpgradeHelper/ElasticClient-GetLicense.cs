using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.License.Get(), please update this usage.")]
		public static GetLicenseResponse GetLicense(this IElasticClient client, Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null)
			=> client.License.Get(selector);

		[Obsolete("Moved to client.License.Get(), please update this usage.")]
		public static GetLicenseResponse GetLicense(this IElasticClient client, IGetLicenseRequest request)
			=> client.License.Get(request);

		[Obsolete("Moved to client.License.GetAsync(), please update this usage.")]
		public static Task<GetLicenseResponse> GetLicenseAsync(this IElasticClient client,
			Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.GetAsync(selector, ct);

		[Obsolete("Moved to client.License.GetAsync(), please update this usage.")]
		public static Task<GetLicenseResponse> GetLicenseAsync(this IElasticClient client, IGetLicenseRequest request, CancellationToken ct = default)
			=> client.License.GetAsync(request, ct);
	}
}
