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
namespace Elastic.Clients.Elasticsearch.TransformManagement
{
	public interface IRetentionPolicyVariant
	{
	}

	[JsonConverter(typeof(RetentionPolicyContainerConverter))]
	public sealed partial class RetentionPolicyContainer
	{
		internal RetentionPolicyContainer(string variantName, IRetentionPolicyVariant variant)
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

		internal IRetentionPolicyVariant Variant { get; }

		internal string VariantName { get; }

		public static RetentionPolicyContainer Time(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy retentionPolicy) => new RetentionPolicyContainer("time", retentionPolicy);
	}

	internal sealed class RetentionPolicyContainerConverter : JsonConverter<RetentionPolicyContainer>
	{
		public override RetentionPolicyContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			if (propertyName == "time")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy?>(ref reader, options);
				reader.Read();
				return new RetentionPolicyContainer(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, RetentionPolicyContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "time":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy>(writer, (Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class RetentionPolicyContainerDescriptor<TDocument> : SerializableDescriptorBase<RetentionPolicyContainerDescriptor<TDocument>>
	{
		internal RetentionPolicyContainerDescriptor(Action<RetentionPolicyContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public RetentionPolicyContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal RetentionPolicyContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the RetentionPolicyContainerDescriptor. Only a single RetentionPolicyContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IRetentionPolicyVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the RetentionPolicyContainerDescriptor. Only a single RetentionPolicyContainer can be added to this container type.");
			Container = new RetentionPolicyContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Time(RetentionPolicy variant) => Set(variant, "time");
		public void Time(Action<RetentionPolicyDescriptor<TDocument>> configure) => Set(configure, "time");
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

	public sealed partial class RetentionPolicyContainerDescriptor : SerializableDescriptorBase<RetentionPolicyContainerDescriptor>
	{
		internal RetentionPolicyContainerDescriptor(Action<RetentionPolicyContainerDescriptor> configure) => configure.Invoke(this);
		public RetentionPolicyContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal RetentionPolicyContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the RetentionPolicyContainerDescriptor. Only a single RetentionPolicyContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IRetentionPolicyVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the RetentionPolicyContainerDescriptor. Only a single RetentionPolicyContainer can be added to this container type.");
			Container = new RetentionPolicyContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void Time(RetentionPolicy variant) => Set(variant, "time");
		public void Time(Action<RetentionPolicyDescriptor> configure) => Set(configure, "time");
		public void Time<TDocument>(Action<RetentionPolicyDescriptor<TDocument>> configure) => Set(configure, "time");
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