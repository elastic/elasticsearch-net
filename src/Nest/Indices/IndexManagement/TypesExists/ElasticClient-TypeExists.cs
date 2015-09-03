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
		IExistsResponse TypeExists(Func<TypeExistsDescriptor, ITypeExistsRequest> selector);

		/// <inheritdoc/>
		IExistsResponse TypeExists(ITypeExistsRequest typeRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> TypeExistsAsync(Func<TypeExistsDescriptor, ITypeExistsRequest> selector);

		/// <inheritdoc/>
		Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest typeRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse TypeExists(Func<TypeExistsDescriptor, ITypeExistsRequest> selector) =>
			this.TypeExists(selector?.Invoke(new TypeExistsDescriptor()));

		/// <inheritdoc/>
		public IExistsResponse TypeExists(ITypeExistsRequest typeRequest) => 
			this.Dispatcher.Dispatch<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse>(
				typeRequest,
				new TypeExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTypeDispatch<ExistsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> TypeExistsAsync(Func<TypeExistsDescriptor, ITypeExistsRequest> selector) =>
			this.TypeExistsAsync(selector?.Invoke(new TypeExistsDescriptor()));

		/// <inheritdoc/>
		public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest typeRequest) => 
			this.Dispatcher.DispatchAsync<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse, IExistsResponse>(
				typeRequest,
				new TypeExistConverter(DeserializeExistsResponse),
				(p, d) => this.LowLevelDispatch.IndicesExistsTypeDispatchAsync<ExistsResponse>(p)
			);
	}
}