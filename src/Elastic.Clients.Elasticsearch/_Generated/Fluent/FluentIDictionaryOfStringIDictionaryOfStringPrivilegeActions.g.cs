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

public readonly partial struct FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions
{
	private readonly System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> _items = new();

	private System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> Value => _items;

	public FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions Add(string key, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions> value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions Add(string key)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringPrivilegeActions.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions Add(string key, System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringPrivilegeActions>? action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringPrivilegeActions.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringIDictionaryOfStringPrivilegeActions();
		action.Invoke(builder);
		return builder.Value;
	}
}