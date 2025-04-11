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

internal sealed partial class UriPartsProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropKeepOriginal = System.Text.Json.JsonEncodedText.Encode("keep_original");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropRemoveIfSuccessful = System.Text.Json.JsonEncodedText.Encode("remove_if_successful");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTargetField = System.Text.Json.JsonEncodedText.Encode("target_field");

	public override Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<bool?> propKeepOriginal = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<bool?> propRemoveIfSuccessful = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propTargetField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
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

			if (propKeepOriginal.TryReadProperty(ref reader, options, PropKeepOriginal, null))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propRemoveIfSuccessful.TryReadProperty(ref reader, options, PropRemoveIfSuccessful, null))
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

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			KeepOriginal = propKeepOriginal.Value,
			OnFailure = propOnFailure.Value,
			RemoveIfSuccessful = propRemoveIfSuccessful.Value,
			Tag = propTag.Value,
			TargetField = propTargetField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, null);
		writer.WriteProperty(options, PropKeepOriginal, value.KeepOriginal, null, null);
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropRemoveIfSuccessful, value.RemoveIfSuccessful, null, null);
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTargetField, value.TargetField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorConverter))]
public sealed partial class UriPartsProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UriPartsProcessor(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public UriPartsProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public UriPartsProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UriPartsProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	/// Field containing the URI string.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

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
	/// If <c>true</c>, the processor copies the unparsed URI to <c>&lt;target_field>.original</c>.
	/// </para>
	/// </summary>
	public bool? KeepOriginal { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the processor removes the <c>field</c> after parsing the URI string.
	/// If parsing fails, the processor does not remove the <c>field</c>.
	/// </para>
	/// </summary>
	public bool? RemoveIfSuccessful { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// Output field for the URI object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? TargetField { get; set; }
}

public readonly partial struct UriPartsProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UriPartsProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UriPartsProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing the URI string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing the URI string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the processor copies the unparsed URI to <c>&lt;target_field>.original</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> KeepOriginal(bool? value = true)
	{
		Instance.KeepOriginal = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// If <c>true</c>, the processor removes the <c>field</c> after parsing the URI string.
	/// If parsing fails, the processor does not remove the <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> RemoveIfSuccessful(bool? value = true)
	{
		Instance.RemoveIfSuccessful = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Output field for the URI object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Output field for the URI object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument> TargetField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct UriPartsProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UriPartsProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UriPartsProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing the URI string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field containing the URI string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the processor copies the unparsed URI to <c>&lt;target_field>.original</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor KeepOriginal(bool? value = true)
	{
		Instance.KeepOriginal = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// If <c>true</c>, the processor removes the <c>field</c> after parsing the URI string.
	/// If parsing fails, the processor does not remove the <c>field</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor RemoveIfSuccessful(bool? value = true)
	{
		Instance.RemoveIfSuccessful = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Output field for the URI object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Output field for the URI object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor TargetField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.UriPartsProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}