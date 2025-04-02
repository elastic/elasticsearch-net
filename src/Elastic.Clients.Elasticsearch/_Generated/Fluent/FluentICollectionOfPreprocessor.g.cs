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

public readonly partial struct FluentICollectionOfPreprocessor
{
	private readonly System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor> _items = new();

	private System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor> Value => _items;

	public FluentICollectionOfPreprocessor()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor Add(Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor value)
	{
		_items.Add(value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor Add(params Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor[] values)
	{
		_items.AddRange(values);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor Add(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PreprocessorDescriptor> action)
	{
		_items.Add(Elastic.Clients.Elasticsearch.MachineLearning.PreprocessorDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor Add(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.PreprocessorDescriptor>[] actions)
	{
		foreach (var action in actions)
		{
			_items.Add(Elastic.Clients.Elasticsearch.MachineLearning.PreprocessorDescriptor.Build(action));
		}

		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.Preprocessor> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor>? action)
	{
		if (action is null)
		{
			return [];
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfPreprocessor();
		action.Invoke(builder);
		return builder.Value;
	}
}