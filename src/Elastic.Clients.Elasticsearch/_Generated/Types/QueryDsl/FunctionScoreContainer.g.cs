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
	public interface IFunctionScoreContainerVariant
	{
		string FunctionScoreContainerVariantName { get; }
	}

	[JsonConverter(typeof(FunctionScoreContainerConverter))]
	public partial class FunctionScoreContainer : IContainer
	{
		public FunctionScoreContainer(IFunctionScoreContainerVariant variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
		internal IFunctionScoreContainerVariant Variant { get; }

		[JsonInclude]
		[JsonPropertyName("filter")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? Filter { get; set; }

		[JsonInclude]
		[JsonPropertyName("weight")]
		public double? Weight { get; set; }
	}

	internal sealed class FunctionScoreContainerConverter : JsonConverter<FunctionScoreContainer>
	{
		public override FunctionScoreContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var readerCopy = reader;
			readerCopy.Read();
			if (readerCopy.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = readerCopy.GetString();
			if (propertyName == "field_value_factor")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction?>(ref reader, options);
				return new FunctionScoreContainer(variant);
			}

			if (propertyName == "random_score")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction?>(ref reader, options);
				return new FunctionScoreContainer(variant);
			}

			if (propertyName == "script_score")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction?>(ref reader, options);
				return new FunctionScoreContainer(variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, FunctionScoreContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.Variant.FunctionScoreContainerVariantName);
			switch (value.Variant)
			{
				case Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class FunctionScoreContainerDescriptor<TDocument> : DescriptorBase<FunctionScoreContainerDescriptor<TDocument>>
	{
		internal FunctionScoreContainerDescriptor(Action<FunctionScoreContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public FunctionScoreContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal FunctionScoreContainer Container { get; private set; }

		internal IDescriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : IDescriptor, new()
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

		private void Set(IFunctionScoreContainerVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new FunctionScoreContainer(variant);
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

		public void FieldValueFactor(FieldValueFactorScoreFunction variant) => Set(variant, "field_value_factor");
		public void FieldValueFactor(Action<FieldValueFactorScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "field_value_factor");
		public void RandomScore(RandomScoreFunction variant) => Set(variant, "random_score");
		public void RandomScore(Action<RandomScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "random_score");
		public void ScriptScore(ScriptScoreFunction variant) => Set(variant, "script_score");
		public void ScriptScore(Action<ScriptScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "script_score");
	}

	public sealed partial class FunctionScoreContainerDescriptor : DescriptorBase<FunctionScoreContainerDescriptor>
	{
		internal FunctionScoreContainerDescriptor(Action<FunctionScoreContainerDescriptor> configure) => configure.Invoke(this);
		public FunctionScoreContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal FunctionScoreContainer Container { get; private set; }

		internal IDescriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : IDescriptor, new()
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

		private void Set(IFunctionScoreContainerVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new FunctionScoreContainer(variant);
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

		public void FieldValueFactor(FieldValueFactorScoreFunction variant) => Set(variant, "field_value_factor");
		public void FieldValueFactor(Action<FieldValueFactorScoreFunctionDescriptor> configure) => Set(configure, "field_value_factor");
		public void FieldValueFactor<TDocument>(Action<FieldValueFactorScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "field_value_factor");
		public void RandomScore(RandomScoreFunction variant) => Set(variant, "random_score");
		public void RandomScore(Action<RandomScoreFunctionDescriptor> configure) => Set(configure, "random_score");
		public void RandomScore<TDocument>(Action<RandomScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "random_score");
		public void ScriptScore(ScriptScoreFunction variant) => Set(variant, "script_score");
		public void ScriptScore(Action<ScriptScoreFunctionDescriptor> configure) => Set(configure, "script_score");
		public void ScriptScore<TDocument>(Action<ScriptScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "script_score");
	}
}