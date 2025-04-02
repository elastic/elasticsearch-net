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

internal sealed partial class FieldValueFactorScoreFunctionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction>
{
	private static readonly System.Text.Json.JsonEncodedText PropFactor = System.Text.Json.JsonEncodedText.Encode("factor");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropMissing = System.Text.Json.JsonEncodedText.Encode("missing");
	private static readonly System.Text.Json.JsonEncodedText PropModifier = System.Text.Json.JsonEncodedText.Encode("modifier");

	public override Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propFactor = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<double?> propMissing = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier?> propModifier = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFactor.TryReadProperty(ref reader, options, PropFactor, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propMissing.TryReadProperty(ref reader, options, PropMissing, null))
			{
				continue;
			}

			if (propModifier.TryReadProperty(ref reader, options, PropModifier, null))
			{
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Factor = propFactor.Value,
			Field = propField.Value,
			Missing = propMissing.Value,
			Modifier = propModifier.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFactor, value.Factor, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropMissing, value.Missing, null, null);
		writer.WriteProperty(options, PropModifier, value.Modifier, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionConverter))]
public sealed partial class FieldValueFactorScoreFunction
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public FieldValueFactorScoreFunction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FieldValueFactorScoreFunction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Optional factor to multiply the field value with.
	/// </para>
	/// </summary>
	public double? Factor { get; set; }

	/// <summary>
	/// <para>
	/// Field to be extracted from the document.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Value used if the document doesn’t have that field.
	/// The modifier and factor are still applied to it as though it were read from the document.
	/// </para>
	/// </summary>
	public double? Missing { get; set; }

	/// <summary>
	/// <para>
	/// Modifier to apply to the field value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? Modifier { get; set; }
}

public readonly partial struct FieldValueFactorScoreFunctionDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldValueFactorScoreFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldValueFactorScoreFunctionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction instance) => new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Optional factor to multiply the field value with.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument> Factor(double? value)
	{
		Instance.Factor = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field to be extracted from the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field to be extracted from the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used if the document doesn’t have that field.
	/// The modifier and factor are still applied to it as though it were read from the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument> Missing(double? value)
	{
		Instance.Missing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Modifier to apply to the field value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument> Modifier(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? value)
	{
		Instance.Modifier = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct FieldValueFactorScoreFunctionDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldValueFactorScoreFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldValueFactorScoreFunctionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction instance) => new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Optional factor to multiply the field value with.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor Factor(double? value)
	{
		Instance.Factor = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field to be extracted from the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field to be extracted from the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used if the document doesn’t have that field.
	/// The modifier and factor are still applied to it as though it were read from the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor Missing(double? value)
	{
		Instance.Missing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Modifier to apply to the field value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor Modifier(Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorModifier? value)
	{
		Instance.Modifier = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunctionDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.FieldValueFactorScoreFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}