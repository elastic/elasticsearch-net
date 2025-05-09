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

public readonly partial struct FluentDictionaryOfStringFieldSuggester<TDocument>
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester> Value => _items;

	public FluentDictionaryOfStringFieldSuggester()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester<TDocument> Add(string key, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester<TDocument> Add(string key, System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument>> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester<TDocument>>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentDictionaryOfStringFieldSuggester
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester> Value => _items;

	public FluentDictionaryOfStringFieldSuggester()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester Add(string key, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester Add(string key, System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester Add<T>(string key, System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<T>> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<T>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringFieldSuggester();
		action.Invoke(builder);
		return builder.Value;
	}
}