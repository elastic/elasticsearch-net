using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetAliasesConverter = Func<IElasticsearchResponse, Stream, GetAliasesResponse>;
	using CrazyAliasesResponse = Dictionary<string, Dictionary<string, Dictionary<string, AliasDefinition>>>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse Alias(IAliasRequest aliasRequest)
		{
			return this.Dispatch<IAliasRequest, AliasRequestParameters, IndicesOperationResponse>(
				aliasRequest,
				(p, d) => this.RawDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse Alias(Func<AliasDescriptor, AliasDescriptor> aliasSelector)
		{
			return this.Dispatch<AliasDescriptor, AliasRequestParameters, IndicesOperationResponse>(
				aliasSelector,
				(p, d) => this.RawDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> AliasAsync(IAliasRequest aliasRequest)
		{
			return this.DispatchAsync<IAliasRequest, AliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasRequest,
				(p, d) => this.RawDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> AliasAsync(Func<AliasDescriptor, AliasDescriptor> aliasSelector)
		{
			return this.DispatchAsync<AliasDescriptor, AliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasSelector,
				(p, d) => this.RawDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, GetAliasDescriptor> GetAliasDescriptor)
		{
			return this.Dispatch<GetAliasDescriptor, GetAliasRequestParameters, GetAliasesResponse>(
				GetAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesGetAliasDispatch<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public IGetAliasesResponse GetAlias(IGetAliasRequest GetAliasRequest)
		{
			return this.Dispatch<IGetAliasRequest, GetAliasRequestParameters, GetAliasesResponse>(
				GetAliasRequest,
				(p, d) => this.RawDispatch.IndicesGetAliasDispatch<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, GetAliasDescriptor> GetAliasDescriptor)
		{
			return this.DispatchAsync<GetAliasDescriptor, GetAliasRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				GetAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesGetAliasDispatchAsync<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest GetAliasRequest)
		{
			return this.DispatchAsync<IGetAliasRequest, GetAliasRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				GetAliasRequest,
				(p, d) => this.RawDispatch.IndicesGetAliasDispatchAsync<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}


		/// <inheritdoc />
		public IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor)
		{
			return this.Dispatch<GetAliasesDescriptor, GetAliasesRequestParameters, GetAliasesResponse>(
				getAliasesDescriptor,
				(p, d) => this.RawDispatch.IndicesGetAliasesDispatch<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public IGetAliasesResponse GetAliases(IGetAliasesRequest getAliasesRequest)
		{
			return this.Dispatch<IGetAliasesRequest, GetAliasesRequestParameters, GetAliasesResponse>(
				getAliasesRequest,
				(p, d) => this.RawDispatch.IndicesGetAliasesDispatch<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor)
		{
			return this.DispatchAsync<GetAliasesDescriptor, GetAliasesRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				getAliasesDescriptor,
				(p, d) => this.RawDispatch.IndicesGetAliasesDispatchAsync<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest getAliasesRequest)
		{
			return this.DispatchAsync<IGetAliasesRequest, GetAliasesRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				getAliasesRequest,
				(p, d) => this.RawDispatch.IndicesGetAliasesDispatchAsync<GetAliasesResponse>(
					p.DeserializationState(new GetAliasesConverter(DeserializeGetAliasesResponse))
				)
			);
		}

		/// <inheritdoc />
		public IPutAliasResponse PutAlias(IPutAliasRequest putAliasRequest)
		{
			return this.Dispatch<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse>(
				putAliasRequest,
				(p, d) => this.RawDispatch.IndicesPutAliasDispatch<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest putAliasRequest)
		{
			return this.DispatchAsync<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse, IPutAliasResponse>(
				putAliasRequest,
				(p, d) => this.RawDispatch.IndicesPutAliasDispatchAsync<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IPutAliasResponse PutAlias(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor)
		{
			return this.Dispatch<PutAliasDescriptor, PutAliasRequestParameters, PutAliasResponse>(
				putAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesPutAliasDispatch<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPutAliasResponse> PutAliasAsync(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor)
		{
			return this.DispatchAsync<PutAliasDescriptor, PutAliasRequestParameters, PutAliasResponse, IPutAliasResponse>(
				putAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesPutAliasDispatchAsync<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest deleteAliasRequest)
		{
			return this.Dispatch<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse>(
				deleteAliasRequest,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatch<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest deleteAliasRequest)
		{
			return this.DispatchAsync<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse, IDeleteAliasResponse>(
				deleteAliasRequest,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatchAsync<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor)
			where T : class
		{
			return this.Dispatch<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters, DeleteAliasResponse>(
				deleteAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatch<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor)
			where T : class
		{
			return this.DispatchAsync<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters, DeleteAliasResponse, IDeleteAliasResponse>(
				deleteAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatchAsync<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		private GetAliasesResponse DeserializeGetAliasesResponse(IElasticsearchResponse connectionStatus, Stream stream)
		{
			if (!connectionStatus.Success)
				return new GetAliasesResponse() { IsValid = false };

			var dict = this.Serializer.Deserialize<CrazyAliasesResponse>(stream);

			var d = new Dictionary<string, IList<AliasDefinition>>();

			foreach (var kv in dict)
			{
				var indexDict = kv.Key;
				var aliases = new List<AliasDefinition>();
				if (kv.Value != null && kv.Value.ContainsKey("aliases"))
				{
					var aliasDict = kv.Value["aliases"];
					if (aliasDict != null)
						aliases = aliasDict.Select(kva =>
						{
							var alias = kva.Value;
							alias.Name = kva.Key;
							return alias;
						}).ToList();
				}

				d.Add(indexDict, aliases);
			}

			return new GetAliasesResponse()
			{
				IsValid = true,
				Indices = d
			};
		}
	}
}