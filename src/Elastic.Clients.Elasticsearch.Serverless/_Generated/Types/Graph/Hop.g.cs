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

namespace Elastic.Clients.Elasticsearch.Serverless.Graph;

public sealed partial class Hop
{
	/// <summary>
	/// <para>Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("connections")]
	public Elastic.Clients.Elasticsearch.Serverless.Graph.Hop? Connections { get; set; }

	/// <summary>
	/// <para>An optional guiding query that constrains the Graph API as it explores connected terms.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query Query { get; set; }

	/// <summary>
	/// <para>Contains the fields you are interested in.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("vertices")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Graph.VertexDefinition> Vertices { get; set; }
}

public sealed partial class HopDescriptor<TDocument> : SerializableDescriptor<HopDescriptor<TDocument>>
{
	internal HopDescriptor(Action<HopDescriptor<TDocument>> configure) => configure.Invoke(this);

	public HopDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Graph.Hop? ConnectionsValue { get; set; }
	private HopDescriptor<TDocument> ConnectionsDescriptor { get; set; }
	private Action<HopDescriptor<TDocument>> ConnectionsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query QueryValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Graph.VertexDefinition> VerticesValue { get; set; }
	private VertexDefinitionDescriptor<TDocument> VerticesDescriptor { get; set; }
	private Action<VertexDefinitionDescriptor<TDocument>> VerticesDescriptorAction { get; set; }
	private Action<VertexDefinitionDescriptor<TDocument>>[] VerticesDescriptorActions { get; set; }

	/// <summary>
	/// <para>Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.</para>
	/// </summary>
	public HopDescriptor<TDocument> Connections(Elastic.Clients.Elasticsearch.Serverless.Graph.Hop? connections)
	{
		ConnectionsDescriptor = null;
		ConnectionsDescriptorAction = null;
		ConnectionsValue = connections;
		return Self;
	}

	public HopDescriptor<TDocument> Connections(HopDescriptor<TDocument> descriptor)
	{
		ConnectionsValue = null;
		ConnectionsDescriptorAction = null;
		ConnectionsDescriptor = descriptor;
		return Self;
	}

	public HopDescriptor<TDocument> Connections(Action<HopDescriptor<TDocument>> configure)
	{
		ConnectionsValue = null;
		ConnectionsDescriptor = null;
		ConnectionsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>An optional guiding query that constrains the Graph API as it explores connected terms.</para>
	/// </summary>
	public HopDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public HopDescriptor<TDocument> Query(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public HopDescriptor<TDocument> Query(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Contains the fields you are interested in.</para>
	/// </summary>
	public HopDescriptor<TDocument> Vertices(ICollection<Elastic.Clients.Elasticsearch.Serverless.Graph.VertexDefinition> vertices)
	{
		VerticesDescriptor = null;
		VerticesDescriptorAction = null;
		VerticesDescriptorActions = null;
		VerticesValue = vertices;
		return Self;
	}

	public HopDescriptor<TDocument> Vertices(VertexDefinitionDescriptor<TDocument> descriptor)
	{
		VerticesValue = null;
		VerticesDescriptorAction = null;
		VerticesDescriptorActions = null;
		VerticesDescriptor = descriptor;
		return Self;
	}

	public HopDescriptor<TDocument> Vertices(Action<VertexDefinitionDescriptor<TDocument>> configure)
	{
		VerticesValue = null;
		VerticesDescriptor = null;
		VerticesDescriptorActions = null;
		VerticesDescriptorAction = configure;
		return Self;
	}

	public HopDescriptor<TDocument> Vertices(params Action<VertexDefinitionDescriptor<TDocument>>[] configure)
	{
		VerticesValue = null;
		VerticesDescriptor = null;
		VerticesDescriptorAction = null;
		VerticesDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ConnectionsDescriptor is not null)
		{
			writer.WritePropertyName("connections");
			JsonSerializer.Serialize(writer, ConnectionsDescriptor, options);
		}
		else if (ConnectionsDescriptorAction is not null)
		{
			writer.WritePropertyName("connections");
			JsonSerializer.Serialize(writer, new HopDescriptor<TDocument>(ConnectionsDescriptorAction), options);
		}
		else if (ConnectionsValue is not null)
		{
			writer.WritePropertyName("connections");
			JsonSerializer.Serialize(writer, ConnectionsValue, options);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (VerticesDescriptor is not null)
		{
			writer.WritePropertyName("vertices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, VerticesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (VerticesDescriptorAction is not null)
		{
			writer.WritePropertyName("vertices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new VertexDefinitionDescriptor<TDocument>(VerticesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (VerticesDescriptorActions is not null)
		{
			writer.WritePropertyName("vertices");
			writer.WriteStartArray();
			foreach (var action in VerticesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new VertexDefinitionDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("vertices");
			JsonSerializer.Serialize(writer, VerticesValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class HopDescriptor : SerializableDescriptor<HopDescriptor>
{
	internal HopDescriptor(Action<HopDescriptor> configure) => configure.Invoke(this);

	public HopDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Graph.Hop? ConnectionsValue { get; set; }
	private HopDescriptor ConnectionsDescriptor { get; set; }
	private Action<HopDescriptor> ConnectionsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query QueryValue { get; set; }
	private QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Graph.VertexDefinition> VerticesValue { get; set; }
	private VertexDefinitionDescriptor VerticesDescriptor { get; set; }
	private Action<VertexDefinitionDescriptor> VerticesDescriptorAction { get; set; }
	private Action<VertexDefinitionDescriptor>[] VerticesDescriptorActions { get; set; }

	/// <summary>
	/// <para>Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.</para>
	/// </summary>
	public HopDescriptor Connections(Elastic.Clients.Elasticsearch.Serverless.Graph.Hop? connections)
	{
		ConnectionsDescriptor = null;
		ConnectionsDescriptorAction = null;
		ConnectionsValue = connections;
		return Self;
	}

	public HopDescriptor Connections(HopDescriptor descriptor)
	{
		ConnectionsValue = null;
		ConnectionsDescriptorAction = null;
		ConnectionsDescriptor = descriptor;
		return Self;
	}

	public HopDescriptor Connections(Action<HopDescriptor> configure)
	{
		ConnectionsValue = null;
		ConnectionsDescriptor = null;
		ConnectionsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>An optional guiding query that constrains the Graph API as it explores connected terms.</para>
	/// </summary>
	public HopDescriptor Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public HopDescriptor Query(QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public HopDescriptor Query(Action<QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Contains the fields you are interested in.</para>
	/// </summary>
	public HopDescriptor Vertices(ICollection<Elastic.Clients.Elasticsearch.Serverless.Graph.VertexDefinition> vertices)
	{
		VerticesDescriptor = null;
		VerticesDescriptorAction = null;
		VerticesDescriptorActions = null;
		VerticesValue = vertices;
		return Self;
	}

	public HopDescriptor Vertices(VertexDefinitionDescriptor descriptor)
	{
		VerticesValue = null;
		VerticesDescriptorAction = null;
		VerticesDescriptorActions = null;
		VerticesDescriptor = descriptor;
		return Self;
	}

	public HopDescriptor Vertices(Action<VertexDefinitionDescriptor> configure)
	{
		VerticesValue = null;
		VerticesDescriptor = null;
		VerticesDescriptorActions = null;
		VerticesDescriptorAction = configure;
		return Self;
	}

	public HopDescriptor Vertices(params Action<VertexDefinitionDescriptor>[] configure)
	{
		VerticesValue = null;
		VerticesDescriptor = null;
		VerticesDescriptorAction = null;
		VerticesDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ConnectionsDescriptor is not null)
		{
			writer.WritePropertyName("connections");
			JsonSerializer.Serialize(writer, ConnectionsDescriptor, options);
		}
		else if (ConnectionsDescriptorAction is not null)
		{
			writer.WritePropertyName("connections");
			JsonSerializer.Serialize(writer, new HopDescriptor(ConnectionsDescriptorAction), options);
		}
		else if (ConnectionsValue is not null)
		{
			writer.WritePropertyName("connections");
			JsonSerializer.Serialize(writer, ConnectionsValue, options);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (VerticesDescriptor is not null)
		{
			writer.WritePropertyName("vertices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, VerticesDescriptor, options);
			writer.WriteEndArray();
		}
		else if (VerticesDescriptorAction is not null)
		{
			writer.WritePropertyName("vertices");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new VertexDefinitionDescriptor(VerticesDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (VerticesDescriptorActions is not null)
		{
			writer.WritePropertyName("vertices");
			writer.WriteStartArray();
			foreach (var action in VerticesDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new VertexDefinitionDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("vertices");
			JsonSerializer.Serialize(writer, VerticesValue, options);
		}

		writer.WriteEndObject();
	}
}