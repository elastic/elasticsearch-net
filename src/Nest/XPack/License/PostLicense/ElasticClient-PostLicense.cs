using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null);

		/// <inheritdoc />
		IPostLicenseResponse PostLicense(IPostLicenseRequest request);

		/// <inheritdoc />
		Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null) =>
			PostLicense(selector.InvokeOrDefault(new PostLicenseDescriptor()));

		/// <inheritdoc />
		public IPostLicenseResponse PostLicense(IPostLicenseRequest request) =>
			DoRequest<IPostLicenseRequest, PostLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPostLicenseResponse> PostLicenseAsync(
			Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null,
			CancellationToken ct = default
		) => PostLicenseAsync(selector.InvokeOrDefault(new PostLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPostLicenseRequest, IPostLicenseResponse, PostLicenseResponse>(request, request.RequestParameters, ct);
	}
}
