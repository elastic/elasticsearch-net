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

public readonly partial struct FluentICollectionOfIndicesPrivileges<TDocument>
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> Value => _items;

	public FluentICollectionOfIndicesPrivileges()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges<TDocument> Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivileges value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges<TDocument> Add(params Elastic.Clients.Elasticsearch.Security.IndicesPrivileges[] values)
	{
		_items.AddRange(values);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges<TDocument> Add(System.Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges<TDocument> Add(params System.Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<TDocument>.Build(action));
		}

		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges<TDocument>>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentICollectionOfIndicesPrivileges
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> Value => _items;

	public FluentICollectionOfIndicesPrivileges()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivileges value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges Add(params Elastic.Clients.Elasticsearch.Security.IndicesPrivileges[] values)
	{
		_items.AddRange(values);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges Add(System.Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges Add(params System.Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor.Build(action));
		}

		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges Add<T>(System.Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<T>> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<T>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges Add<T>(params System.Action<Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<T>>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.Security.IndicesPrivilegesDescriptor<T>.Build(action));
		}

		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfIndicesPrivileges();
		action.Invoke(builder);
		return builder.Value;
	}
}