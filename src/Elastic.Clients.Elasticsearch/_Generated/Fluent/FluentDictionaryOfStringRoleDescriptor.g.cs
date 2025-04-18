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

public readonly partial struct FluentDictionaryOfStringRoleDescriptor<TDocument>
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> Value => _items;

	public FluentDictionaryOfStringRoleDescriptor()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor<TDocument> Add(string key, Elastic.Clients.Elasticsearch.Security.RoleDescriptor value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor<TDocument> Add(string key)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor<TDocument> Add(string key, System.Action<Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>? action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentDictionaryOfStringRoleDescriptor
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> Value => _items;

	public FluentDictionaryOfStringRoleDescriptor()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor Add(string key, Elastic.Clients.Elasticsearch.Security.RoleDescriptor value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor Add(string key)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor Add(string key, System.Action<Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>? action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor Add<T>(string key, System.Action<Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<T>>? action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<T>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringRoleDescriptor();
		action.Invoke(builder);
		return builder.Value;
	}
}