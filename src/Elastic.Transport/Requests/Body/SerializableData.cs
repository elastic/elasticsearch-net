// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Elasticsearch.Net.SerializationFormatting;

namespace Elasticsearch.Net
{
	public class SerializableData<T> : PostData, IPostData<T>
	{
		private readonly T _serializable;

		public SerializableData(T item)
		{
			Type = PostType.Serializable;
			_serializable = item;
		}

		public static implicit operator SerializableData<T>(T serializableData) => new SerializableData<T>(serializableData);

		public override void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			MemoryStream buffer = null;
			var stream = writableStream;
			BufferIfNeeded(settings, ref buffer, ref stream);

			var indent = settings.PrettyJson ? Indented : None;
			settings.RequestResponseSerializer.Serialize(_serializable, stream, indent);

			FinishStream(writableStream, buffer, settings);
		}

		public override async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
		{
			MemoryStream buffer = null;
            var stream = writableStream;
            BufferIfNeeded(settings, ref buffer, ref stream);

            var indent = settings.PrettyJson ? Indented : None;
			await settings.RequestResponseSerializer.SerializeAsync(_serializable, stream, indent, cancellationToken)
				.ConfigureAwait(false);

			await FinishStreamAsync(writableStream, buffer, settings, cancellationToken).ConfigureAwait(false);
		}
	}
}
