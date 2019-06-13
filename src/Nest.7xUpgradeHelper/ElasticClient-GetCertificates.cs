using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The certificates API enables you to retrieve information about the X.509 certificates
		/// that are used to encrypt communications in your Elasticsearch cluster.
		/// </summary>
		public static GetCertificatesResponse GetCertificates(this IElasticClient client,Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public static GetCertificatesResponse GetCertificates(this IElasticClient client,IGetCertificatesRequest request);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public static Task<GetCertificatesResponse> GetCertificatesAsync(this IElasticClient client,Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public static Task<GetCertificatesResponse> GetCertificatesAsync(this IElasticClient client,IGetCertificatesRequest request,
			CancellationToken ct = default
		);
	}

}
