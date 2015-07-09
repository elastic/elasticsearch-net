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
		public IVerifyRepositoryResponse VerifyRepository(string name, Func<VerifyRepositoryDescriptor, VerifyRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters, VerifyRepositoryResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.LowLevelDispatch.SnapshotVerifyRepositoryDispatch<VerifyRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public IVerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest verifyRepositoryRequest)
		{
			return this.Dispatcher.Dispatch<IVerifyRepositoryRequest, VerifyRepositoryRequestParameters, VerifyRepositoryResponse>(
				verifyRepositoryRequest,
				(p, d) => this.LowLevelDispatch.SnapshotVerifyRepositoryDispatch<VerifyRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(string name, Func<VerifyRepositoryDescriptor, VerifyRepositoryDescriptor> selector = null)
		{
			name.ThrowIfNullOrEmpty("name");
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters, VerifyRepositoryResponse, IVerifyRepositoryResponse>(
				s => selector(s.Repository(name)),
				(p, d) => this.LowLevelDispatch.SnapshotVerifyRepositoryDispatchAsync<VerifyRepositoryResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest verifyRepositoryRequest)
		{
			return this.Dispatcher.DispatchAsync<IVerifyRepositoryRequest, VerifyRepositoryRequestParameters, VerifyRepositoryResponse, IVerifyRepositoryResponse>(
				verifyRepositoryRequest,
				(p, d) => this.LowLevelDispatch.SnapshotVerifyRepositoryDispatchAsync<VerifyRepositoryResponse>(p)
			);
		}
	}
}