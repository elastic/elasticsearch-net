using System;
using System.Linq;
using System.Text;
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