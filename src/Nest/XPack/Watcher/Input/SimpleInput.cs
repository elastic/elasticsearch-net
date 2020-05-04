// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(SimpleInputFormatter))]
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

	internal class SimpleInputFormatter : IJsonFormatter<ISimpleInput>
	{
		public ISimpleInput Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var formatter = formatterResolver.GetFormatter<IDictionary<string, object>>();
			var dictionary = formatter.Deserialize(ref reader, formatterResolver);

			return new SimpleInput(dictionary);
		}

		public void Serialize(ref JsonWriter writer, ISimpleInput value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Payload == null)
				return;

			var formatter = formatterResolver.GetFormatter<IDictionary<string, object>>();
			formatter.Serialize(ref writer, value.Payload, formatterResolver);
		}
	}
}
