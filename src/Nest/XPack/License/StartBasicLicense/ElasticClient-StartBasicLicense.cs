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
		StartBasicLicenseResponse StartBasicLicense(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		StartBasicLicenseResponse StartBasicLicense(IStartBasicLicenseRequest request);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		Task<StartBasicLicenseResponse> StartBasicLicenseAsync(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		Task<StartBasicLicenseResponse> StartBasicLicenseAsync(IStartBasicLicenseRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public StartBasicLicenseResponse StartBasicLicense(Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null) =>
			StartBasicLicense(selector.InvokeOrDefault(new StartBasicLicenseDescriptor()));

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public StartBasicLicenseResponse StartBasicLicense(IStartBasicLicenseRequest request) =>
			DoRequest<IStartBasicLicenseRequest, StartBasicLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public Task<StartBasicLicenseResponse> StartBasicLicenseAsync(
			Func<StartBasicLicenseDescriptor, IStartBasicLicenseRequest> selector = null,
			CancellationToken ct = default
		) => StartBasicLicenseAsync(selector.InvokeOrDefault(new StartBasicLicenseDescriptor()), ct);

		/// <inheritdoc cref="StartBasicLicense(System.Func{Nest.StartBasicLicenseDescriptor,Nest.IStartBasicLicenseRequest})"/>
		public Task<StartBasicLicenseResponse> StartBasicLicenseAsync(IStartBasicLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStartBasicLicenseRequest, StartBasicLicenseResponse>(request, request.RequestParameters, ct);
	}
}
