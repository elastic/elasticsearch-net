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

public readonly partial struct FluentDictionaryOfStringQueryFeatureExtractor<TDocument>
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor> Value => _items;

	public FluentDictionaryOfStringQueryFeatureExtractor()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor<TDocument> Add(string key, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor<TDocument> Add(string key, System.Action<Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractorDescriptor<TDocument>> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractorDescriptor<TDocument>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor<TDocument>>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor<TDocument>();
		action.Invoke(builder);
		return builder.Value;
	}
}

public readonly partial struct FluentDictionaryOfStringQueryFeatureExtractor
{
	private readonly System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor> _items = new();

	private System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor> Value => _items;

	public FluentDictionaryOfStringQueryFeatureExtractor()
	{
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor Add(string key, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor value)
	{
		_items.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor Add(string key, System.Action<Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractorDescriptor> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractorDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor Add<T>(string key, System.Action<Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractorDescriptor<T>> action)
	{
		_items.Add(key, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractorDescriptor<T>.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor> Build(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor>? action)
	{
		if (action is null)
		{
			return new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.MachineLearning.QueryFeatureExtractor>();
		}

		var builder = new Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringQueryFeatureExtractor();
		action.Invoke(builder);
		return builder.Value;
	}
}