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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class MigrateToDataStreamRequestParameters : Elastic.Transport.RequestParameters
{
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

internal sealed partial class MigrateToDataStreamRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Convert an index alias to a data stream.
/// Converts an index alias to a data stream.
/// You must have a matching index template that is data stream enabled.
/// The alias must meet the following criteria:
/// The alias must have a write index;
/// All indices for the alias must have a <c>@timestamp</c> field mapping of a <c>date</c> or <c>date_nanos</c> field type;
/// The alias must not have any filters;
/// The alias must not use custom routing.
/// If successful, the request removes the alias and creates a data stream with the same name.
/// The indices for the alias become hidden backing indices for the stream.
/// The write index for the alias becomes the write index for the stream.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestConverter))]
public sealed partial class MigrateToDataStreamRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.IndexName name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public MigrateToDataStreamRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementMigrateToDataStream;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.migrate_to_data_stream";

	/// <summary>
	/// <para>
	/// Name of the index alias to convert to a data stream.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Name { get => P<Elastic.Clients.Elasticsearch.IndexName>("name"); set => PR("name", value); }

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

/// <summary>
/// <para>
/// Convert an index alias to a data stream.
/// Converts an index alias to a data stream.
/// You must have a matching index template that is data stream enabled.
/// The alias must meet the following criteria:
/// The alias must have a write index;
/// All indices for the alias must have a <c>@timestamp</c> field mapping of a <c>date</c> or <c>date_nanos</c> field type;
/// The alias must not have any filters;
/// The alias must not use custom routing.
/// If successful, the request removes the alias and creates a data stream with the same name.
/// The indices for the alias become hidden backing indices for the stream.
/// The write index for the alias becomes the write index for the stream.
/// </para>
/// </summary>
public readonly partial struct MigrateToDataStreamRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MigrateToDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest instance)
	{
		Instance = instance;
	}

	public MigrateToDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName name)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(name);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public MigrateToDataStreamRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the index alias to convert to a data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor Name(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Convert an index alias to a data stream.
/// Converts an index alias to a data stream.
/// You must have a matching index template that is data stream enabled.
/// The alias must meet the following criteria:
/// The alias must have a write index;
/// All indices for the alias must have a <c>@timestamp</c> field mapping of a <c>date</c> or <c>date_nanos</c> field type;
/// The alias must not have any filters;
/// The alias must not use custom routing.
/// If successful, the request removes the alias and creates a data stream with the same name.
/// The indices for the alias become hidden backing indices for the stream.
/// The write index for the alias becomes the write index for the stream.
/// </para>
/// </summary>
public readonly partial struct MigrateToDataStreamRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MigrateToDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest instance)
	{
		Instance = instance;
	}

	public MigrateToDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName name)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(name);
	}

	public MigrateToDataStreamRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(typeof(TDocument));
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the index alias to convert to a data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.MigrateToDataStreamRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}