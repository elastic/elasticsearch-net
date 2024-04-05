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

namespace Elastic.Clients.Elasticsearch.Serverless.TransformManagement;

public sealed partial class TimeSync
{
	/// <summary>
	/// <para>The time delay between the current time and the latest input data time.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("delay")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Delay { get; set; }

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.TransformManagement.Sync(TimeSync timeSync) => Elastic.Clients.Elasticsearch.Serverless.TransformManagement.Sync.Time(timeSync);
}

public sealed partial class TimeSyncDescriptor<TDocument> : SerializableDescriptor<TimeSyncDescriptor<TDocument>>
{
	internal TimeSyncDescriptor(Action<TimeSyncDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TimeSyncDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Duration? DelayValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }

	/// <summary>
	/// <para>The time delay between the current time and the latest input data time.</para>
	/// </summary>
	public TimeSyncDescriptor<TDocument> Delay(Elastic.Clients.Elasticsearch.Serverless.Duration? delay)
	{
		DelayValue = delay;
		return Self;
	}

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	public TimeSyncDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	public TimeSyncDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	public TimeSyncDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DelayValue is not null)
		{
			writer.WritePropertyName("delay");
			JsonSerializer.Serialize(writer, DelayValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WriteEndObject();
	}
}

public sealed partial class TimeSyncDescriptor : SerializableDescriptor<TimeSyncDescriptor>
{
	internal TimeSyncDescriptor(Action<TimeSyncDescriptor> configure) => configure.Invoke(this);

	public TimeSyncDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Duration? DelayValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }

	/// <summary>
	/// <para>The time delay between the current time and the latest input data time.</para>
	/// </summary>
	public TimeSyncDescriptor Delay(Elastic.Clients.Elasticsearch.Serverless.Duration? delay)
	{
		DelayValue = delay;
		return Self;
	}

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	public TimeSyncDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	public TimeSyncDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The date field that is used to identify new documents in the source. In general, it’s a good idea to use a field<br/>that contains the ingest timestamp. If you use a different field, you might need to set the delay such that it<br/>accounts for data transmission delays.</para>
	/// </summary>
	public TimeSyncDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DelayValue is not null)
		{
			writer.WritePropertyName("delay");
			JsonSerializer.Serialize(writer, DelayValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WriteEndObject();
	}
}