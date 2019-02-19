using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The start basic API enables you to initiate an indefinite basic license, which gives access to all of
		/// the basic features. If the basic license does not support all of the features that are
		/// available with your current license, however, you are notified in the response. You must then
		/// re-submit the API request with the acknowledge parameter set to true.
		/// </summary>
		IStartBasicLicenseResponse StartBasicLicense(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		IStartBasicLicenseResponse StartBasicLicense(IStartBasicLicenseRequest request);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		Task<IStartBasicLicenseResponse> StartBasicLicenseAsync(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		Task<IStartBasicLicenseResponse> StartBasicLicenseAsync(IStartBasicLicenseRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public IStartBasicLicenseResponse StartBasicLicense(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null) =>
			StartBasicLicense(selector.InvokeOrDefault(new StartBasicLicenseDescriptor()));

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public IStartBasicLicenseResponse StartBasicLicense(IStartBasicLicenseRequest request) =>
			Dispatcher.Dispatch<IStartBasicLicenseRequest, StartBasicLicenseRequestParameters, StartBasicLicenseResponse>(
				request,
				(p, d) => LowLevelDispatch.LicensePostStartBasicDispatch<StartBasicLicenseResponse>(p)
			);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public Task<IStartBasicLicenseResponse> StartBasicLicenseAsync(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			StartBasicLicenseAsync(selector.InvokeOrDefault(new StartBasicLicenseDescriptor()), cancellationToken);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public Task<IStartBasicLicenseResponse> StartBasicLicenseAsync(IStartBasicLicenseRequest request, CancellationToken cancellationToken = default
		) =>
			Dispatcher.DispatchAsync<IStartBasicLicenseRequest, StartBasicLicenseRequestParameters, StartBasicLicenseResponse, IStartBasicLicenseResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.LicensePostStartBasicDispatchAsync<StartBasicLicenseResponse>(p, c)
			);
	}
}
