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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.MSearchTemplate;

public sealed partial class TemplateConfig
{
	/// <summary>
	/// <para>
	/// If <c>true</c>, returns detailed information about score calculation as part of each hit.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("explain")]
	public bool? Explain { get; init; }

	/// <summary>
	/// <para>
	/// ID of the search template to use. If no source is specified,
	/// this parameter is required.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("id")]
	public Elastic.Clients.Elasticsearch.Id? Id { get; init; }

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("params")]
	public IReadOnlyDictionary<string, object>? Params { get; init; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the query execution is profiled.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("profile")]
	public bool? Profile { get; init; }

	/// <summary>
	/// <para>
	/// An inline search template. Supports the same parameters as the search API's
	/// request body. Also supports Mustache variables. If no id is specified, this
	/// parameter is required.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("source")]
	public string? Source { get; init; }
}