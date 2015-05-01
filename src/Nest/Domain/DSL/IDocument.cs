using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(DocumentConverter))]
	public interface IDocument
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T OfType<T>() where T : class;
	}

	public class DocumentConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var d = value as Document;
			if (d == null || d._Value == null)
				return;
			writer.WriteToken(d._Value.CreateReader());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var document = serializer.Deserialize(reader) as JToken;
			return new Document {_Value = document};
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
	public class Document : IDocument
	{
		internal JToken _Value { get; set; }

		public Document()
		{
			//_value = new Lazy<object>(deserializer);
		}

		public T OfType<T>() where T : class
		{
			var jToken = this._Value;
			return jToken != null ? jToken.ToObject<T>() : null;
		}
	}
}