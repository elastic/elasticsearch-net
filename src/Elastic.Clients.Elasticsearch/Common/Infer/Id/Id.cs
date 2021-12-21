using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	[JsonConverter(typeof(Id))]
	public class Id : IEquatable<Id>, IUrlParameter
	{
		public Id(string id)
		{
			Tag = 0;
			StringValue = id;
		}

		public Id(long id)
		{
			Tag = 1;
			LongValue = id;
		}

		public Id(object document)
		{
			Tag = 2;
			Document = document;
		}

		internal object Document { get; }
		internal long? LongValue { get; }
		internal string StringOrLongValue => StringValue ?? LongValue?.ToString(CultureInfo.InvariantCulture);
		internal string StringValue { get; }
		internal int Tag { get; }

		private string DebugDisplay => StringOrLongValue ?? "Id from instance typeof: " + Document?.GetType().Name;

		private static int TypeHashCode { get; } = typeof(Id).GetHashCode();

		public bool Equals(Id other)
		{
			if (Tag + other.Tag == 1)
				return StringOrLongValue == other.StringOrLongValue;
			else if (Tag != other.Tag)
				return false;

			switch (Tag)
			{
				case 0:
				case 1:
					return StringOrLongValue == other.StringOrLongValue;
				default:
					return Document?.Equals(other.Document) ?? false;
			}
		}

		string IUrlParameter.GetString(ITransportConfiguration settings)
		{
			var elasticClientSettings = (IElasticsearchClientSettings)settings;
			return GetString(elasticClientSettings);
		}

		internal string GetString(IElasticsearchClientSettings settings) => settings.Inferrer.Id(Document) ?? StringOrLongValue;

		public static implicit operator Id(string id) => id.IsNullOrEmpty() ? null : new Id(id);

		public static implicit operator Id(long id) => new(id);

		public static implicit operator Id(Guid id) => new(id.ToString("D"));

		public static Id From<T>(T document) => new(document);

		public override string ToString() => DebugDisplay;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case Id r:
					return Equals(r);
				case string s:
					return Equals(s);
				case int l:
					return Equals(l);
				case long l:
					return Equals(l);
				case Guid g:
					return Equals(g);
			}
			return Equals(new Id(obj));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (StringValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (LongValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (Document?.GetHashCode() ?? 0);
				return result;
			}
		}

		public static bool operator ==(Id left, Id right) => Equals(left, right);

		public static bool operator !=(Id left, Id right) => !Equals(left, right);
	}

	internal sealed class IdConverter : JsonConverter<Id>
	{
		private readonly IElasticsearchClientSettings _settings;

		public IdConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override Id? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			reader.TokenType == JsonTokenType.Number
				? new Id(reader.GetInt64())
				: new Id(reader.GetString());

		public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNullValue();
				return;
			}

			if (value.Document is not null)
			{
				var documentId = _settings.Inferrer.Id(value.Document.GetType(), value.Document);
				writer.WriteStringValue(documentId);
			}
			else if (value.LongValue.HasValue)
			{
				writer.WriteNumberValue(value.LongValue.Value);
			}
			else
			{
				writer.WriteStringValue(value.StringValue);
			}
		}
	}
}
