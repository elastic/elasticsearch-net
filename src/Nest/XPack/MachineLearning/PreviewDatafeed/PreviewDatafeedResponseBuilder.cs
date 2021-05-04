// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class PreviewDatafeedResponseBuilder<TDocument> : CustomResponseBuilderBase where TDocument : class
	{
		public static PreviewDatafeedResponseBuilder<TDocument> Instance { get; } = new PreviewDatafeedResponseBuilder<TDocument>();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			response.Success
				? new PreviewDatafeedResponse<TDocument> { Data = builtInSerializer.Deserialize<IReadOnlyCollection<TDocument>>(stream) }
				: new PreviewDatafeedResponse<TDocument>();

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		) =>
			response.Success
				? new PreviewDatafeedResponse<TDocument>
				{
					Data = await builtInSerializer.DeserializeAsync<IReadOnlyCollection<TDocument>>(stream, ctx).ConfigureAwait(false)
				}
				: new PreviewDatafeedResponse<TDocument>();
	}
}
