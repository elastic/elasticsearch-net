using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(PostJobDataConverter))]
	public partial interface IPostJobDataRequest
	{
		/// <summary>
		/// The job data.
		/// </summary>
		/// <remarks>
		/// The job must have a state of open to receive and process the data.
		/// </remarks>
		[JsonIgnore]
		IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc cref="IPostJobDataRequest" />
	public partial class PostJobDataRequest
	{
		/// <inheritdoc />
		public IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc cref="IPostJobDataRequest" />
	[DescriptorFor("XpackMlPostData")]
	public partial class PostJobDataDescriptor
	{
		IEnumerable<object> IPostJobDataRequest.Data { get; set; }

		/// <inheritdoc cref="IPostJobDataRequest.Data" />
		public PostJobDataDescriptor Data(IEnumerable<object> data) => Assign(data, (a, v) => a.Data = v);

		/// <inheritdoc cref="IPostJobDataRequest.Data" />
		public PostJobDataDescriptor Data(params object[] data) => Assign(data, (a, v) =>
		{
			if (v != null && v.Length == 1 && v[0] is IEnumerable)
				a.Data = ((IEnumerable)v[0]).Cast<object>();
			else a.Data = v;
		});
	}

	internal class PostJobDataConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IPostJobDataRequest)value;
			if (request?.Data == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.RequestResponseSerializer;
			foreach (var data in request.Data)
			{
				var bodyJson = elasticsearchSerializer.SerializeToString(data, SerializationFormatting.None);
				writer.WriteRaw(bodyJson);
				writer.WriteRaw("\n");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override bool CanConvert(Type objectType) => true;
	}
}
