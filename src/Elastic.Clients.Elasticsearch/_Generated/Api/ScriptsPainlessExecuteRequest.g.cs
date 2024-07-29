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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScriptsPainlessExecuteRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>Run a script.<br/>Runs a script and returns a result.</para>
/// </summary>
public sealed partial class ScriptsPainlessExecuteRequest : PlainRequest<ScriptsPainlessExecuteRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceScriptsPainlessExecute;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "scripts_painless_execute";

	/// <summary>
	/// <para>The context that the script should run in.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("context")]
	public string? Context { get; set; }

	/// <summary>
	/// <para>Additional parameters for the `context`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("context_setup")]
	public Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetup? ContextSetup { get; set; }

	/// <summary>
	/// <para>The Painless script to execute.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
}

/// <summary>
/// <para>Run a script.<br/>Runs a script and returns a result.</para>
/// </summary>
public sealed partial class ScriptsPainlessExecuteRequestDescriptor<TDocument> : RequestDescriptor<ScriptsPainlessExecuteRequestDescriptor<TDocument>, ScriptsPainlessExecuteRequestParameters>
{
	internal ScriptsPainlessExecuteRequestDescriptor(Action<ScriptsPainlessExecuteRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ScriptsPainlessExecuteRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceScriptsPainlessExecute;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "scripts_painless_execute";

	private string? ContextValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetup? ContextSetupValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor<TDocument> ContextSetupDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor<TDocument>> ContextSetupDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }

	/// <summary>
	/// <para>The context that the script should run in.</para>
	/// </summary>
	public ScriptsPainlessExecuteRequestDescriptor<TDocument> Context(string? context)
	{
		ContextValue = context;
		return Self;
	}

	/// <summary>
	/// <para>Additional parameters for the `context`.</para>
	/// </summary>
	public ScriptsPainlessExecuteRequestDescriptor<TDocument> ContextSetup(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetup? contextSetup)
	{
		ContextSetupDescriptor = null;
		ContextSetupDescriptorAction = null;
		ContextSetupValue = contextSetup;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor<TDocument> ContextSetup(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor<TDocument> descriptor)
	{
		ContextSetupValue = null;
		ContextSetupDescriptorAction = null;
		ContextSetupDescriptor = descriptor;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor<TDocument> ContextSetup(Action<Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor<TDocument>> configure)
	{
		ContextSetupValue = null;
		ContextSetupDescriptor = null;
		ContextSetupDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>The Painless script to execute.</para>
	/// </summary>
	public ScriptsPainlessExecuteRequestDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ContextValue))
		{
			writer.WritePropertyName("context");
			writer.WriteStringValue(ContextValue);
		}

		if (ContextSetupDescriptor is not null)
		{
			writer.WritePropertyName("context_setup");
			JsonSerializer.Serialize(writer, ContextSetupDescriptor, options);
		}
		else if (ContextSetupDescriptorAction is not null)
		{
			writer.WritePropertyName("context_setup");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor<TDocument>(ContextSetupDescriptorAction), options);
		}
		else if (ContextSetupValue is not null)
		{
			writer.WritePropertyName("context_setup");
			JsonSerializer.Serialize(writer, ContextSetupValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>Run a script.<br/>Runs a script and returns a result.</para>
/// </summary>
public sealed partial class ScriptsPainlessExecuteRequestDescriptor : RequestDescriptor<ScriptsPainlessExecuteRequestDescriptor, ScriptsPainlessExecuteRequestParameters>
{
	internal ScriptsPainlessExecuteRequestDescriptor(Action<ScriptsPainlessExecuteRequestDescriptor> configure) => configure.Invoke(this);

	public ScriptsPainlessExecuteRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceScriptsPainlessExecute;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "scripts_painless_execute";

	private string? ContextValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetup? ContextSetupValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor ContextSetupDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor> ContextSetupDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }

	/// <summary>
	/// <para>The context that the script should run in.</para>
	/// </summary>
	public ScriptsPainlessExecuteRequestDescriptor Context(string? context)
	{
		ContextValue = context;
		return Self;
	}

	/// <summary>
	/// <para>Additional parameters for the `context`.</para>
	/// </summary>
	public ScriptsPainlessExecuteRequestDescriptor ContextSetup(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetup? contextSetup)
	{
		ContextSetupDescriptor = null;
		ContextSetupDescriptorAction = null;
		ContextSetupValue = contextSetup;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor ContextSetup(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor descriptor)
	{
		ContextSetupValue = null;
		ContextSetupDescriptorAction = null;
		ContextSetupDescriptor = descriptor;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor ContextSetup(Action<Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor> configure)
	{
		ContextSetupValue = null;
		ContextSetupDescriptor = null;
		ContextSetupDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>The Painless script to execute.</para>
	/// </summary>
	public ScriptsPainlessExecuteRequestDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptsPainlessExecuteRequestDescriptor Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ContextValue))
		{
			writer.WritePropertyName("context");
			writer.WriteStringValue(ContextValue);
		}

		if (ContextSetupDescriptor is not null)
		{
			writer.WritePropertyName("context_setup");
			JsonSerializer.Serialize(writer, ContextSetupDescriptor, options);
		}
		else if (ContextSetupDescriptorAction is not null)
		{
			writer.WritePropertyName("context_setup");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextSetupDescriptor(ContextSetupDescriptorAction), options);
		}
		else if (ContextSetupValue is not null)
		{
			writer.WritePropertyName("context_setup");
			JsonSerializer.Serialize(writer, ContextSetupValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}