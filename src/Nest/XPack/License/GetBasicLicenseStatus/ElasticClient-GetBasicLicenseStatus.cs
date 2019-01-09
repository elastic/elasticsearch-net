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
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(IGetBasicLicenseStatusRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public IGetBasicLicenseStatusResponse GetBasicLicenseStatus(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null) =>
			GetBasicLicenseStatus(selector.InvokeOrDefault(new GetBasicLicenseStatusDescriptor()));

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public IGetBasicLicenseStatusResponse GetBasicLicenseStatus(IGetBasicLicenseStatusRequest request) =>
			Dispatcher.Dispatch<IGetBasicLicenseStatusRequest, GetBasicLicenseStatusRequestParameters, GetBasicLicenseStatusResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackLicenseGetBasicStatusDispatch<GetBasicLicenseStatusResponse>(p)
			);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(Func<GetBasicLicenseStatusDescriptor, IGetBasicLicenseStatusRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetBasicLicenseStatusAsync(selector.InvokeOrDefault(new GetBasicLicenseStatusDescriptor()), cancellationToken);

		/// <inheritdoc see cref="GetBasicLicenseStatus(System.Func{Nest.GetBasicLicenseStatusDescriptor,Nest.IGetBasicLicenseStatusRequest})"/>
		public Task<IGetBasicLicenseStatusResponse> GetBasicLicenseStatusAsync(IGetBasicLicenseStatusRequest request, CancellationToken cancellationToken = default
		) =>
			Dispatcher.DispatchAsync<IGetBasicLicenseStatusRequest, GetBasicLicenseStatusRequestParameters, GetBasicLicenseStatusResponse, IGetBasicLicenseStatusResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackLicenseGetBasicStatusDispatchAsync<GetBasicLicenseStatusResponse>(p, c)
			);
	}
}
