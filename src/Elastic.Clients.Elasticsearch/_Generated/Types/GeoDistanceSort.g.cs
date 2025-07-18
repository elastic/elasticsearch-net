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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class GeoDistanceSortConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.GeoDistanceSort>
{
	private static readonly System.Text.Json.JsonEncodedText PropDistanceType = System.Text.Json.JsonEncodedText.Encode("distance_type");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreUnmapped = System.Text.Json.JsonEncodedText.Encode("ignore_unmapped");
	private static readonly System.Text.Json.JsonEncodedText PropMode = System.Text.Json.JsonEncodedText.Encode("mode");
	private static readonly System.Text.Json.JsonEncodedText PropNested = System.Text.Json.JsonEncodedText.Encode("nested");
	private static readonly System.Text.Json.JsonEncodedText PropOrder = System.Text.Json.JsonEncodedText.Encode("order");
	private static readonly System.Text.Json.JsonEncodedText PropUnit = System.Text.Json.JsonEncodedText.Encode("unit");

	public override Elastic.Clients.Elasticsearch.GeoDistanceSort Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoDistanceType?> propDistanceType = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<bool?> propIgnoreUnmapped = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation>> propLocation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SortMode?> propMode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.NestedSortValue?> propNested = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SortOrder?> propOrder = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DistanceUnit?> propUnit = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDistanceType.TryReadProperty(ref reader, options, PropDistanceType, static Elastic.Clients.Elasticsearch.GeoDistanceType? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.GeoDistanceType>(o)))
			{
				continue;
			}

			if (propIgnoreUnmapped.TryReadProperty(ref reader, options, PropIgnoreUnmapped, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propMode.TryReadProperty(ref reader, options, PropMode, static Elastic.Clients.Elasticsearch.SortMode? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.SortMode>(o)))
			{
				continue;
			}

			if (propNested.TryReadProperty(ref reader, options, PropNested, null))
			{
				continue;
			}

			if (propOrder.TryReadProperty(ref reader, options, PropOrder, static Elastic.Clients.Elasticsearch.SortOrder? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.SortOrder>(o)))
			{
				continue;
			}

			if (propUnit.TryReadProperty(ref reader, options, PropUnit, static Elastic.Clients.Elasticsearch.DistanceUnit? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.DistanceUnit>(o)))
			{
				continue;
			}

			propField.Initialized = propLocation.Initialized = true;
			reader.ReadProperty(options, out propField.Value, out propLocation.Value, static Elastic.Clients.Elasticsearch.Field (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadPropertyName<Elastic.Clients.Elasticsearch.Field>(o), static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.GeoLocation>(o, null)!);
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DistanceType = propDistanceType.Value,
			Field = propField.Value,
			IgnoreUnmapped = propIgnoreUnmapped.Value,
			Location = propLocation.Value,
			Mode = propMode.Value,
			Nested = propNested.Value,
			Order = propOrder.Value,
			Unit = propUnit.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.GeoDistanceSort value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDistanceType, value.DistanceType, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.GeoDistanceType? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.GeoDistanceType>(o, v));
		writer.WriteProperty(options, PropIgnoreUnmapped, value.IgnoreUnmapped, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropMode, value.Mode, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.SortMode? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.SortMode>(o, v));
		writer.WriteProperty(options, PropNested, value.Nested, null, null);
		writer.WriteProperty(options, PropOrder, value.Order, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.SortOrder? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.SortOrder>(o, v));
		writer.WriteProperty(options, PropUnit, value.Unit, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.DistanceUnit? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.DistanceUnit>(o, v));
		writer.WriteProperty(options, value.Field, value.Location, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Field v) => w.WritePropertyName<Elastic.Clients.Elasticsearch.Field>(o, v), static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation> v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.GeoLocation>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.GeoDistanceSortConverter))]
public sealed partial class GeoDistanceSort
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceSort(Elastic.Clients.Elasticsearch.Field field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation> location)
	{
		Field = field;
		Location = location;
	}
#if NET7_0_OR_GREATER
	public GeoDistanceSort()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoDistanceSort()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoDistanceSort(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceType? DistanceType { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }
	public bool? IgnoreUnmapped { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation> Location { get; set; }
	public Elastic.Clients.Elasticsearch.SortMode? Mode { get; set; }
	public Elastic.Clients.Elasticsearch.NestedSortValue? Nested { get; set; }
	public Elastic.Clients.Elasticsearch.SortOrder? Order { get; set; }
	public Elastic.Clients.Elasticsearch.DistanceUnit? Unit { get; set; }
}

public readonly partial struct GeoDistanceSortDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.GeoDistanceSort Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceSortDescriptor(Elastic.Clients.Elasticsearch.GeoDistanceSort instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceSortDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument>(Elastic.Clients.Elasticsearch.GeoDistanceSort instance) => new Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> DistanceType(Elastic.Clients.Elasticsearch.GeoDistanceType? value)
	{
		Instance.DistanceType = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> IgnoreUnmapped(bool? value = true)
	{
		Instance.IgnoreUnmapped = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Location(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation> value)
	{
		Instance.Location = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Location(params Elastic.Clients.Elasticsearch.GeoLocation[] values)
	{
		Instance.Location = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Location(params System.Func<Elastic.Clients.Elasticsearch.GeoLocationFactory, Elastic.Clients.Elasticsearch.GeoLocation>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.GeoLocation>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.GeoLocationFactory.Build(action));
		}

		Instance.Location = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Mode(Elastic.Clients.Elasticsearch.SortMode? value)
	{
		Instance.Mode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Nested(Elastic.Clients.Elasticsearch.NestedSortValue? value)
	{
		Instance.Nested = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Nested(System.Action<Elastic.Clients.Elasticsearch.NestedSortValueDescriptor<TDocument>> action)
	{
		Instance.Nested = Elastic.Clients.Elasticsearch.NestedSortValueDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Order(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.Order = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument> Unit(Elastic.Clients.Elasticsearch.DistanceUnit? value)
	{
		Instance.Unit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.GeoDistanceSort Build(System.Action<Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoDistanceSortDescriptor
{
	internal Elastic.Clients.Elasticsearch.GeoDistanceSort Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceSortDescriptor(Elastic.Clients.Elasticsearch.GeoDistanceSort instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoDistanceSortDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor(Elastic.Clients.Elasticsearch.GeoDistanceSort instance) => new Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor DistanceType(Elastic.Clients.Elasticsearch.GeoDistanceType? value)
	{
		Instance.DistanceType = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor IgnoreUnmapped(bool? value = true)
	{
		Instance.IgnoreUnmapped = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Location(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.GeoLocation> value)
	{
		Instance.Location = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Location(params Elastic.Clients.Elasticsearch.GeoLocation[] values)
	{
		Instance.Location = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Location(params System.Func<Elastic.Clients.Elasticsearch.GeoLocationFactory, Elastic.Clients.Elasticsearch.GeoLocation>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.GeoLocation>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.GeoLocationFactory.Build(action));
		}

		Instance.Location = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Mode(Elastic.Clients.Elasticsearch.SortMode? value)
	{
		Instance.Mode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Nested(Elastic.Clients.Elasticsearch.NestedSortValue? value)
	{
		Instance.Nested = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Nested(System.Action<Elastic.Clients.Elasticsearch.NestedSortValueDescriptor> action)
	{
		Instance.Nested = Elastic.Clients.Elasticsearch.NestedSortValueDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Nested<T>(System.Action<Elastic.Clients.Elasticsearch.NestedSortValueDescriptor<T>> action)
	{
		Instance.Nested = Elastic.Clients.Elasticsearch.NestedSortValueDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Order(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.Order = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor Unit(Elastic.Clients.Elasticsearch.DistanceUnit? value)
	{
		Instance.Unit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.GeoDistanceSort Build(System.Action<Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.GeoDistanceSortDescriptor(new Elastic.Clients.Elasticsearch.GeoDistanceSort(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}