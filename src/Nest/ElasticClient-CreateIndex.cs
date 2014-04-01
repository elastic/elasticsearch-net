using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse CreateIndex(string index,
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			index.ThrowIfEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			var descriptor = createIndexSelector(new CreateIndexDescriptor(_connectionSettings));
			descriptor._Index = index;
			return this.Dispatch<CreateIndexDescriptor, CreateIndexRequestParameters, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesCreateDispatch<IndicesOperationResponse>(p, d._IndexSettings)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CreateIndexAsync(string index,
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			index.ThrowIfEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			var descriptor = createIndexSelector(new CreateIndexDescriptor(_connectionSettings));
			descriptor._Index = index;
			return this.DispatchAsync
				<CreateIndexDescriptor, CreateIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					descriptor,
					(p, d) => this.RawDispatch.IndicesCreateDispatchAsync<IndicesOperationResponse>(p, d._IndexSettings)
				);
		}
	}
}