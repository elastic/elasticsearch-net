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
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class SettingsSimilarityScripted : ISettingsSimilarity
{
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script Script { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "scripted";

	[JsonInclude, JsonPropertyName("weight_script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? WeightScript { get; set; }
}

public sealed partial class SettingsSimilarityScriptedDescriptor : SerializableDescriptor<SettingsSimilarityScriptedDescriptor>, IBuildableDescriptor<SettingsSimilarityScripted>
{
	internal SettingsSimilarityScriptedDescriptor(Action<SettingsSimilarityScriptedDescriptor> configure) => configure.Invoke(this);

	public SettingsSimilarityScriptedDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Script ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? WeightScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor WeightScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> WeightScriptDescriptorAction { get; set; }

	public SettingsSimilarityScriptedDescriptor Script(Elastic.Clients.Elasticsearch.Serverless.Script script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public SettingsSimilarityScriptedDescriptor Script(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public SettingsSimilarityScriptedDescriptor Script(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	public SettingsSimilarityScriptedDescriptor WeightScript(Elastic.Clients.Elasticsearch.Serverless.Script? weightScript)
	{
		WeightScriptDescriptor = null;
		WeightScriptDescriptorAction = null;
		WeightScriptValue = weightScript;
		return Self;
	}

	public SettingsSimilarityScriptedDescriptor WeightScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		WeightScriptValue = null;
		WeightScriptDescriptorAction = null;
		WeightScriptDescriptor = descriptor;
		return Self;
	}

	public SettingsSimilarityScriptedDescriptor WeightScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		WeightScriptValue = null;
		WeightScriptDescriptor = null;
		WeightScriptDescriptorAction = configure;
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("scripted");
		if (WeightScriptDescriptor is not null)
		{
			writer.WritePropertyName("weight_script");
			JsonSerializer.Serialize(writer, WeightScriptDescriptor, options);
		}
		else if (WeightScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("weight_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(WeightScriptDescriptorAction), options);
		}
		else if (WeightScriptValue is not null)
		{
			writer.WritePropertyName("weight_script");
			JsonSerializer.Serialize(writer, WeightScriptValue, options);
		}

		writer.WriteEndObject();
	}

	private Elastic.Clients.Elasticsearch.Serverless.Script BuildScript()
	{
		if (ScriptValue is not null)
		{
			return ScriptValue;
		}

		if ((object)ScriptDescriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script> buildable)
		{
			return buildable.Build();
		}

		if (ScriptDescriptorAction is not null)
		{
			var descriptor = new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction);
			if ((object)descriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script> buildableFromAction)
			{
				return buildableFromAction.Build();
			}
		}

		return null;
	}

	private Elastic.Clients.Elasticsearch.Serverless.Script? BuildWeightScript()
	{
		if (WeightScriptValue is not null)
		{
			return WeightScriptValue;
		}

		if ((object)WeightScriptDescriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script?> buildable)
		{
			return buildable.Build();
		}

		if (WeightScriptDescriptorAction is not null)
		{
			var descriptor = new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(WeightScriptDescriptorAction);
			if ((object)descriptor is IBuildableDescriptor<Elastic.Clients.Elasticsearch.Serverless.Script?> buildableFromAction)
			{
				return buildableFromAction.Build();
			}
		}

		return null;
	}

	SettingsSimilarityScripted IBuildableDescriptor<SettingsSimilarityScripted>.Build() => new()
	{
		Script = BuildScript(),
		WeightScript = BuildWeightScript()
	};
}