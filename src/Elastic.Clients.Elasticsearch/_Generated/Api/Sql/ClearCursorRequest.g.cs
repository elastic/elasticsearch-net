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

namespace Elastic.Clients.Elasticsearch.Sql;

public sealed partial class ClearCursorRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Clear an SQL search cursor.
/// </para>
/// </summary>
public sealed partial class ClearCursorRequest : PlainRequest<ClearCursorRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SqlClearCursor;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "sql.clear_cursor";

	/// <summary>
	/// <para>
	/// Cursor to clear.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cursor")]
	public string Cursor { get; set; }
}

/// <summary>
/// <para>
/// Clear an SQL search cursor.
/// </para>
/// </summary>
public sealed partial class ClearCursorRequestDescriptor : RequestDescriptor<ClearCursorRequestDescriptor, ClearCursorRequestParameters>
{
	internal ClearCursorRequestDescriptor(Action<ClearCursorRequestDescriptor> configure) => configure.Invoke(this);

	public ClearCursorRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SqlClearCursor;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "sql.clear_cursor";

	private string CursorValue { get; set; }

	/// <summary>
	/// <para>
	/// Cursor to clear.
	/// </para>
	/// </summary>
	public ClearCursorRequestDescriptor Cursor(string cursor)
	{
		CursorValue = cursor;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("cursor");
		writer.WriteStringValue(CursorValue);
		writer.WriteEndObject();
	}
}