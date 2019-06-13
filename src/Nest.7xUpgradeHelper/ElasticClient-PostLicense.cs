using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static PostLicenseResponse PostLicense(this IElasticClient client,Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null);

		/// <inheritdoc />
		public static PostLicenseResponse PostLicense(this IElasticClient client,IPostLicenseRequest request);

		/// <inheritdoc />
		public static Task<PostLicenseResponse> PostLicenseAsync(this IElasticClient client,Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PostLicenseResponse> PostLicenseAsync(this IElasticClient client,IPostLicenseRequest request, CancellationToken ct = default);
	}

}
