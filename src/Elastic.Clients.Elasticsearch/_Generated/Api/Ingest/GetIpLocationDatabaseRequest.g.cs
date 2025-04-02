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

public sealed partial class GetIpLocationDatabaseRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class GetIpLocationDatabaseRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest>
{
	public override Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get IP geolocation database configurations.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestConverter))]
public sealed partial class GetIpLocationDatabaseRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestParameters>
{
	public GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Ids? id) : base(r => r.Optional("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetIpLocationDatabaseRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetIpLocationDatabaseRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IngestGetIpLocationDatabase;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.get_ip_location_database";

	/// <summary>
	/// <para>
	/// Comma-separated list of database configuration IDs to retrieve.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To get all database configurations, omit this parameter or use <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ids? Id { get => P<Elastic.Clients.Elasticsearch.Ids?>("id"); set => PO("id", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get IP geolocation database configurations.
/// </para>
/// </summary>
public readonly partial struct GetIpLocationDatabaseRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest instance)
	{
		Instance = instance;
	}

	public GetIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ids id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest(id);
	}

	public GetIpLocationDatabaseRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest instance) => new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of database configuration IDs to retrieve.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To get all database configurations, omit this parameter or use <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor Id(Elastic.Clients.Elasticsearch.Ids? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor(new Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetIpLocationDatabaseRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}