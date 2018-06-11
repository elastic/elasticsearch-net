using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal static class JTokenExtensions
	{
		/// <summary>
		/// Writes a <see cref="JToken"/> to a <see cref="MemoryStream"/> using <see cref="InternalSerializer.ExpectedEncoding"/>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MemoryStream ToStream(
			this JToken token,
			IMemoryStreamFactory memoryStreamFactory = null)
		{
			var ms = memoryStreamFactory?.Create() ?? new MemoryStream();
			using (var streamWriter = new StreamWriter(ms, InternalSerializer.ExpectedEncoding, InternalSerializer.DefaultBufferSize, leaveOpen: true))
			using (var writer = new JsonTextWriter(streamWriter))
			{
				token.WriteTo(writer);
				writer.Flush();
				ms.Position = 0;
				return ms;
			}
		}

		/// <summary>
		/// Writes a <see cref="JToken"/> asynchronously to a <see cref="MemoryStream"/> using <see cref="InternalSerializer.ExpectedEncoding"/>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static async Task<MemoryStream> ToStreamAsync(
			this JToken token,
			IMemoryStreamFactory memoryStreamFactory = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			var ms = memoryStreamFactory?.Create() ?? new MemoryStream();
			using (var streamWriter = new StreamWriter(ms, InternalSerializer.ExpectedEncoding, InternalSerializer.DefaultBufferSize, leaveOpen: true))
			using (var writer = new JsonTextWriter(streamWriter))
			{
				await token.WriteToAsync(writer, cancellationToken).ConfigureAwait(false);
				await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
				ms.Position = 0;
				return ms;
			}
		}
	}
}
