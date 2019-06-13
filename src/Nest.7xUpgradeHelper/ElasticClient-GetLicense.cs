using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetLicenseResponse GetLicense(this IElasticClient client,Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null);

		/// <inheritdoc />
		public static GetLicenseResponse GetLicense(this IElasticClient client,IGetLicenseRequest request);

		/// <inheritdoc />
		public static Task<GetLicenseResponse> GetLicenseAsync(this IElasticClient client,Func<GetLicenseDescriptor, IGetLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetLicenseResponse> GetLicenseAsync(this IElasticClient client,IGetLicenseRequest request, CancellationToken ct = default);
	}

}
