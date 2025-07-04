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

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class FunctionScoreConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore>
{
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropWeight = System.Text.Json.JsonEncodedText.Encode("weight");
	private static readonly System.Text.Json.JsonEncodedText VariantExp = System.Text.Json.JsonEncodedText.Encode("exp");
	private static readonly System.Text.Json.JsonEncodedText VariantFieldValueFactor = System.Text.Json.JsonEncodedText.Encode("field_value_factor");
	private static readonly System.Text.Json.JsonEncodedText VariantGauss = System.Text.Json.JsonEncodedText.Encode("gauss");
	private static readonly System.Text.Json.JsonEncodedText VariantLinear = System.Text.Json.JsonEncodedText.Encode("linear");
	private static readonly System.Text.Json.JsonEncodedText VariantRandomScore = System.Text.Json.JsonEncodedText.Encode("random_score");
	private static readonly System.Text.Json.JsonEncodedText VariantScriptScore = System.Text.Json.JsonEncodedText.Encode("script_score");

	public override Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propFilter = default;
		LocalJsonValue<double?> propWeight = default;
		string? variantType = null;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFilter.TryReadProperty(ref reader, options, PropFilter, null))
			{
				continue;
			}

			if (propWeight.TryReadProperty(ref reader, options, PropWeight, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (reader.ValueTextEquals(VariantExp))
			{
				variantType = VariantExp.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantFieldValueFactor))
			{
				variantType = VariantFieldValueFactor.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantGauss))
			{
				variantType = VariantGauss.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantLinear))
			{
				variantType = VariantLinear.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRandomScore))
			{
				variantType = VariantRandomScore.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantScriptScore))
			{
				variantType = VariantScriptScore.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction>(options, null);
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant,
			Filter = propFilter.Value,
			Weight = propWeight.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case null:
				break;
			case "exp":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction)value.Variant, null, null);
				break;
			case "field_value_factor":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction)value.Variant, null, null);
				break;
			case "gauss":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction)value.Variant, null, null);
				break;
			case "linear":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction)value.Variant, null, null);
				break;
			case "random_score":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction)value.Variant, null, null);
				break;
			case "script_score":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore)}'.");
		}

		writer.WriteProperty(options, PropFilter, value.Filter, null, null);
		writer.WriteProperty(options, PropWeight, value.Weight, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreConverter))]
public sealed partial class FunctionScore
{
	internal string? VariantType { get; set; }
	internal object? Variant { get; set; }
#if NET7_0_OR_GREATER
	public FunctionScore()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public FunctionScore()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FunctionScore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a exponential decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? Exp { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>("exp"); set => SetVariant("exp", value); }

	/// <summary>
	/// <para>
	/// Function allows you to use a field from a document to influence the score.
	/// It’s similar to using the script_score function, however, it avoids the overhead of scripting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction? FieldValueFactor { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction>("field_value_factor"); set => SetVariant("field_value_factor", value); }

	/// <summary>
	/// <para>
	/// Function that scores a document with a normal decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? Gauss { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>("gauss"); set => SetVariant("gauss", value); }

	/// <summary>
	/// <para>
	/// Function that scores a document with a linear decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? Linear { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>("linear"); set => SetVariant("linear", value); }

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction? RandomScore { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction>("random_score"); set => SetVariant("random_score", value); }

	/// <summary>
	/// <para>
	/// Enables you to wrap another query and customize the scoring of it optionally with a computation derived from other numeric field values in the doc using a script expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction? ScriptScore { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction>("script_score"); set => SetVariant("script_score", value); }
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Filter { get; set; }
	public double? Weight { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction value) => new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore { FieldValueFactor = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction value) => new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore { RandomScore = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction value) => new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore { ScriptScore = value };

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}

public readonly partial struct FunctionScoreDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FunctionScoreDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FunctionScoreDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore instance) => new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Function that scores a document with a exponential decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Exp(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? value)
	{
		Instance.Exp = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a exponential decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Exp(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Exp = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function allows you to use a field from a document to influence the score.
	/// It’s similar to using the script_score function, however, it avoids the overhead of scripting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> FieldValueFactor(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction? value)
	{
		Instance.FieldValueFactor = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function allows you to use a field from a document to influence the score.
	/// It’s similar to using the script_score function, however, it avoids the overhead of scripting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> FieldValueFactor(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument>> action)
	{
		Instance.FieldValueFactor = Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a normal decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Gauss(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? value)
	{
		Instance.Gauss = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a normal decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Gauss(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Gauss = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a linear decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Linear(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? value)
	{
		Instance.Linear = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a linear decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Linear(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Linear = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> RandomScore(Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction? value)
	{
		Instance.RandomScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> RandomScore()
	{
		Instance.RandomScore = Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> RandomScore(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor<TDocument>>? action)
	{
		Instance.RandomScore = Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Enables you to wrap another query and customize the scoring of it optionally with a computation derived from other numeric field values in the doc using a script expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> ScriptScore(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction? value)
	{
		Instance.ScriptScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Enables you to wrap another query and customize the scoring of it optionally with a computation derived from other numeric field values in the doc using a script expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> ScriptScore(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunctionDescriptor> action)
	{
		Instance.ScriptScore = Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunctionDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument> Weight(double? value)
	{
		Instance.Weight = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct FunctionScoreDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FunctionScoreDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FunctionScoreDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore instance) => new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Function that scores a document with a exponential decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Exp(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? value)
	{
		Instance.Exp = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a exponential decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Exp(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Exp = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a exponential decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Exp<T>(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<T>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Exp = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function allows you to use a field from a document to influence the score.
	/// It’s similar to using the script_score function, however, it avoids the overhead of scripting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor FieldValueFactor(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction? value)
	{
		Instance.FieldValueFactor = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function allows you to use a field from a document to influence the score.
	/// It’s similar to using the script_score function, however, it avoids the overhead of scripting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor FieldValueFactor(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor> action)
	{
		Instance.FieldValueFactor = Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function allows you to use a field from a document to influence the score.
	/// It’s similar to using the script_score function, however, it avoids the overhead of scripting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor FieldValueFactor<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<T>> action)
	{
		Instance.FieldValueFactor = Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a normal decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Gauss(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? value)
	{
		Instance.Gauss = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a normal decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Gauss(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Gauss = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a normal decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Gauss<T>(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<T>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Gauss = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a linear decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Linear(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction? value)
	{
		Instance.Linear = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a linear decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Linear(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Linear = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Function that scores a document with a linear decay, depending on the distance of a numeric field value of the document from an origin.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Linear<T>(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<T>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		Instance.Linear = Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor RandomScore(Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunction? value)
	{
		Instance.RandomScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor RandomScore()
	{
		Instance.RandomScore = Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor RandomScore(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor>? action)
	{
		Instance.RandomScore = Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Generates scores that are uniformly distributed from 0 up to but not including 1.
	/// In case you want scores to be reproducible, it is possible to provide a <c>seed</c> and <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor RandomScore<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor<T>>? action)
	{
		Instance.RandomScore = Elastic.Clients.Elasticsearch.QueryDsl.RandomScoreFunctionDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Enables you to wrap another query and customize the scoring of it optionally with a computation derived from other numeric field values in the doc using a script expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor ScriptScore(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunction? value)
	{
		Instance.ScriptScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Enables you to wrap another query and customize the scoring of it optionally with a computation derived from other numeric field values in the doc using a script expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor ScriptScore(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunctionDescriptor> action)
	{
		Instance.ScriptScore = Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreFunctionDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor Weight(double? value)
	{
		Instance.Weight = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScoreDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.FunctionScore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}