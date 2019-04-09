using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>This API enables you to check the status of your basic license</summary>
		IGetBasicLicenseStatusResponse GetBasicLicenseStatus(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		IGetBasicLicenseStatusResponse GetBasicLicenseStatus(IGetBasicLicenseStatusRequest request);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(IGetBasicLicenseStatusRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public IGetBasicLicenseStatusResponse GetBasicLicenseStatus(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null) =>
			GetBasicLicenseStatus(selector.InvokeOrDefault(new GetBasicLicenseStatusDescriptor()));

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public IGetBasicLicenseStatusResponse GetBasicLicenseStatus(IGetBasicLicenseStatusRequest request) =>
			Dispatch2<IGetBasicLicenseStatusRequest, GetBasicLicenseStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(
			Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken ct = default
		) => GetBasicLicenseStatusAsync(selector.InvokeOrDefault(new GetBasicLicenseStatusDescriptor()), ct);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(IGetBasicLicenseStatusRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetBasicLicenseStatusRequest, IGetBasicLicenseStatusResponse, GetBasicLicenseStatusResponse>(request, request.RequestParameters, ct);
	}
}
