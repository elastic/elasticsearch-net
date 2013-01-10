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

		public IIndicesResponse CreateIndex<T>(Func<CreateIndexDescriptor<T>>)

		/// <summary>
		/// Create an index with the specified index settings
		/// </summary>
		public IIndicesResponse CreateIndex(string index, IndexSettings settings)
		{
			string data = JsonConvert.SerializeObject(settings, Formatting.None, SerializationSettings);
			return CreateIndexRaw(index, data);
		}

		/// <summary>
		/// Create an index with the specified index settings
		/// </summary>
		public IIndicesResponse CreateIndexRaw(string index, string settings)
		{
			string path = this.PathResolver.CreateIndexPath(index);
			var status = this.Connection.PostSync(path, settings);
			var response = new IndicesResponse
			{
				ConnectionStatus = status
			};
			try
			{
				response = this.Deserialize<IndicesResponse>(status.Result);
				response.IsValid = true;
			}
			catch { }
			return response;
		}

	}
}