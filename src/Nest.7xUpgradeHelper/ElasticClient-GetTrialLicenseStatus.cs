using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		public static GetTrialLicenseStatusResponse GetTrialLicenseStatus(this IElasticClient client,Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		public static GetTrialLicenseStatusResponse GetTrialLicenseStatus(this IElasticClient client,IGetTrialLicenseStatusRequest request);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		public static Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(this IElasticClient client,
			Func<GetTrialLicenseStatusDescriptor, IGetTrialLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Checks the status of a trial license.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+.
		/// </remarks>
		public static Task<GetTrialLicenseStatusResponse> GetTrialLicenseStatusAsync(this IElasticClient client,IGetTrialLicenseStatusRequest request,
			CancellationToken ct = default
		);
	}

}
