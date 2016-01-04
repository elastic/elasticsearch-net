using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TypeExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;
	
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExistsResponse TypeExists(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null);

		/// <inheritdoc/>
		IExistsResponse TypeExists(ITypeExistsRequest request);

		/// <inheritdoc/>
		Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null);

		/// <inheritdoc/>
		Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse TypeExists(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null) =>
			this.TypeExists(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, types)));

		/// <inheritdoc/>
		public IExistsResponse TypeExists(ITypeExistsRequest request) => 
			this.Dispatcher.Dispatch<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse>(
				request,
				new TypeExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTypeDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector = null) =>
			this.TypeExistsAsync(selector.InvokeOrDefault(new TypeExistsDescriptor(indices, types)));

		/// <inheritdoc/>
		public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request) => 
			this.Dispatcher.DispatchAsync<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse, IExistsResponse>(
				request,
				new TypeExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTypeDispatchAsync<ExistsResponse>(p)
			);
	}
}