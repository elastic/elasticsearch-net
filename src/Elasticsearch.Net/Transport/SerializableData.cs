using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
		public static implicit operator SerializableData<T>(T serialiableData) => new SerializableData<T>(serialiableData);

		public override void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			var stream = writableStream;
			MemoryStream ms = null;
			if (this.DisableDirectStreaming ?? settings.DisableDirectStreaming)
			{
				ms = new MemoryStream();
				stream = ms;
			}
			settings.RequestResponseSerializer.Serialize(this._serializable, stream, indent);
			if (ms != null)
			{
				ms.Position = 0;
				ms.CopyTo(writableStream, BufferSize);
			}
			if (this.Type != 0)
				this.WrittenBytes = ms?.ToArray();
		}

		public override async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
		{
			var indent = settings.PrettyJson ? SerializationFormatting.Indented : SerializationFormatting.None;
			var stream = writableStream;
			MemoryStream ms = null;
			if (this.DisableDirectStreaming ?? settings.DisableDirectStreaming)
			{
				ms = new MemoryStream();
				stream = ms;
			}
			await settings.RequestResponseSerializer.SerializeAsync(this._serializable, stream, indent, cancellationToken).ConfigureAwait(false);
			if (ms != null)
			{
				ms.Position = 0;
				await ms.CopyToAsync(writableStream, BufferSize, cancellationToken).ConfigureAwait(false);
			}
			if (this.Type != 0)
				this.WrittenBytes = ms?.ToArray();
		}
	}
}
