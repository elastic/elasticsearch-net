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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class MappingLimitSettingsNestedFields
{
	/// <summary>
	/// <para>
	/// The maximum number of distinct nested mappings in an index. The nested type should only be used in special cases, when
	/// arrays of objects need to be queried independently of each other. To safeguard against poorly designed mappings, this
	/// setting limits the number of unique nested types per index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("limit")]
	public long? Limit { get; set; }
}

public sealed partial class MappingLimitSettingsNestedFieldsDescriptor : SerializableDescriptor<MappingLimitSettingsNestedFieldsDescriptor>
{
	internal MappingLimitSettingsNestedFieldsDescriptor(Action<MappingLimitSettingsNestedFieldsDescriptor> configure) => configure.Invoke(this);

	public MappingLimitSettingsNestedFieldsDescriptor() : base()
	{
	}

	private long? LimitValue { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of distinct nested mappings in an index. The nested type should only be used in special cases, when
	/// arrays of objects need to be queried independently of each other. To safeguard against poorly designed mappings, this
	/// setting limits the number of unique nested types per index.
	/// </para>
	/// </summary>
	public MappingLimitSettingsNestedFieldsDescriptor Limit(long? limit)
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