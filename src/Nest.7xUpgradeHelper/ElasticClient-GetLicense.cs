using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetLicenseResponse GetLicense(this IElasticClient client, Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null)
			=> client.License.Get(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetLicenseResponse GetLicense(this IElasticClient client, IGetLicenseRequest request)
			=> client.License.Get(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetLicenseResponse> GetLicenseAsync(this IElasticClient client,
			Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.GetAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetLicenseResponse> GetLicenseAsync(this IElasticClient client, IGetLicenseRequest request, CancellationToken ct = default)
			=> client.License.GetAsync(request, ct);
	}
}
