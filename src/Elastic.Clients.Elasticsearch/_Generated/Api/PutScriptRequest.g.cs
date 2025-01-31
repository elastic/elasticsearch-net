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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class PutScriptRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Create or update a script or search template.
/// Creates or updates a stored script or search template.
/// </para>
/// </summary>
public sealed partial class PutScriptRequest : PlainRequest<PutScriptRequestParameters>
{
	public PutScriptRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	public PutScriptRequest(Elastic.Clients.Elasticsearch.Id id, Elastic.Clients.Elasticsearch.Name? context) : base(r => r.Required("id", id).Optional("context", context))
	{
	}

	[JsonConstructor]
	internal PutScriptRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespacePutScript;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "put_script";

	/// <summary>
	/// <para>
	/// Context in which the script or search template should run.
	/// To prevent errors, the API immediately compiles the script or template in this context.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Name? Context { get => P<Elastic.Clients.Elasticsearch.Name?>("context"); set => PO("context", value); }

	/// <summary>
	/// <para>
	/// Identifier for the stored script or search template.
	/// Must be unique within the cluster.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Contains the script or search template, its parameters, and its language.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.StoredScript Script { get; set; }
}

/// <summary>
/// <para>
/// Create or update a script or search template.
/// Creates or updates a stored script or search template.
/// </para>
/// </summary>
public sealed partial class PutScriptRequestDescriptor<TDocument> : RequestDescriptor<PutScriptRequestDescriptor<TDocument>, PutScriptRequestParameters>
{
	internal PutScriptRequestDescriptor(Action<PutScriptRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PutScriptRequestDescriptor(Elastic.Clients.Elasticsearch.Id id, Elastic.Clients.Elasticsearch.Name? context) : base(r => r.Required("id", id).Optional("context", context))
	{
	}

	public PutScriptRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespacePutScript;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "put_script";

	public PutScriptRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutScriptRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public PutScriptRequestDescriptor<TDocument> Context(Elastic.Clients.Elasticsearch.Name? context)
	{
		RouteValues.Optional("context", context);
		return Self;
	}

	public PutScriptRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.StoredScript ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.StoredScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.StoredScriptDescriptor> ScriptDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Contains the script or search template, its parameters, and its language.
	/// </para>
	/// </summary>
	public PutScriptRequestDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.StoredScript script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public PutScriptRequestDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.StoredScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public PutScriptRequestDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.StoredScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.StoredScriptDescriptor(ScriptDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create or update a script or search template.
/// Creates or updates a stored script or search template.
/// </para>
/// </summary>
public sealed partial class PutScriptRequestDescriptor : RequestDescriptor<PutScriptRequestDescriptor, PutScriptRequestParameters>
{
	internal PutScriptRequestDescriptor(Action<PutScriptRequestDescriptor> configure) => configure.Invoke(this);

	public PutScriptRequestDescriptor(Elastic.Clients.Elasticsearch.Id id, Elastic.Clients.Elasticsearch.Name? context) : base(r => r.Required("id", id).Optional("context", context))
	{
	}

	public PutScriptRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespacePutScript;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "put_script";

	public PutScriptRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutScriptRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public PutScriptRequestDescriptor Context(Elastic.Clients.Elasticsearch.Name? context)
	{
		RouteValues.Optional("context", context);
		return Self;
	}

	public PutScriptRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.StoredScript ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.StoredScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.StoredScriptDescriptor> ScriptDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Contains the script or search template, its parameters, and its language.
	/// </para>
	/// </summary>
	public PutScriptRequestDescriptor Script(Elastic.Clients.Elasticsearch.StoredScript script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public PutScriptRequestDescriptor Script(Elastic.Clients.Elasticsearch.StoredScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public PutScriptRequestDescriptor Script(Action<Elastic.Clients.Elasticsearch.StoredScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.StoredScriptDescriptor(ScriptDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}