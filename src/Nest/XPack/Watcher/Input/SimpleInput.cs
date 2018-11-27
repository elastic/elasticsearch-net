using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(SimpleInputJsonConverter))]
	public interface ISimpleInput : IInput
	{
		IDictionary<string, object> Payload { get; }
	}

	public class SimpleInput : InputBase, ISimpleInput, IEnumerable<KeyValuePair<string, object>>
	{
		public SimpleInput() { }

		public SimpleInput(IDictionary<string, object> payload)
			: this() => Payload = payload;

		public IDictionary<string, object> Payload { get; private set; }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
			Payload?.GetEnumerator() ?? Enumerable.Empty<KeyValuePair<string, object>>().GetEnumerator();

		public void Add(string key, object value)
		{
			if (Payload == null) Payload = new Dictionary<string, object>();
			Payload.Add(key, value);
		}

		public void Remove(string key) => Payload?.Remove(key);

		internal override void WrapInContainer(IInputContainer container) => container.Simple = this;
	}

	public class SimpleInputDescriptor : ISimpleInput, IDescriptor
	{
		private IDictionary<string, object> _payload;

		public SimpleInputDescriptor() { }

		public SimpleInputDescriptor(IDictionary<string, object> payload) => _payload = payload;

		IDictionary<string, object> ISimpleInput.Payload => _payload;

		public SimpleInputDescriptor Add(string key, object value)
		{
			if (_payload == null) _payload = new Dictionary<string, object>();
			_payload.Add(key, value);
			return this;
		}

		public SimpleInputDescriptor Remove(string key)
		{
			if (_payload == null) return this;

			_payload.Remove(key);
			return this;
		}
	}

	internal class SimpleInputJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;

			var dictionary = serializer.Deserialize<IDictionary<string, object>>(reader);
			return new SimpleInput(dictionary);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = value as ISimpleInput;
			if (s?.Payload == null) return;

			serializer.Serialize(writer, s.Payload);
		}
	}
}
