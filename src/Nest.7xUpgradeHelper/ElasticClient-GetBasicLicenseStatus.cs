using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>This API enables you to check the status of your basic license</summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetBasicLicenseStatusResponse GetBasicLicenseStatus(this IElasticClient client,
			Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null
		)
			=> client.License.GetBasicStatus(selector);

		/// <inheritdoc see
		///     cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetBasicLicenseStatusResponse GetBasicLicenseStatus(this IElasticClient client, IGetBasicLicenseStatusRequest request)
			=> client.License.GetBasicStatus(request);

		/// <inheritdoc see
		///     cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(this IElasticClient client,
			Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.GetBasicStatusAsync(selector, ct);

		/// <inheritdoc see
		///     cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(this IElasticClient client,
			IGetBasicLicenseStatusRequest request, CancellationToken ct = default
		)
			=> client.License.GetBasicStatusAsync(request, ct);
	}
}
