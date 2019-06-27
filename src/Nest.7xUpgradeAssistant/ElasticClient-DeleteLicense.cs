using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.License.Delete(), please update this usage.")]
		public static DeleteLicenseResponse DeleteLicense(this IElasticClient client,
			Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null
		)
			=> client.License.Delete(selector);

		[Obsolete("Moved to client.License.Delete(), please update this usage.")]
		public static DeleteLicenseResponse DeleteLicense(this IElasticClient client, IDeleteLicenseRequest request)
			=> client.License.Delete(request);

		[Obsolete("Moved to client.License.DeleteAsync(), please update this usage.")]
		public static Task<DeleteLicenseResponse> DeleteLicenseAsync(this IElasticClient client,
			Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.DeleteAsync(selector, ct);

		[Obsolete("Moved to client.License.DeleteAsync(), please update this usage.")]
		public static Task<DeleteLicenseResponse> DeleteLicenseAsync(this IElasticClient client, IDeleteLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.DeleteAsync(request, ct);
	}
}
