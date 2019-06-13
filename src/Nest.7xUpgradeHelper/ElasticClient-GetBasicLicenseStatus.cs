using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>This API enables you to check the status of your basic license</summary>
		public static GetBasicLicenseStatusResponse GetBasicLicenseStatus(this IElasticClient client,Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public static GetBasicLicenseStatusResponse GetBasicLicenseStatus(this IElasticClient client,IGetBasicLicenseStatusRequest request);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public static Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(this IElasticClient client,Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public static Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(this IElasticClient client,IGetBasicLicenseStatusRequest request, CancellationToken ct = default);
	}

}
