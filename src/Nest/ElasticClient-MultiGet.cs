using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{

	public partial class ElasticClient
	{
		public IEnumerable<T> Get<T>(IEnumerable<int> ids)
			where T : class
		{
			return this.Get<T>(ids.Select(i => Convert.ToString(i)));
		}


		/// <summary>
		/// Gets multiple documents of T by id in the default index and the inferred typename for T
		/// </summary>
		public IEnumerable<T> Get<T>(IEnumerable<string> ids)
			where T : class
		{
	  var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();

			return this.Get<T>(ids, this.PathResolver.CreateIndexTypePath(index, typeName));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index and the specified typename for T
		/// </summary>
		public IEnumerable<T> Get<T>(string index, string type, IEnumerable<int> ids)
			where T : class
		{
			return this.Get<T>(index, type, ids.Select(i => Convert.ToString(i)));
		}
		/// <summary>
		/// Gets multiple documents of T by id in the specified index and the specified typename for T
		/// </summary>
		public IEnumerable<T> Get<T>(string index, string type, IEnumerable<string> ids)
			where T : class
		{
			return this.Get<T>(ids, this.PathResolver.CreateIndexTypePath(index, type));
		}

		private IEnumerable<T> Get<T>(IEnumerable<string> ids, string path)
			where T : class
		{
			var data = @"{{ ""ids"": {0} }}".F(JsonConvert.SerializeObject(ids));
			var response = this.Connection.PostSync(path + "_mget", data);

			if (response.Result == null)
				return null;

			return this.Deserialize<MultiHit<T>>(response.Result)
				.Hits
				.Where(h => h.Source != null) // non-existent ids come back as a hit without a "_source"
				.Select(h => h.Source);
		}
		
	}
}
