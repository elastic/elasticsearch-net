using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The certificates API enables you to retrieve information about the X.509 certificates
		/// that are used to encrypt communications in your Elasticsearch cluster.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCertificatesResponse GetCertificates(this IElasticClient client,
			Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null
		)
			=> client.Security.GetCertificates(selector);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCertificatesResponse GetCertificates(this IElasticClient client, IGetCertificatesRequest request)
			=> client.Security.GetCertificates(request);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCertificatesResponse> GetCertificatesAsync(this IElasticClient client,
			Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetCertificatesAsync(selector, ct);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCertificatesResponse> GetCertificatesAsync(this IElasticClient client, IGetCertificatesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetCertificatesAsync(request, ct);
	}
}
