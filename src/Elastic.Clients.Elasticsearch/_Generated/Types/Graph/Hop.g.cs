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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Graph;

internal sealed partial class HopConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Graph.Hop>
{
	private static readonly System.Text.Json.JsonEncodedText PropConnections = System.Text.Json.JsonEncodedText.Encode("connections");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropVertices = System.Text.Json.JsonEncodedText.Encode("vertices");

	public override Elastic.Clients.Elasticsearch.Graph.Hop Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Graph.Hop?> propConnections = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propQuery = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition>> propVertices = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propConnections.TryReadProperty(ref reader, options, PropConnections, null))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propVertices.TryReadProperty(ref reader, options, PropVertices, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Graph.VertexDefinition>(o, null)!))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Connections = propConnections.Value,
			Query = propQuery.Value,
			Vertices = propVertices.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Graph.Hop value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropConnections, value.Connections, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropVertices, value.Vertices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Graph.VertexDefinition>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Graph.HopConverter))]
public sealed partial class Hop
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Hop(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition> vertices)
	{
		Vertices = vertices;
	}
#if NET7_0_OR_GREATER
	public Hop()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Hop()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Hop(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.Hop? Connections { get; set; }

	/// <summary>
	/// <para>
	/// An optional guiding query that constrains the Graph API as it explores connected terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition> Vertices { get; set; }
}

public readonly partial struct HopDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Graph.Hop Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HopDescriptor(Elastic.Clients.Elasticsearch.Graph.Hop instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HopDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Graph.Hop instance) => new Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Connections(Elastic.Clients.Elasticsearch.Graph.Hop? value)
	{
		Instance.Connections = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Connections(System.Action<Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument>> action)
	{
		Instance.Connections = Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional guiding query that constrains the Graph API as it explores connected terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional guiding query that constrains the Graph API as it explores connected terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Vertices(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition> value)
	{
		Instance.Vertices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Vertices()
	{
		Instance.Vertices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Vertices(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition<TDocument>>? action)
	{
		Instance.Vertices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Vertices(params Elastic.Clients.Elasticsearch.Graph.VertexDefinition[] values)
	{
		Instance.Vertices = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument> Vertices(params System.Action<Elastic.Clients.Elasticsearch.Graph.VertexDefinitionDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Graph.VertexDefinition>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Graph.VertexDefinitionDescriptor<TDocument>.Build(action));
		}

		Instance.Vertices = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Graph.Hop Build(System.Action<Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.HopDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct HopDescriptor
{
	internal Elastic.Clients.Elasticsearch.Graph.Hop Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HopDescriptor(Elastic.Clients.Elasticsearch.Graph.Hop instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HopDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Graph.HopDescriptor(Elastic.Clients.Elasticsearch.Graph.Hop instance) => new Elastic.Clients.Elasticsearch.Graph.HopDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Graph.HopDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Connections(Elastic.Clients.Elasticsearch.Graph.Hop? value)
	{
		Instance.Connections = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Connections(System.Action<Elastic.Clients.Elasticsearch.Graph.HopDescriptor> action)
	{
		Instance.Connections = Elastic.Clients.Elasticsearch.Graph.HopDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies one or more fields from which you want to extract terms that are associated with the specified vertices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Connections<T>(System.Action<Elastic.Clients.Elasticsearch.Graph.HopDescriptor<T>> action)
	{
		Instance.Connections = Elastic.Clients.Elasticsearch.Graph.HopDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional guiding query that constrains the Graph API as it explores connected terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional guiding query that constrains the Graph API as it explores connected terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional guiding query that constrains the Graph API as it explores connected terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Graph.VertexDefinition> value)
	{
		Instance.Vertices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices()
	{
		Instance.Vertices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition>? action)
	{
		Instance.Vertices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition<T>>? action)
	{
		Instance.Vertices = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfVertexDefinition<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices(params Elastic.Clients.Elasticsearch.Graph.VertexDefinition[] values)
	{
		Instance.Vertices = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices(params System.Action<Elastic.Clients.Elasticsearch.Graph.VertexDefinitionDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Graph.VertexDefinition>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Graph.VertexDefinitionDescriptor.Build(action));
		}

		Instance.Vertices = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Contains the fields you are interested in.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Graph.HopDescriptor Vertices<T>(params System.Action<Elastic.Clients.Elasticsearch.Graph.VertexDefinitionDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Graph.VertexDefinition>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Graph.VertexDefinitionDescriptor<T>.Build(action));
		}

		Instance.Vertices = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Graph.Hop Build(System.Action<Elastic.Clients.Elasticsearch.Graph.HopDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.HopDescriptor(new Elastic.Clients.Elasticsearch.Graph.Hop(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}