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

public sealed partial class PutPipelineRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Required version for optimistic concurrency control for pipeline updates
	/// </para>
	/// </summary>
	public long? IfVersion { get => Q<long?>("if_version"); set => Q("if_version", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class PutPipelineRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeprecated = System.Text.Json.JsonEncodedText.Encode("deprecated");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropProcessors = System.Text.Json.JsonEncodedText.Encode("processors");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Deprecated = propDeprecated.Value,
			Description = propDescription.Value,
			Meta = propMeta.Value,
			OnFailure = propOnFailure.Value,
			Processors = propProcessors.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest value, System.Text.Json.JsonSerializerOptions options)
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

/// <summary>
/// <para>
/// Create or update a pipeline.
/// Changes made using this API take effect immediately.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestConverter))]
public sealed partial class PutPipelineRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutPipelineRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public PutPipelineRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IngestPutPipeline;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ingest.put_pipeline";

	/// <summary>
	/// <para>
	/// ID of the ingest pipeline to create or update.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// Required version for optimistic concurrency control for pipeline updates
	/// </para>
	/// </summary>
	public long? IfVersion { get => Q<long?>("if_version"); set => Q("if_version", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

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
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? Processors { get; set; }

	/// <summary>
	/// <para>
	/// Version number used by external systems to track ingest pipelines. This parameter is intended for external systems only. Elasticsearch does not use or validate pipeline version numbers.
	/// </para>
	/// </summary>
	public long? Version { get; set; }
}

/// <summary>
/// <para>
/// Create or update a pipeline.
/// Changes made using this API take effect immediately.
/// </para>
/// </summary>
public readonly partial struct PutPipelineRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest instance)
	{
		Instance = instance;
	}

	public PutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(id);
	}

	[System.Obsolete("TODO")]
	public PutPipelineRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest instance) => new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// ID of the ingest pipeline to create or update.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Required version for optimistic concurrency control for pipeline updates
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor IfVersion(long? value)
	{
		Instance.IfVersion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Marks this ingest pipeline as deprecated.
	/// When a deprecated ingest pipeline is referenced as the default or final pipeline when creating or updating a non-deprecated index template, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Deprecated(bool? value = true)
	{
		Instance.Deprecated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the ingest pipeline.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Processors(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.Processors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Processors(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.Processors = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Processors(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
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
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Processors<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
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
	/// Version number used by external systems to track ingest pipelines. This parameter is intended for external systems only. Elasticsearch does not use or validate pipeline version numbers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor(new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Create or update a pipeline.
/// Changes made using this API take effect immediately.
/// </para>
/// </summary>
public readonly partial struct PutPipelineRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest instance)
	{
		Instance = instance;
	}

	public PutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(id);
	}

	[System.Obsolete("TODO")]
	public PutPipelineRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest instance) => new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// ID of the ingest pipeline to create or update.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Required version for optimistic concurrency control for pipeline updates
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> IfVersion(long? value)
	{
		Instance.IfVersion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Marks this ingest pipeline as deprecated.
	/// When a deprecated ingest pipeline is referenced as the default or final pipeline when creating or updating a non-deprecated index template, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Deprecated(bool? value = true)
	{
		Instance.Deprecated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Description of the ingest pipeline.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional metadata about the ingest pipeline. May have any contents. This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors to run immediately after a processor failure. Each processor supports a processor-level <c>on_failure</c> value. If a processor without an <c>on_failure</c> value fails, Elasticsearch uses this pipeline-level parameter as a fallback. The processors in this parameter run sequentially in the order specified. Elasticsearch will not attempt to run the pipeline's remaining processors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Processors(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.Processors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Processors(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.Processors = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Processors used to perform transformations on documents before indexing. Processors run sequentially in the order specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Processors(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
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
	/// Version number used by external systems to track ingest pipelines. This parameter is intended for external systems only. Elasticsearch does not use or validate pipeline version numbers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutPipelineRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}