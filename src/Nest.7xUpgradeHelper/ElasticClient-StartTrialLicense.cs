using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartTrialLicenseResponse StartTrialLicense(this IElasticClient client,
			Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null
		)
			=> client.License.StartTrial(selector);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartTrialLicenseResponse StartTrialLicense(this IElasticClient client, IStartTrialLicenseRequest request)
			=> client.License.StartTrial(request);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartTrialLicenseResponse> StartTrialLicenseAsync(this IElasticClient client,
			Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.License.StartTrialAsync(selector, ct);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartTrialLicenseResponse> StartTrialLicenseAsync(this IElasticClient client, IStartTrialLicenseRequest request,
			CancellationToken ct = default
		)
			=> client.License.StartTrialAsync(request, ct);
	}
}
