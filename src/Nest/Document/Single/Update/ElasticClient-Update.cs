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
		/// <typeparam name="T">The type to describe the document to be updated</typeparam>
		/// <param name="updateSelector">a descriptor that describes the update operation</param>
		IUpdateResponse Update<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class;

		/// <inheritdoc/>
		IUpdateResponse Update<T>(IUpdateRequest<T, T> updateRequest) where T : class;

		/// <inheritdoc/>
		IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class;

		/// <inheritdoc/>
		IUpdateResponse Update<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T>(IUpdateRequest<T, T> updateRequest) where T : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class;
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateResponse Update<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class => 
			this.Update<T, T>(updateSelector);

		/// <inheritdoc/>
		public IUpdateResponse Update<T>(IUpdateRequest<T, T> updateSelector) where T : class => 
			this.Update<T, T>(updateSelector);

		/// <inheritdoc/>
		public IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class => 
			this.Update(updateSelector?.Invoke(new UpdateDescriptor<T, K>()));

		/// <inheritdoc/>
		public IUpdateResponse Update<T, K>(IUpdateRequest<T, K> updateSelector)
			where T : class
			where K : class => 
			this.Dispatcher.Dispatch<IUpdateRequest<T, K>, UpdateRequestParameters, UpdateResponse>(
				updateSelector,
				(p, d) => this.LowLevelDispatch.UpdateDispatch<UpdateResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector)
			where T : class => 
			this.UpdateAsync<T, T>(updateSelector);

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<T>(IUpdateRequest<T, T> updateRequest)
			where T : class => 
			this.UpdateAsync<T, T>(updateRequest);

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class => 
			this.UpdateAsync(updateSelector?.Invoke(new UpdateDescriptor<T, K>()));

		/// <inheritdoc/>
		public Task<IUpdateResponse> UpdateAsync<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class => 
			this.Dispatcher.DispatchAsync<IUpdateRequest<T, K>, UpdateRequestParameters, UpdateResponse, IUpdateResponse>(
				updateRequest,
				this.LowLevelDispatch.UpdateDispatchAsync<UpdateResponse>
			);
	}
}