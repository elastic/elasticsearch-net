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

internal sealed partial class GeoDistanceQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropDistance = System.Text.Json.JsonEncodedText.Encode("distance");
	private static readonly System.Text.Json.JsonEncodedText PropDistanceType = System.Text.Json.JsonEncodedText.Encode("distance_type");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreUnmapped = System.Text.Json.JsonEncodedText.Encode("ignore_unmapped");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropValidationMethod = System.Text.Json.JsonEncodedText.Encode("validation_method");

	public override Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<string> propDistance = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoDistanceType?> propDistanceType = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<bool?> propIgnoreUnmapped = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoLocation> propLocation = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod?> propValidationMethod = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propDistance.TryReadProperty(ref reader, options, PropDistance, null))
			{
				continue;
			}

			if (propDistanceType.TryReadProperty(ref reader, options, PropDistanceType, null))
			{
				continue;
			}

			if (propIgnoreUnmapped.TryReadProperty(ref reader, options, PropIgnoreUnmapped, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propValidationMethod.TryReadProperty(ref reader, options, PropValidationMethod, null))
			{
				continue;
			}

			propField.Initialized = propLocation.Initialized = true;
			reader.ReadProperty(options, out propField.Value, out propLocation.Value, null, null);
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Distance = propDistance.Value,
			DistanceType = propDistanceType.Value,
			Field = propField.Value,
			IgnoreUnmapped = propIgnoreUnmapped.Value,
			Location = propLocation.Value,
			QueryName = propQueryName.Value,
			ValidationMethod = propValidationMethod.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropDistance, value.Distance, null, null);
		writer.WriteProperty(options, PropDistanceType, value.DistanceType, null, null);
		writer.WriteProperty(options, PropIgnoreUnmapped, value.IgnoreUnmapped, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropValidationMethod, value.ValidationMethod, null, null);
		writer.WriteProperty(options, value.Field, value.Location, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryConverter))]
public sealed partial class GeoDistanceQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceQuery(string distance, Elastic.Clients.Elasticsearch.Field field, Elastic.Clients.Elasticsearch.GeoLocation location)
	{
		Distance = distance;
		Field = field;
		Location = location;
	}
#if NET7_0_OR_GREATER
	public GeoDistanceQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoDistanceQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoDistanceQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	/// The radius of the circle centred on the specified location.
	/// Points which fall into this circle are considered to be matches.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Distance { get; set; }

	/// <summary>
	/// <para>
	/// How to compute the distance.
	/// Set to <c>plane</c> for a faster calculation that's inaccurate on long distances and close to the poles.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.GeoDistanceType? DistanceType { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Set to <c>true</c> to ignore an unmapped field and not match any documents for this query.
	/// Set to <c>false</c> to throw an exception if the field is not mapped.
	/// </para>
	/// </summary>
	public bool? IgnoreUnmapped { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.GeoLocation Location { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Set to <c>IGNORE_MALFORMED</c> to accept geo points with invalid latitude or longitude.
	/// Set to <c>COERCE</c> to also try to infer correct latitude or longitude.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? ValidationMethod { get; set; }
}

public readonly partial struct GeoDistanceQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The radius of the circle centred on the specified location.
	/// Points which fall into this circle are considered to be matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> Distance(string value)
	{
		Instance.Distance = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// How to compute the distance.
	/// Set to <c>plane</c> for a faster calculation that's inaccurate on long distances and close to the poles.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> DistanceType(Elastic.Clients.Elasticsearch.GeoDistanceType? value)
	{
		Instance.DistanceType = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Set to <c>true</c> to ignore an unmapped field and not match any documents for this query.
	/// Set to <c>false</c> to throw an exception if the field is not mapped.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> IgnoreUnmapped(bool? value = true)
	{
		Instance.IgnoreUnmapped = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> Location(Elastic.Clients.Elasticsearch.GeoLocation value)
	{
		Instance.Location = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> Location(System.Func<Elastic.Clients.Elasticsearch.GeoLocationFactory, Elastic.Clients.Elasticsearch.GeoLocation> action)
	{
		Instance.Location = Elastic.Clients.Elasticsearch.GeoLocationFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Set to <c>IGNORE_MALFORMED</c> to accept geo points with invalid latitude or longitude.
	/// Set to <c>COERCE</c> to also try to infer correct latitude or longitude.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument> ValidationMethod(Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? value)
	{
		Instance.ValidationMethod = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoDistanceQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The radius of the circle centred on the specified location.
	/// Points which fall into this circle are considered to be matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor Distance(string value)
	{
		Instance.Distance = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// How to compute the distance.
	/// Set to <c>plane</c> for a faster calculation that's inaccurate on long distances and close to the poles.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor DistanceType(Elastic.Clients.Elasticsearch.GeoDistanceType? value)
	{
		Instance.DistanceType = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Set to <c>true</c> to ignore an unmapped field and not match any documents for this query.
	/// Set to <c>false</c> to throw an exception if the field is not mapped.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor IgnoreUnmapped(bool? value = true)
	{
		Instance.IgnoreUnmapped = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor Location(Elastic.Clients.Elasticsearch.GeoLocation value)
	{
		Instance.Location = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor Location(System.Func<Elastic.Clients.Elasticsearch.GeoLocationFactory, Elastic.Clients.Elasticsearch.GeoLocation> action)
	{
		Instance.Location = Elastic.Clients.Elasticsearch.GeoLocationFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Set to <c>IGNORE_MALFORMED</c> to accept geo points with invalid latitude or longitude.
	/// Set to <c>COERCE</c> to also try to infer correct latitude or longitude.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor ValidationMethod(Elastic.Clients.Elasticsearch.QueryDsl.GeoValidationMethod? value)
	{
		Instance.ValidationMethod = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.GeoDistanceQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}