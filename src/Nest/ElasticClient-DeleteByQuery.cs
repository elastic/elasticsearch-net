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
	  public IDeleteResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class
	  {
		  var descriptor = new DeleteByQueryDescriptor<T>();
		  var bq = query(descriptor);
		  var stringQuery = this.Serialize(bq);

		  //return this.RawDispatch.DeleteByQueryDispatch()

		  throw new NotImplementedException();
		  //var path = this.PathResolver.GetPathForTyped(descriptor, "_query");
		  //if (parameters != null)
			  //path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
		  //return this._deleteToPath(path, stringQuery);
	  }

	  /// <summary>
	  /// Deletes all documents that match the query
	  /// </summary>
	  /// <param name="query">RoutingQueryPathDescriptor also allows you to control which indices and types are affected</param>
	  /// <param name="parameters">Control routing/consistency and replication</param>
	  /// <returns>IDeleteResponse, check .IsValid to validate success</returns>
	  public Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class
	  {
		  var descriptor = new DeleteByQueryDescriptor<T>();
		  var bq = query(descriptor);
		  var stringQuery = this.Serialize(bq);

		  throw new NotImplementedException();

		  //var path = this.PathResolver.GetPathForTyped(descriptor, "_query");
		  //if (parameters != null)
		//	  path = this.PathResolver.AppendDeleteByQueryParametersToPath(path, parameters);
		  //return this._deleteToPathAsync(path, stringQuery);
	  }

  }
}
