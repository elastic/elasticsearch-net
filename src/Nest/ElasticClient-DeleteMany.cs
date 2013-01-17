using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
  public partial class ElasticClient
  {
	  /// <summary>
	  /// Deletes all the objects by inferring its id in the default index and the inferred type for T
	  /// </summary>
	  public IBulkResponse DeleteMany<T>(IEnumerable<T> @objects) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects);
		  return this._deleteToBulkPath("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the default index and the inferred type for T
	  /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
	  /// </summary>
	  public IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> @objects) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects);
		  return this._deleteToBulkPath("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the default index and the inferred type for T
	  /// </summary>
	  /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
	  public IBulkResponse DeleteMany<T>(IEnumerable<T> @objects, SimpleBulkParameters bulkParameters) where T : class
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
	  public IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> @objects, SimpleBulkParameters bulkParameters) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects);
		  var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
		  return this._deleteToBulkPath(path, json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
	  /// </summary>
	  public IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index);
		  return this._deleteToBulkPath("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
	  /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
	  /// </summary>
	  public IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index);
		  return this._deleteToBulkPath("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
	  /// </summary>
	  /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
	  public IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
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
	  public IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index);
		  var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
		  return this._deleteToBulkPath(path, json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and type
	  /// </summary>
	  public IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index, string type) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index, type);
		  return this._deleteToBulkPath("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and type
	  /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
	  /// </summary>
	  public IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index, type);
		  return this._deleteToBulkPath("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and type
	  /// </summary>
	  /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
	  public IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
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
	  public IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index, type);
		  var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
		  return this._deleteToBulkPath(path, json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the default index and the inferred type for T
	  /// </summary>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects);
		  return this._deleteToBulkPathAsync("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the default index and the inferred type for T
	  /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
	  /// </summary>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects);
		  return this._deleteToBulkPathAsync("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the default index and the inferred type for T
	  /// </summary>
	  /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class
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
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects);
		  var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
		  return this._deleteToBulkPathAsync(path, json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
	  /// </summary>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index);
		  return this._deleteToBulkPathAsync("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
	  /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
	  /// </summary>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index);
		  return this._deleteToBulkPathAsync("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and the inferred type for T
	  /// </summary>
	  /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class
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
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index);
		  var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
		  return this._deleteToBulkPathAsync(path, json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and type
	  /// </summary>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index, string type) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index, type);
		  return this._deleteToBulkPathAsync("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and type
	  /// By wrapping the T in BulkParamaters<T> one can control each objects parameters
	  /// </summary>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index, type);
		  return this._deleteToBulkPathAsync("_bulk", json);
	  }

	  /// <summary>
	  /// Deletes all the objects by inferring its id in the specified index and type
	  /// </summary>
	  /// <param name="bulkParameters">allows you to control the replication and refresh behavior</param>
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class
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
	  public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class
	  {
		  var json = this.GenerateBulkDeleteCommand(@objects, index, type);
		  var path = this.PathResolver.AppendSimpleParametersToPath("_bulk", bulkParameters);
		  return this._deleteToBulkPathAsync(path, json);
	  }
  }
}
