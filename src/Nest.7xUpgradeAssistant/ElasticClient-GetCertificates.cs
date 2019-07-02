using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.GetCertificates(), please update this usage.")]
		public static GetCertificatesResponse GetCertificates(this IElasticClient client,
			Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null
		)
			=> client.Security.GetCertificates(selector);

		[Obsolete("Moved to client.Security.GetCertificates(), please update this usage.")]
		public static GetCertificatesResponse GetCertificates(this IElasticClient client, IGetCertificatesRequest request)
			=> client.Security.GetCertificates(request);

		[Obsolete("Moved to client.Security.GetCertificatesAsync(), please update this usage.")]
		public static Task<GetCertificatesResponse> GetCertificatesAsync(this IElasticClient client,
			Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetCertificatesAsync(selector, ct);

		[Obsolete("Moved to client.Security.GetCertificatesAsync(), please update this usage.")]
		public static Task<GetCertificatesResponse> GetCertificatesAsync(this IElasticClient client, IGetCertificatesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetCertificatesAsync(request, ct);
	}
}
