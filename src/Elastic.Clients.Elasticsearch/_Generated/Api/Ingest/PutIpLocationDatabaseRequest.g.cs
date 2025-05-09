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

public sealed partial class PutIpLocationDatabaseRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response from all relevant nodes in the cluster after updating the cluster metadata.
	/// If no response is received before the timeout expires, the cluster metadata update still applies but the response indicates that it was not completely acknowledged.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class PutIpLocationDatabaseRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest>
{
	public override Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { Configuration = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration>(options, null) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.Configuration, null);
	}
}

/// <summary>
/// <para>
/// Create or update an IP geolocation database configuration.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestConverter))]
public sealed partial class PutIpLocationDatabaseRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Id id, Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration configuration) : base(r => r.Required("id", id))
	{
		Configuration = configuration;
	}
#if NET7_0_OR_GREATER
	public PutIpLocationDatabaseRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IngestPutIpLocationDatabase;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ingest.put_ip_location_database";

	/// <summary>
	/// <para>
	/// The database configuration identifier.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response from all relevant nodes in the cluster after updating the cluster metadata.
	/// If no response is received before the timeout expires, the cluster metadata update still applies but the response indicates that it was not completely acknowledged.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration Configuration { get; set; }
}

/// <summary>
/// <para>
/// Create or update an IP geolocation database configuration.
/// </para>
/// </summary>
public readonly partial struct PutIpLocationDatabaseRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest instance)
	{
		Instance = instance;
	}

	public PutIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest(id);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutIpLocationDatabaseRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest instance) => new Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The database configuration identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
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
	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a response from all relevant nodes in the cluster after updating the cluster metadata.
	/// If no response is received before the timeout expires, the cluster metadata update still applies but the response indicates that it was not completely acknowledged.
	/// A value of <c>-1</c> indicates that the request should never time out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor Configuration(Elastic.Clients.Elasticsearch.Ingest.DatabaseConfiguration value)
	{
		Instance.Configuration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor Configuration(System.Action<Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor> action)
	{
		Instance.Configuration = Elastic.Clients.Elasticsearch.Ingest.DatabaseConfigurationDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor(new Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.PutIpLocationDatabaseRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}