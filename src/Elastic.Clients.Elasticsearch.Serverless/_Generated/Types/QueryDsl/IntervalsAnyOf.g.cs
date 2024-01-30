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

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

public sealed partial class IntervalsAnyOf
{
	/// <summary>
	/// <para>Rule used to filter returned intervals.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("filter")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsFilter? Filter { get; set; }

	/// <summary>
	/// <para>An array of rules to match.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("intervals")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals> Intervals { get; set; }
}

public sealed partial class IntervalsAnyOfDescriptor<TDocument> : SerializableDescriptor<IntervalsAnyOfDescriptor<TDocument>>
{
	internal IntervalsAnyOfDescriptor(Action<IntervalsAnyOfDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IntervalsAnyOfDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsFilter? FilterValue { get; set; }
	private IntervalsFilterDescriptor FilterDescriptor { get; set; }
	private Action<IntervalsFilterDescriptor> FilterDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals> IntervalsValue { get; set; }
	private IntervalsDescriptor<TDocument> IntervalsDescriptor { get; set; }
	private Action<IntervalsDescriptor<TDocument>> IntervalsDescriptorAction { get; set; }
	private Action<IntervalsDescriptor<TDocument>>[] IntervalsDescriptorActions { get; set; }

	/// <summary>
	/// <para>Rule used to filter returned intervals.</para>
	/// </summary>
	public IntervalsAnyOfDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsFilter? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public IntervalsAnyOfDescriptor<TDocument> Filter(IntervalsFilterDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public IntervalsAnyOfDescriptor<TDocument> Filter(Action<IntervalsFilterDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>An array of rules to match.</para>
	/// </summary>
	public IntervalsAnyOfDescriptor<TDocument> Intervals(ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals> intervals)
	{
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsValue = intervals;
		return Self;
	}

	public IntervalsAnyOfDescriptor<TDocument> Intervals(IntervalsDescriptor<TDocument> descriptor)
	{
		IntervalsValue = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptor = descriptor;
		return Self;
	}

	public IntervalsAnyOfDescriptor<TDocument> Intervals(Action<IntervalsDescriptor<TDocument>> configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptorAction = configure;
		return Self;
	}

	public IntervalsAnyOfDescriptor<TDocument> Intervals(params Action<IntervalsDescriptor<TDocument>>[] configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new IntervalsFilterDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (IntervalsDescriptor is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, IntervalsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (IntervalsDescriptorAction is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new IntervalsDescriptor<TDocument>(IntervalsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IntervalsDescriptorActions is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			foreach (var action in IntervalsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new IntervalsDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("intervals");
			JsonSerializer.Serialize(writer, IntervalsValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class IntervalsAnyOfDescriptor : SerializableDescriptor<IntervalsAnyOfDescriptor>
{
	internal IntervalsAnyOfDescriptor(Action<IntervalsAnyOfDescriptor> configure) => configure.Invoke(this);

	public IntervalsAnyOfDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsFilter? FilterValue { get; set; }
	private IntervalsFilterDescriptor FilterDescriptor { get; set; }
	private Action<IntervalsFilterDescriptor> FilterDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals> IntervalsValue { get; set; }
	private IntervalsDescriptor IntervalsDescriptor { get; set; }
	private Action<IntervalsDescriptor> IntervalsDescriptorAction { get; set; }
	private Action<IntervalsDescriptor>[] IntervalsDescriptorActions { get; set; }

	/// <summary>
	/// <para>Rule used to filter returned intervals.</para>
	/// </summary>
	public IntervalsAnyOfDescriptor Filter(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsFilter? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public IntervalsAnyOfDescriptor Filter(IntervalsFilterDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public IntervalsAnyOfDescriptor Filter(Action<IntervalsFilterDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>An array of rules to match.</para>
	/// </summary>
	public IntervalsAnyOfDescriptor Intervals(ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals> intervals)
	{
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsValue = intervals;
		return Self;
	}

	public IntervalsAnyOfDescriptor Intervals(IntervalsDescriptor descriptor)
	{
		IntervalsValue = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptor = descriptor;
		return Self;
	}

	public IntervalsAnyOfDescriptor Intervals(Action<IntervalsDescriptor> configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptorAction = configure;
		return Self;
	}

	public IntervalsAnyOfDescriptor Intervals(params Action<IntervalsDescriptor>[] configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new IntervalsFilterDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (IntervalsDescriptor is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, IntervalsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (IntervalsDescriptorAction is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new IntervalsDescriptor(IntervalsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IntervalsDescriptorActions is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			foreach (var action in IntervalsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new IntervalsDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("intervals");
			JsonSerializer.Serialize(writer, IntervalsValue, options);
		}

		writer.WriteEndObject();
	}
}