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
    public IDeleteResponse Delete<T>(T @object) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object);
      return this._deleteToPath(path);
    }
    /// <summary>
    /// Synchronously delete the object in the inferred type for T in specified index
    /// </summary>
    public IDeleteResponse Delete<T>(T @object, string index) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index);
      return this._deleteToPath(path);
    }
    /// <summary>
    /// Synchronously delete the object in specified type in the specified index
    /// </summary>
    public IDeleteResponse Delete<T>(T @object, string index, string type) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index, type);
      return this._deleteToPath(path);
    }
    /// <summary>
    /// Synchronously delete the object in the inferred type for T in the default index specified in the client settings
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>	
    public IDeleteResponse Delete<T>(T @object, DeleteParameters deleteParameters) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPath(path);
    }
    /// <summary>
    /// Synchronously delete the object in the inferred type for T in the specified index.
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public IDeleteResponse Delete<T>(T @object, string index, DeleteParameters deleteParameters) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPath(path);
    }
    /// <summary>
    /// Synchronously delete the object in the inferred type for T in the specified index.
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public IDeleteResponse Delete<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index, type);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Asynchronously delete the object in the inferred type for T in the default index specified in the client settings
    /// </summary>
    public Task<IDeleteResponse> DeleteAsync<T>(T @object) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object);
      return this._deleteToPathAsync(path);
    }
    /// <summary>
    /// Asynchronously delete the object in the inferred type for T in specified index
    /// </summary>
    public Task<IDeleteResponse> DeleteAsync<T>(T @object, string index) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index);
      return this._deleteToPathAsync(path);
    }
    /// <summary>
    /// Asynchronously delete the object in specified type in the specified index
    /// </summary>
    public Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, string type) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index, type);
      return this._deleteToPathAsync(path);
    }
    /// Asynchronously delete the object in the inferred type for T in the default index specified in the client settings
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>	
    public Task<IDeleteResponse> DeleteAsync<T>(T @object, DeleteParameters deleteParameters) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPathAsync(path);
    }
    /// <summary>
    /// Asynchronously delete the object in the inferred type for T in the specified index.
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, DeleteParameters deleteParameters) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPathAsync(path);
    }
    /// <summary>
    /// Asynchronously delete the object in the inferred type for T in the specified index.
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class
    {
      var path = this.PathResolver.CreatePathFor<T>(@object, index, type);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPathAsync(path);
    }
    #endregion

    #region Delete by passing an id

    /// <summary>
    /// Synchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    public IDeleteResponse DeleteById<T>(int id) where T : class
    {
      return this.DeleteById<T>(id.ToString());
    }

    /// <summary>
    /// Synchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    public IDeleteResponse DeleteById<T>(string id) where T : class
    {
      var index = this.IndexNameResolver.GetIndexForType<T>();
      index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

      var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
      var path = this.PathResolver.CreateIndexTypeIdPath(index, typeName, id);
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Synchronously deletes a document by id in the specified index and type
    /// </summary>
    public IDeleteResponse DeleteById(string index, string type, string id)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id);
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Synchronously deletes a document by id in the specified index and type
    /// </summary>
    public IDeleteResponse DeleteById(string index, string type, int id)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id.ToString());
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Synchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public IDeleteResponse DeleteById<T>(int id, DeleteParameters deleteParameters) where T : class
    {
      return this.DeleteById<T>(id.ToString(), deleteParameters);
    }

    /// <summary>
    /// Synchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public IDeleteResponse DeleteById<T>(string id, DeleteParameters deleteParameters) where T : class
    {
      var index = this.IndexNameResolver.GetIndexForType<T>();
      index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

      var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
      var path = this.PathResolver.CreateIndexTypeIdPath(index, typeName, id);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Synchronously deletes a document by id in the specified index and type
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public IDeleteResponse DeleteById(string index, string type, string id, DeleteParameters deleteParameters)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Synchronously deletes a document by id in the specified index and type
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public IDeleteResponse DeleteById(string index, string type, int id, DeleteParameters deleteParameters)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id.ToString());
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPath(path);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    public Task<IDeleteResponse> DeleteByIdAsync<T>(int id) where T : class
    {
      return this.DeleteByIdAsync<T>(id.ToString());
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    public Task<IDeleteResponse> DeleteByIdAsync<T>(string id) where T : class
    {
      var index = this.IndexNameResolver.GetIndexForType<T>();
      index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

      var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
      var path = this.PathResolver.CreateIndexTypeIdPath(index, typeName, id);
      return this._deleteToPathAsync(path);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the specified index and type
    /// </summary>
    public Task<IDeleteResponse> DeleteByIdAsync(string index, string type, string id)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id);
      return this._deleteToPathAsync(path);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the specified index and type
    /// </summary>
    public Task<IDeleteResponse> DeleteByIdAsync(string index, string type, int id)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id.ToString());
      return this._deleteToPathAsync(path);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public Task<IDeleteResponse> DeleteByIdAsync<T>(int id, DeleteParameters deleteParameters) where T : class
    {
      return this.DeleteByIdAsync<T>(id.ToString(), deleteParameters);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the default index and the inferred typename for T
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public Task<IDeleteResponse> DeleteByIdAsync<T>(string id, DeleteParameters deleteParameters) where T : class
    {
      var index = this.IndexNameResolver.GetIndexForType<T>();
      index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

      var typeName = this.TypeNameResolver.GetTypeNameFor<T>();
      var path = this.PathResolver.CreateIndexTypeIdPath(index, typeName, id);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPathAsync(path);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the specified index and type
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public Task<IDeleteResponse> DeleteByIdAsync(string index, string type, string id, DeleteParameters deleteParameters)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id);
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPathAsync(path);
    }

    /// <summary>
    /// Asynchronously deletes a document by id in the specified index and type
    /// </summary>
    /// <param name="deleteParameters">Allows you to pass in additional delete parameters such as version and routing</param>
    public Task<IDeleteResponse> DeleteByIdAsync(string index, string type, int id, DeleteParameters deleteParameters)
    {
      var path = this.PathResolver.CreateIndexTypeIdPath(index, type, id.ToString());
      path = this.PathResolver.AppendParametersToPath(path, deleteParameters);
      return this._deleteToPathAsync(path);
    }

    #endregion

    #region Delete by passing an IEnumerable of objects
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// </summary>
    public IBulkResponse Delete<T>(IEnumerable<T> @objects) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      return this._deleteToBulkPath("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    public IBulkResponse Delete<T>(IEnumerable<BulkParameters<T>> @objects) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      return this._deleteToBulkPath("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public IBulkResponse Delete<T>(IEnumerable<T> @objects, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPath(path, json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public IBulkResponse Delete<T>(IEnumerable<BulkParameters<T>> @objects, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPath(path, json);
    }

    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// </summary>
    public IBulkResponse Delete<T>(IEnumerable<T> objects, string index) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      return this._deleteToBulkPath("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    public IBulkResponse Delete<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      return this._deleteToBulkPath("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public IBulkResponse Delete<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPath(path, json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public IBulkResponse Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPath(path, json);
    }

    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// </summary>
    public IBulkResponse Delete<T>(IEnumerable<T> objects, string index, string type) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      return this._deleteToBulkPath("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    public IBulkResponse Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      return this._deleteToBulkPath("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public IBulkResponse Delete<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPath(path, json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public IBulkResponse Delete<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPath(path, json);
    }


    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// </summary>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<T> objects) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      return this._deleteToBulkPathAsync("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      return this._deleteToBulkPathAsync("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPathAsync(path, json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the default index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPathAsync(path, json);
    }

    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// </summary>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<T> objects, string index) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      return this._deleteToBulkPathAsync("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      return this._deleteToBulkPathAsync("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPathAsync(path, json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPathAsync(path, json);
    }

    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// </summary>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<T> objects, string index, string type) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      return this._deleteToBulkPathAsync("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      return this._deleteToBulkPathAsync("_bulk", json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPathAsync(path, json);
    }
    /// <summary>
    /// Deletes all the objects by inferring its id in the specified index and type
    /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
    /// </summary>
    /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
    public Task<IBulkResponse> DeleteAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class
    {
      var json = this.GenerateBulkDeleteCommand(@objects, index, type);
      var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
      return this._deleteToBulkPathAsync(path, json);
    }

    #endregion

    #region Delete by passing a query string
    /// <summary>
    /// Deletes all documents that match the query
    /// </summary>
    /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
    /// <param name="parameters">Control routing/consistency and replication</param>
    /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
    public IDeleteResponse DeleteByQuery<T>(Action<RoutingQueryPathDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class
    {
      var descriptor = new RoutingQueryPathDescriptor<T>();
      query(descriptor);
	  var stringQuery = this.Serialize(descriptor);
      var path = this.PathResolver.GetPathForTyped(descriptor, "_query");
      if (parameters != null)
        path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
      return this._deleteToPath(path, stringQuery);
    }
    /// <summary>
    /// Deletes all documents that match the query, without specifying T the return documents is an IEnumerable<dynamic>
    /// </summary>
    /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
    /// <param name="parameters">Control routing/consistency and replication</param>
    /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
    public IDeleteResponse DeleteByQuery(Action<RoutingQueryPathDescriptor> query, DeleteByQueryParameters parameters = null)
    {
      var descriptor = new RoutingQueryPathDescriptor();
      query(descriptor);
	  var stringQuery = this.Serialize(descriptor);
      var path = this.PathResolver.GetPathForDynamic(descriptor, "_query");
      if (parameters != null)
        path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
      return this._deleteToPath(path, stringQuery);
    }
    /// <summary>
    /// Deletes all documents that match the string query.
    /// </summary>
    /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
    /// <param name="parameters">Control routing/consistency and replication</param>
    /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
    public IDeleteResponse DeleteByQueryRaw(string query, DeleteByQueryParameters parameters = null)
    {
      var descriptor = new RoutingQueryPathDescriptor();
      var path = this.PathResolver.GetPathForDynamic(descriptor, "_query");
      if (parameters != null)
        path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
      return this._deleteToPath(path, query);
    }

    /// <summary>
    /// Deletes all documents that match the query
    /// </summary>
    /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
    /// <param name="parameters">Control routing/consistency and replication</param>
    /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
    public Task<IDeleteResponse> DeleteByQueryAsync<T>(Action<RoutingQueryPathDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class
    {
      var descriptor = new RoutingQueryPathDescriptor<T>();
      query(descriptor);
	  var stringQuery = this.Serialize(descriptor);
      var path = this.PathResolver.GetPathForTyped(descriptor, "_query");
      if (parameters != null)
        path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
      return this._deleteToPathAsync(path, stringQuery);
    }
    /// <summary>
    /// Deletes all documents that match the query, without specifying T the return documents is an IEnumerable<dynamic>
    /// </summary>
    /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
    /// <param name="parameters">Control routing/consistency and replication</param>
    /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
    public Task<IDeleteResponse> DeleteByQueryAsync(Action<RoutingQueryPathDescriptor> query, DeleteByQueryParameters parameters = null)
    {
      var descriptor = new RoutingQueryPathDescriptor();
      query(descriptor);
	  var stringQuery = this.Serialize(descriptor);
      var path = this.PathResolver.GetPathForDynamic(descriptor, "_query");
      if (parameters != null)
        path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
      return this._deleteToPathAsync(path, stringQuery);
    }
    /// <summary>
    /// Deletes all documents that match the string query.
    /// </summary>
    /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
    /// <param name="parameters">Control routing/consistency and replication</param>
    /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
    public Task<IDeleteResponse> DeleteByQueryRawAsync(string query, DeleteByQueryParameters parameters = null)
    {
      var descriptor = new RoutingQueryPathDescriptor();
      var path = this.PathResolver.GetPathForDynamic(descriptor, "_query");
      if (parameters != null)
        path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
      return this._deleteToPathAsync(path, query);
    }
    #endregion

    private IDeleteResponse _deleteToPath(string path)
    {
      path.ThrowIfNull("path");
      var status = this.Connection.DeleteSync(path);
      return this.ToParsedResponse<DeleteResponse>(status);
    }

    private IDeleteResponse _deleteToPath(string path, string data)
    {
      path.ThrowIfNull("path");
      var status = this.Connection.DeleteSync(path, data);
      return this.ToParsedResponse<DeleteResponse>(status);
    }

    private IBulkResponse _deleteToBulkPath(string path, string data)
    {
      path.ThrowIfNull("path");
      var status = this.Connection.PostSync(path, data);
      return this.ToParsedResponse<BulkResponse>(status);
    }


    private Task<IDeleteResponse> _deleteToPathAsync(string path)
    {
      path.ThrowIfNull("path");
      var task = this.Connection.Delete(path);
      return task.ContinueWith<IDeleteResponse>(t => this.ToParsedResponse<DeleteResponse>(t.Result));
    }

    private Task<IDeleteResponse> _deleteToPathAsync(string path, string data)
    {
      path.ThrowIfNull("path");
      var task = this.Connection.Delete(path, data);
      return task.ContinueWith<IDeleteResponse>(t => this.ToParsedResponse<DeleteResponse>(t.Result));
    }
    private Task<IBulkResponse> _deleteToBulkPathAsync(string path, string data)
    {
      path.ThrowIfNull("path");
      var task = this.Connection.Post(path, data);
      return task.ContinueWith<IBulkResponse>(t => this.ToParsedResponse<BulkResponse>(t.Result));
    }
  }
}
