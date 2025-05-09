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

public readonly partial struct FluentDictionaryOfStringPivotGroupBy<TDocument>
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy> Value => _items;

	public FluentDictionaryOfStringPivotGroupBy()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy<TDocument> Add(string key, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy<TDocument> Add(string key, System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupByDescriptor<TDocument>> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupByDescriptor<TDocument>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy<TDocument>>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentDictionaryOfStringPivotGroupBy
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy> Value => _items;

	public FluentDictionaryOfStringPivotGroupBy()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy Add(string key, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy Add(string key, System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupByDescriptor> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupByDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy Add<T>(string key, System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupByDescriptor<T>> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupByDescriptor<T>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.TransformManagement.PivotGroupBy>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPivotGroupBy();
		action.Invoke(builder);
		return builder.Value;
	}
}