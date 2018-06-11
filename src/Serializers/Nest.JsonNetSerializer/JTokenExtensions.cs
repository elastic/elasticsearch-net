using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer
{
	internal static class JTokenExtensions
	{
		/// <summary>
		/// Writes a <see cref="JToken"/> to a <see cref="MemoryStream"/> using <see cref="ConnectionSettingsAwareSerializerBase.ExpectedEncoding"/>
		/// </summary>
		public static MemoryStream ToStream(this JToken token)
		{
			var ms = new MemoryStream();
			using (var streamWriter = new StreamWriter(ms, ConnectionSettingsAwareSerializerBase.ExpectedEncoding, ConnectionSettingsAwareSerializerBase.DefaultBufferSize, leaveOpen: true))
			using (var writer = new JsonTextWriter(streamWriter))
			{
				token.WriteTo(writer);
				writer.Flush();
				ms.Position = 0;
				return ms;
			}
		}

		/// <summary>
		/// Writes a <see cref="JToken"/> asynchronously to a <see cref="MemoryStream"/> using <see cref="ConnectionSettingsAwareSerializerBase.ExpectedEncoding"/>
		/// </summary>
		public static async Task<MemoryStream> ToStreamAsync(this JToken token, CancellationToken cancellationToken = default(CancellationToken))
		{
			var ms = new MemoryStream();
			using (var streamWriter = new StreamWriter(ms, ConnectionSettingsAwareSerializerBase.ExpectedEncoding, ConnectionSettingsAwareSerializerBase.DefaultBufferSize, leaveOpen: true))
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
