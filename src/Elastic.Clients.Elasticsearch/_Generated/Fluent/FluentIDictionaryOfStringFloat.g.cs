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

public readonly partial struct FluentIDictionaryOfStringFloat
{
	private readonly System.Collections.Generic.Dictionary<string, float> _items = new();

	private System.Collections.Generic.IDictionary<string, float> Value => _items;

	public FluentIDictionaryOfStringFloat()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringFloat Add(string key, float value)
	{
		_items.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, float> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringFloat>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, float>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringFloat();
		action.Invoke(builder);
		return builder.Value;
	}
}