using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{

		public IIndicesOperationResponse CreateIndex(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			index.ThrowIfEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			var descriptor = createIndexSelector(new CreateIndexDescriptor(this._connectionSettings));
			descriptor._Index = index;
			return this.Dispatch<CreateIndexDescriptor, CreateIndexQueryString, IndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesCreateDispatch(p, d._IndexSettings)
			);
		}
		public Task<IIndicesOperationResponse> CreateIndexAsync(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			index.ThrowIfEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			var descriptor = createIndexSelector(new CreateIndexDescriptor(this._connectionSettings));
			descriptor._Index = index;
			return this.DispatchAsync<CreateIndexDescriptor, CreateIndexQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndicesCreateDispatchAsync(p, d._IndexSettings)
			);
		}

	}
}