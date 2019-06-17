using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The start basic API enables you to initiate an indefinite basic license, which gives access to all of
		/// the basic features. If the basic license does not support all of the features that are
		/// available with your current license, however, you are notified in the response. You must then
		/// re-submit the API request with the acknowledge parameter set to true.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartBasicLicenseResponse StartBasicLicense(this IElasticClient client,
			Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null
		)
			=> client.License.StartBasic(selector);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartBasicLicenseResponse StartBasicLicense(this IElasticClient client, IStartBasicLicenseRequest request)
			=> client.License.StartBasic(request);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartBasicLicenseResponse> StartBasicLicenseAsync(this IElasticClient client,
			Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.StartBasicAsync(selector, ct);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartBasicLicenseResponse> StartBasicLicenseAsync(this IElasticClient client, IStartBasicLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.StartBasicAsync(request, ct);
	}
}
