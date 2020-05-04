// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public abstract class CustomResponseBuilderBase
	{
		public abstract object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream);

		public abstract Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default);
	}
}
