using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>This API enables you to check the status of your basic license</summary>
		GetBasicLicenseStatusResponse GetBasicLicenseStatus(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		GetBasicLicenseStatusResponse GetBasicLicenseStatus(IGetBasicLicenseStatusRequest request);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(IGetBasicLicenseStatusRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public GetBasicLicenseStatusResponse GetBasicLicenseStatus(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null) =>
			GetBasicLicenseStatus(selector.InvokeOrDefault(new GetBasicLicenseStatusDescriptor()));

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public GetBasicLicenseStatusResponse GetBasicLicenseStatus(IGetBasicLicenseStatusRequest request) =>
			DoRequest<IGetBasicLicenseStatusRequest, GetBasicLicenseStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(
			Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		) => GetBasicLicenseStatusAsync(selector.InvokeOrDefault(new GetBasicLicenseStatusDescriptor()), ct);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public Task<GetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(IGetBasicLicenseStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetBasicLicenseStatusRequest, GetBasicLicenseStatusResponse, GetBasicLicenseStatusResponse>(request, request.RequestParameters, ct);
	}
}
