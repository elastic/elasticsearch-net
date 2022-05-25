// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using System.Threading;

namespace Elastic.Clients.Elasticsearch
{
	public partial class ElasticsearchClient
	{
		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, CancellationToken cancellationToken = default)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}
	}
}
