using System;
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
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe the delete operation, i.e type/index/id</param>
		IDeleteResponse Delete<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IDeleteResponse Delete(IDeleteRequest request);

		/// <inheritdoc/>
		Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<IDeleteResponse> DeleteAsync(IDeleteRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteResponse Delete<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector = null) where T : class =>
			this.Delete(selector.InvokeOrDefault(new DeleteDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IDeleteResponse Delete(IDeleteRequest request) => 
			this.Dispatcher.Dispatch<IDeleteRequest, DeleteRequestParameters, DeleteResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteDispatch<DeleteResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector = null) where T : class => 
			this.DeleteAsync(selector.InvokeOrDefault(new DeleteDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<IDeleteResponse> DeleteAsync(IDeleteRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteRequest, DeleteRequestParameters, DeleteResponse, IDeleteResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<DeleteResponse>(p)
			);
	}
}