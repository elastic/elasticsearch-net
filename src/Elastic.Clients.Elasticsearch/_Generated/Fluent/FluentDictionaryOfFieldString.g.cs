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

public readonly partial struct FluentDictionaryOfFieldString<TDocument>
{
	private readonly System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string> _items = new();

	private System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string> Value => _items;

	public FluentDictionaryOfFieldString()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument> Add(Elastic.Clients.Elasticsearch.Field key, string value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument> Add(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, string value)
	{
		_items.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument>>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentDictionaryOfFieldString
{
	private readonly System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string> _items = new();

	private System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string> Value => _items;

	public FluentDictionaryOfFieldString()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString Add(Elastic.Clients.Elasticsearch.Field key, string value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString Add<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, string value)
	{
		_items.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString();
		action.Invoke(builder);
		return builder.Value;
	}
}