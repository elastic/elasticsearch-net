using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {
		#region Delete by passing an object
		public ConnectionStatus Delete<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		public ConnectionStatus Delete<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		public void DeleteAsync<T>(T @object, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, string type, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteAsync<T>(T @object, string index, string type, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		#endregion

		#region Delete by passing an id
		public ConnectionStatus DeleteById<T>(int id) where T : class
        {
			return this.DeleteById<T>(id.ToString());
        }

		public ConnectionStatus DeleteById<T>(string id) where T : class
        {
            var index = this.Settings.DefaultIndex;
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			return this._deleteToPath(path);
        }
		public ConnectionStatus DeleteById<T>(string index, string type, string id) where T : class
        {
			var path = this.CreatePath(index, type, id);
			return this._deleteToPath(path);
        }

		public ConnectionStatus DeleteById<T>(string index, string type, int id) where T : class
        {
			var path = this.CreatePath(index, type, id.ToString());
			return this._deleteToPath(path);
        }

		public ConnectionStatus DeleteById<T>(int id, DeleteParameters deleteParameters) where T : class
		{
			return this.DeleteById<T>(id.ToString(), deleteParameters);
		}

		public ConnectionStatus DeleteById<T>(string id, DeleteParameters deleteParameters) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		public ConnectionStatus DeleteById<T>(string index, string type, string id, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePath(index, type, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		public ConnectionStatus DeleteById<T>(string index, string type, int id, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePath(index, type, id.ToString());
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}


		public void DeleteByIdAsync<T>(int id, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByIdAsync<T>(id.ToString(), callback);
		}

		public void DeleteByIdAsync<T>(string id, Action<ConnectionStatus> callback) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteByIdAsync<T>(string index, string type, string id, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id);
			this._deleteToPathAsync(path, callback);
		}

		public void DeleteByIdAsync<T>(string index, string type, int id, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id.ToString());
			this._deleteToPathAsync(path, callback);
		}


		public void DeleteByIdAsync<T>(int id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByIdAsync<T>(id.ToString(), deleteParameters, callback);
		}

		public void DeleteByIdAsync<T>(string id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}
		public void DeleteByIdAsync<T>(string index, string type, string id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}

		public void DeleteByIdAsync<T>(string index, string type, int id, DeleteParameters deleteParameters, Action<ConnectionStatus> callback) where T : class
		{
			var path = this.CreatePath(index, type, id.ToString());
			path = this.AppendParametersToPath(path, deleteParameters);
			this._deleteToPathAsync(path, callback);
		}

		#endregion

		#region Delete by passing an IEnumerable of objects
		public ConnectionStatus Delete<T>(IEnumerable<T> @objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> @objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<T> @objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> @objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		public void DeleteAsync<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			this.Connection.Post("_bulk", json, null);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			this.Connection.Post("_bulk", json, null);
		}
		public void DeleteAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, null);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, null);
		}

		public void DeleteAsync<T>(IEnumerable<T> objects, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}


		public void DeleteAsync<T>(IEnumerable<T> objects, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}


		public void DeleteAsync<T>(IEnumerable<T> objects, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			this.Connection.Post("_bulk", json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}
		public void DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, string type, Action<ConnectionStatus> continuation) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			this.Connection.Post(path, json, continuation);
		}

		#endregion

		#region Delete by passing a query string 
		public ConnectionStatus DeleteByQuery<T>(string query) where T : class
		{
			return this.DeleteByQuery<T>(query, default(DeleteByQueryParameters));
		}
		public ConnectionStatus DeleteByQuery<T>(string query, DeleteByQueryParameters parameters) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}
		public void DeleteByQueryAsync<T>(string query, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByQueryAsync<T>(query, default(DeleteByQueryParameters), callback);
		}
		public void DeleteByQueryAsync<T>(string query, DeleteByQueryParameters parameters, Action<ConnectionStatus> callback) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			this._deleteToPathAsync(path, query, callback);
		}

		public ConnectionStatus DeleteByQuery<T>(string query, string index) where T : class
		{
			return this.DeleteByQuery<T>(query, index, default(DeleteByQueryParameters));
		}
		public ConnectionStatus DeleteByQuery<T>(string query, string index, DeleteByQueryParameters parameters) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			index.ThrowIfNullOrEmpty("index cannot be empty");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}
		public void DeleteByQueryAsync<T>(string query, string index, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByQueryAsync<T>(query, index, default(DeleteByQueryParameters), callback);
		}
		public void DeleteByQueryAsync<T>(string query, string index, DeleteByQueryParameters parameters, Action<ConnectionStatus> callback) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			index.ThrowIfNullOrEmpty("index cannot be empty");

			var typeName = this.InferTypeName<T>();
			var path = this.CreatePath(index, typeName);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			this._deleteToPathAsync(path, query, callback);
		}

		public ConnectionStatus DeleteByQuery<T>(string query, string index, string type) where T : class
		{
			return this.DeleteByQuery<T>(query, index, type, default(DeleteByQueryParameters));
		}
		public ConnectionStatus DeleteByQuery<T>(string query, string index, string type, DeleteByQueryParameters parameters) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			index.ThrowIfNullOrEmpty("index cannot be empty");
			type.ThrowIfNullOrEmpty("type cannot be empty");
			var path = this.CreatePath(index, type);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}
		public void DeleteByQueryAsync<T>(string query, string index, string type, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByQueryAsync<T>(query, index, type, default(DeleteByQueryParameters), callback);
		}
		public void DeleteByQueryAsync<T>(string query, string index, string type, DeleteByQueryParameters parameters, Action<ConnectionStatus> callback) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			index.ThrowIfNullOrEmpty("index cannot be empty");
			type.ThrowIfNullOrEmpty("type cannot be empty");
			var path = this.CreatePath(index, type);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			this._deleteToPathAsync(path, query, callback);
		}


		public ConnectionStatus DeleteByQueryOverIndices<T>(string query, IEnumerable<string> indices) where T : class
		{
			return this.DeleteByQueryOverIndices<T>(query, indices, default(DeleteByQueryParameters));
		}
		public ConnectionStatus DeleteByQueryOverIndices<T>(string query, IEnumerable<string> indices, DeleteByQueryParameters parameters) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			if (indices == null || !indices.Any())
				throw new ArgumentNullException("indices");

			var indicesString = string.Join(",", indices.ToArray());
			var path = this.CreatePath(indicesString);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}
		public void DeleteByQueryOverIndicesAsync<T>(string query, IEnumerable<string> indices, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByQueryOverIndicesAsync<T>(query, indices, default(DeleteByQueryParameters), callback);
		}
		public void DeleteByQueryOverIndicesAsync<T>(string query, IEnumerable<string> indices, DeleteByQueryParameters parameters, Action<ConnectionStatus> callback) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			if (indices == null || !indices.Any())
				throw new ArgumentNullException("indices");

			var indicesString = string.Join(",", indices.ToArray());
			var path = this.CreatePath(indicesString);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			this._deleteToPathAsync(path, query, callback);
		}


		public ConnectionStatus DeleteByQueryOverIndices<T>(string query, IEnumerable<string> indices, IEnumerable<string> types) where T : class
		{
			return this.DeleteByQueryOverIndices<T>(query, indices, types, default(DeleteByQueryParameters));
		}
		public ConnectionStatus DeleteByQueryOverIndices<T>(string query, IEnumerable<string> indices, IEnumerable<string> types, DeleteByQueryParameters parameters) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			if (indices == null || !indices.Any())
				throw new ArgumentNullException("indices");
			if (types == null || !types.Any())
				throw new ArgumentNullException("types");

			var indicesString = string.Join(",", indices.ToArray());
			var typesString = string.Join(",", indices.ToArray());
			var path = this.CreatePath(indicesString, typesString);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}
		public void DeleteByQueryOverIndicesAsync<T>(string query, IEnumerable<string> indices, IEnumerable<string> types, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByQueryOverIndicesAsync<T>(query, indices, types, default(DeleteByQueryParameters), callback);
		}
		public void DeleteByQueryOverIndicesAsync<T>(string query, IEnumerable<string> indices, IEnumerable<string> types, 
			DeleteByQueryParameters parameters, Action<ConnectionStatus> callback) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			if (indices == null || !indices.Any())
				throw new ArgumentNullException("indices");
			if (types == null || !types.Any())
				throw new ArgumentNullException("types");

			var indicesString = string.Join(",", indices.ToArray());
			var typesString = string.Join(",", indices.ToArray());
			var path = this.CreatePath(indicesString, typesString);
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			this._deleteToPathAsync(path, query, callback);
		}

		public ConnectionStatus DeleteByQueryOverAll<T>(string query) where T : class
		{
			return this.DeleteByQueryOverAll<T>(query, default(DeleteByQueryParameters));
		}
		public ConnectionStatus DeleteByQueryOverAll<T>(string query, DeleteByQueryParameters parameters) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			var path = this.CreatePath("_all");
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}
		public void DeleteByQueryOverAllAsync<T>(string query, Action<ConnectionStatus> callback) where T : class
		{
			this.DeleteByQueryOverAllAsync<T>(query, default(DeleteByQueryParameters), callback);
		}
		public void DeleteByQueryOverAllAsync<T>(string query, DeleteByQueryParameters parameters, Action<ConnectionStatus> callback) where T : class
		{
			query.ThrowIfNullOrEmpty("query");
			var path = this.CreatePath("_all");
			if (parameters != null && !parameters.Equals(default(DeleteByQueryParameters)))
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			this._deleteToPathAsync(path, query, callback);
		}
		#endregion

		private ConnectionStatus _deleteToPath(string path)
		{
			path.ThrowIfNull("path");
			return this.Connection.DeleteSync(path);
		}
		private ConnectionStatus _deleteToPath(string path, string data)
		{
			path.ThrowIfNull("path");
			return this.Connection.DeleteSync(path, data);
		}
		private void _deleteToPathAsync(string path, Action<ConnectionStatus> callback)
		{
			path.ThrowIfNull("path");
			this.Connection.Delete(path, callback);
		}
		private void _deleteToPathAsync(string path, string data, Action<ConnectionStatus> callback)
		{
			path.ThrowIfNull("path");
			this.Connection.Delete(path, data, callback);
		}
    }
}
