// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class SourceRequestResponseBuilder<TDocument> : CustomResponseBuilderBase
	{
		public static SourceRequestResponseBuilder<TDocument> Instance { get; } = new SourceRequestResponseBuilder<TDocument>();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			if (response.Success)
			{
				if (builtInSerializer is IInternalSerializer internalSerializer &&
					internalSerializer.TryGetJsonFormatter(out var formatter))
				{
					var sourceSerializer = formatter.GetConnectionSettings().SourceSerializer;
					return new SourceResponse<TDocument> { Body = sourceSerializer.Deserialize<TDocument>(stream) };
				}

				return new SourceResponse<TDocument> { Body = builtInSerializer.Deserialize<TDocument>(stream) };
			}

			return new SourceResponse<TDocument>();
		}

		public override async Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default)
		{
			if (response.Success)
			{
				if (builtInSerializer is IInternalSerializer internalSerializer &&
					internalSerializer.TryGetJsonFormatter(out var formatter))
				{
					var sourceSerializer = formatter.GetConnectionSettings().SourceSerializer;
					return new SourceResponse<TDocument>
					{
						Body = await sourceSerializer.DeserializeAsync<TDocument>(stream, ctx).ConfigureAwait(false)
					};
				}

				return new SourceResponse<TDocument>
				{
					Body = await builtInSerializer.DeserializeAsync<TDocument>(stream, ctx).ConfigureAwait(false)
				};
			}

			return new SourceResponse<TDocument>();
		}
	}
}
