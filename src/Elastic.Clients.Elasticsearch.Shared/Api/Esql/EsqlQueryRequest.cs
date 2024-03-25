// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_STACK

using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Esql;

internal sealed class EsqlResponseBuilder : CustomResponseBuilder
{
	public override object DeserializeResponse(Serializer serializer, ApiCallDetails response, Stream stream)
	{
		var bytes = stream switch
		{
			MemoryStream ms => ms.ToArray(),
			_ => BytesFromStream(stream)
		};

		return new EsqlQueryResponse { Data = bytes };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static byte[] BytesFromStream(Stream stream)
		{
			using var binaryReader = new BinaryReader(stream);

			return binaryReader.ReadBytes((int)stream.Length);
		}
	}

	public override async Task<object> DeserializeResponseAsync(Serializer serializer, ApiCallDetails response, Stream stream,
		CancellationToken ctx = new CancellationToken())
	{
		var bytes = stream switch
		{
			MemoryStream ms => ms.ToArray(),
			_ => await BytesFromStreamAsync(stream, ctx).ConfigureAwait(false)
		};

		return new EsqlQueryResponse { Data = bytes };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static async Task<byte[]> BytesFromStreamAsync(Stream stream, CancellationToken cancellationToken)
		{
			using var ms = new MemoryStream();
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
			await stream.CopyToAsync(ms, cancellationToken).ConfigureAwait(false);
#else
			await stream.CopyToAsync(ms).ConfigureAwait(false);
#endif

			return ms.ToArray();
		}
	}
}

public sealed partial class EsqlQueryRequestParameters
{
	private static readonly EsqlResponseBuilder ResponseBuilder = new();

	public EsqlQueryRequestParameters() => CustomResponseBuilder = ResponseBuilder;
}

#endif
