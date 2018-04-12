using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		IStartTrialLicenseResponse StartTrialLicense(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		IStartTrialLicenseResponse StartTrialLicense(IStartTrialLicenseRequest request);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		Task<IStartTrialLicenseResponse> StartTrialLicenseAsync(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		Task<IStartTrialLicenseResponse> StartTrialLicenseAsync(IStartTrialLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStartTrialLicenseResponse StartTrialLicense(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null) =>
			this.StartTrialLicense(selector.InvokeOrDefault(new StartTrialLicenseDescriptor()));

		/// <inheritdoc/>
		public IStartTrialLicenseResponse StartTrialLicense(IStartTrialLicenseRequest request) =>
			this.Dispatcher.Dispatch<IStartTrialLicenseRequest, StartTrialLicenseRequestParameters, StartTrialLicenseResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackLicensePostStartTrialDispatch<StartTrialLicenseResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStartTrialLicenseResponse> StartTrialLicenseAsync(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.StartTrialLicenseAsync(selector.InvokeOrDefault(new StartTrialLicenseDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IStartTrialLicenseResponse> StartTrialLicenseAsync(IStartTrialLicenseRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IStartTrialLicenseRequest, StartTrialLicenseRequestParameters, StartTrialLicenseResponse, IStartTrialLicenseResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackLicensePostStartTrialDispatchAsync<StartTrialLicenseResponse>(p, c)
			);
	}
}
