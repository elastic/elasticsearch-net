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
	public partial class FieldValueFactorScoreFunction : IFunctionScoreVariant
	{
		[JsonIgnore]
		string IFunctionScoreVariant.FunctionScoreVariantName => "field_value_factor";
		[JsonInclude]
		[JsonPropertyName("factor")]
		public double? Factor { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing")]
		public double? Missing { get; set; }

		[JsonInclude]
		[JsonPropertyName("modifier")]
		public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? Modifier { get; set; }
	}

	public sealed partial class FieldValueFactorScoreFunctionDescriptor<TDocument> : SerializableDescriptorBase<FieldValueFactorScoreFunctionDescriptor<TDocument>>
	{
		internal FieldValueFactorScoreFunctionDescriptor(Action<FieldValueFactorScoreFunctionDescriptor<TDocument>> configure) => configure.Invoke(this);
		public FieldValueFactorScoreFunctionDescriptor() : base()
		{
		}

		private double? FactorValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private double? MissingValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? ModifierValue { get; set; }

		public FieldValueFactorScoreFunctionDescriptor<TDocument> Factor(double? factor)
		{
			FactorValue = factor;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor<TDocument> Missing(double? missing)
		{
			MissingValue = missing;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor<TDocument> Modifier(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? modifier)
		{
			ModifierValue = modifier;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FactorValue.HasValue)
			{
				writer.WritePropertyName("factor");
				writer.WriteNumberValue(FactorValue.Value);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (MissingValue.HasValue)
			{
				writer.WritePropertyName("missing");
				writer.WriteNumberValue(MissingValue.Value);
			}

			if (ModifierValue is not null)
			{
				writer.WritePropertyName("modifier");
				JsonSerializer.Serialize(writer, ModifierValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class FieldValueFactorScoreFunctionDescriptor : SerializableDescriptorBase<FieldValueFactorScoreFunctionDescriptor>
	{
		internal FieldValueFactorScoreFunctionDescriptor(Action<FieldValueFactorScoreFunctionDescriptor> configure) => configure.Invoke(this);
		public FieldValueFactorScoreFunctionDescriptor() : base()
		{
		}

		private double? FactorValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private double? MissingValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? ModifierValue { get; set; }

		public FieldValueFactorScoreFunctionDescriptor Factor(double? factor)
		{
			FactorValue = factor;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor Missing(double? missing)
		{
			MissingValue = missing;
			return Self;
		}

		public FieldValueFactorScoreFunctionDescriptor Modifier(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? modifier)
		{
			ModifierValue = modifier;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FactorValue.HasValue)
			{
				writer.WritePropertyName("factor");
				writer.WriteNumberValue(FactorValue.Value);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (MissingValue.HasValue)
			{
				writer.WritePropertyName("missing");
				writer.WriteNumberValue(MissingValue.Value);
			}

			if (ModifierValue is not null)
			{
				writer.WritePropertyName("modifier");
				JsonSerializer.Serialize(writer, ModifierValue, options);
			}

			writer.WriteEndObject();
		}
	}
}