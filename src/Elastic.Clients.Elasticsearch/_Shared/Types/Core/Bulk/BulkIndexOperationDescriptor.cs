// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkIndexOperationDescriptor<TSource> :
	BulkOperationDescriptor<BulkIndexOperationDescriptor<TSource>>
{
	internal new BulkIndexOperation<TSource> Instance => (BulkIndexOperation<TSource>)base.Instance;

	public BulkIndexOperationDescriptor(TSource source) :
		base(new BulkIndexOperation<TSource>(source))
	{
	}

	public BulkIndexOperationDescriptor(TSource source, IndexName index) : base(
		new BulkIndexOperation<TSource>(source, index))
	{
	}

	public BulkIndexOperationDescriptor<TSource> Pipeline(string pipeline)
	{
		Instance.Pipeline = pipeline;
		return Self;
	}

	public BulkIndexOperationDescriptor<TSource> DynamicTemplates(IDictionary<string, string>? value)
	{
		Instance.DynamicTemplates = value;
		return Self;
	}

	public BulkIndexOperationDescriptor<TSource> DynamicTemplates(Action<FluentDictionaryOfStringString> action)
	{
		Instance.DynamicTemplates = FluentDictionaryOfStringString.Build(action);
		return this;
	}
}
