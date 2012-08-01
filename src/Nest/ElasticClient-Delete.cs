using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		#region Delete by passing an object

		/// <summary>
		/// Synchronously delete the object in the inferred type for T in the default index specified in the client settings
		/// </summary>
		public ConnectionStatus Delete<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			return this._deleteToPath(path);
		}
		/// <summary>
		/// Synchronously delete the object in the inferred type for T in specified index
		/// </summary>
		public ConnectionStatus Delete<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._deleteToPath(path);
		}
		/// <summary>
		/// Synchronously delete the object in specified type in the specified index
		/// </summary>
		public ConnectionStatus Delete<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this._deleteToPath(path);
		}
		/// <summary>
		/// Synchronously delete the object in the inferred type for T in the default index specified in the client settings
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>	
		public ConnectionStatus Delete<T>(T @object, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		/// <summary>
		/// Synchronously delete the object in the inferred type for T in the specified index.
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public ConnectionStatus Delete<T>(T @object, string index, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}
		/// <summary>
		/// Synchronously delete the object in the inferred type for T in the specified index.
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public ConnectionStatus Delete<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Asynchronously delete the object in the inferred type for T in the default index specified in the client settings
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(T @object) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			return this._deleteToPathAsync(path);
		}
		/// <summary>
		/// Asynchronously delete the object in the inferred type for T in specified index
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(T @object, string index) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			return this._deleteToPathAsync(path);
		}
		/// <summary>
		/// Asynchronously delete the object in specified type in the specified index
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(T @object, string index, string type) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			return this._deleteToPathAsync(path);
		}
		/// Asynchronously delete the object in the inferred type for T in the default index specified in the client settings
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>	
		public Task<ConnectionStatus> DeleteAsync<T>(T @object, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPathAsync(path);
		}
		/// <summary>
		/// Asynchronously delete the object in the inferred type for T in the specified index.
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public Task<ConnectionStatus> DeleteAsync<T>(T @object, string index, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPathAsync(path);
		}
		/// <summary>
		/// Asynchronously delete the object in the inferred type for T in the specified index.
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public Task<ConnectionStatus> DeleteAsync<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class
		{
			var path = this.CreatePathFor<T>(@object, index, type);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPathAsync(path);
		}
		#endregion

		#region Delete by passing an id

		/// <summary>
		/// Synchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		public ConnectionStatus DeleteById<T>(int id) where T : class
		{
			return this.DeleteById<T>(id.ToString());
		}

		/// <summary>
		/// Synchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		public ConnectionStatus DeleteById<T>(string id) where T : class
		{
			var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
			var path = this.CreatePath(index, typeName, id);
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Synchronously deletes a document by id in the specified index and type
		/// </summary>
		public ConnectionStatus DeleteById(string index, string type, string id)
		{
			var path = this.CreatePath(index, type, id);
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Synchronously deletes a document by id in the specified index and type
		/// </summary>
		public ConnectionStatus DeleteById(string index, string type, int id)
		{
			var path = this.CreatePath(index, type, id.ToString());
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Synchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public ConnectionStatus DeleteById<T>(int id, DeleteParameters deleteParameters) where T : class
		{
			return this.DeleteById<T>(id.ToString(), deleteParameters);
		}

		/// <summary>
		/// Synchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public ConnectionStatus DeleteById<T>(string id, DeleteParameters deleteParameters) where T : class
		{
			var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
			var path = this.CreatePath(index, typeName, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Synchronously deletes a document by id in the specified index and type
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public ConnectionStatus DeleteById(string index, string type, string id, DeleteParameters deleteParameters)
		{
			var path = this.CreatePath(index, type, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Synchronously deletes a document by id in the specified index and type
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public ConnectionStatus DeleteById(string index, string type, int id, DeleteParameters deleteParameters)
		{
			var path = this.CreatePath(index, type, id.ToString());
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPath(path);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		public Task<ConnectionStatus> DeleteByIdAsync<T>(int id) where T : class
		{
			return this.DeleteByIdAsync<T>(id.ToString());
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		public Task<ConnectionStatus> DeleteByIdAsync<T>(string id) where T : class
		{
			var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
			var path = this.CreatePath(index, typeName, id);
			return this._deleteToPathAsync(path);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the specified index and type
		/// </summary>
		public Task<ConnectionStatus> DeleteByIdAsync(string index, string type, string id)
		{
			var path = this.CreatePath(index, type, id);
			return this._deleteToPathAsync(path);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the specified index and type
		/// </summary>
		public Task<ConnectionStatus> DeleteByIdAsync(string index, string type, int id)
		{
			var path = this.CreatePath(index, type, id.ToString());
			return this._deleteToPathAsync(path);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public Task<ConnectionStatus> DeleteByIdAsync<T>(int id, DeleteParameters deleteParameters) where T : class
		{
			return this.DeleteByIdAsync<T>(id.ToString(), deleteParameters);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the default index and the inferred typename for T
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public Task<ConnectionStatus> DeleteByIdAsync<T>(string id, DeleteParameters deleteParameters) where T : class
		{
			var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
			var path = this.CreatePath(index, typeName, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPathAsync(path);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the specified index and type
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public Task<ConnectionStatus> DeleteByIdAsync(string index, string type, string id, DeleteParameters deleteParameters)
		{
			var path = this.CreatePath(index, type, id);
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPathAsync(path);
		}

		/// <summary>
		/// Asynchronously deletes a document by id in the specified index and type
		/// </summary>
		/// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
		public Task<ConnectionStatus> DeleteByIdAsync(string index, string type, int id, DeleteParameters deleteParameters)
		{
			var path = this.CreatePath(index, type, id.ToString());
			path = this.AppendParametersToPath(path, deleteParameters);
			return this._deleteToPathAsync(path);
		}

		#endregion

		#region Delete by passing an IEnumerable of objects

		//TODO soooooo many overloads, need to come up with a better way to pass index, type combinations.

		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// </summary>
		public ConnectionStatus Delete<T>(IEnumerable<T> @objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> @objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public ConnectionStatus Delete<T>(IEnumerable<T> @objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> @objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// </summary>
		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}

		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// </summary>
		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			return this.Connection.PostSync("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public ConnectionStatus Delete<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public ConnectionStatus Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.PostSync(path, json);
		}


		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<T> objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the default index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}

		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<T> objects, string index) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and the inferred type for T
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}

		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<T> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			return this.Connection.Post("_bulk", json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}
		/// <summary>
		/// Deletes all the objects by inferring its id in the specified index and type
		/// By wrapping the T in BulkParamaters<T> one can control each objects parameters
		/// </summary>
		/// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
		public Task<ConnectionStatus> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class
		{
			var json = this.GenerateBulkDeleteCommand(@objects, index, type);
			var path = this.AppendSimpleParametersToPath("_bulk", bulkParameters);
			return this.Connection.Post(path, json);
		}

		#endregion

		#region Delete by passing a query string
		/// <summary>
		/// Deletes all documents that match the query
		/// </summary>
		/// <param name="query">QueryPathDescriptor also allows you to control which indices and types are affected</param>
		/// <param name="parameters">Control routing/consistency and replication</param>
		/// <returns>ConnectionStatus, check .IsValid to validate success</returns>
		public ConnectionStatus DeleteByQuery<T>(Action<QueryPathDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class
		{
			var descriptor = new QueryPathDescriptor<T>();
			query(descriptor);
			var stringQuery = this.Serialize(descriptor);
			var path = this.GetPathForTyped(descriptor);
			if (parameters != null)
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, stringQuery);
		}
		/// <summary>
		/// Deletes all documents that match the query, without specifying T the return documents is an IEnumerable<dynamic>
		/// </summary>
		/// <param name="query">QueryPathDescriptor also allows you to control which indices and types are affected</param>
		/// <param name="parameters">Control routing/consistency and replication</param>
		/// <returns>ConnectionStatus, check .IsValid to validate success</returns>
		public ConnectionStatus DeleteByQuery(Action<QueryPathDescriptor> query, DeleteByQueryParameters parameters = null)
		{
			var descriptor = new QueryPathDescriptor();
			query(descriptor);
			var stringQuery = this.Serialize(descriptor);
			var path = this.GetPathForDynamic(descriptor);
			if (parameters != null)
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, stringQuery);
		}
		/// <summary>
		/// Deletes all documents that match the string query. OBSOLETE
		/// </summary>
		/// <param name="query">QueryPathDescriptor also allows you to control which indices and types are affected</param>
		/// <param name="parameters">Control routing/consistency and replication</param>
		/// <returns>ConnectionStatus, check .IsValid to validate success</returns>
		[Obsolete("Deprecated but will never be removed. Found a bug in the DSL? https://github.com/Mpdreamz/NEST/issues")]
		public ConnectionStatus DeleteByQuery(string query, DeleteByQueryParameters parameters = null)
		{
			var descriptor = new QueryPathDescriptor();
			var path = this.GetPathForDynamic(descriptor);
			if (parameters != null)
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPath(path, query);
		}

		/// <summary>
		/// Deletes all documents that match the query
		/// </summary>
		/// <param name="query">QueryPathDescriptor also allows you to control which indices and types are affected</param>
		/// <param name="parameters">Control routing/consistency and replication</param>
		/// <returns>ConnectionStatus, check .IsValid to validate success</returns>
		public Task<ConnectionStatus> DeleteByQueryAsync<T>(Action<QueryPathDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class
		{
			var descriptor = new QueryPathDescriptor<T>();
			query(descriptor);
			var stringQuery = this.Serialize(descriptor);
			var path = this.GetPathForTyped(descriptor);
			if (parameters != null)
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPathAsync(path, stringQuery);
		}
		/// <summary>
		/// Deletes all documents that match the query, without specifying T the return documents is an IEnumerable<dynamic>
		/// </summary>
		/// <param name="query">QueryPathDescriptor also allows you to control which indices and types are affected</param>
		/// <param name="parameters">Control routing/consistency and replication</param>
		/// <returns>ConnectionStatus, check .IsValid to validate success</returns>
		public Task<ConnectionStatus> DeleteByQueryAsync(Action<QueryPathDescriptor> query, DeleteByQueryParameters parameters = null)
		{
			var descriptor = new QueryPathDescriptor();
			query(descriptor);
			var stringQuery = this.Serialize(descriptor);
			var path = this.GetPathForDynamic(descriptor);
			if (parameters != null)
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPathAsync(path, stringQuery);
		}
		/// <summary>
		/// Deletes all documents that match the string query. OBSOLETE
		/// </summary>
		/// <param name="query">QueryPathDescriptor also allows you to control which indices and types are affected</param>
		/// <param name="parameters">Control routing/consistency and replication</param>
		/// <returns>ConnectionStatus, check .IsValid to validate success</returns>
		[Obsolete("Deprecated but will never be removed. Found a bug in the DSL? https://github.com/Mpdreamz/NEST/issues")]
		public Task<ConnectionStatus> DeleteByQueryAsync(string query, DeleteByQueryParameters parameters = null)
		{
			var descriptor = new QueryPathDescriptor();
			var path = this.GetPathForDynamic(descriptor);
			if (parameters != null)
				path = this.AppendDeleteByQueryParametersToPath(path, parameters);
			return this._deleteToPathAsync(path, query);
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
		private Task<ConnectionStatus> _deleteToPathAsync(string path)
		{
			path.ThrowIfNull("path");
			return this.Connection.Delete(path);
				
		}
		private Task<ConnectionStatus> _deleteToPathAsync(string path, string data)
		{
			path.ThrowIfNull("path");
			return this.Connection.Delete(path, data);
				
		}
	}
}
