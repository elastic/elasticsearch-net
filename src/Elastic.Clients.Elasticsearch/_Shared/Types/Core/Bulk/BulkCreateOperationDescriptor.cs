// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkCreateOperationDescriptor<TSource> :
	BulkOperationDescriptor<BulkCreateOperationDescriptor<TSource>>
{
	internal new BulkCreateOperation<TSource> Instance => (BulkCreateOperation<TSource>)base.Instance;

	public BulkCreateOperationDescriptor(TSource source) :
		base(new BulkCreateOperation<TSource>(source))
	{
	}

	public BulkCreateOperationDescriptor(TSource source, IndexName index) :
		base(new BulkCreateOperation<TSource>(source, index))
	{
	}

	public BulkCreateOperationDescriptor<TSource> Pipeline(string pipeline)
	{
		Instance.Pipeline = pipeline;
		return Self;
	}

	public BulkCreateOperationDescriptor<TSource> DynamicTemplates(IDictionary<string, string>? value)
	{
		Instance.DynamicTemplates = value;
		return Self;
	}

	public BulkCreateOperationDescriptor<TSource> DynamicTemplates(Action<FluentIDictionaryOfStringString> action)
	{
		Instance.DynamicTemplates = FluentIDictionaryOfStringString.Build(action);
		return this;
	}
}
