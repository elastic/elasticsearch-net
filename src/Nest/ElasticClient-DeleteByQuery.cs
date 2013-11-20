using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
  public partial class ElasticClient
  {
	  /// <summary>
	  /// Deletes all documents that match the query
	  /// </summary>
	  /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
	  /// <param name="parameters">Control routing/consistency and replication</param>
	  /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
	  public IDeleteResponse DeleteByQuery<T>(Func<RoutingQueryPathDescriptor<T>, BaseQuery> query, DeleteByQueryParameters parameters = null) where T : class
	  {
		  var descriptor = new RoutingQueryPathDescriptor<T>();
		  var bq = query(descriptor);
		  var stringQuery = this.Serialize(bq);
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
	  public IDeleteResponse DeleteByQuery(Func<RoutingQueryPathDescriptor, BaseQuery> query, DeleteByQueryParameters parameters = null)
	  {
		  var descriptor = new RoutingQueryPathDescriptor();
		  var bq = query(descriptor);
		  var stringQuery = this.Serialize(bq);
		  var path = this.PathResolver.GetDeleteByQueryPathForDynamic(descriptor, "_query");
		  if (parameters != null)
			  path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
		  return this._deleteToPath(path, stringQuery);
	  }

	  /// <summary>
	  /// Deletes all documents that match the query
	  /// </summary>
	  /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
	  /// <param name="parameters">Control routing/consistency and replication</param>
	  /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
	  public Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<RoutingQueryPathDescriptor<T>, BaseQuery> query, DeleteByQueryParameters parameters = null) where T : class
	  {
		  var descriptor = new RoutingQueryPathDescriptor<T>();
		  var bq = query(descriptor);
		  var stringQuery = this.Serialize(bq);
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
	  public Task<IDeleteResponse> DeleteByQueryAsync(Func<RoutingQueryPathDescriptor, BaseQuery> query, DeleteByQueryParameters parameters = null)
	  {
		  var descriptor = new RoutingQueryPathDescriptor();
		  var bq = query(descriptor);
		  var stringQuery = this.Serialize(bq);
		  var path = this.PathResolver.GetDeleteByQueryPathForDynamic(descriptor, "_query");
		  if (parameters != null)
			  path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
		  return this._deleteToPathAsync(path, stringQuery);
	  }

  }
}
