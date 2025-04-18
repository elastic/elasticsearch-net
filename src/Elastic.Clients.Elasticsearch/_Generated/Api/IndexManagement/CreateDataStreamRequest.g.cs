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

public sealed partial class CreateDataStreamRequestParameters : Elastic.Transport.RequestParameters
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

internal sealed partial class CreateDataStreamRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a data stream.
/// </para>
/// <para>
/// You must have a matching index template with data stream enabled.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestConverter))]
public sealed partial class CreateDataStreamRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateDataStreamRequest(Elastic.Clients.Elasticsearch.DataStreamName name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public CreateDataStreamRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CreateDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementCreateDataStream;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.create_data_stream";

	/// <summary>
	/// <para>
	/// Name of the data stream, which must meet the following criteria:
	/// Lowercase only;
	/// Cannot include <c>\</c>, <c>/</c>, <c>*</c>, <c>?</c>, <c>"</c>, <c>&lt;</c>, <c>></c>, <c>|</c>, <c>,</c>, <c>#</c>, <c>:</c>, or a space character;
	/// Cannot start with <c>-</c>, <c>_</c>, <c>+</c>, or <c>.ds-</c>;
	/// Cannot be <c>.</c> or <c>..</c>;
	/// Cannot be longer than 255 bytes. Multi-byte characters count towards this limit faster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.DataStreamName Name { get => P<Elastic.Clients.Elasticsearch.DataStreamName>("name"); set => PR("name", value); }

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
/// Create a data stream.
/// </para>
/// <para>
/// You must have a matching index template with data stream enabled.
/// </para>
/// </summary>
public readonly partial struct CreateDataStreamRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest instance)
	{
		Instance = instance;
	}

	public CreateDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.DataStreamName name)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest(name);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public CreateDataStreamRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest(Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the data stream, which must meet the following criteria:
	/// Lowercase only;
	/// Cannot include <c>\</c>, <c>/</c>, <c>*</c>, <c>?</c>, <c>"</c>, <c>&lt;</c>, <c>></c>, <c>|</c>, <c>,</c>, <c>#</c>, <c>:</c>, or a space character;
	/// Cannot start with <c>-</c>, <c>_</c>, <c>+</c>, or <c>.ds-</c>;
	/// Cannot be <c>.</c> or <c>..</c>;
	/// Cannot be longer than 255 bytes. Multi-byte characters count towards this limit faster.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor Name(Elastic.Clients.Elasticsearch.DataStreamName value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.CreateDataStreamRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}