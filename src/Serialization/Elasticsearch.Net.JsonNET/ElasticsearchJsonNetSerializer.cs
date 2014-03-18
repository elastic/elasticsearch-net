using System;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Serialization;
using Newtonsoft.Json;

namespace Elasticsearch.Net.JsonNet
{
	public class ElasticsearchJsonNetSerializer: IElasticsearchSerializer
	{
		private readonly JsonSerializerSettings _settings;

		public ElasticsearchJsonNetSerializer(JsonSerializerSettings settings = null)
		{
			_settings = settings;
		}

		public T Deserialize<T>(byte[] bytes) where T : class
		{
			if (bytes == null) return null;

			var s = Encoding.UTF8.GetString(bytes);
			return JsonConvert.DeserializeObject<T>(s, this._settings);

		//var serializer = new JsonSerializer();
		//var jsonTextReader = new JsonTextReader(new StreamReader(stream));
		//return serializer.Deserialize(jsonTextReader);
		//	JsonConvert.DeserializeObject()
		}

		public byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var format = formatting == SerializationFormatting.Indented
				? Formatting.Indented
				: Formatting.None;
			var json = JsonConvert.SerializeObject(data, format, this._settings);

			return Encoding.UTF8.GetBytes(json);
		}
	}
}