using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The update API allows to update a document based on a script provided. 
		/// <para>The operation gets the document (collocated with the shard) from the index, runs the script 
		/// (with optional script language and parameters), and index back the result 
		/// (also allows to delete, or ignore the operation). </para>
		/// <para>It uses versioning to make sure no updates have happened during the "get" and "reindex".</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-update.html
		/// </summary>
		/// <typeparam name="TDocument">The type to describe the document to be updated</typeparam>
		/// <param name="updateSelector">a descriptor that describes the update operation</param>
		IUpdateResponse Update<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> updateSelector) where TDocument : class;

		/// <inheritdoc/>
		IUpdateResponse Update<TDocument>(IUpdateRequest<TDocument, TDocument> updateRequest) where TDocument : class;

		/// <inheritdoc/>
		IUpdateResponse Update<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> updateSelector)
			where TDocument : class
			where TPartialDocument : class;

		/// <inheritdoc/>
		IUpdateResponse Update<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> updateRequest)
			where TDocument : class
			where TPartialDocument : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> updateSelector) where TDocument : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<TDocument>(IUpdateRequest<TDocument, TDocument> updateRequest) where TDocument : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> updateSelector)
			where TDocument : class
			where TPartialDocument : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> updateRequest)
			where TDocument : class
			where TPartialDocument : class;
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateResponse Update<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> updateSelector) where TDocument : class => 
			this.Update<TDocument, TDocument>(documentPath, updateSelector);

		/// <inheritdoc/>
		public IUpdateResponse Update<TDocument>(IUpdateRequest<TDocument, TDocument> updateSelector) where TDocument : class => 
			this.Update<TDocument, TDocument>(updateSelector);

		/// <inheritdoc/>
		public IUpdateResponse Update<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> updateSelector)
			where TDocument : class
			where TPartialDocument : class => 
			this.Update(updateSelector?.Invoke(new UpdateDescriptor<TDocument, TPartialDocument>(documentPath)));

		/// <inheritdoc/>
		public IUpdateResponse Update<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> updateSelector)
			where TDocument : class
			where TPartialDocument : class => 
			this.Dispatcher.Dispatch<IUpdateRequest<TDocument, TPartialDocument>, UpdateRequestParameters, UpdateResponse>(
				updateSelector,
				(p, d) => this.LowLevelDispatch.UpdateDispatch<UpdateResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> updateSelector)
			where TDocument : class => 
			this.UpdateAsync<TDocument, TDocument>(documentPath, updateSelector);

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<TDocument>(IUpdateRequest<TDocument, TDocument> updateRequest)
			where TDocument : class => 
			this.UpdateAsync<TDocument, TDocument>(updateRequest);

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> updateSelector)
			where TDocument : class
			where TPartialDocument : class => 
			this.UpdateAsync(updateSelector?.Invoke(new UpdateDescriptor<TDocument, TPartialDocument>(documentPath)));

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> updateRequest)
			where TDocument : class
			where TPartialDocument : class => 
			this.Dispatcher.DispatchAsync<IUpdateRequest<TDocument, TPartialDocument>, UpdateRequestParameters, UpdateResponse, IUpdateResponse>(
				updateRequest,
				this.LowLevelDispatch.UpdateDispatchAsync<UpdateResponse>
			);
	}
}