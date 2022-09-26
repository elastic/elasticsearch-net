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
	public interface IFunctionScoreVariant
	{
	}

	[JsonConverter(typeof(FunctionScoreContainerConverter))]
	public sealed partial class FunctionScoreContainer
	{
		internal FunctionScoreContainer(string variantName, IFunctionScoreVariant variant)
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

		internal IFunctionScoreVariant Variant { get; }

		internal string VariantName { get; }

		public static FunctionScoreContainer FieldValueFactor(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction fieldValueFactorScoreFunction) => new FunctionScoreContainer("field_value_factor", fieldValueFactorScoreFunction);
		public static FunctionScoreContainer RandomScore(Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction randomScoreFunction) => new FunctionScoreContainer("random_score", randomScoreFunction);
		public static FunctionScoreContainer ScriptScore(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction scriptScoreFunction) => new FunctionScoreContainer("script_score", scriptScoreFunction);
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
			if (propertyName == "field_value_factor")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction?>(ref reader, options);
				reader.Read();
				return new FunctionScoreContainer(propertyName, variant);
			}

			if (propertyName == "random_score")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction?>(ref reader, options);
				reader.Read();
				return new FunctionScoreContainer(propertyName, variant);
			}

			if (propertyName == "script_score")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction?>(ref reader, options);
				reader.Read();
				return new FunctionScoreContainer(propertyName, variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, FunctionScoreContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "field_value_factor":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction)value.Variant, options);
					break;
				case "random_score":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction)value.Variant, options);
					break;
				case "script_score":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction>(writer, (Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction)value.Variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class FunctionScoreContainerDescriptor<TDocument> : SerializableDescriptorBase<FunctionScoreContainerDescriptor<TDocument>>
	{
		internal FunctionScoreContainerDescriptor(Action<FunctionScoreContainerDescriptor<TDocument>> configure) => configure.Invoke(this);
		public FunctionScoreContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal FunctionScoreContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the FunctionScoreContainerDescriptor. Only a single FunctionScoreContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IFunctionScoreVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the FunctionScoreContainerDescriptor. Only a single FunctionScoreContainer can be added to this container type.");
			Container = new FunctionScoreContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; set; }

		private QueryContainerDescriptor<TDocument> FilterDescriptor { get; set; }

		private Action<QueryContainerDescriptor<TDocument>> FilterDescriptorAction { get; set; }

		private double? WeightValue { get; set; }

		public FunctionScoreContainerDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public FunctionScoreContainerDescriptor<TDocument> Filter(QueryContainerDescriptor<TDocument> descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public FunctionScoreContainerDescriptor<TDocument> Filter(Action<QueryContainerDescriptor<TDocument>> configure)
		{
			FilterValue = null;
			FilterDescriptor = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public FunctionScoreContainerDescriptor<TDocument> Weight(double? weight)
		{
			WeightValue = weight;
			return Self;
		}

		public void FieldValueFactor(FieldValueFactorScoreFunction variant) => Set(variant, "field_value_factor");
		public void FieldValueFactor(Action<FieldValueFactorScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "field_value_factor");
		public void RandomScore(RandomScoreFunction variant) => Set(variant, "random_score");
		public void RandomScore(Action<RandomScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "random_score");
		public void ScriptScore(ScriptScoreFunction variant) => Set(variant, "script_score");
		public void ScriptScore(Action<ScriptScoreFunctionDescriptor> configure) => Set(configure, "script_score");
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

	public sealed partial class FunctionScoreContainerDescriptor : SerializableDescriptorBase<FunctionScoreContainerDescriptor>
	{
		internal FunctionScoreContainerDescriptor(Action<FunctionScoreContainerDescriptor> configure) => configure.Invoke(this);
		public FunctionScoreContainerDescriptor() : base()
		{
		}

		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal FunctionScoreContainer Container { get; private set; }

		internal Descriptor Descriptor { get; private set; }

		internal Type DescriptorType { get; private set; }

		private void Set<T>(Action<T> descriptorAction, string variantName)
			where T : Descriptor
		{
			if (ContainsVariant)
				throw new InvalidOperationException("A variant has already been assigned to the FunctionScoreContainerDescriptor. Only a single FunctionScoreContainer can be added to this container type.");
			ContainedVariantName = variantName;
			ContainsVariant = true;
			DescriptorType = typeof(T);
			var descriptor = (T)Activator.CreateInstance(typeof(T), true);
			descriptorAction?.Invoke(descriptor);
			Descriptor = descriptor;
		}

		private void Set(IFunctionScoreVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("A variant has already been assigned to the FunctionScoreContainerDescriptor. Only a single FunctionScoreContainer can be added to this container type.");
			Container = new FunctionScoreContainer(variantName, variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; set; }

		private QueryContainerDescriptor FilterDescriptor { get; set; }

		private Action<QueryContainerDescriptor> FilterDescriptorAction { get; set; }

		private double? WeightValue { get; set; }

		public FunctionScoreContainerDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public FunctionScoreContainerDescriptor Filter(QueryContainerDescriptor descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public FunctionScoreContainerDescriptor Filter(Action<QueryContainerDescriptor> configure)
		{
			FilterValue = null;
			FilterDescriptor = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public FunctionScoreContainerDescriptor Weight(double? weight)
		{
			WeightValue = weight;
			return Self;
		}

		public void FieldValueFactor(FieldValueFactorScoreFunction variant) => Set(variant, "field_value_factor");
		public void FieldValueFactor(Action<FieldValueFactorScoreFunctionDescriptor> configure) => Set(configure, "field_value_factor");
		public void FieldValueFactor<TDocument>(Action<FieldValueFactorScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "field_value_factor");
		public void RandomScore(RandomScoreFunction variant) => Set(variant, "random_score");
		public void RandomScore(Action<RandomScoreFunctionDescriptor> configure) => Set(configure, "random_score");
		public void RandomScore<TDocument>(Action<RandomScoreFunctionDescriptor<TDocument>> configure) => Set(configure, "random_score");
		public void ScriptScore(ScriptScoreFunction variant) => Set(variant, "script_score");
		public void ScriptScore(Action<ScriptScoreFunctionDescriptor> configure) => Set(configure, "script_score");
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