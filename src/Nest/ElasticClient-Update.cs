using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateResponse Update<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class
		{
			return this.Update<T, T>(updateSelector);
		}
		
		/// <inheritdoc />
		public IUpdateResponse Update<T>(IUpdateRequest<T, T> updateSelector) where T : class
		{
			return this.Update<T, T>(updateSelector);
		}

		/// <inheritdoc />
		public IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class
		{
			return this.Dispatch<UpdateDescriptor<T, K>, UpdateRequestParameters, UpdateResponse>(
				updateSelector,
				(p, d) => this.RawDispatch.UpdateDispatch<UpdateResponse>(p, d)
			);
		}
		/// <inheritdoc />
		public IUpdateResponse Update<T, K>(IUpdateRequest<T, K> updateSelector)
			where T : class
			where K : class
		{
			return this.Dispatch<IUpdateRequest<T, K>, UpdateRequestParameters, UpdateResponse>(
				updateSelector,
				(p, d) => this.RawDispatch.UpdateDispatch<UpdateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector)
			where T : class
		{
			return this.UpdateAsync<T, T>(updateSelector);
		}

		/// <inheritdoc />
		public Task<IUpdateResponse> UpdateAsync<T>(IUpdateRequest<T, T> updateRequest)
			where T : class
		{
			return this.UpdateAsync<T, T>(updateRequest);
		}

		/// <inheritdoc />
		public Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class
		{
			return this.DispatchAsync<UpdateDescriptor<T, K>, UpdateRequestParameters, UpdateResponse, IUpdateResponse>(
				updateSelector,
				(p, d) => this.RawDispatch.UpdateDispatchAsync<UpdateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IUpdateResponse> UpdateAsync<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class
		{
			return this.DispatchAsync<IUpdateRequest<T, K>, UpdateRequestParameters, UpdateResponse, IUpdateResponse>(
				updateRequest,
				(p, d) => this.RawDispatch.UpdateDispatchAsync<UpdateResponse>(p, d)
			);
		}

	}
}