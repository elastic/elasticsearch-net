// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	internal class TranslateSqlResponseBuilder : CustomResponseBuilderBase
	{
		public static TranslateSqlResponseBuilder Instance { get; } = new TranslateSqlResponseBuilder();

		public override object DeserializeResponse(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			response.Success
				? new TranslateSqlResponse { Result = builtInSerializer.Deserialize<ISearchRequest>(stream) }
				: new TranslateSqlResponse();

		public override async Task<object> DeserializeResponseAsync(
			ITransportSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		) =>
			response.Success
				? new TranslateSqlResponse
				{
					Result = await builtInSerializer.DeserializeAsync<ISearchRequest>(stream, ctx).ConfigureAwait(false)
				}
				: new TranslateSqlResponse();
	}
}
