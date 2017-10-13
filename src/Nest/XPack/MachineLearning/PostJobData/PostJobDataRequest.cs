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

	/// <inheritdoc />
	public partial class PostJobDataRequest
	{
		/// <inheritdoc />
		public IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlPostData")]
	public partial class PostJobDataDescriptor
	{
		/// <inheritdoc />
		IEnumerable<object> IPostJobDataRequest.Data { get; set; }

		/// <inheritdoc />
		public PostJobDataDescriptor Data(IEnumerable<object> data) => Assign(a => a.Data = data);

		/// <inheritdoc />
		public PostJobDataDescriptor Data(params object[] data) => Assign(a =>
		{
			if(data != null && data.Length == 1 && data[0] is IEnumerable)
			{
				a.Data = ((IEnumerable)data[0]).Cast<object>();
			}
			else a.Data = data;
		});
	}

	internal class PostJobDataConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IPostJobDataRequest) value;
			if (request?.Data == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.Serializer;
			foreach (var data in request.Data)
			{
				var bodyJson = elasticsearchSerializer.SerializeToString(data, SerializationFormatting.None);
				writer.WriteRaw(bodyJson + "\n");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override bool CanConvert(Type objectType) => true;
	}
}
