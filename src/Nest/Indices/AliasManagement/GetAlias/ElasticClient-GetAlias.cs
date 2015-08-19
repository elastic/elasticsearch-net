using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetAliasesConverter = Func<IApiCallDetails, Stream, GetAliasesResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, GetAliasDescriptor> GetAliasDescriptor)
		{
			return this.Dispatcher.Dispatch<GetAliasDescriptor, GetAliasRequestParameters, GetAliasesResponse>(
				GetAliasDescriptor,
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatch<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public IGetAliasesResponse GetAlias(IGetAliasRequest GetAliasRequest)
		{
			return this.Dispatcher.Dispatch<IGetAliasRequest, GetAliasRequestParameters, GetAliasesResponse>(
				GetAliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatch<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, GetAliasDescriptor> GetAliasDescriptor)
		{
			return this.Dispatcher.DispatchAsync<GetAliasDescriptor, GetAliasRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				GetAliasDescriptor,
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatchAsync<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest GetAliasRequest)
		{
			return this.Dispatcher.DispatchAsync<IGetAliasRequest, GetAliasRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				GetAliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatchAsync<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

	}
}