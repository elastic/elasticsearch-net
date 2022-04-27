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
	public interface IIntervalsFilterVariant
	{
		string IntervalsFilterVariantName { get; }
	}

	[JsonConverter(typeof(IntervalsFilterConverter))]
	public partial class IntervalsFilter : IContainer
	{
		public IntervalsFilter(IIntervalsFilterVariant variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
		internal IIntervalsFilterVariant Variant { get; }
	}

	internal sealed class IntervalsFilterConverter : JsonConverter<IntervalsFilter>
	{
		public override IntervalsFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var readerCopy = reader;
			readerCopy.Read();
			if (readerCopy.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = readerCopy.GetString();
			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, IntervalsFilter value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.Variant.IntervalsFilterVariantName);
			writer.WriteEndObject();
		}
	}

	public sealed partial class IntervalsFilterDescriptor<TDocument> : SerializableDescriptorBase<IntervalsFilterDescriptor<TDocument>>
	{
		internal IntervalsFilterDescriptor(Action<IntervalsFilterDescriptor<TDocument>> configure) => configure.Invoke(this);
		public IntervalsFilterDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal IntervalsFilter Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor, new()
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = new T();
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IIntervalsFilterVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new IntervalsFilter(variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
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

	public sealed partial class IntervalsFilterDescriptor : SerializableDescriptorBase<IntervalsFilterDescriptor>
	{
		internal IntervalsFilterDescriptor(Action<IntervalsFilterDescriptor> configure) => configure.Invoke(this);
		public IntervalsFilterDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal IntervalsFilter Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor, new()
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = new T();
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IIntervalsFilterVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new IntervalsFilter(variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
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