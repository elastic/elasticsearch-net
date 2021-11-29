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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public interface IIntervalsContainerVariant
	{
		string IntervalsContainerVariantName { get; }
	}

	[JsonConverter(typeof(IntervalsContainerConverter))]
	public partial class IntervalsContainer : IContainer
	{
		public IntervalsContainer(IIntervalsContainerVariant variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
		internal IIntervalsContainerVariant Variant { get; }
	}

	internal sealed class IntervalsContainerConverter : JsonConverter<IntervalsContainer>
	{
		public override IntervalsContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = reader.GetString();
			if (propertyName == "all_of")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf?>(ref reader, options);
				reader.Read();
				return new IntervalsContainer(variant);
			}

			if (propertyName == "any_of")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf?>(ref reader, options);
				reader.Read();
				return new IntervalsContainer(variant);
			}

			if (propertyName == "fuzzy")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy?>(ref reader, options);
				reader.Read();
				return new IntervalsContainer(variant);
			}

			if (propertyName == "match")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch?>(ref reader, options);
				reader.Read();
				return new IntervalsContainer(variant);
			}

			if (propertyName == "prefix")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix?>(ref reader, options);
				reader.Read();
				return new IntervalsContainer(variant);
			}

			if (propertyName == "wildcard")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard?>(ref reader, options);
				reader.Read();
				return new IntervalsContainer(variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, IntervalsContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.Variant.IntervalsContainerVariantName);
			switch (value.Variant)
			{
				case Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class IntervalsContainerDescriptor<T> : DescriptorBase<IntervalsContainerDescriptor<T>>
	{
		public IntervalsContainerDescriptor()
		{
		}

		internal IntervalsContainerDescriptor(Action<IntervalsContainerDescriptor<T>> configure) => configure.Invoke(this);
		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal IntervalsContainer Container { get; private set; }

		internal object ContainerVariantDescriptorAction { get; private set; }

		private void Set(object descriptorAction, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainerVariantDescriptorAction = descriptorAction;
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private void Set(IIntervalsContainerVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new IntervalsContainer(variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void AllOf(IntervalsAllOf variant) => Set(variant, "all_of");
		public void AllOf(Action<IntervalsAllOfDescriptor> configure) => Set(configure, "all_of");
		public void AnyOf(IntervalsAnyOf variant) => Set(variant, "any_of");
		public void AnyOf(Action<IntervalsAnyOfDescriptor> configure) => Set(configure, "any_of");
		public void Fuzzy(IntervalsFuzzy variant) => Set(variant, "fuzzy");
		public void Fuzzy(Action<IntervalsFuzzyDescriptor<T>> configure) => Set(configure, "fuzzy");
		public void Match(IntervalsMatch variant) => Set(variant, "match");
		public void Match(Action<IntervalsMatchDescriptor<T>> configure) => Set(configure, "match");
		public void Prefix(IntervalsPrefix variant) => Set(variant, "prefix");
		public void Prefix(Action<IntervalsPrefixDescriptor<T>> configure) => Set(configure, "prefix");
		public void Wildcard(IntervalsWildcard variant) => Set(variant, "wildcard");
		public void Wildcard(Action<IntervalsWildcardDescriptor<T>> configure) => Set(configure, "wildcard");
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
			writer.WriteStartObject();
			if (ContainedVariantName == "all_of")
			{
				var descriptor = new IntervalsAllOfDescriptor();
				((Action<IntervalsAllOfDescriptor>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "any_of")
			{
				var descriptor = new IntervalsAnyOfDescriptor();
				((Action<IntervalsAnyOfDescriptor>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "fuzzy")
			{
				var descriptor = new IntervalsFuzzyDescriptor<T>();
				((Action<IntervalsFuzzyDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "match")
			{
				var descriptor = new IntervalsMatchDescriptor<T>();
				((Action<IntervalsMatchDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "prefix")
			{
				var descriptor = new IntervalsPrefixDescriptor<T>();
				((Action<IntervalsPrefixDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "wildcard")
			{
				var descriptor = new IntervalsWildcardDescriptor<T>();
				((Action<IntervalsWildcardDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
			void Finalise()
			{
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
		}
	}
}