using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The certificates API enables you to retrieve information about the X.509 certificates
		/// that are used to encrypt communications in your Elasticsearch cluster.
		/// </summary>
		IGetCertificatesResponse GetCertificates(Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		IGetCertificatesResponse GetCertificates(IGetCertificatesRequest request);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		Task<IGetCertificatesResponse> GetCertificatesAsync(Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		Task<IGetCertificatesResponse> GetCertificatesAsync(IGetCertificatesRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public IGetCertificatesResponse GetCertificates(Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null) =>
			GetCertificates(selector.InvokeOrDefault(new GetCertificatesDescriptor()));

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public IGetCertificatesResponse GetCertificates(IGetCertificatesRequest request) =>
			Dispatcher.Dispatch<IGetCertificatesRequest, GetCertificatesRequestParameters, GetCertificatesResponse>(
				request,
				ToCertificatesResponse,
				(p, d) => LowLevelDispatch.XpackSslCertificatesDispatch<GetCertificatesResponse>(p)
			);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public Task<IGetCertificatesResponse> GetCertificatesAsync(Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetCertificatesAsync(selector.InvokeOrDefault(new GetCertificatesDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public Task<IGetCertificatesResponse> GetCertificatesAsync(IGetCertificatesRequest request,
			CancellationToken cancellationToken = default
		) =>
			Dispatcher
				.DispatchAsync<IGetCertificatesRequest, GetCertificatesRequestParameters, GetCertificatesResponse,
					IGetCertificatesResponse>(
					request,
					cancellationToken,
					ToCertificatesResponse,
					(p, d, c) => LowLevelDispatch.XpackSslCertificatesDispatchAsync<GetCertificatesResponse>(p, c)
				);

		private GetCertificatesResponse ToCertificatesResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			var result = RequestResponseSerializer.Deserialize<ClusterCertificateInformation[]>(stream);
			return new GetCertificatesResponse { Certificates = result };
		}
	}
}
