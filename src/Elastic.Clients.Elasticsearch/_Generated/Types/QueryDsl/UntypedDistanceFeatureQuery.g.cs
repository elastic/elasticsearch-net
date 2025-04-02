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

internal sealed partial class UntypedDistanceFeatureQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropOrigin = System.Text.Json.JsonEncodedText.Encode("origin");
	private static readonly System.Text.Json.JsonEncodedText PropPivot = System.Text.Json.JsonEncodedText.Encode("pivot");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");

	public override Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<object> propOrigin = default;
		LocalJsonValue<object> propPivot = default;
		LocalJsonValue<string?> propQueryName = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propOrigin.TryReadProperty(ref reader, options, PropOrigin, null))
			{
				continue;
			}

			if (propPivot.TryReadProperty(ref reader, options, PropPivot, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Field = propField.Value,
			Origin = propOrigin.Value,
			Pivot = propPivot.Value,
			QueryName = propQueryName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropOrigin, value.Origin, null, null);
		writer.WriteProperty(options, PropPivot, value.Pivot, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryConverter))]
public sealed partial class UntypedDistanceFeatureQuery : Elastic.Clients.Elasticsearch.QueryDsl.IDistanceFeatureQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Field field, object origin, object pivot)
	{
		Field = field;
		Origin = origin;
		Pivot = pivot;
	}
#if NET7_0_OR_GREATER
	public UntypedDistanceFeatureQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public UntypedDistanceFeatureQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Date or point of origin used to calculate distances.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>origin</c> value must be a date.
	/// Date Math, such as <c>now-1h</c>, is supported.
	/// If the field value is a <c>geo_point</c> field, the <c>origin</c> value must be a geopoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	object Origin { get; set; }

	/// <summary>
	/// <para>
	/// Distance from the <c>origin</c> at which relevance scores receive half of the <c>boost</c> value.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>pivot</c> value must be a time unit, such as <c>1h</c> or <c>10d</c>. If the <c>field</c> value is a <c>geo_point</c> field, the <c>pivot</c> value must be a distance unit, such as <c>1km</c> or <c>12m</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	object Pivot { get; set; }
	public string? QueryName { get; set; }

	string Elastic.Clients.Elasticsearch.QueryDsl.IDistanceFeatureQuery.Type => "untyped";
}

public readonly partial struct UntypedDistanceFeatureQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDistanceFeatureQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDistanceFeatureQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Date or point of origin used to calculate distances.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>origin</c> value must be a date.
	/// Date Math, such as <c>now-1h</c>, is supported.
	/// If the field value is a <c>geo_point</c> field, the <c>origin</c> value must be a geopoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> Origin(object value)
	{
		Instance.Origin = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Distance from the <c>origin</c> at which relevance scores receive half of the <c>boost</c> value.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>pivot</c> value must be a time unit, such as <c>1h</c> or <c>10d</c>. If the <c>field</c> value is a <c>geo_point</c> field, the <c>pivot</c> value must be a distance unit, such as <c>1km</c> or <c>12m</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> Pivot(object value)
	{
		Instance.Pivot = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct UntypedDistanceFeatureQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDistanceFeatureQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UntypedDistanceFeatureQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the field used to calculate distances. This field must meet the following criteria:
	/// be a <c>date</c>, <c>date_nanos</c> or <c>geo_point</c> field;
	/// have an <c>index</c> mapping parameter value of <c>true</c>, which is the default;
	/// have an <c>doc_values</c> mapping parameter value of <c>true</c>, which is the default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Date or point of origin used to calculate distances.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>origin</c> value must be a date.
	/// Date Math, such as <c>now-1h</c>, is supported.
	/// If the field value is a <c>geo_point</c> field, the <c>origin</c> value must be a geopoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor Origin(object value)
	{
		Instance.Origin = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Distance from the <c>origin</c> at which relevance scores receive half of the <c>boost</c> value.
	/// If the <c>field</c> value is a <c>date</c> or <c>date_nanos</c> field, the <c>pivot</c> value must be a time unit, such as <c>1h</c> or <c>10d</c>. If the <c>field</c> value is a <c>geo_point</c> field, the <c>pivot</c> value must be a distance unit, such as <c>1km</c> or <c>12m</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor Pivot(object value)
	{
		Instance.Pivot = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.UntypedDistanceFeatureQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}