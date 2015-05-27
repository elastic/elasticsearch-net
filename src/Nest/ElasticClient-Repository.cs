using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		
		/// <inheritdoc />
		public IAcknowledgedResponse CreateRepository(string name, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector)
		{
			name.ThrowIfNullOrEmpty("name");
			return this.Dispatcher.Dispatch<CreateRepositoryDescriptor, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p, ((ICreateRepositoryRequest)d).Repository)
			);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest request)
		{
			return this.Dispatcher.Dispatch<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.RawDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p, ((ICreateRepositoryRequest)d).Repository)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(string name, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector)
		{
			name.ThrowIfNullOrEmpty("name");
			return this.Dispatcher.DispatchAsync<CreateRepositoryDescriptor, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p, ((ICreateRepositoryRequest)d).Repository)
			);
		}
		
		/// <inheritdoc />
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest request)
		{
			return this.Dispatcher.DispatchAsync<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.RawDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p, ((ICreateRepositoryRequest)d).Repository)
			);
		}

		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, GetRepositoryDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<GetRepositoryDescriptor, GetRepositoryRequestParameters, GetRepositoryResponse>(
				selector,
				(p, d) => this.RawDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public IGetRepositoryResponse GetRepository(IGetRepositoryRequest request)
		{
			return this.Dispatcher.Dispatch<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse>(
				request,
				(p, d) => this.RawDispatch.SnapshotGetRepositoryDispatch<GetRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, GetRepositoryDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<GetRepositoryDescriptor, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				selector,
				(p, d) => this.RawDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request)
		{
			return this.Dispatcher.DispatchAsync<IGetRepositoryRequest, GetRepositoryRequestParameters, GetRepositoryResponse, IGetRepositoryResponse>(
				request,
				(p, d) => this.RawDispatch.SnapshotGetRepositoryDispatchAsync<GetRepositoryResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public IAcknowledgedResponse DeleteRepository(string name, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, AcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse DeleteRepository(IDeleteRepositoryRequest deleteRepositoryRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, AcknowledgedResponse>(
				deleteRepositoryRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatch<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(string name, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatchAsync<AcknowledgedResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest deleteRepositoryRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteRepositoryRequest, DeleteRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				deleteRepositoryRequest,
				(p, d) => this.RawDispatch.SnapshotDeleteRepositoryDispatchAsync<AcknowledgedResponse>(p)
			);
		}
	
		/// <inheritdoc />
		public IVerifyRepositoryResponse VerifyRepository(string name, Func<VerifyRepositoryDescriptor, VerifyRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters, VerifyRepositoryResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotVerifyRepositoryDispatch<VerifyRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public IVerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest verifyRepositoryRequest)
		{
			return this.Dispatcher.Dispatch<IVerifyRepositoryRequest, VerifyRepositoryRequestParameters, VerifyRepositoryResponse>(
				verifyRepositoryRequest,
				(p, d) => this.RawDispatch.SnapshotVerifyRepositoryDispatch<VerifyRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(string name, Func<VerifyRepositoryDescriptor, VerifyRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters, VerifyRepositoryResponse, IVerifyRepositoryResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.RawDispatch.SnapshotVerifyRepositoryDispatchAsync<VerifyRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest verifyRepositoryRequest)
		{
			return this.Dispatcher.DispatchAsync<IVerifyRepositoryRequest, VerifyRepositoryRequestParameters, VerifyRepositoryResponse, IVerifyRepositoryResponse>(
				verifyRepositoryRequest,
				(p, d) => this.RawDispatch.SnapshotVerifyRepositoryDispatchAsync<VerifyRepositoryResponse>(p)
			);
		}
	}
}