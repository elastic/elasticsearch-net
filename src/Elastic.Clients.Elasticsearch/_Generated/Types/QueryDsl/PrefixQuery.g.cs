// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	internal sealed class PrefixQueryConverter : JsonConverter<PrefixQuery>
	{
		public override PrefixQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new PrefixQuery();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "_name")
					{
						variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "boost")
					{
						variant.Boost = JsonSerializer.Deserialize<float?>(ref reader, options);
						continue;
					}

					if (property == "case_insensitive")
					{
						variant.CaseInsensitive = JsonSerializer.Deserialize<bool?>(ref reader, options);
						continue;
					}

					if (property == "rewrite")
					{
						variant.Rewrite = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "value")
					{
						variant.Value = JsonSerializer.Deserialize<string>(ref reader, options);
						continue;
					}

					if (property == "field")
					{
						variant.Field = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						continue;
					}
				}
			}

			reader.Read();
			return variant;
		}

		public override void Write(Utf8JsonWriter writer, PrefixQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value.QueryName))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(value.QueryName);
			}

			if (value.Boost.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(value.Boost.Value);
			}

			if (value.CaseInsensitive.HasValue)
			{
				writer.WritePropertyName("case_insensitive");
				writer.WriteBooleanValue(value.CaseInsensitive.Value);
			}

			if (value.Rewrite is not null)
			{
				writer.WritePropertyName("rewrite");
				JsonSerializer.Serialize(writer, value.Rewrite, options);
			}

			writer.WritePropertyName("value");
			writer.WriteStringValue(value.Value);
			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(PrefixQueryConverter))]
	public sealed partial class PrefixQuery : IQueryVariant
	{
		public string? QueryName { get; set; }

		public float? Boost { get; set; }

		public bool? CaseInsensitive { get; set; }

		public string? Rewrite { get; set; }

		public string Value { get; set; }

		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }
	}

	public sealed partial class PrefixQueryDescriptor<TDocument> : SerializableDescriptorBase<PrefixQueryDescriptor<TDocument>>
	{
		internal PrefixQueryDescriptor(Action<PrefixQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public PrefixQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private bool? CaseInsensitiveValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private string? RewriteValue { get; set; }

		private string ValueValue { get; set; }

		public PrefixQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public PrefixQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public PrefixQueryDescriptor<TDocument> CaseInsensitive(bool? caseInsensitive = true)
		{
			CaseInsensitiveValue = caseInsensitive;
			return Self;
		}

		public PrefixQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public PrefixQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public PrefixQueryDescriptor<TDocument> Rewrite(string? rewrite)
		{
			RewriteValue = rewrite;
			return Self;
		}

		public PrefixQueryDescriptor<TDocument> Value(string value)
		{
			ValueValue = value;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			if (CaseInsensitiveValue.HasValue)
			{
				writer.WritePropertyName("case_insensitive");
				writer.WriteBooleanValue(CaseInsensitiveValue.Value);
			}

			if (RewriteValue is not null)
			{
				writer.WritePropertyName("rewrite");
				JsonSerializer.Serialize(writer, RewriteValue, options);
			}

			writer.WritePropertyName("value");
			writer.WriteStringValue(ValueValue);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}

	public sealed partial class PrefixQueryDescriptor : SerializableDescriptorBase<PrefixQueryDescriptor>
	{
		internal PrefixQueryDescriptor(Action<PrefixQueryDescriptor> configure) => configure.Invoke(this);
		public PrefixQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private bool? CaseInsensitiveValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private string? RewriteValue { get; set; }

		private string ValueValue { get; set; }

		public PrefixQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public PrefixQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public PrefixQueryDescriptor CaseInsensitive(bool? caseInsensitive = true)
		{
			CaseInsensitiveValue = caseInsensitive;
			return Self;
		}

		public PrefixQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public PrefixQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public PrefixQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public PrefixQueryDescriptor Rewrite(string? rewrite)
		{
			RewriteValue = rewrite;
			return Self;
		}

		public PrefixQueryDescriptor Value(string value)
		{
			ValueValue = value;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			if (CaseInsensitiveValue.HasValue)
			{
				writer.WritePropertyName("case_insensitive");
				writer.WriteBooleanValue(CaseInsensitiveValue.Value);
			}

			if (RewriteValue is not null)
			{
				writer.WritePropertyName("rewrite");
				JsonSerializer.Serialize(writer, RewriteValue, options);
			}

			writer.WritePropertyName("value");
			writer.WriteStringValue(ValueValue);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}