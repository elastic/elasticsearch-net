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

internal sealed partial class RemoveProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropKeep = System.Text.Json.JsonEncodedText.Encode("keep");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");

	public override Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propKeep = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<string?> propTag = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, static Elastic.Clients.Elasticsearch.Fields (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propIf.TryReadProperty(ref reader, options, PropIf, null))
			{
				continue;
			}

			if (propIgnoreFailure.TryReadProperty(ref reader, options, PropIgnoreFailure, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propIgnoreMissing.TryReadProperty(ref reader, options, PropIgnoreMissing, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propKeep.TryReadProperty(ref reader, options, PropKeep, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propTag.TryReadProperty(ref reader, options, PropTag, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			Keep = propKeep.Value,
			OnFailure = propOnFailure.Value,
			Tag = propTag.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropKeep, value.Keep, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorConverter))]
public sealed partial class RemoveProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveProcessor(Elastic.Clients.Elasticsearch.Fields field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public RemoveProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RemoveProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RemoveProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// Fields to be removed. Supports template snippets.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Fields Field { get; set; }

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
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public bool? IgnoreMissing { get; set; }

	/// <summary>
	/// <para>
	/// Fields to be kept. When set, all fields other than those specified are removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Keep { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }
}

public readonly partial struct RemoveProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be removed. Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Fields value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be removed. Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> Field(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be kept. When set, all fields other than those specified are removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> Keep(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Keep = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be kept. When set, all fields other than those specified are removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> Keep(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Keep = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct RemoveProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoveProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be removed. Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Fields value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be removed. Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor Field<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist or is <c>null</c>, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be kept. When set, all fields other than those specified are removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor Keep(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Keep = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Fields to be kept. When set, all fields other than those specified are removed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor Keep<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Keep = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.RemoveProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}