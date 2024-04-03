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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class IntervalsAllOf
{
	/// <summary>
	/// <para>Rule used to filter returned intervals.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("filter")]
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? Filter { get; set; }

	/// <summary>
	/// <para>An array of rules to combine. All rules must produce a match in a document for the overall source to match.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("intervals")]
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Intervals> Intervals { get; set; }

	/// <summary>
	/// <para>Maximum number of positions between the matching terms.<br/>Intervals produced by the rules further apart than this are not considered matches.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_gaps")]
	public int? MaxGaps { get; set; }

	/// <summary>
	/// <para>If `true`, intervals produced by the rules should appear in the order in which they are specified.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ordered")]
	public bool? Ordered { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(IntervalsAllOf intervalsAllOf) => Elastic.Clients.Elasticsearch.QueryDsl.Intervals.AllOf(intervalsAllOf);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsQuery(IntervalsAllOf intervalsAllOf) => Elastic.Clients.Elasticsearch.QueryDsl.IntervalsQuery.AllOf(intervalsAllOf);
}

public sealed partial class IntervalsAllOfDescriptor<TDocument> : SerializableDescriptor<IntervalsAllOfDescriptor<TDocument>>
{
	internal IntervalsAllOfDescriptor(Action<IntervalsAllOfDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IntervalsAllOfDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument> FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument>> FilterDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Intervals> IntervalsValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> IntervalsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>> IntervalsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>>[] IntervalsDescriptorActions { get; set; }
	private int? MaxGapsValue { get; set; }
	private bool? OrderedValue { get; set; }

	/// <summary>
	/// <para>Rule used to filter returned intervals.</para>
	/// </summary>
	public IntervalsAllOfDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public IntervalsAllOfDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument> descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public IntervalsAllOfDescriptor<TDocument> Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument>> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>An array of rules to combine. All rules must produce a match in a document for the overall source to match.</para>
	/// </summary>
	public IntervalsAllOfDescriptor<TDocument> Intervals(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Intervals> intervals)
	{
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsValue = intervals;
		return Self;
	}

	public IntervalsAllOfDescriptor<TDocument> Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> descriptor)
	{
		IntervalsValue = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptor = descriptor;
		return Self;
	}

	public IntervalsAllOfDescriptor<TDocument> Intervals(Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>> configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptorAction = configure;
		return Self;
	}

	public IntervalsAllOfDescriptor<TDocument> Intervals(params Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>>[] configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>Maximum number of positions between the matching terms.<br/>Intervals produced by the rules further apart than this are not considered matches.</para>
	/// </summary>
	public IntervalsAllOfDescriptor<TDocument> MaxGaps(int? maxGaps)
	{
		MaxGapsValue = maxGaps;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, intervals produced by the rules should appear in the order in which they are specified.</para>
	/// </summary>
	public IntervalsAllOfDescriptor<TDocument> Ordered(bool? ordered = true)
	{
		OrderedValue = ordered;
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument>(FilterDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>(IntervalsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IntervalsDescriptorActions is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			foreach (var action in IntervalsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("intervals");
			JsonSerializer.Serialize(writer, IntervalsValue, options);
		}

		if (MaxGapsValue.HasValue)
		{
			writer.WritePropertyName("max_gaps");
			writer.WriteNumberValue(MaxGapsValue.Value);
		}

		if (OrderedValue.HasValue)
		{
			writer.WritePropertyName("ordered");
			writer.WriteBooleanValue(OrderedValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class IntervalsAllOfDescriptor : SerializableDescriptor<IntervalsAllOfDescriptor>
{
	internal IntervalsAllOfDescriptor(Action<IntervalsAllOfDescriptor> configure) => configure.Invoke(this);

	public IntervalsAllOfDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor> FilterDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Intervals> IntervalsValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor IntervalsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor> IntervalsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor>[] IntervalsDescriptorActions { get; set; }
	private int? MaxGapsValue { get; set; }
	private bool? OrderedValue { get; set; }

	/// <summary>
	/// <para>Rule used to filter returned intervals.</para>
	/// </summary>
	public IntervalsAllOfDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public IntervalsAllOfDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public IntervalsAllOfDescriptor Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>An array of rules to combine. All rules must produce a match in a document for the overall source to match.</para>
	/// </summary>
	public IntervalsAllOfDescriptor Intervals(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Intervals> intervals)
	{
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsValue = intervals;
		return Self;
	}

	public IntervalsAllOfDescriptor Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor descriptor)
	{
		IntervalsValue = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptor = descriptor;
		return Self;
	}

	public IntervalsAllOfDescriptor Intervals(Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor> configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorActions = null;
		IntervalsDescriptorAction = configure;
		return Self;
	}

	public IntervalsAllOfDescriptor Intervals(params Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor>[] configure)
	{
		IntervalsValue = null;
		IntervalsDescriptor = null;
		IntervalsDescriptorAction = null;
		IntervalsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>Maximum number of positions between the matching terms.<br/>Intervals produced by the rules further apart than this are not considered matches.</para>
	/// </summary>
	public IntervalsAllOfDescriptor MaxGaps(int? maxGaps)
	{
		MaxGapsValue = maxGaps;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, intervals produced by the rules should appear in the order in which they are specified.</para>
	/// </summary>
	public IntervalsAllOfDescriptor Ordered(bool? ordered = true)
	{
		OrderedValue = ordered;
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor(FilterDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor(IntervalsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IntervalsDescriptorActions is not null)
		{
			writer.WritePropertyName("intervals");
			writer.WriteStartArray();
			foreach (var action in IntervalsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("intervals");
			JsonSerializer.Serialize(writer, IntervalsValue, options);
		}

		if (MaxGapsValue.HasValue)
		{
			writer.WritePropertyName("max_gaps");
			writer.WriteNumberValue(MaxGapsValue.Value);
		}

		if (OrderedValue.HasValue)
		{
			writer.WritePropertyName("ordered");
			writer.WriteBooleanValue(OrderedValue.Value);
		}

		writer.WriteEndObject();
	}
}