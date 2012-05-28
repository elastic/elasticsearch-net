using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nest
{

	public partial class ElasticClient
	{
		/// <summary>
		/// Index object to the default index and the inferred type name for T
		/// </summary>
		public ConnectionStatus Index<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
		    return this._indexToPath(@object, path);
		}
		/// <summary>
		/// Index object to the default index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public ConnectionStatus Index<T>(T @object, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name, using index parameters to further control the operation
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index = null, string type = null, IndexParameters indexParameters = null) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}

		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index, string type, string id) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index, string type, int id) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public ConnectionStatus Index<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}
		
		private ConnectionStatus _indexToPath<T>(T @object, string path) where T : class
		{
			path.ThrowIfNull("path");
			
			string json = JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);

			return this.Connection.PostSync(path, json);
		}

		/// <summary>
		/// Index object to the default index and the inferred type name for T
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the default index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name, using index parameters to further control the operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, string type, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}

		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, string type, string id) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, string type, int id) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			path = this.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		
		private Task<ConnectionStatus> _indexAsyncToPath<T>(T @object, string path) where T : class
		{
			string json = JsonConvert.SerializeObject(@object, Formatting.None, SerializationSettings);
			return this.Connection.Post(path, json);
		}

		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public ConnectionStatus Index<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		} 
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<ConnectionStatus> IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}

	}
	
}
