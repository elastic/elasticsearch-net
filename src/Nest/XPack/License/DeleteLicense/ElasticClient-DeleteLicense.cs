using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null);

		/// <inheritdoc />
		IDeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request);

		/// <inheritdoc />
		Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null) =>
			DeleteLicense(selector.InvokeOrDefault(new DeleteLicenseDescriptor()));

		/// <inheritdoc />
		public IDeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request) =>
			DoRequest<IDeleteLicenseRequest, DeleteLicenseResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteLicenseResponse> DeleteLicenseAsync(
			Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken ct = default
		) => DeleteLicenseAsync(selector.InvokeOrDefault(new DeleteLicenseDescriptor()), ct);

		/// <inheritdoc />
		public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteLicenseRequest, IDeleteLicenseResponse, DeleteLicenseResponse>(request, request.RequestParameters, ct);
	}
}
