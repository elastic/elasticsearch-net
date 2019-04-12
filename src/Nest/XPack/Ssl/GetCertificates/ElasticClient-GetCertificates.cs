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
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		Task<IGetCertificatesResponse> GetCertificatesAsync(IGetCertificatesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public IGetCertificatesResponse GetCertificates(Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null) =>
			GetCertificates(selector.InvokeOrDefault(new GetCertificatesDescriptor()));

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public IGetCertificatesResponse GetCertificates(IGetCertificatesRequest request)
		{
			request.RequestParameters.DeserializationOverride = ToCertificatesResponse;
			return DoRequest<IGetCertificatesRequest, GetCertificatesResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public Task<IGetCertificatesResponse> GetCertificatesAsync(
			Func<GetCertificatesDescriptor, IGetCertificatesRequest> selector = null,
			CancellationToken ct = default
		) => GetCertificatesAsync(selector.InvokeOrDefault(new GetCertificatesDescriptor()), ct);

		/// <inheritdoc cref="GetCertificates(System.Func{Nest.GetCertificatesDescriptor,Nest.IGetCertificatesRequest})" />
		public Task<IGetCertificatesResponse> GetCertificatesAsync(IGetCertificatesRequest request, CancellationToken ct = default)
		{
			request.RequestParameters.DeserializationOverride = ToCertificatesResponse;
			return DoRequestAsync<IGetCertificatesRequest, IGetCertificatesResponse, GetCertificatesResponse>
				(request, request.RequestParameters, ct);
		}

		private GetCertificatesResponse ToCertificatesResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			var result = RequestResponseSerializer.Deserialize<ClusterCertificateInformation[]>(stream);
			return new GetCertificatesResponse { Certificates = result };
		}
	}
}
