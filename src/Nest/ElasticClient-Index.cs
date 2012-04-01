using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

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
		public ConnectionStatus Index<T>(T @object, string index, string type, IndexParameters indexParameters) where T : class
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
		public void IndexAsync<T>(T @object, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the default index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public void IndexAsync<T>(T @object, IndexParameters indexParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, indexParameters);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T
		/// </summary>
		public void IndexAsync<T>(T @object, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public void IndexAsync<T>(T @object, string index, IndexParameters indexParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, indexParameters);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name
		/// </summary>
		public void IndexAsync<T>(T @object, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name, using index parameters to further control the operation
		/// </summary>
		public void IndexAsync<T>(T @object, string index, string type, IndexParameters indexParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, indexParameters);
			this._indexAsyncToPath(@object, path, continuation);
		}

		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public void IndexAsync<T>(T @object, string index, string type, string id, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public void IndexAsync<T>(T @object, string index, string type, string id, IndexParameters indexParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id);
			path = this.AppendParametersToPath(path, indexParameters);
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public void IndexAsync<T>(T @object, string index, string type, int id, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			this._indexAsyncToPath(@object, path, continuation);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public void IndexAsync<T>(T @object, string index, string type, int id, IndexParameters indexParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type, id.ToString());
			path = this.AppendParametersToPath(path, indexParameters);
			this._indexAsyncToPath(@object, path, continuation);
		}
		
		private void _indexAsyncToPath<T>(T @object, string path, Action<ConnectionStatus> continuation) where T : class
		{
			string json = JsonConvert.SerializeObject(@object, Formatting.None, SerializationSettings);
			this.Connection.Post(path, json, continuation);
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
		public void IndexAsync<T>(IEnumerable<T> objects, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			this.Connection.Post("_bulk", json, continuation);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public void IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			this.Connection.Post("_bulk", json, continuation);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public void IndexAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public void IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}

		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// </summary>
		public void IndexAsync<T>(IEnumerable<T> objects, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			this.Connection.Post("_bulk", json, continuation);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public void IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			this.Connection.Post("_bulk", json, continuation);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public void IndexAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public void IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}

		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// </summary>
		public void IndexAsync<T>(IEnumerable<T> objects, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			this.Connection.Post("_bulk", json, continuation);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// </summary>
		public void IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			this.Connection.Post("_bulk", json, continuation);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public void IndexAsync<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public void IndexAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}

	}
	
}
