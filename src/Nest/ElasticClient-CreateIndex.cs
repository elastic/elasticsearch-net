using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Create an index with the specified index settings
		/// </summary>
		public IIndicesOperationResponse CreateIndex(string index, IndexSettings settings)
		{
			string data = this.Serialize(settings);
			string path = this.PathResolver.CreateIndexPath(index);
			var status = this.Connection.PostSync(path, data);
			return this.ToParsedResponse<IndicesOperationResponse>(status);
		}
		
		/// <summary>
		/// Create an index with the specified index settings
		/// </summary>
		public IIndicesOperationResponse CreateIndex(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector)
		{
			index.ThrowIfEmpty("index");
			createIndexSelector.ThrowIfNull("createIndexSelector");

			var d = createIndexSelector(new CreateIndexDescriptor(this.Settings));
			var settings = d._IndexSettings;
			return this.CreateIndex(index, settings);

		}

	}
}