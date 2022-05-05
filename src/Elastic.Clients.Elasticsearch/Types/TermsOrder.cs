// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	[JsonConverter(typeof(TermsOrderConverter))]
	public readonly struct TermsOrder : IEquatable<TermsOrder>
	{
		public TermsOrder(string key, SortOrder order) => (Key, Order) = (key, order);

		public static TermsOrder CountAscending => new() { Key = "_count", Order = SortOrder.Asc };
		public static TermsOrder CountDescending => new() { Key = "_count", Order = SortOrder.Desc };
		public static TermsOrder KeyAscending => new() { Key = "_key", Order = SortOrder.Asc };
		public static TermsOrder KeyDescending => new() { Key = "_key", Order = SortOrder.Desc };

		public string Key { get; init; }
		public SortOrder Order { get; init; }

		public bool Equals(TermsOrder other) => Key == other.Key && Order == other.Order;
		public override bool Equals(object obj) => obj is TermsOrder other && Equals(other);
		public override int GetHashCode() => (Key, Order).GetHashCode();
		public static bool operator ==(TermsOrder lhs, TermsOrder rhs) => lhs.Equals(rhs);
		public static bool operator !=(TermsOrder lhs, TermsOrder rhs) => !(lhs == rhs);
	}

	internal sealed class TermsOrderConverter : JsonConverter<TermsOrder>
	{
		public override TermsOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				return default;

			reader.Read();
			var key = reader.GetString();

			reader.Read();
			var valueString = reader.GetString();
			var value = valueString switch
			{
				"asc" => SortOrder.Asc,
				"desc" => SortOrder.Desc,
				_ => throw new JsonException("Unexpected sort order in JSON"),
			};

			reader.Read();

			if (reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException("JSON did not conform to expected shape");

			return new TermsOrder(key, value);
		}

		public override void Write(Utf8JsonWriter writer, TermsOrder value, JsonSerializerOptions options)
		{
			if (string.IsNullOrEmpty(value.Key))
			{
				writer.WriteNullValue();
				return;
			}

			writer.WriteStartObject();
			writer.WritePropertyName(value.Key);
			switch (value.Order)
			{
				case SortOrder.Asc:
					writer.WriteStringValue("asc");
					break;
				case SortOrder.Desc:
					writer.WriteStringValue("desc");
					break;
				default:
					throw new JsonException("Unknown sort order specified.");
			}
			writer.WriteEndObject();
		}
	}

	public sealed class TermsOrderDescriptor : DescriptorPromiseBase<TermsOrderDescriptor, IList<TermsOrder>>
	{
		public TermsOrderDescriptor() : base(new List<TermsOrder>()) { }

		internal TermsOrderDescriptor(Action<TermsOrderDescriptor> configure) : this() => configure?.Invoke(this);

		public TermsOrderDescriptor CountAscending() => Assign(a => a.Add(TermsOrder.CountAscending));

		public TermsOrderDescriptor CountDescending() => Assign(a => a.Add(TermsOrder.CountDescending));

		public TermsOrderDescriptor KeyAscending() => Assign(a => a.Add(TermsOrder.KeyAscending));

		public TermsOrderDescriptor KeyDescending() => Assign(a => a.Add(TermsOrder.KeyDescending));

		public TermsOrderDescriptor Ascending(string key) =>
			string.IsNullOrWhiteSpace(key) ? this : Assign(key, (a, v) => a.Add(new TermsOrder { Key = v, Order = SortOrder.Asc }));

		public TermsOrderDescriptor Descending(string key) =>
			string.IsNullOrWhiteSpace(key) ? this : Assign(key, (a, v) => a.Add(new TermsOrder { Key = v, Order = SortOrder.Desc }));
	}
}
