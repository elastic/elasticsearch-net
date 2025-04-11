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

internal sealed partial class GeoDecayFunctionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction>
{
	private static readonly System.Text.Json.JsonEncodedText PropMultiValueMode = System.Text.Json.JsonEncodedText.Encode("multi_value_mode");

	public override Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode?> propMultiValueMode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string>> propPlacement = default;
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			MultiValueMode = propMultiValueMode.Value,
			Placement = propPlacement.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMultiValueMode, value.MultiValueMode, null, null);
		writer.WriteProperty(options, value.Field, value.Placement, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionConverter))]
public sealed partial class GeoDecayFunction : Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDecayFunction(Elastic.Clients.Elasticsearch.Field field, Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> placement)
	{
		Field = field;
		Placement = placement;
	}
#if NET7_0_OR_GREATER
	public GeoDecayFunction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoDecayFunction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> Placement { get; set; }

	string Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction.Type => "geo";
}

public readonly partial struct GeoDecayFunctionDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDecayFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDecayFunctionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction instance) => new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? value)
	{
		Instance.MultiValueMode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> value)
	{
		Instance.Placement = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> Placement()
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfGeoLocationStringDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument> Placement(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfGeoLocationStringDescriptor>? action)
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfGeoLocationStringDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoDecayFunctionDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDecayFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDecayFunctionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction instance) => new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? value)
	{
		Instance.MultiValueMode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> value)
	{
		Instance.Placement = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor Placement()
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfGeoLocationStringDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor Placement(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfGeoLocationStringDescriptor>? action)
	{
		Instance.Placement = Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacementOfGeoLocationStringDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}