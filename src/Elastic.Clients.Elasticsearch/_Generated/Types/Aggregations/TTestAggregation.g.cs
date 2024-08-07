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

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class TTestAggregation
{
	/// <summary>
	/// <para>
	/// Test population A.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("a")]
	public Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? a { get; set; }

	/// <summary>
	/// <para>
	/// Test population B.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("b")]
	public Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? b { get; set; }

	/// <summary>
	/// <para>
	/// The type of test.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("type")]
	public Elastic.Clients.Elasticsearch.Aggregations.TTestType? Type { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(TTestAggregation tTestAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.TTest(tTestAggregation);
}

public sealed partial class TTestAggregationDescriptor<TDocument> : SerializableDescriptor<TTestAggregationDescriptor<TDocument>>
{
	internal TTestAggregationDescriptor(Action<TTestAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TTestAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? aValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument> aDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument>> aDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? bValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument> bDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument>> bDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TTestType? TypeValue { get; set; }

	/// <summary>
	/// <para>
	/// Test population A.
	/// </para>
	/// </summary>
	public TTestAggregationDescriptor<TDocument> a(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? a)
	{
		aDescriptor = null;
		aDescriptorAction = null;
		aValue = a;
		return Self;
	}

	public TTestAggregationDescriptor<TDocument> a(Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument> descriptor)
	{
		aValue = null;
		aDescriptorAction = null;
		aDescriptor = descriptor;
		return Self;
	}

	public TTestAggregationDescriptor<TDocument> a(Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument>> configure)
	{
		aValue = null;
		aDescriptor = null;
		aDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Test population B.
	/// </para>
	/// </summary>
	public TTestAggregationDescriptor<TDocument> b(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? b)
	{
		bDescriptor = null;
		bDescriptorAction = null;
		bValue = b;
		return Self;
	}

	public TTestAggregationDescriptor<TDocument> b(Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument> descriptor)
	{
		bValue = null;
		bDescriptorAction = null;
		bDescriptor = descriptor;
		return Self;
	}

	public TTestAggregationDescriptor<TDocument> b(Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument>> configure)
	{
		bValue = null;
		bDescriptor = null;
		bDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The type of test.
	/// </para>
	/// </summary>
	public TTestAggregationDescriptor<TDocument> Type(Elastic.Clients.Elasticsearch.Aggregations.TTestType? type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (aDescriptor is not null)
		{
			writer.WritePropertyName("a");
			JsonSerializer.Serialize(writer, aDescriptor, options);
		}
		else if (aDescriptorAction is not null)
		{
			writer.WritePropertyName("a");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument>(aDescriptorAction), options);
		}
		else if (aValue is not null)
		{
			writer.WritePropertyName("a");
			JsonSerializer.Serialize(writer, aValue, options);
		}

		if (bDescriptor is not null)
		{
			writer.WritePropertyName("b");
			JsonSerializer.Serialize(writer, bDescriptor, options);
		}
		else if (bDescriptorAction is not null)
		{
			writer.WritePropertyName("b");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor<TDocument>(bDescriptorAction), options);
		}
		else if (bValue is not null)
		{
			writer.WritePropertyName("b");
			JsonSerializer.Serialize(writer, bValue, options);
		}

		if (TypeValue is not null)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, TypeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class TTestAggregationDescriptor : SerializableDescriptor<TTestAggregationDescriptor>
{
	internal TTestAggregationDescriptor(Action<TTestAggregationDescriptor> configure) => configure.Invoke(this);

	public TTestAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? aValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor aDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor> aDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? bValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor bDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor> bDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TTestType? TypeValue { get; set; }

	/// <summary>
	/// <para>
	/// Test population A.
	/// </para>
	/// </summary>
	public TTestAggregationDescriptor a(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? a)
	{
		aDescriptor = null;
		aDescriptorAction = null;
		aValue = a;
		return Self;
	}

	public TTestAggregationDescriptor a(Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor descriptor)
	{
		aValue = null;
		aDescriptorAction = null;
		aDescriptor = descriptor;
		return Self;
	}

	public TTestAggregationDescriptor a(Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor> configure)
	{
		aValue = null;
		aDescriptor = null;
		aDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Test population B.
	/// </para>
	/// </summary>
	public TTestAggregationDescriptor b(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? b)
	{
		bDescriptor = null;
		bDescriptorAction = null;
		bValue = b;
		return Self;
	}

	public TTestAggregationDescriptor b(Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor descriptor)
	{
		bValue = null;
		bDescriptorAction = null;
		bDescriptor = descriptor;
		return Self;
	}

	public TTestAggregationDescriptor b(Action<Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor> configure)
	{
		bValue = null;
		bDescriptor = null;
		bDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The type of test.
	/// </para>
	/// </summary>
	public TTestAggregationDescriptor Type(Elastic.Clients.Elasticsearch.Aggregations.TTestType? type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (aDescriptor is not null)
		{
			writer.WritePropertyName("a");
			JsonSerializer.Serialize(writer, aDescriptor, options);
		}
		else if (aDescriptorAction is not null)
		{
			writer.WritePropertyName("a");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor(aDescriptorAction), options);
		}
		else if (aValue is not null)
		{
			writer.WritePropertyName("a");
			JsonSerializer.Serialize(writer, aValue, options);
		}

		if (bDescriptor is not null)
		{
			writer.WritePropertyName("b");
			JsonSerializer.Serialize(writer, bDescriptor, options);
		}
		else if (bDescriptorAction is not null)
		{
			writer.WritePropertyName("b");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TestPopulationDescriptor(bDescriptorAction), options);
		}
		else if (bValue is not null)
		{
			writer.WritePropertyName("b");
			JsonSerializer.Serialize(writer, bValue, options);
		}

		if (TypeValue is not null)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, TypeValue, options);
		}

		writer.WriteEndObject();
	}
}