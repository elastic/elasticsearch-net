using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;
using Newtonsoft.Json;

namespace Elasticsearch.Net.JsonNet
{
	public class ElasticsearchJsonNetSerializer: IElasticsearchSerializer
	{
		private readonly JsonSerializerSettings _settings;

		public ElasticsearchJsonNetSerializer(JsonSerializerSettings settings = null)
		{
			_settings = settings ?? CreateSettings();
		}

		/// <summary>
		/// Deserialize an object 
		/// </summary>
		public virtual T Deserialize<T>(Stream stream) 
		{
			var settings = this._settings;
			return _Deserialize<T>(stream, settings);
		}
		public virtual Task<T> DeserializeAsync<T>(Stream stream)
		{
			//TODO sadly json .net async does not read the stream async so 
			//figure out wheter reading the stream async on our own might be beneficial 
			//over memory possible memory usage
			var tcs = new TaskCompletionSource<T>();
			var r = this.Deserialize<T>(stream);
			tcs.SetResult(r);
			return tcs.Task;
		}
		private JsonSerializerSettings CreateSettings()
		{
			var settings = new JsonSerializerSettings()
			{
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore,
			};
			return settings;
		}

		protected internal  T _Deserialize<T>(Stream stream, JsonSerializerSettings settings = null)
		{
			settings = settings ?? this._settings;
			var serializer = JsonSerializer.Create(settings);
			var jsonTextReader = new JsonTextReader(new StreamReader(stream));
			var t = (T) serializer.Deserialize(jsonTextReader, typeof (T));
			return t;	
		}

		public byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var format = formatting == SerializationFormatting.Indented
				? Formatting.Indented
				: Formatting.None;
			var json = JsonConvert.SerializeObject(data, format, this._settings);

			return Encoding.UTF8.GetBytes(json);
		}

		public string Stringify(object valueType)
		{
			return ElasticsearchDefaultSerializer.DefaultStringify(valueType);
		}
	}
}