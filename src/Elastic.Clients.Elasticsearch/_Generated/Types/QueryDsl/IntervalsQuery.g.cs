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
	public interface IIntervalsQueryVariant
	{
	}

	[JsonConverter(typeof(IntervalsQueryConverter))]
	public sealed partial class IntervalsQuery : Query, IQueryVariant
	{
		internal IntervalsQuery(string variantName, IIntervalsQueryVariant variant)
		{
			if (variantName is null)
				throw new ArgumentNullException(nameof(variantName));
			if (variant is null)
				throw new ArgumentNullException(nameof(variant));
			if (string.IsNullOrWhiteSpace(variantName))
				throw new ArgumentException("Variant name must not be empty or whitespace.");
			VariantName = variantName;
			Variant = variant;
		}

		internal IntervalsQuery(Field field, string variantName, IIntervalsQueryVariant variant)
		{
			if (variantName is null)
				throw new ArgumentNullException(nameof(variantName));
			if (variant is null)
				throw new ArgumentNullException(nameof(variant));
			if (string.IsNullOrWhiteSpace(variantName))
				throw new ArgumentException("Variant name must not be empty or whitespace.");
			if (field is null)
				throw new ArgumentNullException(nameof(field));
			VariantName = variantName;
			Variant = variant;
			Field = field;
		}

		internal IIntervalsQueryVariant Variant { get; }

		internal string VariantName { get; }

		public static IntervalsQuery AllOf(Field field, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf intervalsAllOf) => new IntervalsQuery(field, "all_of", intervalsAllOf);
		public static IntervalsQuery AnyOf(Field field, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf intervalsAnyOf) => new IntervalsQuery(field, "any_of", intervalsAnyOf);
		public static IntervalsQuery Fuzzy(Field field, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy intervalsFuzzy) => new IntervalsQuery(field, "fuzzy", intervalsFuzzy);
		public static IntervalsQuery Match(Field field, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch intervalsMatch) => new IntervalsQuery(field, "match", intervalsMatch);
		public static IntervalsQuery Prefix(Field field, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix intervalsPrefix) => new IntervalsQuery(field, "prefix", intervalsPrefix);
		public static IntervalsQuery Wildcard(Field field, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard intervalsWildcard) => new IntervalsQuery(field, "wildcard", intervalsWildcard);
		[JsonInclude]
		[JsonPropertyName("_name")]
		public string? QueryName { get; set; }

		[JsonInclude]
		[JsonPropertyName("boost")]
		public float? Boost { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	}

	internal sealed class IntervalsQueryConverter : JsonConverter<IntervalsQuery>
	{
		public override IntervalsQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
			{
				throw new JsonException("Expected start token.");
			}

			reader.Read();
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected property name token.");
			}

			var propertyName = reader.GetString();
			reader.Read();
			if (propertyName == "all_of")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf?>(ref reader, options);
				reader.Read();
				return new IntervalsQuery(propertyName, variant);
			}

			if (propertyName == "any_of")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf?>(ref reader, options);
				reader.Read();
				return new IntervalsQuery(propertyName, variant);
			}

			if (propertyName == "fuzzy")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy?>(ref reader, options);
				reader.Read();
				return new IntervalsQuery(propertyName, variant);
			}

			if (propertyName == "match")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch?>(ref reader, options);
				reader.Read();
				return new IntervalsQuery(propertyName, variant);
			}

			if (propertyName == "prefix")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix?>(ref reader, options);
				reader.Read();
				return new IntervalsQuery(propertyName, variant);
			}

			if (propertyName == "wildcard")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard?>(ref reader, options);
				reader.Read();
				return new IntervalsQuery(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, IntervalsQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "all_of":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf)value.Variant, options);
					break;
				case "any_of":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf)value.Variant, options);
					break;
				case "fuzzy":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy)value.Variant, options);
					break;
				case "match":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch)value.Variant, options);
					break;
				case "prefix":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix)value.Variant, options);
					break;
				case "wildcard":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class IntervalsQueryDescriptor<TDocument> : SerializableDescriptorBase<IntervalsQueryDescriptor<TDocument>>
	{
		internal IntervalsQueryDescriptor(Action<IntervalsQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal IntervalsQueryDescriptor() : base()
		{
		}

		public IntervalsQueryDescriptor(Field field)
		{
			if (field is null)
				throw new ArgumentNullException(nameof(field));
			FieldValue = field;
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal IntervalsQuery Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the IntervalsQueryDescriptor. Only a single IntervalsQuery can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IIntervalsQueryVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the IntervalsQueryDescriptor. Only a single IntervalsQuery can be added to this container type.");
			Container = new IntervalsQuery(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		public IntervalsQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public IntervalsQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public IntervalsQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public IntervalsQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public void AllOf(IntervalsAllOf variant) => Set(variant, "all_of");
		public void AllOf(Action<IntervalsAllOfDescriptor<TDocument>> configure) => Set(configure, "all_of");
		public void AnyOf(IntervalsAnyOf variant) => Set(variant, "any_of");
		public void AnyOf(Action<IntervalsAnyOfDescriptor<TDocument>> configure) => Set(configure, "any_of");
		public void Fuzzy(IntervalsFuzzy variant) => Set(variant, "fuzzy");
		public void Fuzzy(Action<IntervalsFuzzyDescriptor<TDocument>> configure) => Set(configure, "fuzzy");
		public void Match(IntervalsMatch variant) => Set(variant, "match");
		public void Match(Action<IntervalsMatchDescriptor<TDocument>> configure) => Set(configure, "match");
		public void Prefix(IntervalsPrefix variant) => Set(variant, "prefix");
		public void Prefix(Action<IntervalsPrefixDescriptor<TDocument>> configure) => Set(configure, "prefix");
		public void Wildcard(IntervalsWildcard variant) => Set(variant, "wildcard");
		public void Wildcard(Action<IntervalsWildcardDescriptor<TDocument>> configure) => Set(configure, "wildcard");
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!ContainsVariant)
			{
				writer.WriteNullValue();
				return;
			}

			if (Container is not null)
			{
				JsonSerializer.Serialize(writer, Container, options);
				return;
			}

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

			writer.WritePropertyName(ContainedVariantName);
			JsonSerializer.Serialize(writer, Descriptor, DescriptorType, options);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}

	public sealed partial class IntervalsQueryDescriptor : SerializableDescriptorBase<IntervalsQueryDescriptor>
	{
		internal IntervalsQueryDescriptor(Action<IntervalsQueryDescriptor> configure) => configure.Invoke(this);
		internal IntervalsQueryDescriptor() : base()
		{
		}

		public IntervalsQueryDescriptor(Field field)
		{
			if (field is null)
				throw new ArgumentNullException(nameof(field));
			FieldValue = field;
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal IntervalsQuery Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the IntervalsQueryDescriptor. Only a single IntervalsQuery can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IIntervalsQueryVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the IntervalsQueryDescriptor. Only a single IntervalsQuery can be added to this container type.");
			Container = new IntervalsQuery(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		public IntervalsQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public IntervalsQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public IntervalsQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public IntervalsQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public IntervalsQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public void AllOf(IntervalsAllOf variant) => Set(variant, "all_of");
		public void AllOf(Action<IntervalsAllOfDescriptor> configure) => Set(configure, "all_of");
		public void AllOf<TDocument>(Action<IntervalsAllOfDescriptor<TDocument>> configure) => Set(configure, "all_of");
		public void AnyOf(IntervalsAnyOf variant) => Set(variant, "any_of");
		public void AnyOf(Action<IntervalsAnyOfDescriptor> configure) => Set(configure, "any_of");
		public void AnyOf<TDocument>(Action<IntervalsAnyOfDescriptor<TDocument>> configure) => Set(configure, "any_of");
		public void Fuzzy(IntervalsFuzzy variant) => Set(variant, "fuzzy");
		public void Fuzzy(Action<IntervalsFuzzyDescriptor> configure) => Set(configure, "fuzzy");
		public void Fuzzy<TDocument>(Action<IntervalsFuzzyDescriptor<TDocument>> configure) => Set(configure, "fuzzy");
		public void Match(IntervalsMatch variant) => Set(variant, "match");
		public void Match(Action<IntervalsMatchDescriptor> configure) => Set(configure, "match");
		public void Match<TDocument>(Action<IntervalsMatchDescriptor<TDocument>> configure) => Set(configure, "match");
		public void Prefix(IntervalsPrefix variant) => Set(variant, "prefix");
		public void Prefix(Action<IntervalsPrefixDescriptor> configure) => Set(configure, "prefix");
		public void Prefix<TDocument>(Action<IntervalsPrefixDescriptor<TDocument>> configure) => Set(configure, "prefix");
		public void Wildcard(IntervalsWildcard variant) => Set(variant, "wildcard");
		public void Wildcard(Action<IntervalsWildcardDescriptor> configure) => Set(configure, "wildcard");
		public void Wildcard<TDocument>(Action<IntervalsWildcardDescriptor<TDocument>> configure) => Set(configure, "wildcard");
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!ContainsVariant)
			{
				writer.WriteNullValue();
				return;
			}

			if (Container is not null)
			{
				JsonSerializer.Serialize(writer, Container, options);
				return;
			}

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

			writer.WritePropertyName(ContainedVariantName);
			JsonSerializer.Serialize(writer, Descriptor, DescriptorType, options);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}