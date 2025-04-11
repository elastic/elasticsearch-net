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

internal sealed partial class PipelineConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.Pipeline>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeprecated = System.Text.Json.JsonEncodedText.Encode("deprecated");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropProcessors = System.Text.Json.JsonEncodedText.Encode("processors");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Ingest.Pipeline Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propDeprecated = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propMeta = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propProcessors = default;
		LocalJsonValue<long?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDeprecated.TryReadProperty(ref reader, options, PropDeprecated, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propProcessors.TryReadProperty(ref reader, options, PropProcessors, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Deprecated = propDeprecated.Value,
			Description = propDescription.Value,
			Meta = propMeta.Value,
			OnFailure = propOnFailure.Value,
			Processors = propProcessors.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.Pipeline value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDeprecated, value.Deprecated, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropProcessors, value.Processors, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.PipelineConverter))]
public sealed partial class Pipeline
{
#if NET7_0_OR_GREATER
	public Pipeline()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Pipeline()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Marks this ingest pipeline as deprecated.
	/// When a deprecated ingest pipeline is referenced as the default or final pipeline when creating or updating a non-deprecated index template, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public bool? Deprecated { get; set; }

	/// <summary>
	/// <para>
	/// Description of the ingest pipeline.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? Processors { get; set; }

	/// <summary>
	/// <para>
	/// Version number used by external systems to track ingest pipelines.
	/// </para>
	/// </summary>
	public long? Version { get; set; }
}

public readonly partial struct PipelineDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.Pipeline Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PipelineDescriptor(Elastic.Clients.Elasticsearch.Ingest.Pipeline instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PipelineDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.Pipeline instance) => new Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Marks this ingest pipeline as deprecated.
	/// When a deprecated ingest pipeline is referenced as the default or final pipeline when creating or updating a non-deprecated index template, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Deprecated(bool? value = true)
	{
		Instance.Deprecated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the ingest pipeline.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Processors(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.Processors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Processors(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.Processors = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Processors(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>.Build(action));
		}

		Instance.Processors = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Version number used by external systems to track ingest pipelines.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.Pipeline Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct PipelineDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.Pipeline Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PipelineDescriptor(Elastic.Clients.Elasticsearch.Ingest.Pipeline instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PipelineDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor(Elastic.Clients.Elasticsearch.Ingest.Pipeline instance) => new Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Marks this ingest pipeline as deprecated.
	/// When a deprecated ingest pipeline is referenced as the default or final pipeline when creating or updating a non-deprecated index template, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Deprecated(bool? value = true)
	{
		Instance.Deprecated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the ingest pipeline.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata about the ingest pipeline. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	/// Processors to run immediately after a processor failure.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Processors(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.Processors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Processors(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.Processors = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Processors(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor.Build(action));
		}

		Instance.Processors = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing.
	/// Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Processors<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>.Build(action));
		}

		Instance.Processors = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Version number used by external systems to track ingest pipelines.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.Pipeline Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Ingest.PipelineDescriptor(new Elastic.Clients.Elasticsearch.Ingest.Pipeline(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}