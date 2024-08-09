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

public sealed partial class MappingLimitSettingsFieldNameLength
{
	/// <summary>
	/// <para>
	/// Setting for the maximum length of a field name. This setting isn’t really something that addresses mappings explosion but
	/// might still be useful if you want to limit the field length. It usually shouldn’t be necessary to set this setting. The
	/// default is okay unless a user starts to add a huge number of fields with really long names. Default is <c>Long.MAX_VALUE</c> (no limit).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("limit")]
	public long? Limit { get; set; }
}

public sealed partial class MappingLimitSettingsFieldNameLengthDescriptor : SerializableDescriptor<MappingLimitSettingsFieldNameLengthDescriptor>
{
	internal MappingLimitSettingsFieldNameLengthDescriptor(Action<MappingLimitSettingsFieldNameLengthDescriptor> configure) => configure.Invoke(this);

	public MappingLimitSettingsFieldNameLengthDescriptor() : base()
	{
	}

	private long? LimitValue { get; set; }

	/// <summary>
	/// <para>
	/// Setting for the maximum length of a field name. This setting isn’t really something that addresses mappings explosion but
	/// might still be useful if you want to limit the field length. It usually shouldn’t be necessary to set this setting. The
	/// default is okay unless a user starts to add a huge number of fields with really long names. Default is <c>Long.MAX_VALUE</c> (no limit).
	/// </para>
	/// </summary>
	public MappingLimitSettingsFieldNameLengthDescriptor Limit(long? limit)
	{
		LimitValue = limit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (LimitValue.HasValue)
		{
			writer.WritePropertyName("limit");
			writer.WriteNumberValue(LimitValue.Value);
		}

		writer.WriteEndObject();
	}
}