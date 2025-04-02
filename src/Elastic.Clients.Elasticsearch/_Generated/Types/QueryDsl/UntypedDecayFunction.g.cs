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

internal sealed partial class UntypedDecayFunctionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction>
{
	private static readonly System.Text.Json.JsonEncodedText PropMultiValueMode = System.Text.Json.JsonEncodedText.Encode("multi_value_mode");

	public override Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode?> propMultiValueMode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<object, object>> propPlacement = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMultiValueMode.TryReadProperty(ref reader, options, PropMultiValueMode, null))
			{
				continue;
			}

			propField.Initialized = propPlacement.Initialized = true;
			reader.ReadProperty(options, out propField.Value, out propPlacement.Value, null, null);
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			MultiValueMode = propMultiValueMode.Value,
			Placement = propPlacement.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMultiValueMode, value.MultiValueMode, null, null);
		writer.WriteProperty(options, value.Field, value.Placement, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionConverter))]
public sealed partial class UntypedDecayFunction : Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDecayFunction(Elastic.Clients.Elasticsearch.Field field, Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<object, object> placement)
	{
		Field = field;
		Placement = placement;
	}
#if NET7_0_OR_GREATER
	public UntypedDecayFunction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public UntypedDecayFunction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UntypedDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueMode { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<object, object> Placement { get; set; }

	string Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction.Type => "untyped";
}

public readonly partial struct UntypedDecayFunctionDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDecayFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDecayFunctionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction instance) => new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? value)
	{
		Instance.MultiValueMode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<object, object> value)
	{
		Instance.Placement = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> Placement()
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfObjectObjectDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument> Placement(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfObjectObjectDescriptor>? action)
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfObjectObjectDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct UntypedDecayFunctionDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDecayFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDecayFunctionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction instance) => new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? value)
	{
		Instance.MultiValueMode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<object, object> value)
	{
		Instance.Placement = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor Placement()
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfObjectObjectDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor Placement(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfObjectObjectDescriptor>? action)
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfObjectObjectDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}