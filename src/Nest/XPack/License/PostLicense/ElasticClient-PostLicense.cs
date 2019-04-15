using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		PostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null);

		/// <inheritdoc />
		PostLicenseResponse PostLicense(IPostLicenseRequest request);

		/// <inheritdoc />
		Task<PostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<PostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PostLicenseResponse PostLicense(Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null) =>
			PostLicense(selector.InvokeOrDefault(new PostLicenseDescriptor()));

		/// <inheritdoc />
		public PostLicenseResponse PostLicense(IPostLicenseRequest request) =>
			DoRequest<IPostLicenseRequest, PostLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PostLicenseResponse> PostLicenseAsync(
			Func<PostLicenseDescriptor, IPostLicenseRequest> selector = null,
			CancellationToken ct = default
		) => PostLicenseAsync(selector.InvokeOrDefault(new PostLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<PostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPostLicenseRequest, PostLicenseResponse, PostLicenseResponse>(request, request.RequestParameters, ct);
	}
}
