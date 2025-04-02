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

public readonly partial struct FluentICollectionOfICharFilter
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Analysis.ICharFilter> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Analysis.ICharFilter> Value => _items;

	public FluentICollectionOfICharFilter()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfICharFilter Add(Elastic.Clients.Elasticsearch.Analysis.ICharFilter value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfICharFilter Add(params Elastic.Clients.Elasticsearch.Analysis.ICharFilter[] values)
	{
		_items.AddRange(values);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfICharFilter Add(System.Func<Elastic.Clients.Elasticsearch.Analysis.ICharFilterBuilder, Elastic.Clients.Elasticsearch.Analysis.ICharFilter> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.Analysis.ICharFilterBuilder.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfICharFilter Add(params System.Func<Elastic.Clients.Elasticsearch.Analysis.ICharFilterBuilder, Elastic.Clients.Elasticsearch.Analysis.ICharFilter>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.Analysis.ICharFilterBuilder.Build(action));
		}

		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Analysis.ICharFilter> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfICharFilter>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfICharFilter();
		action.Invoke(builder);
		return builder.Value;
	}
}