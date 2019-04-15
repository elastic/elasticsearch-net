using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		StartTrialLicenseResponse StartTrialLicense(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		StartTrialLicenseResponse StartTrialLicense(IStartTrialLicenseRequest request);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		Task<StartTrialLicenseResponse> StartTrialLicenseAsync(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Starts a 30-day trial license, allowing an upgrade from a basic license to a 30-day trial license,
		/// which gives access to all X-Pack features.
		/// </summary>
		/// <remarks>
		/// Valid in Elasticsearch 6.2.0+. You are allowed to initiate a trial license only if your cluster
		/// has not already activated a trial license for the current major X-Pack version.
		/// </remarks>
		Task<StartTrialLicenseResponse> StartTrialLicenseAsync(IStartTrialLicenseRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public StartTrialLicenseResponse StartTrialLicense(Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null) =>
			StartTrialLicense(selector.InvokeOrDefault(new StartTrialLicenseDescriptor()));

		/// <inheritdoc />
		public StartTrialLicenseResponse StartTrialLicense(IStartTrialLicenseRequest request) =>
			DoRequest<IStartTrialLicenseRequest, StartTrialLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<StartTrialLicenseResponse> StartTrialLicenseAsync(
			Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> selector = null,
			CancellationToken ct = default
		) =>
			StartTrialLicenseAsync(selector.InvokeOrDefault(new StartTrialLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<StartTrialLicenseResponse> StartTrialLicenseAsync(IStartTrialLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStartTrialLicenseRequest, StartTrialLicenseResponse, StartTrialLicenseResponse>(request, request.RequestParameters, ct);
	}
}
