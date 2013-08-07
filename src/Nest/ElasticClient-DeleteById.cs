using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
  public partial class ElasticClient
  {
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

		  var typeName = this.GetTypeNameFor<T>();
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

		  var typeName = this.GetTypeNameFor<T>();
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

		  var typeName = this.GetTypeNameFor<T>();
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

		  var typeName = this.GetTypeNameFor<T>();
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
  }
}
