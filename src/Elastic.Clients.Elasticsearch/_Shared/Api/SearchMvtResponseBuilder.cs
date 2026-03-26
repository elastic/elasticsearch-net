// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal sealed class SearchMvtResponseBuilder : TypedResponseBuilder<SearchMvtResponse>
{
	protected override SearchMvtResponse? Build(ApiCallDetails apiCallDetails, BoundConfiguration boundConfiguration,
		Stream responseStream,
		string contentType, long contentLength)
	{
		var bytes = responseStream switch
		{
			MemoryStream ms => ms.ToArray(),
			_ => BytesFromStream(responseStream)
		};

		return new SearchMvtResponse { Data = bytes };

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static byte[] BytesFromStream(Stream stream)
		{
			using var binaryReader = new BinaryReader(stream);

			return binaryReader.ReadBytes((int)stream.Length);
		}
	}

	protected override async Task<SearchMvtResponse?> BuildAsync(ApiCallDetails apiCallDetails, BoundConfiguration boundConfiguration,
		Stream responseStream,
		string contentType, long contentLength, CancellationToken cancellationToken = default)
	{
		var bytes = responseStream switch
		{
			MemoryStream ms => ms.ToArray(),
			_ => await BytesFromStreamAsync(responseStream, cancellationToken).ConfigureAwait(false)
		};

		return new SearchMvtResponse { Data = bytes };

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
