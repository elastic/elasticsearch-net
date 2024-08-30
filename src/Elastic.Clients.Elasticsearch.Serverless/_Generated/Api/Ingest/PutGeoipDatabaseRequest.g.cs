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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Ingest;

public sealed partial class PutGeoipDatabaseRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Returns information about one or more geoip database configurations.
/// </para>
/// </summary>
public sealed partial class PutGeoipDatabaseRequest : PlainRequest<PutGeoipDatabaseRequestParameters>
{
	public PutGeoipDatabaseRequest(Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestPutGeoipDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ingest.put_geoip_database";

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The configuration necessary to identify which IP geolocation provider to use to download the database, as well as any provider-specific configuration necessary for such downloading.
	/// At present, the only supported provider is maxmind, and the maxmind provider requires that an account_id (string) is configured.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("maxmind")]
	public Elastic.Clients.Elasticsearch.Serverless.Ingest.Maxmind Maxmind { get; set; }

	/// <summary>
	/// <para>
	/// The provider-assigned name of the IP geolocation database to download.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("name")]
	public Elastic.Clients.Elasticsearch.Serverless.Name Name { get; set; }
}

/// <summary>
/// <para>
/// Returns information about one or more geoip database configurations.
/// </para>
/// </summary>
public sealed partial class PutGeoipDatabaseRequestDescriptor<TDocument> : RequestDescriptor<PutGeoipDatabaseRequestDescriptor<TDocument>, PutGeoipDatabaseRequestParameters>
{
	internal PutGeoipDatabaseRequestDescriptor(Action<PutGeoipDatabaseRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PutGeoipDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestPutGeoipDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ingest.put_geoip_database";

	public PutGeoipDatabaseRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutGeoipDatabaseRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public PutGeoipDatabaseRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Serverless.Ingest.Maxmind MaxmindValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor MaxmindDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor> MaxmindDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Name NameValue { get; set; }

	/// <summary>
	/// <para>
	/// The configuration necessary to identify which IP geolocation provider to use to download the database, as well as any provider-specific configuration necessary for such downloading.
	/// At present, the only supported provider is maxmind, and the maxmind provider requires that an account_id (string) is configured.
	/// </para>
	/// </summary>
	public PutGeoipDatabaseRequestDescriptor<TDocument> Maxmind(Elastic.Clients.Elasticsearch.Serverless.Ingest.Maxmind maxmind)
	{
		MaxmindDescriptor = null;
		MaxmindDescriptorAction = null;
		MaxmindValue = maxmind;
		return Self;
	}

	public PutGeoipDatabaseRequestDescriptor<TDocument> Maxmind(Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor descriptor)
	{
		MaxmindValue = null;
		MaxmindDescriptorAction = null;
		MaxmindDescriptor = descriptor;
		return Self;
	}

	public PutGeoipDatabaseRequestDescriptor<TDocument> Maxmind(Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor> configure)
	{
		MaxmindValue = null;
		MaxmindDescriptor = null;
		MaxmindDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The provider-assigned name of the IP geolocation database to download.
	/// </para>
	/// </summary>
	public PutGeoipDatabaseRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Serverless.Name name)
	{
		NameValue = name;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxmindDescriptor is not null)
		{
			writer.WritePropertyName("maxmind");
			JsonSerializer.Serialize(writer, MaxmindDescriptor, options);
		}
		else if (MaxmindDescriptorAction is not null)
		{
			writer.WritePropertyName("maxmind");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor(MaxmindDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("maxmind");
			JsonSerializer.Serialize(writer, MaxmindValue, options);
		}

		writer.WritePropertyName("name");
		JsonSerializer.Serialize(writer, NameValue, options);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Returns information about one or more geoip database configurations.
/// </para>
/// </summary>
public sealed partial class PutGeoipDatabaseRequestDescriptor : RequestDescriptor<PutGeoipDatabaseRequestDescriptor, PutGeoipDatabaseRequestParameters>
{
	internal PutGeoipDatabaseRequestDescriptor(Action<PutGeoipDatabaseRequestDescriptor> configure) => configure.Invoke(this);

	public PutGeoipDatabaseRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestPutGeoipDatabase;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ingest.put_geoip_database";

	public PutGeoipDatabaseRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutGeoipDatabaseRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public PutGeoipDatabaseRequestDescriptor Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Serverless.Ingest.Maxmind MaxmindValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor MaxmindDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor> MaxmindDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Name NameValue { get; set; }

	/// <summary>
	/// <para>
	/// The configuration necessary to identify which IP geolocation provider to use to download the database, as well as any provider-specific configuration necessary for such downloading.
	/// At present, the only supported provider is maxmind, and the maxmind provider requires that an account_id (string) is configured.
	/// </para>
	/// </summary>
	public PutGeoipDatabaseRequestDescriptor Maxmind(Elastic.Clients.Elasticsearch.Serverless.Ingest.Maxmind maxmind)
	{
		MaxmindDescriptor = null;
		MaxmindDescriptorAction = null;
		MaxmindValue = maxmind;
		return Self;
	}

	public PutGeoipDatabaseRequestDescriptor Maxmind(Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor descriptor)
	{
		MaxmindValue = null;
		MaxmindDescriptorAction = null;
		MaxmindDescriptor = descriptor;
		return Self;
	}

	public PutGeoipDatabaseRequestDescriptor Maxmind(Action<Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor> configure)
	{
		MaxmindValue = null;
		MaxmindDescriptor = null;
		MaxmindDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The provider-assigned name of the IP geolocation database to download.
	/// </para>
	/// </summary>
	public PutGeoipDatabaseRequestDescriptor Name(Elastic.Clients.Elasticsearch.Serverless.Name name)
	{
		NameValue = name;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxmindDescriptor is not null)
		{
			writer.WritePropertyName("maxmind");
			JsonSerializer.Serialize(writer, MaxmindDescriptor, options);
		}
		else if (MaxmindDescriptorAction is not null)
		{
			writer.WritePropertyName("maxmind");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Ingest.MaxmindDescriptor(MaxmindDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("maxmind");
			JsonSerializer.Serialize(writer, MaxmindValue, options);
		}

		writer.WritePropertyName("name");
		JsonSerializer.Serialize(writer, NameValue, options);
		writer.WriteEndObject();
	}
}