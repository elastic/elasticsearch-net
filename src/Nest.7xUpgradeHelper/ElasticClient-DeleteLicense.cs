using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteLicenseResponse DeleteLicense(this IElasticClient client,
			Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null
		)
			=> client.License.Delete(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteLicenseResponse DeleteLicense(this IElasticClient client, IDeleteLicenseRequest request)
			=> client.License.Delete(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteLicenseResponse> DeleteLicenseAsync(this IElasticClient client,
			Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.DeleteAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteLicenseResponse> DeleteLicenseAsync(this IElasticClient client, IDeleteLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.DeleteAsync(request, ct);
	}
}
