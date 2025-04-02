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

namespace Elastic.Clients.Elasticsearch.Fluent;

public readonly partial struct FluentICollectionOfQuery<TDocument>
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query> Value => _items;

	public FluentICollectionOfQuery()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument> Add(Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument> Add(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		_items.AddRange(values);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument> Add(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument> Add(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action));
		}

		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentICollectionOfQuery
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query> Value => _items;

	public FluentICollectionOfQuery()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery Add(Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery Add(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		_items.AddRange(values);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery Add(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery Add(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action));
		}

		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery Add<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery Add<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action));
		}

		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery();
		action.Invoke(builder);
		return builder.Value;
	}
}