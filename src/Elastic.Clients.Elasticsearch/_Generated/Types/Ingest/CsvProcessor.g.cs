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

internal sealed partial class CsvProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.CsvProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropEmptyValue = System.Text.Json.JsonEncodedText.Encode("empty_value");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropQuote = System.Text.Json.JsonEncodedText.Encode("quote");
	private static readonly System.Text.Json.JsonEncodedText PropSeparator = System.Text.Json.JsonEncodedText.Encode("separator");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTargetFields = System.Text.Json.JsonEncodedText.Encode("target_fields");
	private static readonly System.Text.Json.JsonEncodedText PropTrim = System.Text.Json.JsonEncodedText.Encode("trim");

	public override Elastic.Clients.Elasticsearch.Ingest.CsvProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<object?> propEmptyValue = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<string?> propQuote = default;
		LocalJsonValue<string?> propSeparator = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields> propTargetFields = default;
		LocalJsonValue<bool?> propTrim = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propEmptyValue.TryReadProperty(ref reader, options, PropEmptyValue, null))
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

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propQuote.TryReadProperty(ref reader, options, PropQuote, null))
			{
				continue;
			}

			if (propSeparator.TryReadProperty(ref reader, options, PropSeparator, null))
			{
				continue;
			}

			if (propTag.TryReadProperty(ref reader, options, PropTag, null))
			{
				continue;
			}

			if (propTargetFields.TryReadProperty(ref reader, options, PropTargetFields, static Elastic.Clients.Elasticsearch.Fields (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propTrim.TryReadProperty(ref reader, options, PropTrim, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			EmptyValue = propEmptyValue.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			OnFailure = propOnFailure.Value,
			Quote = propQuote.Value,
			Separator = propSeparator.Value,
			Tag = propTag.Value,
			TargetFields = propTargetFields.Value,
			Trim = propTrim.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.CsvProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropEmptyValue, value.EmptyValue, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, null);
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropQuote, value.Quote, null, null);
		writer.WriteProperty(options, PropSeparator, value.Separator, null, null);
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTargetFields, value.TargetFields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropTrim, value.Trim, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.CsvProcessorConverter))]
public sealed partial class CsvProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CsvProcessor(Elastic.Clients.Elasticsearch.Field field, Elastic.Clients.Elasticsearch.Fields targetFields)
	{
		Field = field;
		TargetFields = targetFields;
	}
#if NET7_0_OR_GREATER
	public CsvProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CsvProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CsvProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	/// Value used to fill empty fields.
	/// Empty fields are skipped if this is not provided.
	/// An empty field is one with no value (2 consecutive separators) or empty quotes (<c>""</c>).
	/// </para>
	/// </summary>
	public object? EmptyValue { get; set; }

	/// <summary>
	/// <para>
	/// The field to extract data from.
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
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Quote used in CSV, has to be single character string.
	/// </para>
	/// </summary>
	public string? Quote { get; set; }

	/// <summary>
	/// <para>
	/// Separator used in CSV, has to be single character string.
	/// </para>
	/// </summary>
	public string? Separator { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// The array of fields to assign extracted values to.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Fields TargetFields { get; set; }

	/// <summary>
	/// <para>
	/// Trim whitespaces in unquoted fields.
	/// </para>
	/// </summary>
	public bool? Trim { get; set; }
}

public readonly partial struct CsvProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.CsvProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CsvProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.CsvProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CsvProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.CsvProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to fill empty fields.
	/// Empty fields are skipped if this is not provided.
	/// An empty field is one with no value (2 consecutive separators) or empty quotes (<c>""</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> EmptyValue(object? value)
	{
		Instance.EmptyValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to extract data from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to extract data from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// Quote used in CSV, has to be single character string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Quote(string? value)
	{
		Instance.Quote = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Separator used in CSV, has to be single character string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Separator(string? value)
	{
		Instance.Separator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The array of fields to assign extracted values to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> TargetFields(Elastic.Clients.Elasticsearch.Fields value)
	{
		Instance.TargetFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The array of fields to assign extracted values to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> TargetFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.TargetFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Trim whitespaces in unquoted fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument> Trim(bool? value = true)
	{
		Instance.Trim = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.CsvProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct CsvProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.CsvProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CsvProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.CsvProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CsvProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.CsvProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Value used to fill empty fields.
	/// Empty fields are skipped if this is not provided.
	/// An empty field is one with no value (2 consecutive separators) or empty quotes (<c>""</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor EmptyValue(object? value)
	{
		Instance.EmptyValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to extract data from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to extract data from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// Quote used in CSV, has to be single character string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Quote(string? value)
	{
		Instance.Quote = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Separator used in CSV, has to be single character string.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Separator(string? value)
	{
		Instance.Separator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The array of fields to assign extracted values to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor TargetFields(Elastic.Clients.Elasticsearch.Fields value)
	{
		Instance.TargetFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The array of fields to assign extracted values to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor TargetFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.TargetFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Trim whitespaces in unquoted fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor Trim(bool? value = true)
	{
		Instance.Trim = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.CsvProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.CsvProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.CsvProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}