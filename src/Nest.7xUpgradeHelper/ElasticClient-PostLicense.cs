using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PostLicenseResponse PostLicense(this IElasticClient client, Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null)
			=> client.License.Post(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PostLicenseResponse PostLicense(this IElasticClient client, IPostLicenseRequest request)
			=> client.License.Post(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PostLicenseResponse> PostLicenseAsync(this IElasticClient client,
			Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.PostAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PostLicenseResponse> PostLicenseAsync(this IElasticClient client, IPostLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.PostAsync(request, ct);
	}
}
