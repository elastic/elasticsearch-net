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

internal sealed partial class DateIndexNameProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDateFormats = System.Text.Json.JsonEncodedText.Encode("date_formats");
	private static readonly System.Text.Json.JsonEncodedText PropDateRounding = System.Text.Json.JsonEncodedText.Encode("date_rounding");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIndexNameFormat = System.Text.Json.JsonEncodedText.Encode("index_name_format");
	private static readonly System.Text.Json.JsonEncodedText PropIndexNamePrefix = System.Text.Json.JsonEncodedText.Encode("index_name_prefix");
	private static readonly System.Text.Json.JsonEncodedText PropLocale = System.Text.Json.JsonEncodedText.Encode("locale");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTimezone = System.Text.Json.JsonEncodedText.Encode("timezone");

	public override Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propDateFormats = default;
		LocalJsonValue<string> propDateRounding = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<string?> propIndexNameFormat = default;
		LocalJsonValue<string?> propIndexNamePrefix = default;
		LocalJsonValue<string?> propLocale = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<string?> propTimezone = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDateFormats.TryReadProperty(ref reader, options, PropDateFormats, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propDateRounding.TryReadProperty(ref reader, options, PropDateRounding, null))
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

			if (propIgnoreFailure.TryReadProperty(ref reader, options, PropIgnoreFailure, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propIndexNameFormat.TryReadProperty(ref reader, options, PropIndexNameFormat, null))
			{
				continue;
			}

			if (propIndexNamePrefix.TryReadProperty(ref reader, options, PropIndexNamePrefix, null))
			{
				continue;
			}

			if (propLocale.TryReadProperty(ref reader, options, PropLocale, null))
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

			if (propTimezone.TryReadProperty(ref reader, options, PropTimezone, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DateFormats = propDateFormats.Value,
			DateRounding = propDateRounding.Value,
			Description = propDescription.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IndexNameFormat = propIndexNameFormat.Value,
			IndexNamePrefix = propIndexNamePrefix.Value,
			Locale = propLocale.Value,
			OnFailure = propOnFailure.Value,
			Tag = propTag.Value,
			Timezone = propTimezone.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDateFormats, value.DateFormats, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropDateRounding, value.DateRounding, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropIndexNameFormat, value.IndexNameFormat, null, null);
		writer.WriteProperty(options, PropIndexNamePrefix, value.IndexNamePrefix, null, null);
		writer.WriteProperty(options, PropLocale, value.Locale, null, null);
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTimezone, value.Timezone, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorConverter))]
public sealed partial class DateIndexNameProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateIndexNameProcessor(string dateRounding, Elastic.Clients.Elasticsearch.Field field)
	{
		DateRounding = dateRounding;
		Field = field;
	}
#if NET7_0_OR_GREATER
	public DateIndexNameProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DateIndexNameProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// An array of the expected date formats for parsing dates / timestamps in the document being preprocessed.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? DateFormats { get; set; }

	/// <summary>
	/// <para>
	/// How to round the date when formatting the date into the index name. Valid values are:
	/// <c>y</c> (year), <c>M</c> (month), <c>w</c> (week), <c>d</c> (day), <c>h</c> (hour), <c>m</c> (minute) and <c>s</c> (second).
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string DateRounding { get; set; }

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The field to get the date or timestamp from.
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
	/// The format to be used when printing the parsed date into the index name.
	/// A valid java time pattern is expected here.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public string? IndexNameFormat { get; set; }

	/// <summary>
	/// <para>
	/// A prefix of the index name to be prepended before the printed date.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public string? IndexNamePrefix { get; set; }

	/// <summary>
	/// <para>
	/// The locale to use when parsing the date from the document being preprocessed, relevant when parsing month names or week days.
	/// </para>
	/// </summary>
	public string? Locale { get; set; }

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

	/// <summary>
	/// <para>
	/// The timezone to use when parsing the date and when date math index supports resolves expressions into concrete index names.
	/// </para>
	/// </summary>
	public string? Timezone { get; set; }
}

public readonly partial struct DateIndexNameProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateIndexNameProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateIndexNameProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// An array of the expected date formats for parsing dates / timestamps in the document being preprocessed.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> DateFormats(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.DateFormats = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of the expected date formats for parsing dates / timestamps in the document being preprocessed.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> DateFormats(params string[] values)
	{
		Instance.DateFormats = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// How to round the date when formatting the date into the index name. Valid values are:
	/// <c>y</c> (year), <c>M</c> (month), <c>w</c> (week), <c>d</c> (day), <c>h</c> (hour), <c>m</c> (minute) and <c>s</c> (second).
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> DateRounding(string value)
	{
		Instance.DateRounding = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to get the date or timestamp from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to get the date or timestamp from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The format to be used when printing the parsed date into the index name.
	/// A valid java time pattern is expected here.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> IndexNameFormat(string? value)
	{
		Instance.IndexNameFormat = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A prefix of the index name to be prepended before the printed date.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> IndexNamePrefix(string? value)
	{
		Instance.IndexNamePrefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The locale to use when parsing the date from the document being preprocessed, relevant when parsing month names or week days.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> Locale(string? value)
	{
		Instance.Locale = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The timezone to use when parsing the date and when date math index supports resolves expressions into concrete index names.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument> Timezone(string? value)
	{
		Instance.Timezone = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct DateIndexNameProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateIndexNameProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateIndexNameProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// An array of the expected date formats for parsing dates / timestamps in the document being preprocessed.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor DateFormats(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.DateFormats = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of the expected date formats for parsing dates / timestamps in the document being preprocessed.
	/// Can be a java time pattern or one of the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor DateFormats(params string[] values)
	{
		Instance.DateFormats = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// How to round the date when formatting the date into the index name. Valid values are:
	/// <c>y</c> (year), <c>M</c> (month), <c>w</c> (week), <c>d</c> (day), <c>h</c> (hour), <c>m</c> (minute) and <c>s</c> (second).
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor DateRounding(string value)
	{
		Instance.DateRounding = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to get the date or timestamp from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to get the date or timestamp from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The format to be used when printing the parsed date into the index name.
	/// A valid java time pattern is expected here.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor IndexNameFormat(string? value)
	{
		Instance.IndexNameFormat = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A prefix of the index name to be prepended before the printed date.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor IndexNamePrefix(string? value)
	{
		Instance.IndexNamePrefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The locale to use when parsing the date from the document being preprocessed, relevant when parsing month names or week days.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor Locale(string? value)
	{
		Instance.Locale = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The timezone to use when parsing the date and when date math index supports resolves expressions into concrete index names.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor Timezone(string? value)
	{
		Instance.Timezone = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.DateIndexNameProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}