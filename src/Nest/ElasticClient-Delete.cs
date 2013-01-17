using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
  public partial class ElasticClient
  {
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
