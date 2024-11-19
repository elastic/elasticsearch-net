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

public sealed partial class GetScriptLanguagesRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Get script languages.
/// </para>
/// <para>
/// Get a list of available script types, languages, and contexts.
/// </para>
/// </summary>
public sealed partial class GetScriptLanguagesRequest : PlainRequest<GetScriptLanguagesRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceGetScriptLanguages;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "get_script_languages";
}

/// <summary>
/// <para>
/// Get script languages.
/// </para>
/// <para>
/// Get a list of available script types, languages, and contexts.
/// </para>
/// </summary>
public sealed partial class GetScriptLanguagesRequestDescriptor : RequestDescriptor<GetScriptLanguagesRequestDescriptor, GetScriptLanguagesRequestParameters>
{
	internal GetScriptLanguagesRequestDescriptor(Action<GetScriptLanguagesRequestDescriptor> configure) => configure.Invoke(this);

	public GetScriptLanguagesRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceGetScriptLanguages;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "get_script_languages";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}