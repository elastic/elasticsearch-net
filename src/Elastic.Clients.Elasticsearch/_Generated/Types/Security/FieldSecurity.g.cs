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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class FieldSecurity
{
	[JsonInclude, JsonPropertyName("except")]
	[JsonConverter(typeof(SingleOrManyFieldsConverter))]
	public Elastic.Clients.Elasticsearch.Fields? Except { get; set; }
	[JsonInclude, JsonPropertyName("grant")]
	[JsonConverter(typeof(SingleOrManyFieldsConverter))]
	public Elastic.Clients.Elasticsearch.Fields? Grant { get; set; }
}

public sealed partial class FieldSecurityDescriptor<TDocument> : SerializableDescriptor<FieldSecurityDescriptor<TDocument>>
{
	internal FieldSecurityDescriptor(Action<FieldSecurityDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FieldSecurityDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields? ExceptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? GrantValue { get; set; }

	public FieldSecurityDescriptor<TDocument> Except(Elastic.Clients.Elasticsearch.Fields? except)
	{
		ExceptValue = except;
		return Self;
	}

	public FieldSecurityDescriptor<TDocument> Grant(Elastic.Clients.Elasticsearch.Fields? grant)
	{
		GrantValue = grant;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExceptValue is not null)
		{
			writer.WritePropertyName("except");
			JsonSerializer.Serialize(writer, ExceptValue, options);
		}

		if (GrantValue is not null)
		{
			writer.WritePropertyName("grant");
			JsonSerializer.Serialize(writer, GrantValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class FieldSecurityDescriptor : SerializableDescriptor<FieldSecurityDescriptor>
{
	internal FieldSecurityDescriptor(Action<FieldSecurityDescriptor> configure) => configure.Invoke(this);

	public FieldSecurityDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields? ExceptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? GrantValue { get; set; }

	public FieldSecurityDescriptor Except(Elastic.Clients.Elasticsearch.Fields? except)
	{
		ExceptValue = except;
		return Self;
	}

	public FieldSecurityDescriptor Grant(Elastic.Clients.Elasticsearch.Fields? grant)
	{
		GrantValue = grant;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExceptValue is not null)
		{
			writer.WritePropertyName("except");
			JsonSerializer.Serialize(writer, ExceptValue, options);
		}

		if (GrantValue is not null)
		{
			writer.WritePropertyName("grant");
			JsonSerializer.Serialize(writer, GrantValue, options);
		}

		writer.WriteEndObject();
	}
}