using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary></summary>
		ICreateFollowIndexResponse CreateFollowIndex(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector);

		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		ICreateFollowIndexResponse CreateFollowIndex(ICreateFollowIndexRequest request);

		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		Task<ICreateFollowIndexResponse> CreateFollowIndexAsync(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		Task<ICreateFollowIndexResponse> CreateFollowIndexAsync(ICreateFollowIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public ICreateFollowIndexResponse CreateFollowIndex(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector) =>
			CreateFollowIndex(selector.InvokeOrDefault(new CreateFollowIndexDescriptor(index)));

		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public ICreateFollowIndexResponse CreateFollowIndex(ICreateFollowIndexRequest request) =>
			Dispatcher.Dispatch<ICreateFollowIndexRequest, CreateFollowIndexRequestParameters, CreateFollowIndexResponse>(
				request,
				(p, d) => LowLevelDispatch.CcrFollowDispatch<CreateFollowIndexResponse>(p, d)
			);

		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public Task<ICreateFollowIndexResponse> CreateFollowIndexAsync(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CreateFollowIndexAsync(selector.InvokeOrDefault(new CreateFollowIndexDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="CreateFollowIndex(System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public Task<ICreateFollowIndexResponse> CreateFollowIndexAsync(ICreateFollowIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<ICreateFollowIndexRequest, CreateFollowIndexRequestParameters, CreateFollowIndexResponse, ICreateFollowIndexResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.CcrFollowDispatchAsync<CreateFollowIndexResponse>(p, d, c)
			);
	}
}
