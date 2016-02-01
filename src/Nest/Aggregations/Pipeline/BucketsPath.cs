using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(BucketsPathJsonConverter))]
	public interface IBucketsPath { }

	public class SingleBucketsPath : IBucketsPath
	{
		internal string BucketsPath { get; private set; }

		public SingleBucketsPath(string bucketsPath)
		{
			this.BucketsPath = bucketsPath;
		}

		public static implicit operator SingleBucketsPath(string bucketsPath) => new SingleBucketsPath(bucketsPath);
	}

	public interface IMultiBucketsPath : IIsADictionary<string, string>, IBucketsPath { }

	public class MultiBucketsPath : IsADictionaryBase<string, string>, IMultiBucketsPath
	{
		public MultiBucketsPath() : base() { }
		public MultiBucketsPath(IDictionary<string, string> container) : base(container) { }
		public MultiBucketsPath(Dictionary<string, string> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, string bucketsPath) => this.BackingDictionary.Add(name, bucketsPath);

		public static implicit operator MultiBucketsPath(Dictionary<string, string> bucketsPath) => new MultiBucketsPath(bucketsPath);
	}

	public class MultiBucketsPathDescriptor
		: IsADictionaryDescriptorBase<MultiBucketsPathDescriptor, IMultiBucketsPath, string, string>
	{
		public MultiBucketsPathDescriptor() : base(new MultiBucketsPath()) { }

		public MultiBucketsPathDescriptor Add(string name, string bucketsPath) => Assign(name, bucketsPath);
	}

	public class BucketsPathJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => 
			typeof(SingleBucketsPath) == objectType || typeof(MultiBucketsPath) == objectType;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return new SingleBucketsPath(reader.Value.ToString());

			if (reader.TokenType == JsonToken.StartObject)
			{
				var dict = new Dictionary<string, string>();
				reader.Read();
				while (reader.TokenType != JsonToken.EndObject)
				{
					var key = reader.Value.ToString();
					reader.Read();
					var value = reader.Value.ToString();
					dict.Add(key, value);
					reader.Read();
				}
				return new MultiBucketsPath(dict);
			}

			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var single = value as SingleBucketsPath;
			if (single != null)
			{
				writer.WriteValue(single.BucketsPath);
				return;
			}
			var multi = value as MultiBucketsPath;
			if (multi != null)
			{
				writer.WriteStartObject();
				foreach(var kv in multi)
				{
					writer.WritePropertyName(kv.Key);
					writer.WriteValue(kv.Value);
				}
				writer.WriteEndObject();
				return;
			}
			writer.WriteNull();
		}
	}
}
