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

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class GeoGridProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropChildrenField = System.Text.Json.JsonEncodedText.Encode("children_field");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropNonChildrenField = System.Text.Json.JsonEncodedText.Encode("non_children_field");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropParentField = System.Text.Json.JsonEncodedText.Encode("parent_field");
	private static readonly System.Text.Json.JsonEncodedText PropPrecisionField = System.Text.Json.JsonEncodedText.Encode("precision_field");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTargetField = System.Text.Json.JsonEncodedText.Encode("target_field");
	private static readonly System.Text.Json.JsonEncodedText PropTargetFormat = System.Text.Json.JsonEncodedText.Encode("target_format");
	private static readonly System.Text.Json.JsonEncodedText PropTileType = System.Text.Json.JsonEncodedText.Encode("tile_type");

	public override Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propChildrenField = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<string> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propNonChildrenField = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propParentField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propPrecisionField = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propTargetField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Ingest.GeoGridTargetFormat?> propTargetFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Ingest.GeoGridTileType> propTileType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propChildrenField.TryReadProperty(ref reader, options, PropChildrenField, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propIf.TryReadProperty(ref reader, options, PropIf, null))
			{
				continue;
			}

			if (propIgnoreFailure.TryReadProperty(ref reader, options, PropIgnoreFailure, null))
			{
				continue;
			}

			if (propIgnoreMissing.TryReadProperty(ref reader, options, PropIgnoreMissing, null))
			{
				continue;
			}

			if (propNonChildrenField.TryReadProperty(ref reader, options, PropNonChildrenField, null))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propParentField.TryReadProperty(ref reader, options, PropParentField, null))
			{
				continue;
			}

			if (propPrecisionField.TryReadProperty(ref reader, options, PropPrecisionField, null))
			{
				continue;
			}

			if (propTag.TryReadProperty(ref reader, options, PropTag, null))
			{
				continue;
			}

			if (propTargetField.TryReadProperty(ref reader, options, PropTargetField, null))
			{
				continue;
			}

			if (propTargetFormat.TryReadProperty(ref reader, options, PropTargetFormat, null))
			{
				continue;
			}

			if (propTileType.TryReadProperty(ref reader, options, PropTileType, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChildrenField = propChildrenField.Value,
			Description = propDescription.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			NonChildrenField = propNonChildrenField.Value,
			OnFailure = propOnFailure.Value,
			ParentField = propParentField.Value,
			PrecisionField = propPrecisionField.Value,
			Tag = propTag.Value,
			TargetField = propTargetField.Value,
			TargetFormat = propTargetFormat.Value,
			TileType = propTileType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropChildrenField, value.ChildrenField, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, null);
		writer.WriteProperty(options, PropNonChildrenField, value.NonChildrenField, null, null);
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropParentField, value.ParentField, null, null);
		writer.WriteProperty(options, PropPrecisionField, value.PrecisionField, null, null);
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTargetField, value.TargetField, null, null);
		writer.WriteProperty(options, PropTargetFormat, value.TargetFormat, null, null);
		writer.WriteProperty(options, PropTileType, value.TileType, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorConverter))]
public sealed partial class GeoGridProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoGridProcessor(string field, Elastic.Clients.Elasticsearch.Ingest.GeoGridTileType tileType)
	{
		Field = field;
		TileType = tileType;
	}
#if NET7_0_OR_GREATER
	public GeoGridProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoGridProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoGridProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If specified and children tiles exist, save those tile addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? ChildrenField { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The field to interpret as a geo-tile.=
	/// The field format is determined by the <c>tile_type</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Field { get; set; }

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? If { get; set; }

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public bool? IgnoreMissing { get; set; }

	/// <summary>
	/// <para>
	/// If specified and intersecting non-child tiles exist, save their addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? NonChildrenField { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// If specified and a parent tile exists, save that tile address to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? ParentField { get; set; }

	/// <summary>
	/// <para>
	/// If specified, save the tile precision (zoom) as an integer to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? PrecisionField { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// The field to assign the polygon shape to, by default, the <c>field</c> is updated in-place.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }

	/// <summary>
	/// <para>
	/// Which format to save the generated polygon in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridTargetFormat? TargetFormat { get; set; }

	/// <summary>
	/// <para>
	/// Three tile formats are understood: geohash, geotile and geohex.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Ingest.GeoGridTileType TileType { get; set; }
}

public readonly partial struct GeoGridProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoGridProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoGridProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If specified and children tiles exist, save those tile addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> ChildrenField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and children tiles exist, save those tile addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> ChildrenField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.ChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to interpret as a geo-tile.=
	/// The field format is determined by the <c>tile_type</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> Field(string value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and intersecting non-child tiles exist, save their addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> NonChildrenField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.NonChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and intersecting non-child tiles exist, save their addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> NonChildrenField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.NonChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and a parent tile exists, save that tile address to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> ParentField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ParentField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and a parent tile exists, save that tile address to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> ParentField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.ParentField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, save the tile precision (zoom) as an integer to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> PrecisionField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.PrecisionField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, save the tile precision (zoom) as an integer to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> PrecisionField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.PrecisionField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to assign the polygon shape to, by default, the <c>field</c> is updated in-place.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to assign the polygon shape to, by default, the <c>field</c> is updated in-place.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> TargetField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Which format to save the generated polygon in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> TargetFormat(Elastic.Clients.Elasticsearch.Ingest.GeoGridTargetFormat? value)
	{
		Instance.TargetFormat = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Three tile formats are understood: geohash, geotile and geohex.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument> TileType(Elastic.Clients.Elasticsearch.Ingest.GeoGridTileType value)
	{
		Instance.TileType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoGridProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoGridProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoGridProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If specified and children tiles exist, save those tile addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor ChildrenField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and children tiles exist, save those tile addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor ChildrenField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.ChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to interpret as a geo-tile.=
	/// The field format is determined by the <c>tile_type</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor Field(string value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and intersecting non-child tiles exist, save their addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor NonChildrenField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.NonChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and intersecting non-child tiles exist, save their addresses to this field as an array of strings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor NonChildrenField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.NonChildrenField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and a parent tile exists, save that tile address to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor ParentField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ParentField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified and a parent tile exists, save that tile address to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor ParentField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.ParentField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, save the tile precision (zoom) as an integer to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor PrecisionField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.PrecisionField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, save the tile precision (zoom) as an integer to this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor PrecisionField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.PrecisionField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to assign the polygon shape to, by default, the <c>field</c> is updated in-place.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to assign the polygon shape to, by default, the <c>field</c> is updated in-place.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor TargetField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Which format to save the generated polygon in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor TargetFormat(Elastic.Clients.Elasticsearch.Ingest.GeoGridTargetFormat? value)
	{
		Instance.TargetFormat = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Three tile formats are understood: geohash, geotile and geohex.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor TileType(Elastic.Clients.Elasticsearch.Ingest.GeoGridTileType value)
	{
		Instance.TileType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.GeoGridProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}