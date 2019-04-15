using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null);

		/// <inheritdoc />
		DeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request);

		/// <inheritdoc />
		Task<DeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null) =>
			DeleteLicense(selector.InvokeOrDefault(new DeleteLicenseDescriptor()));

		/// <inheritdoc />
		public DeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request) =>
			DoRequest<IDeleteLicenseRequest, DeleteLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteLicenseResponse> DeleteLicenseAsync(
			Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		) => DeleteLicenseAsync(selector.InvokeOrDefault(new DeleteLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<DeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteLicenseRequest, DeleteLicenseResponse, DeleteLicenseResponse>(request, request.RequestParameters, ct);
	}
}
