using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nest
{

	public partial class ElasticClient
	{
		/// <summary>
		/// Index object to the default index and the inferred type name for T
		/// </summary>
		public IIndexResponse Index<T>(T @object) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object);
			return this._indexToPath(@object, path);
		}
		/// <summary>
		/// Index object to the default index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public IIndexResponse Index<T>(T @object, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index, string type) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name, using index parameters to further control the operation
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index = null, string type = null, IndexParameters indexParameters = null) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}

		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index, string type, string id) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index, string type, int id) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id.ToString());
			return this._indexToPath<T>(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public IIndexResponse Index<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id.ToString());
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexToPath<T>(@object, path);
		}



		/// <summary>
		/// Index object to the default index and the inferred type name for T
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the default index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the inferred type name for T, using index parameters to further control the operation
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name, using index parameters to further control the operation
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}

		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, string id) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id);
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, int id) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id.ToString());
			return this._indexAsyncToPath(@object, path);
		}
		/// <summary>
		/// Index object to the specified index and the specified type name and force the id of the object to update, using index parameters to further control the operation
		/// </summary>
		public Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class
		{
			var path = this.PathResolver.CreateIdOptionalPathFor<T>(@object, index, type, id.ToString());
			path = this.PathResolver.AppendParametersToPath(path, indexParameters);
			return this._indexAsyncToPath(@object, path);
		}



		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this._indexManyToPath("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this._indexManyToPath("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this._indexManyToPath(path, json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this._indexManyToPath(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            return this._indexManyToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            return this._indexManyToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyToPath(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyToPath(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
            return this._indexManyToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
            return this._indexManyToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyToPath(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyToPath(path, json);
		}




		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this._indexManyAsyncToPath("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			return this._indexManyAsyncToPath("_bulk", json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this._indexManyAsyncToPath(path, json);
		}
		/// <summary>
		/// Index objects to the default index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects);
			var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this._indexManyAsyncToPath(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            return this._indexManyAsyncToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            return this._indexManyAsyncToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyAsyncToPath(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the inferred type name for T, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyAsyncToPath(path, json);
		}

		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
            return this._indexManyAsyncToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			return this._indexManyAsyncToPath(this.BulkIndexPath(index), json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
            var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyAsyncToPath(path, json);
		}
		/// <summary>
		/// Index objects to the specified index and the specified type name, using bulk parameters to control the individual objects
		/// and SimpleBulkParameters to control the entire operation
		/// </summary>
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkIndexCommand(@objects, index, type);
			var path = this.PathResolver.AppendSimpleParametersToPath(this.BulkIndexPath(index), bulkParameters);
			return this._indexManyAsyncToPath(path, json);
		}


		private IIndexResponse _indexToPath<T>(T @object, string path) where T : class
		{
			path.ThrowIfNull("path");

			string json = JsonConvert.SerializeObject(@object, Formatting.Indented, IndexSerializationSettings);

			var status = this.Connection.PostSync(path, json);
			return this.ToParsedResponse<IndexResponse>(status);
		}

		private Task<IIndexResponse> _indexAsyncToPath<T>(T @object, string path) where T : class
		{
			string json = JsonConvert.SerializeObject(@object, Formatting.None, IndexSerializationSettings);
			var postTask = this.Connection.Post(path, json);
			return postTask.ContinueWith<IIndexResponse>(t => this.ToParsedResponse<IndexResponse>(t.Result));
		}

		private IBulkResponse _indexManyToPath(string path, string json)
		{
			var status = this.Connection.PostSync(path, json);
			return this.ToParsedResponse<BulkResponse>(status);
		}

		private Task<IBulkResponse> _indexManyAsyncToPath(string path, string json)
		{
			var postTask = this.Connection.Post(path, json);
			return postTask.ContinueWith<IBulkResponse>(t => this.ToParsedResponse<BulkResponse>(t.Result));
		}
	}
}
