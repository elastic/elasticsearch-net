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

namespace Elastic.Clients.Elasticsearch.Serverless;

public sealed partial class StoredScriptId
{
	/// <summary>
	/// <para>The `id` for a stored script.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("id")]
	public Elastic.Clients.Elasticsearch.Serverless.Id Id { get; set; }

	/// <summary>
	/// <para>Specifies any named parameters that are passed into the script as variables.<br/>Use parameters instead of hard-coded values to decrease compile time.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }
}

public sealed partial class StoredScriptIdDescriptor : SerializableDescriptor<StoredScriptIdDescriptor>
{
	internal StoredScriptIdDescriptor(Action<StoredScriptIdDescriptor> configure) => configure.Invoke(this);

	public StoredScriptIdDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Id IdValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }

	/// <summary>
	/// <para>The `id` for a stored script.</para>
	/// </summary>
	public StoredScriptIdDescriptor Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		IdValue = id;
		return Self;
	}

	/// <summary>
	/// <para>Specifies any named parameters that are passed into the script as variables.<br/>Use parameters instead of hard-coded values to decrease compile time.</para>
	/// </summary>
	public StoredScriptIdDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("id");
		JsonSerializer.Serialize(writer, IdValue, options);
		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		writer.WriteEndObject();
	}
}