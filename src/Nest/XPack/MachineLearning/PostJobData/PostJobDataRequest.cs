using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("ml.post_data.json")]
	[JsonFormatter(typeof(PostJobDataFormatter))]
	public partial interface IPostJobDataRequest
	{
		/// <summary>
		/// The job data.
		/// </summary>
		/// <remarks>
		/// The job must have a state of open to receive and process the data.
		/// </remarks>
		[IgnoreDataMember]
		IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc cref="IPostJobDataRequest" />
	public partial class PostJobDataRequest
	{
		/// <inheritdoc />
		public IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc cref="IPostJobDataRequest" />
	public partial class PostJobDataDescriptor
	{
		IEnumerable<object> IPostJobDataRequest.Data { get; set; }

		/// <inheritdoc cref="IPostJobDataRequest.Data" />
		public PostJobDataDescriptor Data(IEnumerable<object> data) => Assign(data, (a, v) => a.Data = v);

		/// <inheritdoc cref="IPostJobDataRequest.Data" />
		public PostJobDataDescriptor Data(params object[] data) => Assign(data, (a, v) =>
		{
			if (v != null && v.Length == 1 && v[0] is IEnumerable enumerable)
				a.Data = enumerable.Cast<object>();
			else a.Data = v;
		});
	}

	internal class PostJobDataFormatter : IJsonFormatter<IPostJobDataRequest>
	{
		private const byte Newline = (byte)'\n';

		public IPostJobDataRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IPostJobDataRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Data == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var sourceSerializer = settings.SourceSerializer;
			var memoryStreamFactory = settings.MemoryStreamFactory;

			foreach (var data in value.Data)
			{
				var bodyJson = sourceSerializer.SerializeToBytes(data, memoryStreamFactory, SerializationFormatting.None);
				writer.WriteRaw(bodyJson);
				writer.WriteRaw(Newline);
			}
		}
	}
}
