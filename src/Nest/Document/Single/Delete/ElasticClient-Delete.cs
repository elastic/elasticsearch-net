using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	//TODO I Deleted DeleteExtensions, when we introduced Document as a parameter folks can do 

	//.Delete<T>(id)
	//.Delete(Document.Id<T>(2))
	//.Delete<T>(T object)
	//.Delete(Document.Infer<T>(2))


	//Delete(Document.Index("a").Type("x").Id("1"), s=>s)
	//Delete(Document.Infer(doc), s=>s)
	//Delete(Document.Index<T>().Type<TOptional>().Id(2), s=>s)
	public partial interface IElasticClient
	{
		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="deleteSelector">Describe the delete operation, i.e type/index/id</param>
		IDeleteResponse Delete<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> deleteSelector = null) where T : class;

		/// <inheritdoc/>
		IDeleteResponse Delete(IDeleteRequest deleteRequest);

		/// <inheritdoc/>
		Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> deleteSelector = null) where T : class;

		/// <inheritdoc/>
		Task<IDeleteResponse> DeleteAsync(IDeleteRequest deleteRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteResponse Delete<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> deleteSelector = null) where T : class =>
			this.Delete(deleteSelector.InvokeOrDefault(new DeleteDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IDeleteResponse Delete(IDeleteRequest deleteRequest) => 
			this.Dispatcher.Dispatch<IDeleteRequest, DeleteRequestParameters, DeleteResponse>(
				deleteRequest,
				(p, d) => this.LowLevelDispatch.DeleteDispatch<DeleteResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> deleteSelector = null) where T : class => 
			this.DeleteAsync(deleteSelector.InvokeOrDefault(new DeleteDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<IDeleteResponse> DeleteAsync(IDeleteRequest deleteRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteRequest, DeleteRequestParameters, DeleteResponse, IDeleteResponse>(
				deleteRequest,
				(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<DeleteResponse>(p)
			);
	}
}