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
	public interface IPinnedQueryVariant
	{
	}

	[JsonConverter(typeof(PinnedQueryConverter))]
	public sealed partial class PinnedQuery : Query, IQueryVariant
	{
		public PinnedQuery(string variantName, IPinnedQueryVariant variant)
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

		internal IPinnedQueryVariant Variant { get; }

		internal string VariantName { get; }

		[JsonInclude]
		[JsonPropertyName("_name")]
		public string? QueryName { get; set; }

		[JsonInclude]
		[JsonPropertyName("boost")]
		public float? Boost { get; set; }

		[JsonInclude]
		[JsonPropertyName("organic")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer Organic { get; set; }
	}

	internal sealed class PinnedQueryConverter : JsonConverter<PinnedQuery>
	{
		public override PinnedQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, PinnedQuery value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			writer.WriteEndObject();
		}
	}

	public sealed partial class PinnedQueryDescriptor<TDocument> : SerializableDescriptorBase<PinnedQueryDescriptor<TDocument>>
	{
		internal PinnedQueryDescriptor(Action<PinnedQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public PinnedQueryDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal PinnedQuery Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IPinnedQueryVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new PinnedQuery(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer OrganicValue { get; set; }

		private QueryContainerDescriptor<TDocument> OrganicDescriptor { get; set; }

		private Action<QueryContainerDescriptor<TDocument>> OrganicDescriptorAction { get; set; }

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		public PinnedQueryDescriptor<TDocument> Organic(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer organic)
		{
			OrganicDescriptor = null;
			OrganicDescriptorAction = null;
			OrganicValue = organic;
			return Self;
		}

		public PinnedQueryDescriptor<TDocument> Organic(QueryContainerDescriptor<TDocument> descriptor)
		{
			OrganicValue = null;
			OrganicDescriptorAction = null;
			OrganicDescriptor = descriptor;
			return Self;
		}

		public PinnedQueryDescriptor<TDocument> Organic(Action<QueryContainerDescriptor<TDocument>> configure)
		{
			OrganicValue = null;
			OrganicDescriptor = null;
			OrganicDescriptorAction = configure;
			return Self;
		}

		public PinnedQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public PinnedQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

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
			writer.WritePropertyName(ContainedVariantName);
			JsonSerializer.Serialize(writer, Descriptor, DescriptorType, options);
			writer.WriteEndObject();
		}
	}

	public sealed partial class PinnedQueryDescriptor : SerializableDescriptorBase<PinnedQueryDescriptor>
	{
		internal PinnedQueryDescriptor(Action<PinnedQueryDescriptor> configure) => configure.Invoke(this);
		public PinnedQueryDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal PinnedQuery Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IPinnedQueryVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new PinnedQuery(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer OrganicValue { get; set; }

		private QueryContainerDescriptor OrganicDescriptor { get; set; }

		private Action<QueryContainerDescriptor> OrganicDescriptorAction { get; set; }

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		public PinnedQueryDescriptor Organic(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer organic)
		{
			OrganicDescriptor = null;
			OrganicDescriptorAction = null;
			OrganicValue = organic;
			return Self;
		}

		public PinnedQueryDescriptor Organic(QueryContainerDescriptor descriptor)
		{
			OrganicValue = null;
			OrganicDescriptorAction = null;
			OrganicDescriptor = descriptor;
			return Self;
		}

		public PinnedQueryDescriptor Organic(Action<QueryContainerDescriptor> configure)
		{
			OrganicValue = null;
			OrganicDescriptor = null;
			OrganicDescriptorAction = configure;
			return Self;
		}

		public PinnedQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public PinnedQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

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
			writer.WritePropertyName(ContainedVariantName);
			JsonSerializer.Serialize(writer, Descriptor, DescriptorType, options);
			writer.WriteEndObject();
		}
	}
}