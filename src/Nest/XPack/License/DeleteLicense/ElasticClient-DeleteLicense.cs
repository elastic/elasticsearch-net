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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteLicenseResponse DeleteLicense(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null) =>
			DeleteLicense(selector.InvokeOrDefault(new DeleteLicenseDescriptor()));

		/// <inheritdoc />
		public IDeleteLicenseResponse DeleteLicense(IDeleteLicenseRequest request) =>
			Dispatcher.Dispatch<IDeleteLicenseRequest, DeleteLicenseRequestParameters, DeleteLicenseResponse>(
				request,
				(p, d) => LowLevelDispatch.LicenseDeleteDispatch<DeleteLicenseResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteLicenseAsync(selector.InvokeOrDefault(new DeleteLicenseDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeleteLicenseRequest, DeleteLicenseRequestParameters, DeleteLicenseResponse, IDeleteLicenseResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.LicenseDeleteDispatchAsync<DeleteLicenseResponse>(p, c)
			);
	}
}
