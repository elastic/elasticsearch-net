using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DeleteLicenseResponse DeleteLicense(this IElasticClient client,Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null);

		/// <inheritdoc />
		public static DeleteLicenseResponse DeleteLicense(this IElasticClient client,IDeleteLicenseRequest request);

		/// <inheritdoc />
		public static Task<DeleteLicenseResponse> DeleteLicenseAsync(this IElasticClient client,Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteLicenseResponse> DeleteLicenseAsync(this IElasticClient client,IDeleteLicenseRequest request,
			CancellationToken ct = default
		);
	}

}
