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
				(p, d) =>
					this.RawDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p,
						((ICreateRepositoryRequest) d).Repository)
				);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest request)
		{
			return this.Dispatcher.Dispatch<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) =>
					this.RawDispatch.SnapshotCreateRepositoryDispatch<AcknowledgedResponse>(p,
						((ICreateRepositoryRequest) d).Repository)
				);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(string name, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector)
		{
			name.ThrowIfNullOrEmpty("name");
			return this.Dispatcher
				.DispatchAsync
				<CreateRepositoryDescriptor, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					s => selector(s.Repository(name)),
					(p, d) =>
						this.RawDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p,
							((ICreateRepositoryRequest) d).Repository)
				);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest request)
		{
			return this.Dispatcher
				.DispatchAsync
				<ICreateRepositoryRequest, CreateRepositoryRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					request,
					(p, d) =>
						this.RawDispatch.SnapshotCreateRepositoryDispatchAsync<AcknowledgedResponse>(p,
							((ICreateRepositoryRequest) d).Repository)
				);
		}
	}
}