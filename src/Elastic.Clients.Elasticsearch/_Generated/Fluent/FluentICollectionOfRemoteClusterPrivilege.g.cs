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

public readonly partial struct FluentICollectionOfRemoteClusterPrivilege
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege> Value => _items;

	public FluentICollectionOfRemoteClusterPrivilege()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRemoteClusterPrivilege Add(Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRemoteClusterPrivilege Add(params Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege[] values)
	{
		_items.AddRange(values);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRemoteClusterPrivilege>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfRemoteClusterPrivilege();
		action.Invoke(builder);
		return builder.Value;
	}
}