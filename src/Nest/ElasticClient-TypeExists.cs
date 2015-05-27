using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using TypeExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IExistsResponse TypeExists(Func<TypeExistsDescriptor, TypeExistsDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<TypeExistsDescriptor, TypeExistsRequestParameters, ExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsTypeDispatch<ExistsResponse>(
					p.DeserializationState(new TypeExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public IExistsResponse TypeExists(ITypeExistsRequest TypeRequest)
		{
			return this.Dispatcher.Dispatch<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse>(
				TypeRequest,
				(p, d) => this.RawDispatch.IndicesExistsTypeDispatch<ExistsResponse>(
					p.DeserializationState(new TypeExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> TypeExistsAsync(Func<TypeExistsDescriptor, TypeExistsDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<TypeExistsDescriptor, TypeExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsTypeDispatchAsync<ExistsResponse>(
					p.DeserializationState(new TypeExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest TypeRequest)
		{
			return this.Dispatcher.DispatchAsync<ITypeExistsRequest, TypeExistsRequestParameters, ExistsResponse, IExistsResponse>(
				TypeRequest,
				(p, d) => this.RawDispatch.IndicesExistsTypeDispatchAsync<ExistsResponse>(
					p.DeserializationState(new TypeExistConverter(DeserializeExistsResponse))
				)
			);
		}

	}
}